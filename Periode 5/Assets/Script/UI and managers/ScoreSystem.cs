using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScore
{
    public PlayerCurrentScore m_Struct;
    private CharacterControl m_PlayerController;
    private List<IFish> CoughtFish;

    public PlayerScore(CharacterControl observer)
    {
        m_PlayerController = observer;

        observer.M_Catched = new System.Action<IFish>(Catch);
        observer.M_AddPowerup = new System.Action<PowerUp>(AddPowerup);

        m_Struct = new PlayerCurrentScore()
        {
            CurrentPowerups = new List<PowerUp>()
        };

        CoughtFish = new List<IFish>();
    }

    private void Catch(IFish fish)
    {
        CoughtFish.Add(fish);
        Fish actualfish = (Fish)fish;
        string Actualname = actualfish.name.Split('(')[0];
        switch (Actualname)
        {
            case "Fish-Green":
                m_Struct.Score += 1;
                break;
            case "Fish-Orange":
                m_Struct.Score += 1;
                break;
            case "Fish-Pink":
                m_Struct.Score += 1;
                break;
            case "Fish-Red":
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
