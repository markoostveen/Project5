using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : PoolObject {

    [SerializeField]
    ScriptablePowerUp m_obj;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("PickUp", m_obj.stats, SendMessageOptions.RequireReceiver);
            Deactivate();
        }
    }
}
