using UnityEngine;
using System.Collections.Generic;
using System;

public interface ICharacterStates
{
    void InitializeState();

    void UpdateControls(KeyCode[] keyCodes);

    void UpdateState();

    void OnTriggerStay(Collider collider);

    void AddPowerUp(PowerUp Power);
}
