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

    [SerializeField]
    private Image[] m_PowerupImages = new Image[8];

    public override void Initialize(PoolObjectInfo Info)
    {
        for (int i = 0; i < m_PowerupImages.Length; i++)
            m_PowerupImages[i].enabled = false;

        base.Initialize(Info);
    }

    public void UpdateID(byte input, PlayerScore ScoreSystem, int ScoreGoal)
    {
        m_PlayerID.text = "Player: " + input;
        m_ScoreSystem = ScoreSystem;
        m_ScoreGoalText.text = "Fish Goal " + ScoreGoal;
        m_ScoreGoal = ScoreGoal;
    }

    private void Update()
    {
        if (m_ScoreSystem != null)
        {
            for (int i = 0; i < m_ScoreSystem.m_Struct.CurrentPowerups.Count; i++)
            {
                m_PowerupImages[i].enabled = false;
                PowerUp powerup = m_ScoreSystem.m_Struct.CurrentPowerups[i];
                powerup.Update();

                if (powerup.GetStats().m_TimeActive < 0)
                {
                    m_ScoreSystem.m_Struct.CurrentPowerups.Remove(m_ScoreSystem.m_Struct.CurrentPowerups[i]);
                }
                else if (i < m_PowerupImages.Length)
                {
                    m_PowerupImages[i].enabled = true;
                    m_PowerupImages[i].sprite = powerup.GetSprite();
                }
            }


            m_CurrentscorefieldText.text = m_ScoreSystem.m_Struct.Score + " / " + m_ScoreGoal;
            m_CurrentScoreSlider.value = m_ScoreSystem.m_Struct.Score / m_ScoreGoal;
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        m_PlayerID.text = "PlayerID";
        m_ScoreSystem = null;
    }

}
