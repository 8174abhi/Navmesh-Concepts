using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName ="Enemy Configuration",menuName ="ScriptableObject/Enemy Configuration")]
public class EnemySO :ScriptableObject
{
    public int Health = 100;
    public float AIUpdateInterval = .1f;
    public float Acceleration = 10;
    public float AngularSpeed = 120;
    public int AreaMask = -1;
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float Height = 2f;
    public ObstacleAvoidanceType ObstacleAvoidancetype = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = .5f;
    public float Speed = 3f;
    public float StoppingDistance = .5f;
}
