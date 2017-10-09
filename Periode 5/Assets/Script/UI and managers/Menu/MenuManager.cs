using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Plugins.ObjectPool;

namespace Game.UI
{
    //starting the menu manager and updating it
    public partial class MenuManager
    {
        [SerializeField]
        private Text m_TextPlayerCount;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            m_animator = GetComponent<Animator>();
            m_MainBehaviour = m_animator.GetBehaviour<MenuMainBehaviour>();
            m_CreditBehaviour = m_animator.GetBehaviour<MenuCreditsBehaviour>();
        }
    }

    //setting Settings
    public partial class MenuManager : MonoBehaviour {

        private int m_FishSpawningLimit;

        private byte m_PlayerCount;

        public void SetSpawningLimit(int Limit)
        {
            m_FishSpawningLimit = Limit;
        }

        public void SetPlayerCount(int Count)
        {
            m_PlayerCount = (byte)Count;
        }

    }

    public partial class MenuManager
    {
        public void StartGame()
        {
            SceneManager.sceneLoaded += OnGameLoad;
            SceneManager.LoadScene(SceneManager.GetSceneByName("Main").buildIndex,LoadSceneMode.Additive);
        }

        private void OnGameLoad(Scene Level, LoadSceneMode setting)
        {
            GameObject[] rootobjects = Level.GetRootGameObjects();

            Pool mypool;
            GameManager mymanager = null;
            GameObject[] SpawnPoints = null;

            foreach (GameObject i in rootobjects)
            {
                if(i.GetComponent<GameManager>() != null)
                {
                    mymanager = i.GetComponent<GameManager>();
                    SpawnPoints = mymanager.GetScorePoints;
                }


                if (i.GetComponent<Pool>() != null)
                    mypool = i.GetComponent<Pool>();
            }

            for (int i = 0; i < m_PlayerCount; i++)
            {
                Pool.Singleton.Spawn(mymanager.GetPlayerPrefab, SpawnPoints[i].transform.position);
            }

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Menu"));
        }
    }

    //animating the menu
    public partial class MenuManager
    {
        private Animator m_animator;

        private byte m_CurrentMenuScreen;

        private MenuMainBehaviour m_MainBehaviour;
        private MenuCreditsBehaviour m_CreditBehaviour;
    }

    //parrent class for animator behaviours for menu
    public class MenuStatesAnimatorParrent : StateMachineBehaviour
    {
        protected MenuManager m_Manager;

        public void SetManager(MenuManager manager) { m_Manager = manager; }

        [SerializeField][Tooltip("Object that will be lerped")]
        private Transform LerpObj;

        [SerializeField][Tooltip("Postion to lerp to once in this state")]
        private Transform m_EndPosition;
        private float m_Timer;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_Timer = 0;
        }
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            LerpObj.position = Vector3.LerpUnclamped(LerpObj.position, m_EndPosition.position, m_Timer);

            if (m_Timer < 1)
                m_Timer += Time.deltaTime;
        }
    }
}

