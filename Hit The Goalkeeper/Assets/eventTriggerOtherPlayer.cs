using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTriggerOtherPlayer : MonoBehaviour
{  
    #region Singleton
    public static eventTriggerOtherPlayer main;
    // Start is called before the first frame update
    

        #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BallShotOnState()
    {
         BallMove.main._updateStop = false;
            BallMove.main.Movement();
    }
}
