using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcentageSpawnerMultipeSpawnPoints : Spawner {

    [HideInInspector]
    public Transform[] m_SpawnPosition;

    protected override void FixedUpdate()
    {
        for (int i = 0; i < M_Spawners.Count; i++)
        {
            if (Random.Range(0, 100) < M_Spawners[i].m_Obj.m_SpawnProcentage && M_Spawners[i].M_Status)
            {
                int spawnpointindex = Random.Range(0, m_SpawnPosition.Length);
                Spawn(i, M_Spawners[i].m_Pool, M_Spawners[i].m_Obj.m_Prefab, m_SpawnPosition[spawnpointindex]);
            }
        }
    }
}
