using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ISpawner
{
    List<SpawnerInfo> M_Spawners { get; set; }

    void Initialize();
}
