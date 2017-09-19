using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcentageSpawner : Spawner {

    [HideInInspector]
    public Transform[] m_SpawnPosition;

    protected override void MakePoolReference()
    {
        //get pool reference
        for (int i = 0; i < M_Spawners.Count; i++)
        {
            M_Spawners[i] = new SpawnerInfo()
            {
                m_Obj = M_Spawners[i].m_Obj,
                m_Pool = ObjectPool.Pool.GetPool(M_Spawners[i].m_Obj.m_Prefab.name),
                M_Status = M_Spawners[i].M_Status,
                m_SpawnPoint = m_SpawnPosition[0]
            };
        }
    }

    protected override void FixedUpdate()
    {
        for (int i = 0; i < M_Spawners.Count; i++)
        {
            if (Random.Range(0, 100) < M_Spawners[i].m_Obj.m_SpawnProcentage && M_Spawners[i].M_Status)
            {
                Spawn(i,M_Spawners[i].m_Pool, M_Spawners[i].m_Obj.m_Prefab, M_Spawners[i].m_SpawnPoint);
            }
        }
    }
}
