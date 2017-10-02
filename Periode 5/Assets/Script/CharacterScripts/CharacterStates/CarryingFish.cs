using System.Collections.Generic;
using UnityEngine;

public class CarryingFish : ICharacterStates
{
    private List<IFish> m_CaughtFish;

    private KeyCode[] m_KeyCodes;

    private CharacterControl m_CharacterControl;

    private float m_HorMoveSpeed;
    private float m_VerMoveSpeed;


    public CarryingFish(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
        m_KeyCodes = new KeyCode[6];
        m_CaughtFish = new List<IFish>();
        m_CharacterControl = characterController;
        m_HorMoveSpeed = horMoveSpeed;
        m_VerMoveSpeed = verMoveSpeed;
    }

    public void UpdateControls(KeyCode[] keyCodes)
    {
        m_KeyCodes = keyCodes;
    }

    public void InitializeState()
    {
        
    }

    public void GetCaughtFish(List<IFish> caughtFish)
    {
        for (int i = 0; i < caughtFish.Count; i++)
        {
            m_CaughtFish.Add(caughtFish[i]);
        }
    }

    public void UpdateState()
    {
        Debug.Log("Carrying");

        //m_HorMoveSpeed = m_HorMoveSpeed * (1f / m_CaughtFish.Count);
        //m_VerMoveSpeed = m_VerMoveSpeed * (1f / m_CaughtFish.Count);

        if (m_CaughtFish.Count <= 0)
        {
            ToWalking();
        }

        Vector3 currentPosition = m_CharacterControl.gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            m_CharacterControl.gameObject.transform.position = new Vector3(m_CharacterControl.gameObject.transform.position.x,
            m_CharacterControl.gameObject.transform.position.y, m_CharacterControl.gameObject.transform.position.z + m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_CharacterControl.gameObject.transform.position = new Vector3(m_CharacterControl.gameObject.transform.position.x,
                m_CharacterControl.gameObject.transform.position.y, m_CharacterControl.gameObject.transform.position.z - m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_CharacterControl.gameObject.transform.position = new Vector3(m_CharacterControl.gameObject.transform.position.x - m_HorMoveSpeed * Time.deltaTime,
                m_CharacterControl.gameObject.transform.position.y, m_CharacterControl.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_CharacterControl.gameObject.transform.position = new Vector3(m_CharacterControl.gameObject.transform.position.x + m_HorMoveSpeed * Time.deltaTime,
                m_CharacterControl.gameObject.transform.position.y, m_CharacterControl.gameObject.transform.position.z);
        }
    }

    public void DropFish()
    {
        m_CaughtFish.RemoveAt(0);
    }

    public void  DropFishInScorepoint()
    {
        foreach (IFish i in m_CaughtFish)
        {
            m_CharacterControl.M_Catched.Invoke(i);
            Debug.Log("Yeahhh");
        }
    }

    public void ToWalking()
    {
        m_CharacterControl.SwitchToWalkingState();
    }

    public void ToFishing()
    {
        m_CharacterControl.SwitchToFishingState();
    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void AddPowerUp(PowerUp Power)
    {
        m_CharacterControl.M_AddPowerup.Invoke(Power);
    }
}
