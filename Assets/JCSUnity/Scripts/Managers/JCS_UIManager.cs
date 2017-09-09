﻿/**
 * $File: JCS_UIManager.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace JCSUnity
{

    /// <summary>
    /// Manage all the dialogue in the scene.
    /// </summary>
    public class JCS_UIManager 
        : MonoBehaviour
    {

        //----------------------
        // Public Variables
        public static JCS_UIManager instance = null;

        //----------------------
        // Private Variables

        [Header("** Initialize Variables (JCS_UIManager) **")]

        [Tooltip("Game Play UI (Game Layer - Only One)")]
        [SerializeField]
        private JCS_DialogueObject mGameUI = null;          // the most common one!

        [Tooltip("System dialogue (Application Layer - Only One)")]
        [SerializeField]
        private JCS_DialogueObject mForceDialogue = null;

        // Game Dialogue (Game Layer - could have multiple one)
        [Tooltip("Dialogue player are focusing")]
        [SerializeField]
        private JCS_DialogueObject mFocusGameDialogue = null;

        // List of all the window that are opened!
        private LinkedList<JCS_DialogueObject> mOpenWindow = null;


        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public void SetJCSDialogue(JCS_DialogueType type, JCS_DialogueObject jdo)
        {
            switch (type)
            {
                case JCS_DialogueType.GAME_UI:
                    {
                        if (mGameUI != null)
                        {
                            JCS_Debug.LogError("Failed to set \"In Game Dialogue\"...");
                            return;
                        }

                        this.mGameUI = jdo;
                    }
                    break;
                case JCS_DialogueType.PLAYER_DIALOGUE:
                    {
                        //if (mFocusGameDialogue != null)
                        //{
                        //    JCS_Debug.LogError("JCS_UIManager",   "Failed to set \"In Game Dialogue\"...");
                        //    return;
                        //}

                        this.mFocusGameDialogue = jdo;
                    }
                    break;
                case JCS_DialogueType.SYSTEM_DIALOGUE:
                    {
                        if (mForceDialogue != null)
                        {
                            JCS_Debug.LogError("JCS_UIManager",   "Failed to set \"Force Dialogue\"...");
                            return;
                        }

                        mForceDialogue = jdo;
                    }
                    break;
            }
        }
        public JCS_DialogueObject GetJCSDialogue(JCS_DialogueType type)
        {
            switch (type)
            {
                // Game UI
                case JCS_DialogueType.GAME_UI:
                    return this.mGameUI;

                // Dialogue Box
                case JCS_DialogueType.PLAYER_DIALOGUE:
                    return this.mFocusGameDialogue;
                case JCS_DialogueType.SYSTEM_DIALOGUE:
                    return this.mForceDialogue;
            }

            JCS_Debug.LogError("JCS_GameManager",   "Failed to get Dialogue -> " + type);
            return null;
        }
        public LinkedList<JCS_DialogueObject> GetOpenWindow() { return this.mOpenWindow; }

        //========================================
        //      Unity's function
        //------------------------------
        private void Awake()
        {
            instance = this;

            mOpenWindow = new LinkedList<JCS_DialogueObject>();
        }
        private void Start()
        {
            if (JCS_GameSettings.instance == null)
                return;
        }

        private void Update()
        {
#if (UNITY_EDITOR)
            // Test.
            //DialogueTest();
#endif


#if (UNITY_EDITOR || UNITY_STANDALONE)

            // Exit in game diagloue not in game UI!!
            // (Admin input)
            if (Input.GetKeyDown(KeyCode.Escape))
                JCS_UtilityFunctions.DestoryCurrentDialogue(JCS_DialogueType.PLAYER_DIALOGUE);
#endif
        }

        private void DialogueTest()
        {
            if (JCS_Input.GetKeyDown(KeyCode.A))
                JCS_UtilityFunctions.PopIsConnectDialogue();
            if (JCS_Input.GetKeyDown(KeyCode.S))
                JCS_UtilityFunctions.PopSettingDialogue();
            if (JCS_Input.GetKeyDown(KeyCode.D))
                JCS_UtilityFunctions.PopInGameUI();
            if (JCS_Input.GetKeyDown(KeyCode.F))
                JCS_UtilityFunctions.PopTalkDialogue();
        }

        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        /// <summary>
        /// Hide the last open dialogue in the current scene.
        /// </summary>
        public void HideTheLastOpenDialogue()
        {
            // return if nothing in the list
            if (GetOpenWindow().Count == 0)
                return;

            JCS_DialogueObject jdo = GetOpenWindow().Last.Value;

            // once it hide it will remove from the list it self!
            jdo.HideDialogue();
        }

        /// <summary>
        /// Hide all the open dialgoue in the current scene.
        /// </summary>
        public void HideAllOpenDialogue()
        {
            // return if nothing in the list
            if (GetOpenWindow().Count == 0)
                return;

            while (GetOpenWindow().Count != 0)
            {
                GetOpenWindow().First.Value.HideDialogueWithoutSound();
            }

        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}