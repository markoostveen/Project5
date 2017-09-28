using UnityEngine;

[System.Serializable]
public struct PowerupStats
{
    public float m_TimeActive;

    public float m_AddSpeed;
    public float m_AddThrowSpeed;
    public float m_AddCatchSpeed;
}
public delegate void RemovePowerupDelegate(PowerupStats stats);

[CreateAssetMenu(fileName = "NewHat", menuName = "Hat", order = 1)]
public class ScriptablePowerUp : ScriptableObject
{
    //info of object is stored here
    [SerializeField]
    public PowerupStats stats;

    // make this a scriptable object for easy editing
}

public class PowerUp : ScriptableObject
{
    //used for update
    private PowerupStats M_States;

    public PowerupStats GetStats() { return M_States; }

    private RemovePowerupDelegate m_RemoveCallBack;

    public PowerUp(PowerupStats stats, RemovePowerupDelegate callback)
    {
        M_States = stats;
        m_RemoveCallBack = callback;
    }

    public void Update()
    {
        M_States.m_TimeActive -= Time.deltaTime;

        if (M_States.m_TimeActive <= 0)
            m_RemoveCallBack.Invoke(M_States);

        Debug.Log("updating a powerup");
    }
}
