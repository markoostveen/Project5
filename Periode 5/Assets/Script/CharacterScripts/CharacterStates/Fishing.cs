using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : ICharacterStates
{
    private GameObject m_CurrentSelectedFish;

    private List<GameObject> m_FishInArea;

    public Fishing(ChacracterControl characterController)
    {
        m_FishInArea = new List<GameObject>();
    }

    public void InitializeState()
    {
        m_FishInArea.Clear();
        m_CurrentSelectedFish = null;
    }

    public void UpdateState()
    {

    }

    public void ToWalking()
    {
        
    }

    public void ToCarrying()
    {

    }
    public void OnTriggerStay(Collider other)
    {

    }

}
