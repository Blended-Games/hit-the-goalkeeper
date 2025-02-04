﻿using TMPro;
using UnityEngine;


#region TransformPositionsEnum

    public enum TransformPosition
    {
        Head,
        Spine,
        Leg,
        Off
    };

    #endregion
    
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager main;
        public GameObject[] upgradeButtons; //These are the upgrade buttons.

        private void Awake()
        {
            if (main != null && main != this)
            {
                Destroy(gameObject);
                return;
            }

            main = this;
        }

        #endregion

        #region Introducing Global Dependencies

        [Header("Global Dependencies")] public float ballAttackValue; //Balls power value


        public bool firstTouch; //This the first calculation bool, it controls the bars function.

        public bool
            camStopFollow; //This is the condition for offrecords movement. Because we do not want to follow the ball from there.

        public int calculationID; //This id is for changing the value for calculation in shooting mechanics. 

        public TransformPosition ballsHitRoad; //This will be the state for controlling balls transform point. 

        public GameObject powerBarIndicatorParent; //This is the powerbar indicator script.

        public float ballCurveValue; //This will be the value for balls left, right movement.

        public GameObject faceParticleObj, shootParticleObj;

        public TextMeshProUGUI maxDamageText; //This is the text file that will show the players max potential damage to screen.

        public GameObject levelSuccessPanel, levelFailedPanel;
        
        [SerializeField]
       private GameObject startPanel; //These are the after level panels.
       [SerializeField] private Animator cameraMainAnim; //Accessing main cameras animator component for game start animation.
       public GameObject powerBar; //This will be activated after main cam anim finishes.
        
        #endregion

        #region TargetFrameRate

        private void Start()
        {
            Application.targetFrameRate = 30; //Setting the target frame rate for unexpected frame drop rates.
        }

        #endregion
        #region FirstTouchEnable

        public void FirstTouchEnable()
        {
            startPanel.SetActive(false);
            upgradeButtons[0].gameObject.SetActive(false); 
            upgradeButtons[1].gameObject.SetActive(false);
            cameraMainAnim.SetTrigger("CamAnimStop");
        }
        #endregion
    }
