using UnityEngine;

[System.Serializable]
public struct PowerupStats
{
    public float m_TimeActive;

    public float m_AddSpeed;
    public float m_AddThrowSpeed;
    public float m_AddCatchSpeed;
}

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

    public PowerUp(PowerupStats stats)
    {
        M_States = stats;
    }

    public void Update()
    {
        M_States.m_TimeActive -= Time.deltaTime;
        Debug.Log("updating a powerup");
    }
}
