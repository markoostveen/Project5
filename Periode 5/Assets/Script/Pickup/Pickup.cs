using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : ObjectPool.PoolObject
{

    [SerializeField]
    private ScriptableObject m_ScriptableObj;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_ScriptableObj.GetType() == typeof(ScriptablePowerUp))
                other.SendMessage("PickUp", (ScriptablePowerUp)m_ScriptableObj, SendMessageOptions.RequireReceiver);

            Deactivate();
        }
    }
}
