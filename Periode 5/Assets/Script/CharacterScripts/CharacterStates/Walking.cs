using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : ICharacterStates
{
    private CharacterControl m_CharacterController;

    private KeyCode[] m_KeyCodes;

    private float m_HorMoveSpeed;
    private float m_VerMoveSpeed;
    private float m_AttackCooldown;

    public Walking(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed, KeyCode[] keyCodes)
    {
        m_KeyCodes = new KeyCode[6];
        m_CharacterController = characterController;
        m_HorMoveSpeed = horMoveSpeed;
        m_VerMoveSpeed = verMoveSpeed;
        m_AttackCooldown = 0;
        m_KeyCodes = keyCodes;
    }

    public void InitializeState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Walking");
        if (Input.GetKeyDown(m_KeyCodes[4]))
        {
            ToFishing();
        }

        Movement();
    }

    private void Movement()
    {
        Vector3 currentPosition = m_CharacterController.gameObject.transform.position;

        if (Input.GetKey(m_KeyCodes[0]))
        {
          m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x,
            m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z + m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(m_KeyCodes[1]))
        {
            m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x,
                m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z - m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(m_KeyCodes[2]))
        {
            m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x - m_HorMoveSpeed * Time.deltaTime,
                m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z);
        }
        if (Input.GetKey(m_KeyCodes[3]))
        {
            m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x + m_HorMoveSpeed * Time.deltaTime,
                m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z);
        }
    }

    public void ToFishing()
    {
        m_CharacterController.SwitchToFishingState();
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(m_KeyCodes[5]) && m_AttackCooldown >= 2)
        {
            if (other.CompareTag("Player"))
            {
                /// fabio moet slaan en stoppen met vissen vangen
                other.gameObject.GetComponent<CharacterControl>().DropFish();
            }
        }
    }

    public void AddPowerUp(PowerUp Power)
    {
        m_CharacterController.M_AddPowerup.Invoke(Power);
    }
}
