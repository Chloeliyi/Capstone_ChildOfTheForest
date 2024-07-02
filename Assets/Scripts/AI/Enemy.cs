using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [Header("Stats")]
    [SerializeField] private int health;

    [SerializeField] private int Enemydamage = 10;

    [SerializeField] private GameObject Wendigo;

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

    public GameObject Meat;

    void Awake()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Wendigo = transform.gameObject;
        health = 100;
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

        if (!alreadyAttacked)
        {
            //Code
            //animator.SetFloat("Speed", 4.5f);
            animator.SetTrigger(attackAnim);
            GiveDamage();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
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

    public void TakeAxeDamage(int axedamage)
    {
        health -= axedamage;

        Debug.Log("Enemy health : " + health);
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(Wendigo);
        //Wendigo.SetActive(false);
        Instantiate(Meat, Wendigo.gameObject.transform.position, Wendigo.gameObject.transform.rotation);
    }

    public void GiveDamage()
    {
        GameManager.Instance.HealthDamage(Enemydamage);

        if (GameManager.Instance.CurrentHealth <= 0)
        {
            Debug.Log("Wendigo stop");
            playerInSightRange = false;
            playerInAttackRange = false;
            animator.SetFloat("Speed", 3f);
        }

        //StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
    }
}
