﻿/**
 * $File: JCS_TalkObject.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


namespace JCSUnity
{

    /// <summary>
    /// Simulate the object u cant talk to.
    /// </summary>
    public class JCS_TalkObject 
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables

        [Header("** Initialize Variable **")]

        [Tooltip("")]
        [SerializeField]
        private JCS_DialogueScript mDialogueScript = null;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------

        //========================================
        //      Unity's function
        //------------------------------

        private void OnMouseOver()
        {
            if (JCS_Input.OnMouseDoubleClick(JCS_InputType.MOUSE_LEFT))
            {
                JCS_DialogueSystem jcsDs = JCS_UtilitiesManager.instance.GetDialogueSystem();

                if (jcsDs != null)
                {
                    // active dialogue system.
                    jcsDs.ActiveDialogue(mDialogueScript);
                }
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
        private void CreateCharacterImage(Sprite sp)
        {
            if (sp == null)
                return;


        }

    }
}
