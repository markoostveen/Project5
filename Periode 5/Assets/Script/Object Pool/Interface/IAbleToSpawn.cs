using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public interface ISpawner
    {
        List<SpawnerInfo> M_Spawners { get; set; }
        Transform[] M_SpawnPosition { get; set; }

        void Initialize();
    }
}

