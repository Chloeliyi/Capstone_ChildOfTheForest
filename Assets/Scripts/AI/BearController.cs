using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public static BearController Instance;
    [Header("Stats")]
    public int health;

    public int Beardamage;

    public Transform playerTransform;
    [SerializeField]private NavMeshAgent agent;
    Animator animator;

    public LayerMask whatIsGround, whatIsPlayer;

    //[SerializeField] private string attackAnim = "BearAttack";

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (TimeManager.Instance.hours >= 8 || TimeManager.Instance.hours <= 18)
        {
            activeBear = true;
        } 
        else
        {
            activeBear = false;
        }
    }

    void Update()
    {
        //agent.destination = playerTransform.position;
        //animator.SetFloat("Speed", agent.velocity.magnitude);

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
        //animator.SetFloat("Speed", 3f);
        //Debug.Log("Speed : " + agent.velocity.magnitude);
        animator.SetBool("Sleep", false);
        //animator.SetBool("Walk Forward", true);

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
        //animator.SetBool("RunForward", true);
        Debug.Log("Chasing");
        Debug.Log("Speed : " + agent.velocity.magnitude);
        agent.SetDestination(playerTransform.position);
    }

    public void AttackPlayer()
    {
        Debug.Log("Attacking");
        //Debug.Log("Speed : " + agent.velocity.magnitude);
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        //animator.SetFloat("Speed", 4.5f);

        GiveDamage();

        /*if (!alreadyAttacked)
        {
            //Code
            //animator.Play(attackAnim);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }*/
    }

    private void ResetAttack()
        {
            alreadyAttacked = false;
        }

    public void TakeSpearDamage(int Speardamage)
    {
        health -= Speardamage;

        Debug.Log("Enemy health : " + health);

        if (health <= 0)
            DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void GiveDamage()
    {
        float count = 0;
        count += Beardamage;
        Debug.Log("Taking Damage" + count);

        //GameManager.Instance.TakeHealthDamage(Beardamage);

        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
    }
    
}
