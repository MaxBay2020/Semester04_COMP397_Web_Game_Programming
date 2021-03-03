using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    IDLE,
    RUN,
    JUMP,
    DANCE
}

public class ZombieBahavior : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    [Header("Line of Sight")]
    //public LayerMask collisionLayer;
    //public Vector3 LOSoffset = new Vector3(0, 2.6f, 0);
    public bool hasLOS;
    public GameObject player;
    private Vector3 originalPosition;

    [Header("Attack")]
    public float attackDistance = 5.0f;
    private AudioSource audioSource;
    public AudioClip danceClip;
    public PlayerBehavior playerBehavior;
    public float damageValue;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        playerBehavior = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per dframe
    void FixedUpdate()
    {

        if (hasLOS)
        {
            animator.SetInteger("AnimState", (int)ZombieState.RUN);
            agent.SetDestination(player.transform.position);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)ZombieState.JUMP);
            }

            if (Time.frameCount % 60==0)
            {
                DoDanceDamage();
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                animator.SetInteger("AnimState", (int)ZombieState.DANCE);
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = danceClip;
                    audioSource.Play();
                }
                
                transform.LookAt(transform.position-player.transform.forward);
            }
        }
        else if(Vector3.Distance(transform.position, originalPosition)<3.0f)
        {
            animator.SetInteger("AnimState", (int)ZombieState.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetInteger("AnimState", (int)ZombieState.JUMP);

        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasLOS = true;
            player = other.gameObject;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasLOS = false;
            agent.SetDestination(originalPosition);
        }
            
    }

    private void DoDanceDamage()
    {
        playerBehavior.TakeDamage(damageValue);
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 offset = new Vector3(0, 3, 5);
    //    Vector3 size = new Vector3(4, 4, 10);
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireCube(transform.position + offset, size);
    //}
}
