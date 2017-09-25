using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingFish : ICharacterStates
{
    private List<GameObject> m_CaughtFish;

    private CharacterControl m_CharacterControle;

    private float m_HorMovementSpeed;
    private float m_VerMovementSpeed;


    public CarryingFish(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
    {
        m_CaughtFish = new List<GameObject>();
        m_CharacterControle = characterController;
        m_HorMovementSpeed = horMoveSpeed;
        m_VerMovementSpeed = verMoveSpeed;
    }

    public void InitializeState()
    {
        
    }

    public void GetCaughtFish(List<GameObject> caughtFish)
    {
        for (int i = 0; i < caughtFish.Count; i++)
        {
            m_CaughtFish.Add(caughtFish[i]);
        }
    }

    public void UpdateState()
    {
        m_HorMovementSpeed = m_HorMovementSpeed * (1f / m_CaughtFish.Count);
        m_VerMovementSpeed = m_VerMovementSpeed * (1f / m_CaughtFish.Count);

        if (m_CaughtFish.Count >= 0)
        {
            ToWalking();
        }
    }

    public void DropFish()
    {
        m_CaughtFish.RemoveAt(0);
    }

    public void  DropFishInScorepoint()
    {
        //Give list to score system;
    }

    public void ToWalking()
    {
        m_CharacterControle.SwitchToWalkingState();
    }

    public void ToFishing()
    {
        m_CharacterControle.SwitchToFishingState();
    }

    public void OnTriggerStay(Collider other)
    {

    }
}
