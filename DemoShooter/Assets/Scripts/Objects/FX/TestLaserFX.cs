using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaserFX : MonoBehaviour, IPooledObject
{
    private SpriteRenderer _sr;
    private float _innerTimer;
    private float length;
    
    public void OnInitialized()
    {
        poolName = "FX/TestLaserFX";
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.yellow;
    }

    public void Initiate(Vector3 from, Vector3 to)
    {
        Position = (from + to) * 0.5f;
        var dist = to - from;
        Angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        Transform.localEulerAngles = Angle * Vector3.forward;
        length = (to - from).magnitude;
        Transform.localScale = new Vector3(length, 0.3f, 1f);
    }

    public void Dispose()
    {
        _innerTimer = 0f;
        Transform.localScale = Vector3.one;
        Angle = 0f;
    }

    public GameObject GO => gameObject;
    public string poolName { get; private set; }

    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Transform Transform { get => transform; }
    public float Angle { get; private set; }


    private void Update()
    {
        var dt = Time.deltaTime;
        
        if (_innerTimer > 0.3f)
        {
            ObjectPoolController.Instance.GetOrCreate(poolName).Dispose(this);
            return;
        }
        
        Transform.localScale = new Vector3(length, 0.3f - _innerTimer, 1f);

        _innerTimer += dt;
    }
}
