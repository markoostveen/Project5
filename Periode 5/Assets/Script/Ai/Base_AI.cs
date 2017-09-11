using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_AI : PoolAbleObj {

    protected NavMeshAgent m_Agent;

	protected virtual void Start () {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.destination = CreateWanderTarget(50,transform.position);
	}

    protected virtual void Update() { }

    public Vector3 CreateWanderTarget(int Radius, Vector3 Pivot)
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

    protected void Wandering_update()
    {
        if (m_Agent.isOnNavMesh)
        {
            if (m_Agent.remainingDistance < m_Agent.stoppingDistance)
                m_Agent.destination = CreateWanderTarget(50,transform.position);
        }

    }

}
