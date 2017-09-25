using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScore
{
    public PlayerCurrentScore m_Struct;

    public PlayerScore(CharacterControl observer)
    {
        observer.M_Catched = new System.Action<IFish>(Catch);
        observer.M_AddPowerup = new System.Action<PowerUp>(AddPowerup);
        m_Struct = new PlayerCurrentScore()
        {
            CurrentPowerups = new List<PowerUp>()
        };
    }

    private void Catch(IFish fish)
    {
        Fish actualfish = (Fish)fish;
        switch (actualfish.name.Substring(1))
        {
            case "Fish":
                m_Struct.Score += 1;
                break;
        }
    }

    private void AddPowerup(PowerUp Power)
    {
        m_Struct.CurrentPowerups.Add(Power);
    }
}

public struct PlayerCurrentScore
{
    public float Score;
    public List<PowerUp> CurrentPowerups;
}
