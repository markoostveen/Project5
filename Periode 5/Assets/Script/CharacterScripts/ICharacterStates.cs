using UnityEngine;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
    internal interface ICharacterStates
    {
        void InitializeState();

<<<<<<< HEAD
        void UpdateControls(KeyCode[] keyCodes);
=======
    void UpdateControls(string[] keyCodes);
>>>>>>> Fabio

        void UpdateState();

        void OnTriggerStay(Collider collider);

        void AddPowerUp(PowerUp Power);
    }
}


