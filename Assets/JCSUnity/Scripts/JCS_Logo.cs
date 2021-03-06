﻿/**
 * $File: JCS_Logo.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;


namespace JCSUnity
{
    
    /// <summary>
    /// Logo object for next scene.
    /// </summary>
    public class JCS_Logo 
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables
        [Header("** Note: Fade Time can be set at JCS_SceneSettings. **")]

        [SerializeField] private string mNextLevel = "JCS_Demo";

        // second to show logo
        [SerializeField] private float mDelayTime = 1.0f;
        private float mDelayTimer = 0.0f;

        private bool mCycleThrough = false;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------

        //========================================
        //      Unity's function
        //------------------------------

        private void Start()
        {
            // Hide all the open dialogue
            JCS_UIManager.instance.HideAllOpenDialogue();

            // Plus the fade out time
            mDelayTime += JCS_SceneManager.instance.SceneFadeOutTime;
        }

        private void Update()
        {
            JCS_GameManager.instance.GAME_PAUSE = true;

            mDelayTimer += Time.deltaTime;
            if (mDelayTime < mDelayTimer)
            {
                mCycleThrough = true;
            }

            if (mCycleThrough)
            {
                JCS_GameManager.instance.GAME_PAUSE = false;
                JCS_SceneManager.instance.LoadScene(mNextLevel);
            }
        }

        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}
