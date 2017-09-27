using System.Collections.Generic;
using UnityEngine;


public delegate void PoolObjectCallback(List<PoolObject> Pool, PoolObject Script, Transform Parrent);
public struct PoolObjectInfo { public List<PoolObject> Pool; public Transform Parrent; public PoolObjectCallback Callback; }

public class PoolObject :  MonoBehaviour
{

    private PoolObjectInfo m_Info;

    public void Initialize(PoolObjectInfo Info)
    {
        m_Info = Info;
        gameObject.SetActive(false);
    }

    public virtual void Activate()
    {
        transform.SetParent(null);
    }

    protected virtual void Deactivate()
    {
        if (m_Info.Callback != null)
            m_Info.Callback.Invoke(m_Info.Pool, this, m_Info.Parrent);
        else
            Debug.LogError("Callback can't be found", transform);
        gameObject.SetActive(false);
    }
}
