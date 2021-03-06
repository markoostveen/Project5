﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObjectPool;

public class PlayerStats : PoolObject {

    [SerializeField]
    private Text m_PlayerID;

    [SerializeField]
    private Image m_PlayerImage;

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

    //this function is called when a new player will join the game
    public void UpdateID(byte input, PlayerScore ScoreSystem, int ScoreGoal, Sprite ProfileImage)
    {
        m_PlayerID.text = "Player: " + input;
        m_ScoreSystem = ScoreSystem;
        m_ScoreGoal = ScoreGoal;
        m_ScoreGoalText.text = "Fish Goal " + ScoreGoal;
        m_PlayerImage.sprite = ProfileImage;

    }

    private void Update()
    {
        if (m_ScoreSystem != null)
        {
            //used to update the active powerups and draw images of them on UI
            for (int i = 0; i < m_ScoreSystem.GetScore().CurrentPowerups.Count; i++)
            {
                m_PowerupImages[i].enabled = false;
                PowerUp powerup = m_ScoreSystem.GetScore().CurrentPowerups[i];
                powerup.Update();

                if (i < m_PowerupImages.Length)
                {
                    m_PowerupImages[i].enabled = true;
                    m_PowerupImages[i].sprite = powerup.GetSprite();
                }
            }

            //updating the textfields
            m_CurrentscorefieldText.text = m_ScoreSystem.GetScore().Score + " / " + m_ScoreGoal;
            m_CurrentScoreSlider.value = m_ScoreSystem.GetScore().Score / m_ScoreGoal;
        }
    }

    //called when this objects in being placed back into a pool
    protected override void Deactivate()
    {
        base.Deactivate();
        m_PlayerID.text = "PlayerID";
        m_ScoreGoalText.text = "Fish Goal ";
        m_ScoreGoal = 0;
        m_ScoreSystem = null;
        m_PlayerImage.sprite = null;
    }

}
