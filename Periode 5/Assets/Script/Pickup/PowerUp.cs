using UnityEngine;

[CreateAssetMenu(fileName = "NewHat", menuName = "Hat", order = 1)]
public class PowerUp : ScriptableObject
{
    //used for update
    public float m_TimeActive;

    public float m_AddSpeed;
    public float m_AddThrowSpeed;
    public float m_AddCatchSpeed;

    public float M_TimeActiveRef { get; private set; }

    public PowerUp()
    {
        M_TimeActiveRef = m_TimeActive;
    }

    public void Update()
    {
        M_TimeActiveRef -= Time.deltaTime;
        Debug.Log("updating a powerup");
    }
 // make this a scriptable object for easy editing
}
