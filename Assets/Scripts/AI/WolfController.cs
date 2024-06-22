using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfController : MonoBehaviour
{
    public static WolfController Instance;
    [Header("Stats")]

    [SerializeField] private int health = 100;

    [SerializeField] private int Wolfdamage = 10;

    [SerializeField] private GameObject Wolf;

    public Transform playerTransform;
    public Transform Wolfleader;
    [SerializeField]private NavMeshAgent agent;
    Animator animator;

    public TimeManager timeManager;

    public LayerMask whatIsGround, whatIsPlayer;

    public LayerMask whatIsLeader;

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

    public float wolfsightRange;
    public bool wolfInSightRange;

    public bool Leader;

    public bool NormalWolf;
    public bool CorruptWolf;

    public bool activeWolf;
    public bool activeCorruptWolf;

    public GameObject Meat;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Wolf = transform.gameObject;
    }

    void Update()
    {
        //agent.destination = playerTransform.position;
        //animator.SetFloat("Speed", agent.velocity.magnitude);

        if (timeManager.hours >= 8 || timeManager.hours <= 18)
        {
            if (NormalWolf)
            {
                activeWolf = true;
            }
        } 
        else
        {
            if (NormalWolf)
            {
                activeWolf = false;
            }
        }

        if (timeManager.hours >= 20 || timeManager.hours <= 6)
        {
            if (CorruptWolf)
            {
                activeCorruptWolf = true;
            }
        } 
        else
        {
            if (CorruptWolf)
            {
                activeCorruptWolf = false;
            }
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        wolfInSightRange = Physics.CheckSphere(transform.position, wolfsightRange, whatIsLeader);

        if (activeWolf == true)
        {
            Debug.Log("Wolf is active");
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else if (activeWolf != true)
        {
            Debug.Log("Wolf is not active");
            //animator.SetBool("Sleep", true);
        }
        if (activeCorruptWolf == true)
        {
            Debug.Log("Corrupt Wolf is active");
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else if (activeCorruptWolf != true)
        {
            Debug.Log("Corrupt Wolf is not active");
            //animator.SetBool("Sleep", true);
        }
    }

    public void Patroling()
    {
        if (Leader)
        {
            Debug.Log("Patroling");
            //animator.SetBool("Sleep", false);
            animator.SetBool("WalkForward", true);

            if (!walkPointSet) SearchWalkPoint();
            if (walkPointSet) agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
        }
        else
        {
            Debug.Log("Following leader");
            agent.SetDestination(Wolfleader.transform.position);
            //animator.SetBool("Sleep", false);
            animator.SetBool("WalkForward", true);
        }
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
        if (Leader)
        {
            Debug.Log("Chasing");
            //animator.SetBool("WalkForward", false);
            animator.SetBool("Run Forward", true);
            //Debug.Log("Speed : " + agent.velocity.magnitude);
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            Debug.Log("Following leader");
            agent.SetDestination(Wolfleader.transform.position);
            //animator.SetBool("WalkForward", false);
            animator.SetBool("Run Forward", true);
        }
    }

    [SerializeField] private int attackCounter;

    public void AttackPlayer()
    {
        Debug.Log("Attacking");
        //Debug.Log("Speed : " + agent.velocity.magnitude);
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        if (!alreadyAttacked)
        {
            //Code
            animator.SetBool("Run Forward", false);

            attackCounter =  Random.Range(1, 7);
            Debug.Log("Attack: " + attackCounter);

            /*if (attackCounter == 0)
            {
                animator.SetTrigger("Attack1");
            }*/
            if (attackCounter == 1)
            {
                animator.SetTrigger("Attack2");
            }
            else if (attackCounter == 2)
            {
                animator.SetTrigger("Attack3");
            }
            else if (attackCounter == 3)
            {
                animator.SetTrigger("Attack4");
            }
            else if (attackCounter == 4)
            {
                animator.SetTrigger("Attack5");
            }
            else if (attackCounter == 5)
            {
                animator.SetTrigger("Attack6");
            }
            else if (attackCounter == 6)
            {
                animator.SetTrigger("Attack7");
            }
            else if (attackCounter == 7)
            {
                animator.SetTrigger("Attack8");
            }
            GiveDamage();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
        {
            alreadyAttacked = false;
            attackCounter =  Random.Range(1, 7);
            Debug.Log("Attack: " + attackCounter);
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
        GameManager.Instance.HealthDamage(Wolfdamage);

        if (GameManager.Instance.CurrentHealth <= 0)
        {
            Debug.Log("Animal stop");
            animator.SetBool("Idle", true);
        }

        //StartCoroutine(AttackTime());
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(Wolf);
        Instantiate(Meat, gameObject.transform.position, gameObject.transform.rotation);
    }
}
