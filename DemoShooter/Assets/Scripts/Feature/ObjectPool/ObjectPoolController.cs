using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    public static ObjectPoolController Instance;

    public Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public ObjectPool GetOrCreate(string poolName)
    {
        var n = poolName;
        if (pools.ContainsKey(n))
        {
            return pools[n];
        }
        
        var poolGO = new GameObject(n);
        poolGO.transform.SetParent(transform);
        
        var prefab = Resources.Load<GameObject>($"Prefabs/Pools/{poolName}");
        
        var objPool = poolGO.AddComponent<ObjectPool>();
        objPool.SetPrefab(prefab);
        
        pools.Add(n, objPool);

        return objPool;
    }
    
    public ObjectPool GetOrCreate(string bundle, string poolName)
    {
        var n = $"{bundle}/{poolName}";
        return GetOrCreate(n);
    }
}
