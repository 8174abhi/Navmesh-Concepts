using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent (typeof(Collider))]
public class AttackRadius : MonoBehaviour
{
    private List<IDamageable>damages=new List<IDamageable>();
    public int Damage = 10;
    public float AttackDelay = .5f;
    public delegate void  AttackEvent(IDamageable target);
    public AttackEvent OnAttack;
    private Coroutine AttackCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable=other.GetComponent<IDamageable>();
        if(damageable != null )
        {
            damages.Add( damageable );  
            if(AttackCoroutine==null )
            {
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable =other.GetComponent<IDamageable>();  
       if(damageable != null )
        {
            damages.Remove( damageable );
            if(damages.Count==0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }

    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackDelay);
        float ClosestDistance=float.MaxValue;
        IDamageable ClosestDamagable=null;
        while (damages.Count>0)
        {
            for(int i=0; i<damages.Count; i++)
            {
                Transform DamagebleTransform = damages[i].GetTransform();
                float distance=Vector3.Distance(transform.position, DamagebleTransform.position);
                if(distance < ClosestDistance )
                {
                    ClosestDistance = distance;
                    ClosestDamagable = damages[i];
                }
            }
            if(ClosestDamagable != null)
            {
                OnAttack?.Invoke(ClosestDamagable);
                ClosestDamagable.TakeDamage(Damage);

            }
            ClosestDamagable= null;
            ClosestDistance= float.MaxValue;
            yield return new WaitForSeconds (AttackDelay);
            damages.RemoveAll(DisableDamagebles);

        }
        AttackCoroutine= null;
    }
    bool DisableDamagebles(IDamageable Damageble)
    {
        return Damageble !=null && !Damageble.GetTransform().gameObject.activeSelf;
    }


}
