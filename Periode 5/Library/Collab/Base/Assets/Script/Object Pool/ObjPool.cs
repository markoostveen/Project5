using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour {

    [SerializeField]
    private Pooledobj[] m_PooledObjs;

    private List<KeyValuePair<List<PoolAbleObj>,string>> m_Pools;

	private void Start () {

        m_Pools = new List<KeyValuePair<List<PoolAbleObj>, string>>();
        for (int i = 0; i < m_PooledObjs.Length; i++)
        {
            m_Pools.Add( new KeyValuePair<List<PoolAbleObj>, string>(new List<PoolAbleObj>(), m_PooledObjs[i].m_PrefabedObj.name));
            for (int j = 0; j < m_PooledObjs[i].m_Amount; j++)
            {
                GameObject NewObj = Instantiate(m_PooledObjs[i].m_PrefabedObj);
                print(NewObj.GetComponent<PoolAbleObj>());
                NewObj.transform.parent = transform;
                m_Pools[i].Key.Add(NewObj.GetComponent<PoolAbleObj>());
                m_Pools[i].Key[j].Initialize(m_PooledObjs[i].m_PrefabedObj.name, new PoolAbleObjCallback(AddToPool));
            }
        }
	}

    private void AddToPool(string PoolName, PoolAbleObj Script)
    {
        for (int i = 0; i < m_Pools.Count; i++)
        {
            if(m_Pools[i].Value == PoolName)
            {
                m_Pools[i].Key.Add(Script);
                Script.gameObject.transform.parent = transform;
            }
        }
    }

    public void RemoveFromPool(string PoolName, Vector3 Position)
    {
        for (int i = 0; i < m_Pools.Count; i++)
        {
            if (m_Pools[i].Value == PoolName && m_Pools[i].Key.Count > 0)
            {
                PoolAbleObj Script = m_Pools[i].Key[0];
                m_Pools[i].Key.Remove(Script);
                Script.gameObject.SetActive(true);
                Script.transform.position = Position;
                Script.Activate();
            }
        }
    }

}




[System.Serializable]
internal class Pooledobj : PoolObjBase{
        public int m_Amount;
}

public class PoolObjBase {
    public GameObject m_PrefabedObj;
}

