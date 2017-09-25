using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScore
{
    public byte m_Id;
    public float m_Score;

    public PlayerScore(CharacterControl observer)
    {
        observer.M_Catched += Catch;
    }

    public void Catch(IFish fish)
    {

    }

    public void AddPowerup(PowerUp Power)
    {

    }
}
