using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : PoolableObject
{
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public int Health = 100;
    public EnemySO enemySO;
    public void OnEnable()
    {
        SetupAgentFromConfiguration();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        Agent.enabled = false;
    }
    public virtual void SetupAgentFromConfiguration()
    {
        Agent.acceleration = enemySO.Acceleration;
        Agent.angularSpeed = enemySO.AngularSpeed;
        Agent.areaMask=enemySO.AreaMask;
        Agent.avoidancePriority = enemySO.AvoidancePriority;
        Agent.baseOffset = enemySO.BaseOffset;  
        Agent.height = enemySO.Height;
        Agent.obstacleAvoidanceType = enemySO.ObstacleAvoidancetype;
        Agent.radius = enemySO.Radius;
        Agent.speed = enemySO.Speed;    
        Agent.stoppingDistance = enemySO.StoppingDistance;
        Movement.Updatedspeed= enemySO.AIUpdateInterval;
        Health = enemySO.Health;
    }
}
