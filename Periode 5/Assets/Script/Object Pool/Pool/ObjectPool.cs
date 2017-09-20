using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool : MonoBehaviour {

    public static ObjectPool Pool;
    private List<KeyValuePair<List<PoolObject>, string>> m_Pools;

    public Poolobj[] ObjectList;

    #region Startup

    public void Inspector()
    {
        if(ObjectList == null)
            ObjectList = new Poolobj[0];
        if (m_Tags   == null)
            m_Tags = new string[0];
    }

    private void Awake()
    {
        //making pool static instance
        if (Pool == null)
            Pool = this;
        else
            Destroy(gameObject);

        if (m_Tags.Length == 0)
            Debug.LogWarning("No tags are associated with spawners, add them, its strongly suggested. if you do not, it will cost some performace on startup",transform);

        //loading default assets in pool
        m_Pools = new List<KeyValuePair<List<PoolObject>, string>>();
        for (int i = 0; i < ObjectList.Length; i++)
        {
            if (ObjectList[i].m_Prefab != null)
            {
                m_Pools.Add(new KeyValuePair<List<PoolObject>, string>(new List<PoolObject>(), ObjectList[i].m_Prefab.name));
                GameObject Parrent = new GameObject(ObjectList[i].m_Prefab.name);
                Parrent.transform.parent = transform;
                List<PoolObject> pool = GetPool(ObjectList[i].m_Prefab.name);
                for (int j = 0; j < ObjectList[i].m_Amount; j++)
                {
                    SpawnInPool(Parrent.transform, ObjectList[i].m_Prefab, pool);
                }
            }
            else
                Debug.LogError("No object attached a default object", transform);
        }

        m_RemappingRequest = true;
    }
    #endregion


    #region PoolChanges
    #region Spawning
    public PoolObject Spawn(string name)
    {
        List<PoolObject> Poollist = GetPool(name);

        if (Poollist != null)
            if (Poollist.Count < 1)
                return null;

        return RemoveFromPool(GetPool(name));
    }
    public PoolObject Spawn(string name, Vector3 SpawnPosition)
    {
        List<PoolObject> Poollist = GetPool(name);

        if (Poollist != null)
            if (Poollist.Count < 1)
                return null;

        PoolObject obj = RemoveFromPool(GetPool(name));
        if (obj != null)
            obj.transform.position = SpawnPosition;
        return obj;
    }
    public PoolObject Spawn(string name, Transform Parrent)
    {
        List<PoolObject> Poollist = GetPool(name);

        if (Poollist != null)
            if (Poollist.Count < 1)
                return null;

        PoolObject obj = RemoveFromPool(GetPool(name));
        if (obj != null)
            obj.transform.parent = Parrent;
        return obj;
    }
    public PoolObject Spawn(GameObject prefab)
    {
        List<PoolObject> Poollist = GetPool(prefab.name);

        if (Poollist != null)
            if (Poollist.Count < 1)
            {
                Debug.LogWarning("Not enough " + prefab.name + " in objectpool, " + 2 + " new " + prefab.name + " has been instantiated.");
                LoadExtraItems(2, prefab);
            }


        return RemoveFromPool(GetPool(prefab.name));
    }
    public PoolObject Spawn(GameObject prefab, Vector3 SpawnPosition)
    {
        List<PoolObject> Poollist = GetPool(prefab.name);

        if (Poollist != null)
            if (Poollist.Count < 1)
            {
                Debug.LogWarning("Not enough " + prefab.name + " in objectpool, " + 2 + " new " + prefab.name + " has been instantiated.");
                LoadExtraItems(2, prefab);
            }


        PoolObject obj = RemoveFromPool(GetPool(prefab.name));
        if (obj != null)
            obj.transform.position = SpawnPosition;
        return obj;
    }
    public PoolObject Spawn(GameObject prefab, Transform Parrent)
    {
        List<PoolObject> Poollist = GetPool(prefab.name);

        if(Poollist != null)
            if (Poollist.Count < 1)
            {
                Debug.LogWarning("Not enough " + prefab.name + " in objectpool, " + 2 + " new " + prefab.name + " has been instantiated.");
                LoadExtraItems(2, prefab);
            }


        PoolObject obj = RemoveFromPool(GetPool(prefab.name));
        if (obj != null)
            obj.transform.parent = Parrent;
        return obj;
    }
    public PoolObject Spawn(List<PoolObject> objectpool)
    {
        return RemoveFromPool(objectpool);
    }
    public PoolObject Spawn(List<PoolObject> objectpool, GameObject Prefab)
    {
        if (objectpool.Count < 1)
        {
            Debug.LogWarning("Not enough " + Prefab.name + " in objectpool, " + 2 + " new " + Prefab.name + " has been instantiated.");
            LoadExtraItems(2, Prefab);
        }

        PoolObject obj = RemoveFromPool(objectpool);
        return obj;
    }
    public PoolObject Spawn(List<PoolObject> objectpool, Vector3 SpawnPosition)
    {
        PoolObject obj = RemoveFromPool(objectpool);
        if(obj != null)
            obj.transform.position = SpawnPosition;
        return obj;
    }
    public PoolObject Spawn(List<PoolObject> objectpool, Transform Parrent)
    {
        PoolObject obj = RemoveFromPool(objectpool);
        if (obj != null)
            obj.transform.parent = Parrent;
        return obj;
    }
    public PoolObject Spawn(List<PoolObject> objectpool, GameObject prefab , Vector3 SpawnPosition)
    {
        if (objectpool.Count < 1)
        {
            Debug.LogWarning("Not enough " + prefab.name + " in objectpool, " + 2 + " new " + prefab.name + " has been instantiated.");
            LoadExtraItems(2, prefab);
        }

        PoolObject obj = RemoveFromPool(objectpool);
        if (obj != null)
            obj.transform.position = SpawnPosition;
        return obj;
    }
    public PoolObject Spawn(List<PoolObject> objectpool, GameObject prefab, Transform Parrent)
    {
        if (objectpool.Count < 1)
        {
            Debug.LogWarning("Not enough " + prefab.name + " in objectpool, " + 2 + " new " + prefab.name + " has been instantiated.");
            LoadExtraItems(2, prefab);
        }

        PoolObject obj = RemoveFromPool(objectpool);
        if (obj != null)
            obj.transform.parent = Parrent;
        return obj;
    }
    #endregion

    private void AddToPool(List<PoolObject> m_Pool, PoolObject Script, Transform parrent)
    {
        m_Pool.Add(Script);
        Script.gameObject.transform.parent = parrent;
    }

    private PoolObject RemoveFromPool(List<PoolObject> Poollist)
    {
        if (Poollist.Count > 0)
        {
            PoolObject Script = Poollist[0];
            Poollist.Remove(Script);
            Script.gameObject.SetActive(true);
            Script.Activate();
            return Script;
        }

        return null;
    }

    private void SpawnInPool(Transform Parrent, GameObject prefab, List<PoolObject> pool)
    {
        GameObject NewObj = Instantiate(prefab,Parrent);
        PoolObject obj = NewObj.GetComponent<PoolObject>();
        if(obj == null)
        {
            NewObj.AddComponent<PoolObject>();
            obj = NewObj.GetComponent<PoolObject>();
        }
        pool.Add(obj);
        PoolObjectInfo info = new PoolObjectInfo
        {
            Pool = GetPool(prefab.name),
            Parrent = Parrent.transform,
            Callback = new PoolObjectCallback(AddToPool)
        };

        pool[pool.Count - 1].Initialize(info);
    }

    public void LoadExtraItems(int Amount, GameObject Prefab)
    {
        Poolobj Input = new Poolobj()
        {
            m_Amount = Amount,
            m_Prefab = Prefab
        };

        if (GetPool(Input.m_Prefab.name) == null)
        {

            m_Pools.Add(new KeyValuePair<List<PoolObject>, string>(new List<PoolObject>(), Input.m_Prefab.name));
            GameObject Parrent = new GameObject(Input.m_Prefab.name);
            Parrent.transform.parent = transform;
            List<PoolObject> pool = GetPool(Input.m_Prefab.name);
            for (int j = 0; j < Input.m_Amount; j++)
            {
                SpawnInPool(Parrent.transform, Input.m_Prefab, pool);
            }

            m_RemappingRequest = true;
            Debug.Log("New objectpool created, poolname: " + Input.m_Prefab.name);
        }
        else
        {
            for (int j = 0; j < Input.m_Amount; j++)
            {
                Transform Parrent = transform.Find(Input.m_Prefab.name);
                List<PoolObject> pool = GetPool(Input.m_Prefab.name);
                SpawnInPool(Parrent, Input.m_Prefab, pool);
            }
        }

    }

    #endregion


    #region Other

    public bool IsRemapping { get; private set; }
    public bool m_RemappingRequest;
    public string[] m_Tags;

    public IEnumerator RemapPoolData()
    {
        if (!IsRemapping)
        {
            IsRemapping = true;
            m_RemappingRequest = false;
            yield return null;
            foreach (string tagname in m_Tags)
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag(tagname))
                {
                    foreach (ISpawner j in i.GetComponents<ISpawner>())
                        j.Initialize();
                }
            }
            IsRemapping = false;
            Debug.Log("Remapping Pool Completed");
        }
        else
            Debug.LogWarning("Remapping already running, exiting");
    }

    public List<PoolObject> GetPool(string PoolName)
    {
        for (int i = 0; i < m_Pools.Count; i++)
        {
            if (m_Pools[i].Value == PoolName)
                return m_Pools[i].Key;
        }

        return null;
    }

    private void Update()
    {
        if (m_RemappingRequest && !IsRemapping)
            StartCoroutine(RemapPoolData());
    }

    #endregion
}




[System.Serializable]
public sealed class Poolobj : PoolObjBase
{
        public int m_Amount = 1;
}

public class PoolObjBase {
    public GameObject m_Prefab;
}
