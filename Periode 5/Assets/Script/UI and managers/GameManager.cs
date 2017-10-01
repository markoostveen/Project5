using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_PlayerstatsPrefab;
    [SerializeField]
    private GameObject m_playerPrefab;

    public List<GameObject> M_Players { get; private set; }
    private List<PlayerScore> m_Scores;
    public static GameManager Singelton;

    private void Awake()
    {
        if (Singelton == null)
            Singelton = this;
        else
            Destroy(gameObject);

        M_Players = new List<GameObject>();
        m_Scores = new List<PlayerScore>();
    }

    private void Start()
    {
        //Pool.Singleton.Spawn(m_playerPrefab, )
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
        PoolObject playerstatspool = Pool.Singleton.Spawn(m_PlayerstatsPrefab, UI);
        if (playerstatspool == null)
            return;
        GameObject playerstats = playerstatspool.gameObject;
        RectTransform playerstatstransform = playerstats.GetComponent<RectTransform>();

        switch(M_Players.Count){
            case 1:
                playerstatstransform.localPosition = new Vector3(-675, 390, 0);
                Player.ModifyControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E);
                break;

            case 2:
                playerstatstransform.localPosition = new Vector3(675, 390, 0);
                Player.ModifyControls(KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L, KeyCode.U, KeyCode.O);
                break;

            case 3:
                playerstatstransform.localPosition = new Vector3(-675, -390, 0);
                break;

            case 4:
                playerstatstransform.localPosition = new Vector3(675, -390, 0);
                break;
        }

        PlayerStats stats = playerstats.GetComponent<PlayerStats>();
        stats.UpdateID((byte)M_Players.Count, score, 200, new Sprite());
    }

}
