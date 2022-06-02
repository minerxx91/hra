using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Jason : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject player;
    public Vector3 walkPoint;
    bool walkPointSet;
    LayerMask whatIsPlayer;
    Animator animator;
    bool attack = false;
    float attackTime = 0f;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float moveRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public NavMeshAgent navMeshAgent;
    public bool Stun;
    float StunTime = 5f;
    float walkTime = 0f;

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
        try
        {
            navMeshAgent.speed = 7f;
            walkPoint = playerPosition.position;
            navMeshAgent.destination = walkPoint;

            walkPointSet = false;

            if (!source.isPlaying)
            {
                source.PlayOneShot((AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/roar.mp3", typeof(AudioClip)));
            }
        }
        catch { }
    }

    private void Attacking()
    {
        attack = true;
        player.transform.SetParent(hand.transform, true);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.localPosition = new Vector3(-0.839999974f, -0.0900000036f, 0.910000026f);
        player.transform.localRotation = Quaternion.Euler(79.3000336f, 170.300003f, 34.6000137f);
    }
     

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Stun = false;
        animator = GetComponent<Animator>();
        source.pitch = 0.7f;
        source.volume = 0.2f;
    }

    void Update()
    {
        if (transform.position.x-2 < walkPoint.x && transform.position.x + 2 > walkPoint.x && transform.position.z == walkPoint.z) walkPointSet = false;

        var distance = Vector3.Distance(playerPosition.position, transform.position);
        /*print("distance: "+distance);
        print("jason rotation: " + transform.eulerAngles.y);
        print("speed: " + navMeshAgent.speed);*/


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
        if (!attack)
        {
            try
            {
                if (!playerInAttackRange && !playerInSightRange) Patroling();
                else if (!playerInAttackRange && playerInSightRange) Chasing();
                else if (playerInAttackRange && playerInSightRange) Attacking();
            }
            catch { }
            if (walkTime > 2f && navMeshAgent.velocity.magnitude == 0) walkPointSet = false;
        }
        

        walkTime += Time.deltaTime;
        /*print("stun: "+Stun);
        print("stun time: " + StunTime);
        print("navmesh: " + navMeshAgent.enabled);*/
        if (Stun)
        {
            StunTime = 0f;
            Stun = false;
        }

        if (StunTime < 5f)
        {
            StunTime += Time.deltaTime;
            navMeshAgent.enabled = false;
            animator.SetBool("isWalking", false);
        }
        else
        {
            navMeshAgent.enabled = true;
            animator.SetBool("isWalking", true);
        }
        //print("navmesh: " + navMeshAgent.enabled);

        if (attack)
        {
            if (attackTime > 5) SceneManager.LoadScene("Menu");
            else attackTime += Time.deltaTime;
        }

    }
}
