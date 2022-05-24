using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jason : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    public Vector3 walkPoint;
    bool walkPointSet;
    LayerMask whatIsPlayer;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float moveRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private NavMeshAgent navMeshAgent;

    private void Patroling()
    {
        if (!walkPointSet)
        {
            walkPoint = new Vector3(Random.Range(transform.position.x - moveRange, transform.position.x + moveRange), transform.position.y, Random.Range(transform.position.z - moveRange, transform.position.z + moveRange));
            walkPointSet = true;
            walkTime = 0f;
        }
        else
        {
            navMeshAgent.destination = walkPoint;
        }
    }

    private void Chasing()
    {
        walkPoint = playerPosition.position;
        navMeshAgent.destination = walkPoint;
    }

    private void Attacking()
    {

    }

     float walkTime = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (transform.position.x-2 < walkPoint.x && transform.position.x + 2 > walkPoint.x && transform.position.z == walkPoint.z) walkPointSet = false;
        if (walkPoint.x < 5 || walkPoint.x > 995 || walkPoint.z < 5 || walkPoint.z > 995) walkPointSet = false;
        print(navMeshAgent.velocity.magnitude);
        


        /*playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);*/

        if (!playerInAttackRange && !playerInSightRange) Patroling();
        else if (!playerInAttackRange && playerInSightRange) Chasing();
        else if (playerInAttackRange && playerInSightRange) Attacking();
        if (walkTime > 1 && navMeshAgent.velocity.magnitude == 0) walkPointSet = false;

        walkTime += Time.deltaTime;
    }
}
