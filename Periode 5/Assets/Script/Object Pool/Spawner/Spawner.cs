using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour, ISpawner {

    public List<SpawnerInfo> M_Spawners { get; set; }

    public Transform[] M_SpawnPosition { get; set; }

    #region Initialization

    public void Initialize()
    {
        MakePoolReference();
        Checkreferences();
    }

    protected virtual void MakePoolReference()
    {
        //get pool reference
        for (int i = 0; i < M_Spawners.Count; i++)
        {
            try
            {
                M_Spawners[i] = new SpawnerInfo()
                {
                    m_Obj = M_Spawners[i].m_Obj,
                    m_Pool = ObjectPool.Pool.GetPool(M_Spawners[i].m_Obj.m_Prefab.name),
                    M_Status = M_Spawners[i].M_Status
                };
            }
            catch (NullReferenceException) { }
        }
    }

    protected virtual bool Checkreferences()
    {
        //check if all items have a refrence
        for (int i = 0; i < M_Spawners.Count; i++)
        {
            try
            {
                if (M_Spawners[i].m_Pool == null && M_Spawners[i].m_Obj.m_Prefab != null)
                {
                    ObjectPool.Pool.LoadExtraItems(25, M_Spawners[i].m_Obj.m_Prefab);
                    Debug.LogWarning("Spawned 25 " + M_Spawners[i].m_Obj.m_Prefab.name + " because non spawned on default, check objectpool", transform);
                    return true;
                }
            }
            catch (NullReferenceException) { }

        }
        return false;
    }

    #endregion

    #region Spawning

    protected bool Spawn(int index, List<PoolObject> pool, GameObject prefab, Vector3 Spawnpoint)
    {
        if (pool != null && prefab != null)
        {
            //if(Spawnpoint == Vector3.zero)
            //{
            //    Spawnpoint = Vector3.zero;
            //    Debug.LogWarning("No Spawnpoint was given, instead spawning object at " + Vector3.zero, transform);
            //}

            if (ObjectPool.Pool.Spawn(pool,prefab, Spawnpoint) == null)
                return false;
        }
        else
        {
            Initialize();
            if (prefab == null)
                Debug.LogWarning("Trying to spawn Prefab but no prefab is given, aborting spawn", transform);
            else
                Debug.LogWarning("No Pool could be found for " + prefab.name + " remapping ObjectPools for Spawner", transform);
            return false;
        }

        return true;
    }

    #endregion

    protected virtual void FixedUpdate() { }
}

#region Structs and classes

///used to store only iteminfo and a pool which it will be in
[System.Serializable]
public struct SpawnItem
{
    [HideInInspector]
    public GameObject m_prefab;
    [HideInInspector]
    public List<PoolObject> m_Pool;
    public Transform m_SpawnPosition;
}

///Spawner that can be customized in inspector
[System.Serializable]
public struct SpawnerInfo
{
    public SpawnObj m_Obj;
    [HideInInspector]
    public List<PoolObject> m_Pool;
    public Transform m_SpawnPoint;
    public bool M_Status;
}

[System.Serializable]
public sealed class SpawnObj : PoolObjBase
{
    public float m_SpawnProcentage;
}

#endregion
