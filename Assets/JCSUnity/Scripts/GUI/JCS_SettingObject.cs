﻿/**
 * $File: JCS_SettingObject.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace JCSUnity
{

    /// <summary>
    /// Object need to get the information in order to get the correct setting
    /// </summary>
    public class JCS_SettingObject 
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables
        [SerializeField] private JCS_SoundSettingType mSettingType = JCS_SoundSettingType.NONE;
        [SerializeField] private JCS_GUIType mGUIType = JCS_GUIType.NONE;

        // usually should be only one!
        private float mSettingValue = 0.0f;
        private string mSettingString = "";

        // list of all the type
        private Toggle mToggle = null;
        private Slider mSlider = null;
        private Scrollbar mScrollbar = null;
        private Dropdown mDropdown = null;
        private InputField mInputField = null;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public string SettingString { get { return this.mSettingString; } }

        //========================================
        //      Unity's function
        //------------------------------
        private void Start()
        {
            InitValue();

            // get the corrosponding gui type!!
            InitGUIComponent();

        }

        private void Update()
        {
            switch (mGUIType)
            {
                case JCS_GUIType.NONE:
                    return;
                case JCS_GUIType.TOGGLE:
                    {

                    }
                    break;
                case JCS_GUIType.SLIDER:
                    {
                        SliderSettingUpdate();
                    }
                    break;
                case JCS_GUIType.SCROLL_BAR:
                    {

                    }
                    break;
                case JCS_GUIType.DROP_DOWN:
                    {

                    }
                    break;
                case JCS_GUIType.INPUT_FIELD:
                    {

                    }
                    break;
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
        /// <summary>
        /// Set value according to the GUI type!
        /// </summary>
        private void InitValue()
        {
            switch (mSettingType)
            {
                case JCS_SoundSettingType.NONE:
                    JCS_Debug.LogError("JCS_SettingObject",   "Setting with no meaning...");
                    return;
                case JCS_SoundSettingType.BGM_SOUND:
                    this.mSettingValue = JCS_SoundSettings.instance.GetBGM_Volume();
                    break;
                case JCS_SoundSettingType.SFX_SOUND:
                    this.mSettingValue = JCS_SoundSettings.instance.GetSFXSound_Volume();
                    break;
                case JCS_SoundSettingType.SKILLS_SOUND:
                    this.mSettingValue = JCS_SoundSettings.instance.GetSkillsSound_Volume();
                    break;
            }
        }
        private void InitGUIComponent()
        {
            switch (mGUIType)
            {
                case JCS_GUIType.NONE:
                    JCS_Debug.LogError("JCS_SettingObject",   "Setting with no meaning...");
                    return;
                case JCS_GUIType.TOGGLE:
                    {
                        this.mToggle = this.GetComponent<Toggle>();
                        if (mToggle == null)
                        {
                            JCS_Debug.LogError("JCS_SettingObject",   "Toggle with no corrosdoing component...");
                            return;
                        }
                    }
                    break;
                case JCS_GUIType.SLIDER:
                    {
                        this.mSlider = this.GetComponent<Slider>();
                        if (mSlider == null)
                        {
                            JCS_Debug.LogError("JCS_SettingObject",   "Slider with no corrosdoing component...");
                            return;
                        }
                        // assign value;
                        this.mSlider.value = mSettingValue;
                    }
                    break;
                case JCS_GUIType.SCROLL_BAR:
                    {
                        this.mScrollbar = this.GetComponent<Scrollbar>();
                        if (mScrollbar == null)
                        {
                            JCS_Debug.LogError("JCS_SettingObject",   "Scrollbar with no corrosdoing component...");
                            return;
                        }
                    }
                    break;
                case JCS_GUIType.DROP_DOWN:
                    {
                        this.mDropdown = this.GetComponent<Dropdown>();
                        if (mDropdown == null)
                        {
                            JCS_Debug.LogError("JCS_SettingObject",   "Dropdown with no corrosdoing component...");
                            return;
                        }
                    }
                    break;
                case JCS_GUIType.INPUT_FIELD:
                    {
                        this.mInputField = this.GetComponent<InputField>();
                        if (mInputField == null)
                        {
                            JCS_Debug.LogError("JCS_SettingObject",   "InputField with no corrosdoing component...");
                            return;
                        }
                    }
                    break;
            }
        }


        private void SliderSettingUpdate()
        {
            switch (mSettingType)
            {
                case JCS_SoundSettingType.BGM_SOUND:
                    JCS_SoundSettings.instance.SetBGM_Volume(this.mSlider.value);
                    break;
                case JCS_SoundSettingType.SFX_SOUND:
                    JCS_SoundSettings.instance.SetSFXSound_Volume(this.mSlider.value);
                    break;
                case JCS_SoundSettingType.SKILLS_SOUND:
                    JCS_SoundSettings.instance.SetSkillsSound_Volume(this.mSlider.value);
                    break;
            }
        }

    }
}
