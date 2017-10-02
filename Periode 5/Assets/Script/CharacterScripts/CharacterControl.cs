using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ObjectPool;

public class CharacterControl : PoolObject
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

    public Action<IFish> M_Catched { get; set; }
    public Action<PowerUp> M_AddPowerup { get; set; }

    private byte m_PlayerID;
    public byte SetPlayerID { set { m_PlayerID = value; } }

    public void ModifyControls(KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey, KeyCode toFishingKey, KeyCode attackKey)
    {
        m_KeyCodes = new KeyCode[6];
        m_KeyCodes[0] = upKey;
        m_KeyCodes[1] = downKey;
        m_KeyCodes[2] = leftKey;
        m_KeyCodes[3] = rightKey;
        m_KeyCodes[4] = toFishingKey;
        m_KeyCodes[5] = attackKey;

        m_WalkingState.UpdateControls(m_KeyCodes);
        m_FishingState.UpdateControls(m_KeyCodes);
        m_CarryingFishState.UpdateControls(m_KeyCodes);

    }

    internal override void Initialize(PoolObjectInfo Info)
    {
        SetMoveSpeed();
        m_WalkingState = new Walking(this, ref m_HorMoveSpeed, ref m_VerMoveSpeed);
        m_FishingState = new Fishing(this);
        m_CarryingFishState = new CarryingFish(this, ref m_HorMoveSpeed, ref m_VerMoveSpeed);
        m_CurrentState = m_WalkingState;
        GameManager.Singelton.RegisterPlayer(this);
        base.Initialize(Info);
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
        if (m_CurrentState == m_CarryingFishState)
        {
            m_CarryingFishState.DropFish();
        }
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
        m_FishingState.SetCurrentSelecetedFish();
    }

    public void SwitchToCarryingState(List<IFish> caughtFish)
    {
        m_CurrentState = m_CarryingFishState;
        m_CarryingFishState.InitializeState();
        m_CarryingFishState.GetCaughtFish(caughtFish);
    }

    public void OnTriggerStay(Collider other)
    {
        m_CurrentState.OnTriggerStay(other);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_CurrentState == m_FishingState)
        {
            m_FishingState.OnTriggerEnter(other);
        }

        if (other.CompareTag("Scorepoint"))
        {
            if(other.gameObject.GetComponent<ScorePoint>().m_PlayerID == m_PlayerID)
            {
                if (m_CurrentState == m_CarryingFishState)
                {
                    m_CarryingFishState.DropFishInScorepoint();
                }
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (m_CurrentState == m_FishingState)
        {
            m_FishingState.OnTriggerExit(other);
        }
    }

    private void PickUp(ScriptablePowerUp power)
    {
        PowerUp powerup = new PowerUp(power.stats, new RemovePowerupEffectDelegate(AddRemovePowerup), power.m_Image);

        M_AddPowerup.Invoke(powerup);
    }

    private void AddRemovePowerup(PowerupStats stats)
    {
        m_HorMoveSpeed *= stats.m_AddSpeed;
        m_VerMoveSpeed *= stats.m_AddSpeed;
    }
}
