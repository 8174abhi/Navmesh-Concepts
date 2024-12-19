using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    public Transform Player;
    public int NumberofEnemiestoSpawn = 5;
    public float SpawnDelay = 2f;
    public List<EnemyBehaviour> Enemies = new List<EnemyBehaviour>();
    public Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
    public NavMeshTriangulation Triangulation;

    private void Awake()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(Enemies[i], NumberofEnemiestoSpawn));
        }
    }
    private void Start()
    {
        Triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        int SpawnedEnemies = 0;
        while (SpawnedEnemies < NumberofEnemiestoSpawn)
        {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemies(SpawnedEnemies);
            }
            else if (EnemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            SpawnedEnemies++;
            yield return new WaitForSeconds(SpawnDelay);

        }
    }
    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
    void SpawnRoundRobinEnemies(int SpawnedEnemies)
    {
        int SpawnIndex = SpawnedEnemies % Enemies.Count;
        DoSpawnEnemy(SpawnIndex);

    }
    void DoSpawnEnemy(int SpawnIndex)
    {
        PoolableObject poolableObject = EnemyObjectPools[SpawnIndex].GetObject();
        if (poolableObject != null)
        {
            EnemyBehaviour enemy = poolableObject.GetComponent<EnemyBehaviour>();
         
            int vertexIndex = Random.Range(0, Triangulation.vertices.Length);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
                enemy.Movement.target = Player;
                enemy.Agent.enabled = true;
                enemy.Movement.StartChasing();
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on Navmesh,Tried to use{Triangulation.vertices[vertexIndex]}");
            }

        }
        else
        {
            Debug.LogError($"Unable to fetch enemy of type{SpawnIndex}from object pool.Out of Objects?");
        }

    }
    void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, Enemies.Count));
    }


}
