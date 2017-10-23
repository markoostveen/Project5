using UnityEngine;
using Game.Character.player.Powerups;

public interface ICharacterStates
{
    void InitializeState();

<<<<<<< HEAD
<<<<<<< HEAD
        void UpdateControls(KeyCode[] keyCodes);
=======
    void UpdateControls(string[] keyCodes);
>>>>>>> Fabio
=======
    void UpdateControls(string[] keyCodes);
>>>>>>> Mark

    void UpdateState();

    void OnTriggerStay(Collider collider);

    void AddPowerUp(PowerUp Power);
}
