using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : PoolObject {

    [SerializeField]
    private Text m_PlayerID;

    [SerializeField]
    private Text m_ScoreGoalText;

    //score slider
    [SerializeField]
    private Text m_CurrentscorefieldText;
    [SerializeField]
    private Slider m_CurrentScoreSlider;


    private PlayerScore m_ScoreSystem;
    private int m_ScoreGoal;

    public void UpdateID(byte input, PlayerScore ScoreSystem, int ScoreGoal)
    {
        m_PlayerID.text = "Player: " + input;
        m_ScoreSystem = ScoreSystem;
        m_ScoreGoalText.text = "Fish Goal " + ScoreGoal;
        m_ScoreGoal = ScoreGoal;
    }

    private void Update()
    {
        foreach (PowerUp i in m_ScoreSystem.m_Struct.CurrentPowerups)
        {
            i.Update();
            if (i.M_TimeActiveRef < 0)
                m_ScoreSystem.m_Struct.CurrentPowerups.Remove(i);
        }


        m_CurrentscorefieldText.text = m_ScoreSystem.m_Struct.Score + " / " + m_ScoreGoal;
        m_CurrentScoreSlider.value = m_ScoreSystem.m_Struct.Score / m_ScoreGoal;
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        m_PlayerID.text = "PlayerID";
        m_ScoreSystem = null;
    }


}
