using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPooledObject : MonoBehaviour, IFieldObject, IPooledObject
{
    public void OnInitialized()
    {
        poolName = "Test/TestCircle";
        Position = CameraFollow.Instance.transform.position + new Vector3(-2f, 1f, 10f);
    }

    public void Dispose()
    {
    }

    public GameObject GO => gameObject;
    public string poolName { get; private set; }

    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Transform Transform { get => transform; }
    public float Angle { get; private set; }
}
