using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> M_Players { get; private set; }

    public static GameManager Manager;

    private void Awake()
    {
        if (Manager == null)
            Manager = this;
        else
            Destroy(gameObject);

    }

    private void Update()
    {

    }

}
