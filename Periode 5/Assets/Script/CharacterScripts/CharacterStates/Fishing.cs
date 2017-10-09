using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Character.Ai;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
    public class Fishing : ICharacterStates
    {
        private IFish m_CurrentSelectedFish;

        private KeyCode[] m_KeyCodes;

        private List<IFish> M_FishInArea { get; }
        private List<IFish> M_CaughtFish { get; }

        private GameObject M_SelectedFishSprite { get; }

        private CharacterControl M_CharacterControl { get; }

        private int m_SelectedFishIndex;
        private int m_CatchMeter;

        public Action<IFish> M_Catched { get; }

        public Fishing(CharacterControl characterController, ref GameObject selectedSprite)
        {
            m_KeyCodes = new KeyCode[6];
            M_CharacterControl = characterController;
            M_FishInArea = new List<IFish>();
            M_CaughtFish = new List<IFish>();
            M_SelectedFishSprite = selectedSprite;
        }

        public void UpdateControls(KeyCode[] keyCodes)
        {
            m_KeyCodes = keyCodes;
        }

        public void InitializeState()
        {
            m_CurrentSelectedFish = null;
            M_FishInArea.Clear();
            M_CaughtFish.Clear();
            GetAllFishInArea();
            m_SelectedFishIndex = 0;
            m_CatchMeter = 0;
        }

        public void UpdateState()
        {
            //ObjectPool.PoolObject obj = (ObjectPool.PoolObject)m_CurrentSelectedFish;
            //m_SelectedFishSprite.transform.position = obj.transform.position;

            Debug.Log("Fishing");

            SwitchSelectedFish();

            if (m_CurrentSelectedFish != null && Input.GetKeyDown(m_KeyCodes[0]))
            {
                Debug.Log("Catching Fish");

                m_CurrentSelectedFish.BeingCatched();
                m_CatchMeter += 20;


                if (m_CatchMeter >= 100)
                {
                    Debug.Log("Cought it");
                    CatchFish();
                }
            }

            if (Input.GetKeyDown(m_KeyCodes[4]))
            {
                if (M_CaughtFish.Count >= 1)
                {
                    ToCarrying();
                }
                else
                {
                    ToWalking();
                }      
            }
        }

        private void SwitchSelectedFish()
        {
            if (Input.GetKey(m_KeyCodes[2]))
            {
                if (m_SelectedFishIndex == 0)
                {
                    m_SelectedFishIndex = M_FishInArea.Count;
                }
                else if (m_SelectedFishIndex >= 1)
                {
                    m_SelectedFishIndex -= 1;
                }

                m_CatchMeter = 0;
            }

            if (Input.GetKey(m_KeyCodes[3]))
            {
                if (m_SelectedFishIndex == M_FishInArea.Count)
                {
                    m_SelectedFishIndex = 0;
                }
                else if (m_SelectedFishIndex <= 1)
                {
                    m_SelectedFishIndex += 1;
                }

                m_CatchMeter = 0;
            }
        }

        private void CatchFish()
        {
            if(M_FishInArea.Count > 0)
            {
                M_CaughtFish.Add(M_FishInArea[m_SelectedFishIndex]);
                M_CaughtFish[M_CaughtFish.Count - 1].BeingCatched();

                M_FishInArea.RemoveAt(m_SelectedFishIndex);
                m_SelectedFishIndex = 0;


                M_CaughtFish[M_CaughtFish.Count - 1].Catched();
            }
        }

        public void ToWalking()
        {
            M_CharacterControl.SwitchToWalkingState();
        }

        public void ToCarrying()
        {
            M_CharacterControl.SwitchToCarryingState(M_CaughtFish);
        }

        public void OnTriggerStay(Collider collider)
        {

        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.layer == 8)
            {
                GetAllFishInArea();
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == 8)
            {
                GetAllFishInArea();
            }
        }

        public void SetCurrentSelecetedFish()
        {
            m_CurrentSelectedFish = M_FishInArea[0];        
        }

        private void GetAllFishInArea()
        {
            M_FishInArea.Clear();

            Collider[] hitColliders = Physics.OverlapSphere(M_CharacterControl.gameObject.transform.position, 5000f);

            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.layer == 8)
                {
                    M_FishInArea.Add(col.gameObject.GetComponent<IFish>());
                }    
            }
        }

        public void AddPowerUp(PowerUp Power)
        {
            M_CharacterControl.M_AddPowerup.Invoke(Power);
        }

    }
}


