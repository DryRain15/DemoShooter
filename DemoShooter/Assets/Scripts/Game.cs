using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    
    public Character player;

    private List<IPooledObject> test = new List<IPooledObject>();
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        player = ObjectPoolController.Instance.GetOrCreate("Objects", "CharacterHolder").Instantiate() as Character;
        player.Initiate(CharacterType.Player);
        
        var enemy = ObjectPoolController.Instance.GetOrCreate("Objects", "CharacterHolder").Instantiate() as Character;
        enemy.Initiate(CharacterType.TutoEnemy);
        enemy.Position = Vector3.left * 4f;
    }

    // Update is called once per frame
    void Update()
    {
        return;
        
        if (Input.GetMouseButtonDown(0) && test.Count < 8)
        {
            var obj = ObjectPoolController.Instance.GetOrCreate("Test", "TestCircle").Instantiate();
            test.Add(obj);
        }

        if (Input.GetMouseButtonDown(1) && test.Count > 0)
        {
            var disposeTarget = test[0];
            test.RemoveAt(0);
            ObjectPoolController.Instance.GetOrCreate(disposeTarget.poolName).Dispose(disposeTarget);
        }
    }
}
