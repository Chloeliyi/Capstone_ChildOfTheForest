using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public static BearController Instance;
    [Header("Stats")]

    [SerializeField] private int health;

    [SerializeField] private int Beardamage = 10;

    [SerializeField] private GameObject Bear;

    public Transform playerTransform;
    [SerializeField]private NavMeshAgent agent;
    Animator animator;

    public LayerMask whatIsGround, whatIsPlayer;

    public TimeManager timeManager;

    /// <summary>
    /// Patroling
    /// </summary>
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    /// <summary>
    /// Attacking
    /// </summary>
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    /// <summary>
    /// States
    /// </summary>
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool activeBear;

    public GameObject Meat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Bear = transform.gameObject;
        health = 100;
    }

    void Update()
    {
        //agent.destination = playerTransform.position;
        //animator.SetFloat("Speed", agent.velocity.magnitude);

        if (timeManager.hours >= 8 || timeManager.hours <= 18)
        {
            activeBear = true;
        } 
        else
        {
            activeBear = false;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (activeBear == true)
        {
            Debug.Log("Bear is active");
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            Debug.Log("Bear is not active");
            //animator.SetFloat("Speed", 0f);
            animator.SetBool("Sleep", true);
        }

    }

    public void Patroling()
    {
        Debug.Log("Patroling");
        animator.SetBool("Sleep", false);
        animator.SetBool("WalkForward", true);

        if (!walkPointSet) SearchWalkPoint();

         if (walkPointSet) agent.SetDestination(walkPoint);

         Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

     private void SearchWalkPoint()
     {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
     }

    public void ChasePlayer()
    {
        //animator.SetFloat("Speed", 4f);
        //Debug.Log("Speed : " + agent.velocity.magnitude);
        Debug.Log("Chasing");
        animator.SetBool("WalkForward", false);
        animator.SetBool("Run Forward", true);
        agent.SetDestination(playerTransform.position);
    }

    [SerializeField] private int attackCounter;

    [SerializeField] private string attackName;

    public void AttackPlayer()
    {
        //Debug.Log("Speed : " + agent.velocity.magnitude);
        Debug.Log("Attacking");
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        //animator.SetFloat("Speed", 4.5f);

        if (!alreadyAttacked)
        {
            //Code
            animator.SetBool("Run Forward", false);

            attackCounter =  Random.Range(0, 3);
            Debug.Log("Attack: " + attackCounter);

            if (attackCounter == 0)
            {
                attackName = "Attack1";
                animator.SetTrigger("Attack1");
            }
            else if (attackCounter == 1)
            {
                attackName = "Attack2";
                animator.SetTrigger("Attack2");
            }
            else if (attackCounter == 2)
            {
                attackName = "Attack3";
                animator.SetTrigger("Attack3");
            }
            else if (attackCounter == 3)
            {
                attackName = "Attack5";
                animator.SetTrigger("Attack5");
            }

            GiveDamage();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
        {
            alreadyAttacked = false;
            attackCounter =  Random.Range(0, 3);
            Debug.Log("Attack: " + attackCounter);
        }

    public void TakeSpearDamage(int Speardamage)
    {
        animator.SetTrigger("Hit Front");
        health -= Speardamage;

        Debug.Log("Enemy health : " + health);

        if (health <= 0)
            DestroyEnemy();
    }

    public void TakeAxeDamage(int axedamage)
    {
        health -= axedamage;

        Debug.Log("Enemy health : " + health);

        if (health <= 0)
            DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        //Destroy(gameObject);
        animator.SetBool("Death", true);
        StartCoroutine(DeathTime());
        //Instantiate(Meat, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void GiveDamage()
    {
        GameManager.Instance.HealthDamage(Beardamage);

        if (GameManager.Instance.CurrentHealth <= 0)
        {
            Debug.Log("Animal stop");
            playerInSightRange = false;
            playerInAttackRange = false;
            animator.SetBool("WalkForward", true);
        }

        //StartCoroutine(AttackTime());
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(Bear);
        Instantiate(Meat, gameObject.transform.position, gameObject.transform.rotation);
    }
}
