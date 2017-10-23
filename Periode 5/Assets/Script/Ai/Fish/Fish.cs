using UnityEngine;
using UnityEngine.AI;
using Plugins.ObjectPool;

namespace Game.Character.Ai
{
    public class Fish : Base_AI , IFish{

        private enum State
        {
            Swimming,
            beingcatched,
            catched
        }

        private byte m_State;

        public string M_Name { get; private set; }

        public GameObject GetGameObject { get { return gameObject; } }

        internal override void Initialize(PoolObjectInfo Info)
        {
            transform.GetChild(0).transform.localPosition += ((Vector3.down * Random.Range(1, 10)) / 4);
            base.Initialize(Info);
            M_Name = name.Split('(')[0];
        }

        protected internal override void Activate()
        {
            base.Activate();
            m_State = (byte)State.Swimming;
            if (GameManager.Singelton.SpawnLimit > 0)
                GameManager.Singelton.SpawnLimit -= 1;
            else Deactivate();
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
            m_Agent.speed = 0;
        }

        public void Escaped()
        {
            m_State = (byte)State.Swimming;
            m_Agent.speed = m_speed;
            byte retry = 0;
            while (Vector3.Distance(m_Agent.destination, transform.position) < 3 && retry < 5)
            {
                m_Agent.SetDestination(CreateWanderTarget(10, transform.position));
                retry++;
            }
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
}


