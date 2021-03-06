﻿/**
 * $File: JCS_LiveObjectManager.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                    Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;


namespace JCSUnity
{

    /// <summary>
    /// Handle of all the AI in the scene.
    /// </summary>
    public class JCS_LiveObjectManager
        : MonoBehaviour
    {

        //----------------------
        // Public Variables
        public static JCS_LiveObjectManager instance = null;

        //----------------------
        // Private Variables

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------

        //========================================
        //      Unity's function
        //------------------------------
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SetSpecificGameTypeGameManager();
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

        /// <summary>
        /// Base on different type of game add different type of manager.
        /// </summary>
        private void SetSpecificGameTypeGameManager()
        {
            JCS_GameSettings gs = JCS_GameSettings.instance;

            switch (gs.GAME_TYPE)
            {
                case JCS_GameType.GAME_2D:
                    this.gameObject.AddComponent<JCS_2DLiveObjectManager>();
                    break;
            }
        }

    }
}
