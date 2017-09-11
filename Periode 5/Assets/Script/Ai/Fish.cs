using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Base_AI {

    enum State{
        swimming,
        sleeping,
        running
    }

    private byte m_State;

    private float m_SleepTimer;

	protected override void Update() {

        base.Update();
        switch (m_State) {
            case (byte)State.swimming:
                Wandering_update();
                break;

            case (byte)State.sleeping:
                Sleeping_Update();
                break;
        }
	}

    private void Sleeping_Update()
    {
        if(m_SleepTimer > 0)
        {
            m_SleepTimer--;
        }
    }
}
