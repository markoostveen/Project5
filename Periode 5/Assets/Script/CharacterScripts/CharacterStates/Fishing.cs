using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Character.Ai;
using Game.Character.player.Powerups;

namespace Game.Character.player
{
<<<<<<< HEAD
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
=======
    private IFish m_CurrentSelectedFish;
    private List<IFish> m_FishInArea;
    private List<IFish> m_CaughtFish;
    public Action<IFish> m_Catched;

    private GameObject m_SelectedFishSprite;
    private CharacterControl m_CharacterControl;

    private string[] m_Inputs;
    private int m_SelectedFishIndex;
    private float m_CatchMeter;
    private bool m_Catching;


    public Fishing(CharacterControl characterController, GameObject selectedSprite)
    {
        m_Inputs = new string[6];
        m_CharacterControl = characterController;
        m_FishInArea = new List<IFish>();
        m_CaughtFish = new List<IFish>();
        m_SelectedFishSprite = selectedSprite;
        m_Catching = false;
    }

    public void UpdateControls(string[] inputs)
    {
        m_Inputs = inputs;
    }
>>>>>>> Fabio

        public void InitializeState()
        {
            m_CurrentSelectedFish = null;
            M_FishInArea.Clear();
            M_CaughtFish.Clear();
            GetAllFishInArea();
            m_SelectedFishIndex = 0;
            m_CatchMeter = 0;
        }

<<<<<<< HEAD
        public void UpdateState()
        {
            //ObjectPool.PoolObject obj = (ObjectPool.PoolObject)m_CurrentSelectedFish;
            //m_SelectedFishSprite.transform.position = obj.transform.position;

            Debug.Log("Fishing");

            SwitchSelectedFish();

            if (m_CurrentSelectedFish != null && Input.GetKeyDown(m_KeyCodes[0]))
=======

    public void UpdateState()
    {
        Debug.Log("Fishing State");
        SwitchSelectedFish();

        m_CharacterControl.ShowCurrentSelectedfish(m_CurrentSelectedFish.GetGameObject);

        if (m_CurrentSelectedFish != null)
        {
            if (Input.GetButtonDown(m_Inputs[1]) && (m_Catching))
>>>>>>> Fabio
            {
                Debug.Log("Catching Fish");

                m_CharacterControl.UpdateFishingLine(m_CurrentSelectedFish.GetGameObject);
                m_CatchMeter += 20;
                


                if (m_CatchMeter >= 100)
                {
                    Debug.Log("Cought it");
                    CatchFish();
                }
            }
<<<<<<< HEAD

            if (Input.GetKeyDown(m_KeyCodes[4]))
=======
            else if (Input.GetButtonDown(m_Inputs[1]) && (!m_Catching))
            {
                m_CharacterControl.ActivateFishingLine(m_CurrentSelectedFish.GetGameObject);
                m_CurrentSelectedFish.BeingCatched();
                m_Catching = true;
            }

            if (Input.GetButtonDown(m_Inputs[2]) || Input.GetButtonDown(m_Inputs[3]))
            {
                m_CharacterControl.DeactivateFishingLine();

                if (m_Catching)
                {
                    m_CurrentSelectedFish.Escaped();
                    m_CatchMeter = 0;
                    m_CharacterControl.DeactivateFishingLine();
                }
            }
        }


        if (Input.GetButtonDown(m_Inputs[0]))
        {
            m_Catching = false;
            if (m_CaughtFish.Count >= 1)
>>>>>>> Fabio
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

<<<<<<< HEAD
        private void SwitchSelectedFish()
=======
    public List<IFish> GetCaughtFish()
    {
        return m_CaughtFish;
    }

    private void SwitchSelectedFish()
    {
        if (Input.GetButtonDown(m_Inputs[2]))
>>>>>>> Fabio
        {
            if (Input.GetKey(m_KeyCodes[2]))
            {
<<<<<<< HEAD
                if (m_SelectedFishIndex == 0)
                {
                    m_SelectedFishIndex = M_FishInArea.Count;
                }
                else if (m_SelectedFishIndex >= 1)
                {
                    m_SelectedFishIndex -= 1;
                }

                m_CatchMeter = 0;
=======
                m_SelectedFishIndex = m_FishInArea.Count - 1;
>>>>>>> Fabio
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

<<<<<<< HEAD
        private void CatchFish()
=======
        if (Input.GetButtonDown(m_Inputs[3]))
>>>>>>> Fabio
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
<<<<<<< HEAD
=======

        m_CurrentSelectedFish = m_FishInArea[m_SelectedFishIndex];
    }
>>>>>>> Fabio

        public void ToWalking()
        {
<<<<<<< HEAD
            M_CharacterControl.SwitchToWalkingState();
=======
            m_CaughtFish.Add(m_FishInArea[m_SelectedFishIndex]);
            m_CaughtFish[m_CaughtFish.Count - 1].BeingCatched();

            m_FishInArea.RemoveAt(m_SelectedFishIndex);
            m_SelectedFishIndex = 0;

            m_CurrentSelectedFish.Catched();
            //m_CaughtFish[m_CaughtFish.Count - 1].Catched();

            m_CharacterControl.DeactivateFishingLine();
            m_Catching = false;
            m_CatchMeter = 0;
>>>>>>> Fabio
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

<<<<<<< HEAD
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
=======
    private void GetAllFishInArea()
    {
        m_FishInArea.Clear();
        if (m_CurrentSelectedFish != null)
        {
            m_FishInArea.Add(m_CurrentSelectedFish);
        }

        Collider[] hitColliders = Physics.OverlapSphere(m_CharacterControl.gameObject.transform.position, 3.5f);

        if (hitColliders.Length <= 0)
        {
            ToWalking();
        }
        else
        {
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.layer == 8)
                {
                    m_FishInArea.Add(col.gameObject.GetComponent<IFish>());
                }
            }
>>>>>>> Fabio
        }

    }
}


