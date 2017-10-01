using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ObjectPool;

public class Fish : Base_AI , IFish{

    private enum State
    {
        Swimming,
        beingcatched,
        catched
    }

    private byte m_State;

    public string M_Name { get; set; }

    public override void Initialize(PoolObjectInfo Info)
    {
        transform.GetChild(0).transform.localPosition += ((Vector3.down * Random.Range(1, 10)) / 4);
        base.Initialize(Info);
        M_Name = name.Split('(')[0];
    }

    public override void Activate()
    {
        base.Activate();
        m_State = (byte)State.Swimming;
    }

    protected override void AiUpdater()
    {
        if(m_State == (byte)State.Swimming)
            base.AiUpdater();

    }

    public void Atteract(Vector3 destination)
    {
        m_Agent.destination = destination;
    }

    public void Catched()
    {
        m_State = (byte)State.catched;
        Deactivate();
    }
    
    public void BeingCatched()
    {
        m_State = (byte)State.beingcatched;
    }

    public void Escaped()
    {
        m_State = (byte)State.Swimming;
    }

    protected override Vector3 CreateWanderTarget(float Radius, Vector3 Pivot)
    {
        Vector3 newtarget = new Vector3(999, 999, 999);
        NavMeshPath newpath = new NavMeshPath();

        int Retrys = 0;
        while (!m_Agent.CalculatePath(newtarget, newpath) && Retrys < 10) 
        {
            newtarget = Pivot + new Vector3(UnityEngine.Random.insideUnitSphere.x * Radius, transform.position.y, UnityEngine.Random.insideUnitSphere.z * Radius);
            Retrys++;
        }

        return newtarget;
    }
}
