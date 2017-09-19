using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : Spawner, IWaveSpawner {

    public float M_Timer { set; get; }

    [HideInInspector]
    public float M_MaxTimer { get; set; }

    [HideInInspector]
    public Transform[] m_SpawnPosition;

    [HideInInspector]
    public List<SpawnItem> M_Wave { get; set; }

    private void OnEnable()
    {
        M_Wave = new List<SpawnItem>();
    }

    protected override void FixedUpdate()
    {
        M_Timer -= Time.deltaTime;
        if (M_Timer < 0 && !IsSpawning)
        {
            StopCoroutine(WaveSpawn());
            StartCoroutine(WaveSpawn());
        }


        for (int i = 0; i < M_Spawners.Count; i++)
        {
            if (Random.Range(0, 100) < M_Spawners[i].m_Obj.m_SpawnProcentage && M_Spawners[i].M_Status)
            {
                SpawnItem spawnItem = new SpawnItem()
                {
                    m_prefab = M_Spawners[i].m_Obj.m_Prefab,
                    m_Pool = M_Spawners[i].m_Pool,
                    m_SpawnPosition = m_SpawnPosition[0]
                };
                M_Wave.Add(spawnItem);
            }
        }
    }

    private bool IsSpawning;
    protected IEnumerator WaveSpawn()
    {
        IsSpawning = true;

        SpawnItem[] tempwave = M_Wave.ToArray();

        M_Wave.Clear();
        M_Timer = M_MaxTimer;

        for (int i = 0; i < tempwave.Length; i++)
        {
            bool success = Spawn(i, tempwave[i].m_Pool, tempwave[i].m_prefab, tempwave[i].m_SpawnPosition);
            if (!success)
            {
                yield return new WaitForEndOfFrame();
                success = Spawn(i, tempwave[i].m_Pool, tempwave[i].m_prefab, tempwave[i].m_SpawnPosition);
                if(!success)
                    Debug.LogWarning("Spawn function failed");
            }
            yield return new WaitForSecondsRealtime(0.005f);
        }
        IsSpawning = false;
    }
}
