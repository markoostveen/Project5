using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    public delegate void PoolAbleObjCallback(string Name, PoolAbleObj Thisobj);

    public abstract class PoolAbleObj : MonoBehaviour
    {
        private PoolAbleObjCallback m_Callback;
        private string m_Name;

        public void Initialize(string Myname, PoolAbleObjCallback Callback)
        {
            m_Name = Myname;
            m_Callback = Callback;
            gameObject.SetActive(false);
        }

        public virtual void Activate()
        {
            transform.parent = null;
        }

        public virtual void Deactivate()
        {
            m_Callback.Invoke(m_Name, this);
            gameObject.SetActive(false);
        }
    }
}

