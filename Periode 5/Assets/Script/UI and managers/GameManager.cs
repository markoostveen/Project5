using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_PlayerstatsPrefab;

    public List<GameObject> M_Players { get; private set; }
    private List<PlayerScore> m_Scores;
    public static GameManager Manager;

    private void Awake()
    {
        if (Manager == null)
            Manager = this;
        else
            Destroy(gameObject);

        M_Players = new List<GameObject>();
        m_Scores = new List<PlayerScore>();
    }

    private void Update()
    {

    }

    public void RegisterPlayer(CharacterControl Player)
    {

        m_Scores.Add(new PlayerScore(Player));
        PlayerScore score = m_Scores[m_Scores.Count -1];
        M_Players.Add(Player.gameObject);

        Transform UI = GameObject.Find("UI").transform;
        GameObject playerstats = ObjectPool.Pool.Spawn(m_PlayerstatsPrefab, UI).gameObject;
        RectTransform playerstatstransform = playerstats.GetComponent<RectTransform>();

        switch(M_Players.Count){
            case 1:
                playerstatstransform.localPosition = new Vector3(-810, 490, 0);
                break;

            case 2:
                playerstatstransform.localPosition = new Vector3(810, 490, 0);
                break;

            case 3:
                playerstatstransform.localPosition = new Vector3(-810, -490, 0);
                break;

            case 4:
                playerstatstransform.localPosition = new Vector3(810, -490, 0);
                break;
        }

        PlayerStats stats = playerstats.GetComponent<PlayerStats>();
        stats.UpdateID((byte)M_Players.Count);
    }

}
