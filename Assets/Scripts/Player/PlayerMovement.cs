using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Camera Cam;
    private RaycastHit[] hits = new RaycastHit[1];
    [SerializeField] private Animator animator;
    private AgentLinkMover Linkmover;
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
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.RaycastNonAlloc(ray, hits) > 0)
            {
                agent.SetDestination(hits[0].point);
            }
        }
        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.01f);




    }


}

