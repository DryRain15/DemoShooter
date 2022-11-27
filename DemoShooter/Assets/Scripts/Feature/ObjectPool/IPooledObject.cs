using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public void OnInitialized();
    public void Dispose();

    public GameObject GO { get; }
    public string poolName { get; }
}
