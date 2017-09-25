using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerMultipeSpawnPoints : WaveSpawner, IWaveSpawner {

    protected override void FixedUpdate()
    {
        M_Timer -= Time.deltaTime;
        if (M_Timer < 0)
            StartCoroutine(WaveSpawn());

        for (int i = 0; i < M_Spawners.Count; i++)
        {
            if(M_Spawners[i].m_Obj != null)
            {
                if (Random.Range(0, 100) < M_Spawners[i].m_Obj.m_SpawnProcentage && M_Spawners[i].M_Status)
                {
                    int SpawnPointIndex = Random.Range(0, M_SpawnPosition.Length);
                    if (M_SpawnPosition.Length == 0)
                        Debug.LogWarning("No Spawnpoints for Spawner found, Spawn aborted", transform);
                    else
                    {
                        SpawnItem spawnItem = new SpawnItem()
                        {
                            m_prefab = M_Spawners[i].m_Obj.m_Prefab,
                            m_Pool = M_Spawners[i].m_Pool,
                            m_SpawnPosition = M_SpawnPosition[SpawnPointIndex]
                        };
                        M_Wave.Add(spawnItem);
                    }
                }
            }
        }
    }
}
