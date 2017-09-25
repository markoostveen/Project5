using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : PoolObject {

    [SerializeField]
    private Text PlayerID;

    public void UpdateID(byte input)
    {
        PlayerID.text = "Player: " + input;
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        PlayerID.text = "PlayerID";
    }


}
