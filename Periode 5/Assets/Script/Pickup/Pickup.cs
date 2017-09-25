using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour {

    private Collider m_Collider;

    private void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.SendMessage("PickUp");
    }
}
