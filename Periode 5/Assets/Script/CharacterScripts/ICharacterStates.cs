using UnityEngine;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
    internal interface ICharacterStates
    {
        void InitializeState();

        void UpdateControls(KeyCode[] keyCodes);

        void UpdateState();

        void OnTriggerStay(Collider collider);

        void AddPowerUp(PowerUp Power);
    }
}


