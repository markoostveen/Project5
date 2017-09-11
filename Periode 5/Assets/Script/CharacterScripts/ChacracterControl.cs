using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacracterControl : MonoBehaviour
{
    private Walking m_WalkingState;
    private Fishing m_FishingState;
    private CarryingFish m_CarryingFishState;
    private ICharacterStates m_CurrentState;

    [SerializeField]
    private float m_HorMoveSpeed;
    [SerializeField]
    private float m_VerMoveSpeed;

	void Start ()
    {
        SetMoveSpeed();
        m_WalkingState = new Walking(this, ref m_HorMoveSpeed, ref m_VerMoveSpeed);
        m_FishingState = new Fishing(this);
        m_CarryingFishState = new CarryingFish(this, ref m_HorMoveSpeed, ref m_VerMoveSpeed);
        m_CurrentState = m_WalkingState;
    }

    private void SetMoveSpeed()
    {
        if (m_HorMoveSpeed == 0f)
        {
            m_HorMoveSpeed = 2f;
        }

        if (m_VerMoveSpeed == 0f)
        {
            m_VerMoveSpeed = 1f;
        }
    }


    void Update ()
    {
        m_CurrentState.UpdateState();
	}
}
