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
            if (Random.Range(0, 100) < M_Spawners[i].m_Obj.m_SpawnProcentage && M_Spawners[i].M_Status)
            {
                int SpawnPointIndex = Random.Range(0, m_SpawnPosition.Length);
                SpawnItem spawnItem = new SpawnItem()
                {
                    m_prefab = M_Spawners[i].m_Obj.m_Prefab,
                    m_Pool = M_Spawners[i].m_Pool,
                    m_SpawnPosition = m_SpawnPosition[SpawnPointIndex]
                };
                if (spawnItem.m_SpawnPosition == null)
                    Debug.LogError("No Spawnpoints for Spawner found", transform);
                M_Wave.Add(spawnItem);
            }
        }
    }
}
