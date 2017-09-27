using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fishing : ICharacterStates
{
    private IFish m_CurrentSelectedFish;

    private List<IFish> m_FishInArea;
    private List<IFish> m_CaughtFish;

    private CharacterControl m_CharacterControl;

    private int m_SelectedFishIndex;
    private int m_CatchMeter;

    public Action<IFish> m_Catched;

    public Fishing(CharacterControl characterController)
    {
        m_CharacterControl = characterController;
        m_FishInArea = new List<IFish>();
        m_CaughtFish = new List<IFish>();
    }

    public void InitializeState()
    {
        m_CurrentSelectedFish = null;
        m_FishInArea.Clear();
        m_CaughtFish.Clear();
        GetAllFishInArea();
        m_SelectedFishIndex = 0;
        m_CatchMeter = 0;
    }

    public void UpdateState()
    {
        Debug.Log("Fishing");

        SwitchSelectedFish();

        if (m_CurrentSelectedFish != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Catching Fish");

                m_CurrentSelectedFish.BeingCatched();
                m_CatchMeter += 100;


                if (m_CatchMeter >= 100)
                {
                    Debug.Log("Cought it");
                    CatchFish();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_CaughtFish.Count >= 1)
            {
                ToCarrying();
            }
            else
            {
                ToWalking();
            }      
        }
    }

    private void SwitchSelectedFish()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (m_SelectedFishIndex == 0)
            {
                m_SelectedFishIndex = m_FishInArea.Count;
            }
            else if (m_SelectedFishIndex >= 1)
            {
                m_SelectedFishIndex -= 1;
            }

            m_CatchMeter = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (m_SelectedFishIndex == m_FishInArea.Count)
            {
                m_SelectedFishIndex = 0;
            }
            else if (m_SelectedFishIndex <= 1)
            {
                m_SelectedFishIndex += 1;
            }

            m_CatchMeter = 0;
        }
    }

    private void CatchFish()
    {
        if(m_FishInArea.Count > 0)
        {
            m_CaughtFish.Add(m_FishInArea[m_SelectedFishIndex]);
            m_CaughtFish[m_CaughtFish.Count - 1].BeingCatched();

            m_FishInArea.RemoveAt(m_SelectedFishIndex);
            m_SelectedFishIndex = 0;


            m_CaughtFish[m_CaughtFish.Count - 1].Catched();
        }
    }

    public void ToWalking()
    {
        m_CharacterControl.SwitchToWalkingState();
    }

    public void ToCarrying()
    {
        m_CharacterControl.SwitchToCarryingState(m_CaughtFish);
    }

    public void OnTriggerStay(Collider collider)
    {

    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            GetAllFishInArea();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            GetAllFishInArea();
        }
    }

    public void SetCurrentSelecetedFish()
    {
        m_CurrentSelectedFish = m_FishInArea[0];
    }

    private void GetAllFishInArea()
    {
        m_FishInArea.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(m_CharacterControl.gameObject.transform.position, 5000f);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.layer == 8)
            {
                m_FishInArea.Add(col.gameObject.GetComponent<IFish>());
            }    
        }
    }

    public void AddPowerUp(PowerUp Power)
    {
        m_CharacterControl.M_AddPowerup.Invoke(Power);
    }

}
