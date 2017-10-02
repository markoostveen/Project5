using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//starting the menu manager and updating it
public partial class MenuManager
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        m_animator = GetComponent<Animator>();
        m_MainBehaviour = m_animator.GetBehaviour<MenuMainBehaviour>();
        m_CreditBehaviour = m_animator.GetBehaviour<MenuCreditsBehaviour>();
    }
}

//setting variables
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

    public MenuManager SetManager { set { m_Manager = value; } }

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
