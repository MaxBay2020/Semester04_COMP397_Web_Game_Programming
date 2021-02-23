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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 size = new Vector3(4, 4, 10);
        //RaycastHit hit;
        //hasLOS = Physics.BoxCast(transform.position + LOSoffset, size, transform.forward, out hit, transform.rotation, 10.0f, collisionLayer);
        ////Debug.DrawRay(transform.position + LOSoffset, transform.forward);
        //if (hasLOS)
        //{
        //    Debug.Log(hit.transform.name);
        //}

        if (hasLOS)
        {
            animator.SetInteger("AnimState", (int)ZombieState.RUN);
            agent.SetDestination(player.transform.position);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)ZombieState.JUMP);
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= 5.0f)
            {
                animator.SetInteger("AnimState", (int)ZombieState.DANCE);
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

    //private void OnDrawGizmos()
    //{
    //    Vector3 offset = new Vector3(0, 3, 5);
    //    Vector3 size = new Vector3(4, 4, 10);
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireCube(transform.position + offset, size);
    //}
}
