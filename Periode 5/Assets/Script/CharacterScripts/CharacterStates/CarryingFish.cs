using System.Collections.Generic;
using UnityEngine;
using Game.Character.Ai;
using Game.Character.player.Powerups;

public class CarryingFish : ICharacterStates
{
    private List<IFish> m_CaughtFish;

    private string[] m_Inputs;

    private CharacterControl m_CharacterControl;

    private float m_HorMoveSpeed;
    private float m_VerMoveSpeed;


    public CarryingFish(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        m_KeyCodes = new KeyCode[6];
        M_CaughtFish = new List<IFish>();
        M_CharacterControl = characterController;
        M_HorMoveSpeed = horMoveSpeed;
        M_VerMoveSpeed = verMoveSpeed;
=======
=======
>>>>>>> Mark
        m_Inputs = new string[6];
        m_CaughtFish = new List<IFish>();
        m_CharacterControl = characterController;
        m_HorMoveSpeed = horMoveSpeed;
        m_VerMoveSpeed = verMoveSpeed;
<<<<<<< HEAD
>>>>>>> Fabio
=======
>>>>>>> Mark
    }

    public void UpdateControls(string[] inputs)
    {
        m_Inputs = inputs;
    }

    public void InitializeState()
    {
    }

    public void GetCaughtFish(List<IFish> caughtFish)
    {
        if (caughtFish.Count == 0)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            M_CaughtFish.Add(caughtFish[i]);
=======
=======
>>>>>>> Mark
            m_CharacterControl.SwitchToWalkingState();
        }
        else
        {
            for (int i = 0; i < caughtFish.Count; i++)
            {
                m_CaughtFish.Add(caughtFish[i]);
            }
<<<<<<< HEAD
>>>>>>> Fabio
=======
>>>>>>> Mark
        }
    }

    public void UpdateState()
    {
        Debug.Log("Carry Fish State");
        //m_HorMoveSpeed = m_HorMoveSpeed * (1f / m_CaughtFish.Count);
        //m_VerMoveSpeed = m_VerMoveSpeed * (1f / m_CaughtFish.Count);

        if (m_CaughtFish.Count <= 0)
        {
            ToWalking();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        Vector3 currentPosition = M_CharacterControl.gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            M_CharacterControl.gameObject.transform.position = new Vector3(M_CharacterControl.gameObject.transform.position.x,
            M_CharacterControl.gameObject.transform.position.y, M_CharacterControl.gameObject.transform.position.z + M_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            M_CharacterControl.gameObject.transform.position = new Vector3(M_CharacterControl.gameObject.transform.position.x,
                M_CharacterControl.gameObject.transform.position.y, M_CharacterControl.gameObject.transform.position.z - M_VerMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            M_CharacterControl.gameObject.transform.position = new Vector3(M_CharacterControl.gameObject.transform.position.x - M_HorMoveSpeed * Time.deltaTime,
                M_CharacterControl.gameObject.transform.position.y, M_CharacterControl.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            M_CharacterControl.gameObject.transform.position = new Vector3(M_CharacterControl.gameObject.transform.position.x + M_HorMoveSpeed * Time.deltaTime,
                M_CharacterControl.gameObject.transform.position.y, M_CharacterControl.gameObject.transform.position.z);
        }
=======
        m_CharacterControl.gameObject.transform.position += new Vector3(Input.GetAxis(m_Inputs[4]), 0, -Input.GetAxis(m_Inputs[5])) * Time.deltaTime;
>>>>>>> Fabio
=======
        m_CharacterControl.gameObject.transform.position += new Vector3(-Input.GetAxis(m_Inputs[4]), 0, Input.GetAxis(m_Inputs[5])) * Time.deltaTime;
>>>>>>> Mark
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
