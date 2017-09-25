using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : ICharacterStates
{
    private CharacterControl m_CharacterController;

    private float m_HorMoveSpeed;
    private float m_VerMoveSpeed;
    private float m_AttackCooldown;

    public Walking(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
        m_CharacterController = characterController;
        m_HorMoveSpeed = horMoveSpeed;
        m_VerMoveSpeed = verMoveSpeed;
        m_AttackCooldown = 0;
    }

    public void InitializeState()
    {

    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ToFishing();
        }

        Movement();
    }

    private void Movement()
    {
        Vector3 currentPosition = m_CharacterController.gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
          m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x,
            m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z + m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x,
                m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z - m_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_CharacterController.gameObject.transform.position = new Vector3(m_CharacterController.gameObject.transform.position.x - m_HorMoveSpeed * Time.deltaTime,
                m_CharacterController.gameObject.transform.position.y, m_CharacterController.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
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
        if (Input.GetKey(KeyCode.E) && m_AttackCooldown >= 2)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<CharacterControl>().DropFish();
            }
        }
    }
}
