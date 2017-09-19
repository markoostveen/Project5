using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacracterControl : MonoBehaviour
{
    private Walking m_WalkingState;
    private Fishing m_FishingState;
    private CarryingFish m_CarryingFishState;
    private ICharacterStates m_CurrentState;

    private KeyCode[] m_KeyCodes;

    [SerializeField]
    private float m_HorMoveSpeed;
    [SerializeField]
    private float m_VerMoveSpeed;

    public ChacracterControl(KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey, KeyCode toFishingKey)
    {
        m_KeyCodes = new KeyCode[4];

        m_KeyCodes[0] = upKey;
        m_KeyCodes[1] = downKey;
        m_KeyCodes[2] = leftKey;
        m_KeyCodes[3] = rightKey;
        m_KeyCodes[4] = toFishingKey;
    }

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

    public void DropFish()
    {
        
    }

    public void SwitchToWalkingState()
    {
        m_CurrentState = m_WalkingState;
        m_WalkingState.InitializeState();
    }

    public void SwitchToFishingState()
    {
        m_CurrentState = m_FishingState;
        m_FishingState.InitializeState();
    }

    public void SwitchToCarryingState()
    {
        m_CurrentState = m_CarryingFishState;
        m_CarryingFishState.InitializeState();
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}
