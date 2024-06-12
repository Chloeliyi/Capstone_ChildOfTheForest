using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [Header("Stats")]
    public int health;

    public int Enemydamage;

    public Transform playerTransform;
    [SerializeField]private NavMeshAgent agent;
    Animator animator;

    public LayerMask whatIsGround, whatIsPlayer;

    public TimeManager timeManager;

    [SerializeField] private string attackAnim = "Attack";

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

    public bool activeWendigo;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //agent.destination = playerTransform.position;
        //animator.SetFloat("Speed", agent.velocity.magnitude);

        if (timeManager.hours >= 20 || timeManager.hours <= 6)
        {
            activeWendigo = true;
        } 
        else
        {
            activeWendigo = false;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (activeWendigo == true)
        {
            Debug.Log("Wendigo is active");
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            Debug.Log("Wendigo is not active");
            animator.SetFloat("Speed", 0f);
        }

    }

    public void Patroling()
    {
        Debug.Log("Patroling");
        animator.SetFloat("Speed", 3f);
        Debug.Log("Speed : " + agent.velocity.magnitude);

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
        animator.SetFloat("Speed", 4f);
        Debug.Log("Chasing");
        Debug.Log("Speed : " + agent.velocity.magnitude);
        agent.SetDestination(playerTransform.position);
    }

    public void AttackPlayer()
    {
        Debug.Log("Attacking");
        Debug.Log("Speed : " + agent.velocity.magnitude);
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        animator.SetFloat("Speed", 4.5f);

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
        count += Enemydamage;
        Debug.Log("Taking Damage" + count);

        //GameManager.Instance.TakeHealthDamage(Enemydamage);

        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
    }
}
