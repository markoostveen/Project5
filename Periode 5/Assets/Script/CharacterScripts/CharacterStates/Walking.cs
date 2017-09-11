using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : ICharacterStates
{
    private ChacracterControl m_CharacterController;

    private float m_HorMoveSpeed;
    private float m_VerMoveSpeed;

    public Walking(ChacracterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
        m_CharacterController = characterController;
        m_HorMoveSpeed = horMoveSpeed;
        m_VerMoveSpeed = verMoveSpeed;
    }

    public void InitializeState()
    {

    }

    public void UpdateState()
    {
        Debug.Log("Walking");
        Vector3 currentPosition = m_CharacterController.transform.position;

        if (Input.GetKey(KeyCode.W)) { m_CharacterController.transform.position = new Vector3(m_CharacterController.transform.position.x, m_CharacterController.transform.position.y + m_VerMoveSpeed, 0); }
        if (Input.GetKey(KeyCode.S)) { m_CharacterController.transform.position = new Vector3(m_CharacterController.transform.position.x, m_CharacterController.transform.position.y - m_VerMoveSpeed, 0); }
        if (Input.GetKey(KeyCode.A)) { m_CharacterController.transform.position = new Vector3(m_CharacterController.transform.position.x - m_HorMoveSpeed, m_CharacterController.transform.position.y, 0); }
        if (Input.GetKey(KeyCode.D)) { m_CharacterController.transform.position = new Vector3(m_CharacterController.transform.position.x + m_HorMoveSpeed, m_CharacterController.transform.position.y, 0); }
    }

    public void ToWalking()
    {

    }

    public void ToFishing()
    {

    }

    public void ToCarrying()
    {

    }
}
