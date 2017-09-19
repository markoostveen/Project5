using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Base_AI : PoolObject {

    protected NavMeshAgent m_Agent;

    private float UpdateTimer;

    protected virtual void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        UpdateTimer = Random.Range(0, 100) / 100;
    }

    protected virtual void FixedUpdate()
    {
        UpdateTimer += Time.deltaTime;
        if (UpdateTimer >= 60)
            AiUpdater();
    }

    protected virtual void AiUpdater() { }

    protected Vector3 CreateWanderTarget(float Radius, Vector3 Pivot)
    {
        Vector3 newtarget = new Vector3(999,999,999);
        NavMeshPath newpath = new NavMeshPath();

        int Retrys = 0;

        while (!m_Agent.CalculatePath(newtarget, newpath) && Retrys < 50)
        {
            newtarget = Pivot + new Vector3(Random.insideUnitSphere.x * Radius, transform.position.y , Random.insideUnitSphere.z * Radius);
            Retrys++;
        }

        return newtarget;
    }

}
