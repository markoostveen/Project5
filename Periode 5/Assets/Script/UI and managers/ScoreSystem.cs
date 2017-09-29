using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScore
{
    public PlayerCurrentScore m_Struct;
    private CharacterControl m_PlayerController;

    public PlayerScore(CharacterControl observer)
    {
        m_PlayerController = observer;

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
        string Actualname = actualfish.name.Split('(')[0];
        switch (Actualname)
        {
            case "Fish":
                m_Struct.Score += 1;
                break;
        }
    }

    private void AddPowerup(PowerUp Power)
    {
        Power.m_RemovePoolCallback += RemovePowerup;
        m_Struct.CurrentPowerups.Add(Power);
    }

    public void RemovePowerup(PowerUp power)
    {
        m_Struct.CurrentPowerups.Remove(power);

        Debug.Log("Powerup Removed");
    }
}

public struct PlayerCurrentScore
{
    public float Score;
    public List<PowerUp> CurrentPowerups;
}
