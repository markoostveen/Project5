using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
    public class Walking : ICharacterStates
    {
        private CharacterControl M_CharacterController { get; }

        private KeyCode[] m_KeyCodes;

        private float M_HorMoveSpeed { get; }
        private float M_VerMoveSpeed { get; }
        private float M_AttackCooldown { get; }

        public Walking(CharacterControl characterController, ref float horMoveSpeed, ref float verMoveSpeed)
        {
            m_KeyCodes = new KeyCode[6];
            M_CharacterController = characterController;
            M_HorMoveSpeed = horMoveSpeed;
            M_VerMoveSpeed = verMoveSpeed;
            M_AttackCooldown = 0;
        }

        public void UpdateControls(KeyCode[] keyCodes)
        {
            m_KeyCodes = keyCodes;
        }

        public void InitializeState()
        {

        }

        public void UpdateState()
        {
            Debug.Log("Walking");
            if (Input.GetKeyDown(m_KeyCodes[4]))
            {
                ToFishing();
            }

            Movement();
        }

        private void Movement()
        {
            Vector3 currentPosition = M_CharacterController.gameObject.transform.position;

            if (Input.GetKey(m_KeyCodes[1]))
            {
              M_CharacterController.gameObject.transform.position = new Vector3(M_CharacterController.gameObject.transform.position.x,
                M_CharacterController.gameObject.transform.position.y, M_CharacterController.gameObject.transform.position.z + M_VerMoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(m_KeyCodes[0]))
            {
                M_CharacterController.gameObject.transform.position = new Vector3(M_CharacterController.gameObject.transform.position.x,
                    M_CharacterController.gameObject.transform.position.y, M_CharacterController.gameObject.transform.position.z - M_VerMoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(m_KeyCodes[3]))
            {
                M_CharacterController.gameObject.transform.position = new Vector3(M_CharacterController.gameObject.transform.position.x - M_HorMoveSpeed * Time.deltaTime,
                    M_CharacterController.gameObject.transform.position.y, M_CharacterController.gameObject.transform.position.z);
            }
            if (Input.GetKey(m_KeyCodes[2]))
            {
                M_CharacterController.gameObject.transform.position = new Vector3(M_CharacterController.gameObject.transform.position.x + M_HorMoveSpeed * Time.deltaTime,
                    M_CharacterController.gameObject.transform.position.y, M_CharacterController.gameObject.transform.position.z);
            }
        }

        public void ToFishing()
        {
            M_CharacterController.SwitchToFishingState();
        }

        public void OnTriggerStay(Collider collider)
        {
            if (Input.GetKey(m_KeyCodes[5])
                && M_AttackCooldown >= 2
                && collider.CompareTag("Player")
                && collider.gameObject != null)
            {
                /// fabio moet slaan en stoppen met vissen vangen
                collider.gameObject.GetComponent<CharacterControl>().DropFish();
            }
        }

        public void AddPowerUp(PowerUp Power)
        {
            M_CharacterController.M_AddPowerup.Invoke(Power);
        }
    }
}


