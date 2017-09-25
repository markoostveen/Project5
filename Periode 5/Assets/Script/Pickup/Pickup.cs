using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour {

    [SerializeField]
    ScriptableObject m_obj;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.SendMessage("PickUp");
    }
}
