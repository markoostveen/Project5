using System.Collections.Generic;
using UnityEngine;
using Game.Character.Ai;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
    public class CarryingFish : ICharacterStates
{
    private List<IFish> M_CaughtFish { get; }

    private KeyCode[] m_KeyCodes;

    private CharacterControl M_CharacterControl { get; }

    private float M_HorMoveSpeed { get; }
    private float M_VerMoveSpeed { get; }


    public CarryingFish(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
        m_KeyCodes = new KeyCode[6];
        M_CaughtFish = new List<IFish>();
        M_CharacterControl = characterController;
        M_HorMoveSpeed = horMoveSpeed;
        M_VerMoveSpeed = verMoveSpeed;
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
            M_CaughtFish.Add(caughtFish[i]);
        }
    }

    public void UpdateState()
    {
        Debug.Log("Carrying");

        //m_HorMoveSpeed = m_HorMoveSpeed * (1f / m_CaughtFish.Count);
        //m_VerMoveSpeed = m_VerMoveSpeed * (1f / m_CaughtFish.Count);

        if (M_CaughtFish.Count <= 0)
        {
            ToWalking();
        }

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
    }

    public void DropFish()
    {
        M_CaughtFish.RemoveAt(0);
    }

    public void  DropFishInScorepoint()
    {
        foreach (IFish i in M_CaughtFish)
        {
            M_CharacterControl.M_Catched.Invoke(i);
            Debug.Log("Yeahhh");
        }
    }

    public void ToWalking()
    {
        M_CharacterControl.SwitchToWalkingState();
    }

    public void ToFishing()
    {
        M_CharacterControl.SwitchToFishingState();
    }

    public void OnTriggerStay(Collider collider)
    {

    }

    public void AddPowerUp(PowerUp Power)
    {
        M_CharacterControl.M_AddPowerup.Invoke(Power);
    }
}
}


