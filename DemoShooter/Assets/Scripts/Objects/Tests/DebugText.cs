using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private TMP_Text txt;

    private object _target;

    public void SetTarget(object target)
    {
        _target = target;
    }

    public void SetTransform(Vector3 offset, Transform parent = null)
    {
        transform.SetParent(parent);
        transform.localPosition = offset;
    }
    
    private void Update()
    {
        txt.text = _target.ToString();
    }

    public void OnInitialized()
    {
        txt.text = "";
    }

    public void Dispose()
    {
        txt.text = "";
    }

    public GameObject GO => gameObject;
    public string poolName => "Text/DebugText";
}
