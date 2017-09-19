using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Base_AI {

    enum FishState{
        swimming,
        sleeping,
        running
    }

    private byte m_State;

    private float m_SleepTimer;

    public override void Activate()
    {
        base.Activate();
        m_Agent.destination = CreateWanderTarget(50,transform.position);
        m_State = (byte)FishState.swimming;
        m_SleepTimer = Random.Range(1, 3);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        m_SleepTimer += Time.deltaTime;
        if (m_SleepTimer > 17)
            Deactivate();
    }

    protected override void AiUpdater() {

        switch (m_State) {
            case (byte)FishState.swimming:
                Wandering_update();
                break;

            case (byte)FishState.sleeping:
                Sleeping_Update();
                break;
        }
	}

    protected virtual void Sleeping_Update()
    {
        if(m_SleepTimer > 0)
        {
            m_SleepTimer--;
        }
    }

    protected virtual void Wandering_update()
    {
        if (m_Agent.isOnNavMesh)
        {
            if (m_Agent.remainingDistance < m_Agent.stoppingDistance && !GeneratingNewTarget)
            {
                StartCoroutine(AssignNewWanderTarget(50, transform.position));
            }
        }

    }

    private bool GeneratingNewTarget;
    private IEnumerator AssignNewWanderTarget(float Radius, Vector3 Pivot)
    {
        if (!GeneratingNewTarget)
        {
            GeneratingNewTarget = true;
            if (m_Agent.isOnNavMesh)
                m_Agent.destination = CreateWanderTarget(Radius, Pivot);
            GeneratingNewTarget = false;
        }

        yield return null;
    }
}
