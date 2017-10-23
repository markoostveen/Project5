﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Plugins.ObjectPool;
using Game.UI;
using Game.Character.Pickup;

namespace Game
{
    public class GameManager : MonoBehaviour {

        [SerializeField]
        private Transform m_UI;

        [SerializeField]
        [Tooltip("prefab of the UI fields to show playerinfo")]
        private GameObject m_PlayerstatsPrefab;

        [SerializeField]
        [Tooltip("player prefab to spawn into the game")]
        private GameObject m_playerPrefab;

        [SerializeField][Tooltip("Points where people can empty there fishing nets")]
        private GameObject[] m_ScorePoints;

        [SerializeField]
        [Tooltip("Boards where cought fish will display")]
        private Text[] m_ScoreBoards;

        internal List<GameObject> M_Players { get; private set; }
        private List<PlayerScore> m_Scores;

        //used to refrence how many fish can spawn
        [SerializeField]
        internal int SpawnLimit;
    
        //a refrence of the game manager to call from other classes
        public static GameManager Singelton { get; private set; }

        //function will make singleton, and will make new lists
        protected void Awake()
        {
            if (Singelton == null)
                Singelton = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            M_Players = new List<GameObject>();
            m_Scores = new List<PlayerScore>();

<<<<<<< HEAD
            //foreach (Text i in m_ScoreBoards)
            //    i.gameObject.transform.parent.gameObject.SetActive(false);
=======
            for (int i = 0; i < m_ScorePoints.Length; i++)
            {
                m_ScorePoints[i].AddComponent<ScorePoint>();
            }
>>>>>>> parent of 38f45de... holiday changes
        }

        protected void Start()
        {
            for (int i = 0; i < m_ScorePoints.Length; i++)
            {
                Pool.Singleton.Spawn(m_playerPrefab, m_ScorePoints[i].transform.position);
            }
        }

        //function to call when creating a new player
        public void RegisterPlayer(CharacterControl Player)
        {

            //making a new score system for the player
            m_Scores.Add(new PlayerScore(Player));
            PlayerScore score = m_Scores[m_Scores.Count -1];
<<<<<<< HEAD
            m_ScorePoints[m_Scores.Count - 1].AddComponent<ScorePoint>();
            //m_ScoreBoards[m_Scores.Count - 1].gameObject.transform.parent.gameObject.SetActive(true);
=======
>>>>>>> parent of 38f45de... holiday changes
            M_Players.Add(Player.gameObject);

            //spawning the UI element
            PoolObject playerstatspool = Pool.Singleton.Spawn(m_PlayerstatsPrefab, m_UI);


            GameObject playerstats = playerstatspool.gameObject;
            RectTransform playerstatstransform = playerstats.GetComponent<RectTransform>();

            switch(M_Players.Count){
                case 1:
                    playerstatstransform.localPosition = new Vector3(6, -2, 0);
                    Player.ModifyControls("Controller1AButton", "Controller1XButton", "Controller1LeftBumper", "Controller1RightBumper", "Controller1JoystickHorizontal", "Controller1JoystickVertical");
                    m_ScorePoints[0].GetComponent<ScorePoint>().m_PlayerID = 1;
                    break;

                case 2:
                    playerstatstransform.localPosition = new Vector3(-6, -2, 0);
                    Player.ModifyControls("Controller2AButton", "Controller2XButton", "Controller2LeftBumper", "Controller2RightBumper", "Controller2JoystickHorizontal", "Controller2JoystickVertical");
                    m_ScorePoints[1].GetComponent<ScorePoint>().m_PlayerID = 2;
                    break;

                case 3:
                    playerstatstransform.localPosition = new Vector3(6, 2, 0);
                    Player.ModifyControls("Controller3AButton", "Controller3XButton", "Controller3LeftBumper", "Controller3RightBumper", "Controller3JoystickHorizontal", "Controller3JoystickVertical");
                    m_ScorePoints[2].GetComponent<ScorePoint>().m_PlayerID = 3;
                    break;

                case 4:
                    playerstatstransform.localPosition = new Vector3(-6, 2, 0);
                    Player.ModifyControls("Controller4AButton", "Controller4XButton", "Controller4LeftBumper", "Controller4RightBumper", "Controller4JoystickHorizontal", "Controller4JoystickVertical");
                    m_ScorePoints[3].GetComponent<ScorePoint>().m_PlayerID = 4;
                    break;
            }
            //call the registerfunction in the UI, this will setup all fields inside of them
            PlayerStats stats = playerstats.GetComponent<PlayerStats>();
            stats.UpdateID((byte)M_Players.Count, score, 200, m_ScoreBoards[M_Players.Count - 1]);

            //update playerID in the character controller
            Player.SetPlayerID = (byte)M_Players.Count;
        }

        protected void Update()
        {
            if(Input.GetKeyDown(KeyCode.RightAlt))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

