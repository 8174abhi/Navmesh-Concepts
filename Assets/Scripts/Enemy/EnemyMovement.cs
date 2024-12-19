using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float Updatedspeed = .1f;
    private NavMeshAgent agent;
    public Animator animator;
    AgentLinkMover Linkmover;
    [SerializeField] private Coroutine FollowCoroutine;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Linkmover = GetComponent<AgentLinkMover>();

        Linkmover.OnLinkStart += HandleLinkStart;
        Linkmover.OnLinkStart += HandleLinkEnd;
    }
    void HandleLinkStart()
    {
        animator.SetTrigger("Jump");

    }
    void HandleLinkEnd()
    {
        animator.SetTrigger("Landed");
    }
    //private void Start()
    //{
    //    StartCoroutine(FollowTarget());
    //}
    IEnumerator FollowTarget()
    {
        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            yield return new WaitForSeconds(Updatedspeed);

        }
    }
    public void StartChasing()
    {
        if (FollowCoroutine == null)
        {
            FollowCoroutine = StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.LogWarning("Called StartChasing On Enemy that is already Chasing! This is likely a bug in some chasing class!");
        }

    }
    private void Update()
    {
        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.01f);

    }
}
