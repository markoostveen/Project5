using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject m_SpawnPosition;

    [SerializeField]
    private ObjPool m_Pool;

    [SerializeField]
    private SpawnObj[] m_Spawners;

    private void FixedUpdate()
    {
        for (int i = 0; i < m_Spawners.Length; i++)
        {
            if(Random.Range(0,100) < m_Spawners[i].m_SpawnProcentage)
            {
                m_Pool.RemoveFromPool(m_Spawners[i].m_PrefabedObj.name, m_SpawnPosition.transform.position);
            }
        }
    }

}

[System.Serializable]
internal class SpawnObj : PoolObjBase
{
    public float m_SpawnProcentage;
}
