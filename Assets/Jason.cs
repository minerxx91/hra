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
        navMeshAgent.speed = 4.5f;
        if (!walkPointSet)
        {
            walkPoint = new Vector3(Random.Range(transform.position.x - moveRange, transform.position.x + moveRange), transform.position.y, Random.Range(transform.position.z - moveRange, transform.position.z + moveRange));
            walkPointSet = true;
            walkTime = 0f;
        }
        navMeshAgent.destination = walkPoint;
        
    }

    private void Chasing()
    {
        navMeshAgent.speed = 7f;
        walkPoint = playerPosition.position;
        navMeshAgent.destination = walkPoint;
        walkPointSet = false;
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

        var distance = Vector3.Distance(playerPosition.position, transform.position);
        print("distance: "+distance);
        print("jason rotation: " + transform.eulerAngles.y);
        print("speed: " + navMeshAgent.speed);


        //check if player is in sight range
        if (distance <= 50)
        {
            if (transform.eulerAngles.y > 210 && transform.eulerAngles.y < 330 && (playerPosition.position.x - transform.position.x) < 0) playerInSightRange = true;
            else if (transform.eulerAngles.y > 30 && transform.eulerAngles.y < 150 && (playerPosition.position.x - transform.position.x) > 0) playerInSightRange = true;
            else if (transform.eulerAngles.y > 120 && transform.eulerAngles.y < 240 && (playerPosition.position.z - transform.position.z) < 0) playerInSightRange = true;
            else if (((transform.eulerAngles.y > 300 && transform.eulerAngles.y < 360) || transform.eulerAngles.y < 60) && (playerPosition.position.z - transform.position.z) > 0) playerInSightRange = true;
            else playerInSightRange = false;
        }
        else playerInSightRange = false;
        if (distance <= 20) playerInSightRange = true;

        //check if player is in attack range
        if (distance <= 1)
            playerInAttackRange = true;
        else playerInAttackRange = false;
        

        if (!playerInAttackRange && !playerInSightRange) Patroling();
        else if (!playerInAttackRange && playerInSightRange) Chasing();
        else if (playerInAttackRange && playerInSightRange) Attacking();
        if (walkTime > 2f && navMeshAgent.velocity.magnitude == 0) walkPointSet = false;

        walkTime += Time.deltaTime;
    }
}
