using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject prefab;

    private List<IPooledObject> inPool = new List<IPooledObject>();
    private List<IPooledObject> onUsePool = new List<IPooledObject>();

    public void SetPrefab(GameObject pf)
    {
        prefab = pf;
    }

    public void Dispose(IPooledObject target)
    {
        target.Dispose();
        target.GO.SetActive(false);
        target.GO.transform.SetParent(transform);

        var disposeSuccess = onUsePool.Remove(target);
        
        if (!disposeSuccess)
            Debug.LogWarning("Object Disposed but not in use pool!");
        
        inPool.Add(target);
    }

    public IPooledObject Instantiate()
    {
        if (inPool.Count == 0)
        {
            // 새로 생성이 필요할 때 마다 2의 배수씩 늘어나도록 처리해야 함.
            var obj = Instantiate(prefab).GetComponent<IPooledObject>();
            onUsePool.Add(obj);
            obj.GO.SetActive(true);
            obj.OnInitialized();
            return obj;
        }

        var getObj = inPool[0];
        inPool.RemoveAt(0);
        onUsePool.Add(getObj);
        getObj.GO.SetActive(true);
        getObj.OnInitialized();

        return getObj;
    }
}
