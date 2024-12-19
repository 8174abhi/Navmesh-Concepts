using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AttackRadius AttackRadius;
    [SerializeField] Animator animator;
    private Coroutine LookCoroutine;
    [SerializeField] private int Health = 300;
    private const string Attack_Trigger = "Attack";
    
    void Awake()
    {
        AttackRadius = GetComponent<AttackRadius>();

    }
    void OnAttack(IDamageable Target)
    {
        animator.SetTrigger(Attack_Trigger);
        if(LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);   
        }
        //LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));

    }
}
