using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private PoolableObject Prefab;
    private List<PoolableObject> Availableobjects;
    private ObjectPool(PoolableObject Prefab, int Size)
    {
        this.Prefab = Prefab;
        Availableobjects = new List<PoolableObject>();
    }
    public static ObjectPool CreateInstance(PoolableObject Prefab, int Size)
    {
        ObjectPool pool = new ObjectPool(Prefab, Size);
        GameObject PoolObject = new GameObject(Prefab.name + "Pool");
        pool.CreateObjects(PoolObject.transform, Size);
        return pool;

    }
    public void CreateObjects(Transform parent, int Size)
    {
        for (int i = 0; i < Size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }
    public void ReturnObjectstopool(PoolableObject poolableObjects)

    {
        Availableobjects.Add(poolableObjects);
    }
    public PoolableObject GetObject()
    {
        if (Availableobjects.Count > 0)
        {
            PoolableObject instance = Availableobjects[0];
            Availableobjects.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return null;
        }
    }
}
