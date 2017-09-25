using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : ICharacterStates
{
    private GameObject m_CurrentSelectedFish;

    private List<GameObject> m_FishInArea;
    private List<GameObject> m_CaughtFish;

    private CharacterControl m_CharacterControl;

    private int m_SelectedFishIndex;
    private int m_CatchMeter;

    public Fishing(CharacterControl characterController)
    {
        m_FishInArea = new List<GameObject>();
        m_CaughtFish = new List<GameObject>();
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
        SwitchSelectedFish();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (m_CurrentSelectedFish != null)
            {
                //In vis stil laten staan.
                m_CatchMeter += 1;
            }

            if (m_CatchMeter >= 100)
            {
                CatchFish();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            ToWalking();
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
        m_CaughtFish.Add(m_FishInArea[m_SelectedFishIndex]);
        m_FishInArea.RemoveAt(m_SelectedFishIndex);
        m_SelectedFishIndex = 0;
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

    private void GetAllFishInArea()
    {
        m_FishInArea.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(m_CurrentSelectedFish.gameObject.transform.position, 4f, 8);

        foreach (Collider col in hitColliders)
        {
            m_FishInArea.Add(col.gameObject);
        }
    }


}
