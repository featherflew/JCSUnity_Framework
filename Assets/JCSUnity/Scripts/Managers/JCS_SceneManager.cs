﻿/**
 * $File: JCS_SceneManager.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace JCSUnity
{

    /// <summary>
    /// Manage scenes changes.
    /// </summary>
    public class JCS_SceneManager
        : MonoBehaviour
    {

        //----------------------
        // Public Variables
        public static JCS_SceneManager instance = null;

        [SerializeField]
        private JCS_SwitchSceneType mType = JCS_SwitchSceneType.BLACK_SCREEN;

        //----------------------
        // Private Variables
        private bool mSwitchSceneEffect = false;
        private string mNextSceneName = "";

        // Async loading scene operation. (thread)
        private AsyncOperation mAsyncOperation = null;

        [Header("** Game Settings (JCS_SceneManager) **")]

        [Tooltip("Pause the game while the scene start to load?")]
        [SerializeField]
        private bool mPauseGameWhileLoadingScene = true;


        [Header("** Black Screen Settings (JCS_SceneManager) **")]

        [SerializeField]
        private JCS_BlackScreen mJCSBlackScreen = null;

        [Tooltip("Which direction to fade slide.")]
        [SerializeField]
        private JCS_Align mAlign = JCS_Align.ALIGN_LEFT;


        [Header("** Black Slide Screen Settings (JCS_SceneManager) **")]

        [SerializeField]
        private JCS_BlackSlideScreen mJCSBlackSlideScreen = null;


        [Header("** White Screen Settings (JCS_SceneManager) **")]

        [Tooltip("Do u need the white screen in ur game?")]
        [SerializeField]
        private bool mPopWhiteScreen = false;

        [SerializeField]
        private JCS_WhiteScreen mJCSWhiteScreen = null;


        [Header("** General Screen Settings (JCS_SceneManager) **")]

        [Tooltip("Do this scene using the specific setting?")]
        [SerializeField]
        private bool mOverrideSetting = false;

        [Tooltip("Fade in time. (For this scene)")]
        [SerializeField]
        private float mSceneFadeInTime = 1.0f;

        [Tooltip("Fade out time. (For this scene)")]
        [SerializeField]
        private float mSceneFadeOutTime = 1.0f;

        // fade the sound while switching the scene.
        private JCS_FadeSound mJCSFadeSound = null;

        // Scene in the game so is dynamic instead of Unity's scene system's scene
        // Unity 自帶就有Scene這個物件. 在這個Unity Scene裡面自己宣告Scene
        // 比起Unity的Scene比較動態, 因為自己宣告的操控比較多.
        private JCS_DynamicScene mDynamicScene = null;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public JCS_DynamicScene GetDynamicScene() { return this.mDynamicScene; }
        public void SetDynamicScene(JCS_DynamicScene ds) { this.mDynamicScene = ds; }
        public void SetJCSBlackScreen(JCS_BlackScreen bs) { this.mJCSBlackScreen = bs; }
        public void SetJCSBlackSlideScreen(JCS_BlackSlideScreen bs) { this.mJCSBlackSlideScreen = bs; }
        public void SetJCSWhiteScreen(JCS_WhiteScreen ws) { this.mJCSWhiteScreen = ws; }
        public JCS_WhiteScreen GetJCSWhiteScreen() { return this.mJCSWhiteScreen; }
        private JCS_BlackScreen GetJCSBlackScreen() { return this.mJCSBlackScreen; }

        public bool OverrideSetting { get { return this.mOverrideSetting; } }
        public float SceneFadeInTime { get { return this.mSceneFadeInTime; } set { this.mSceneFadeInTime = value; } }
        public float SceneFadeOutTime { get { return this.mSceneFadeOutTime; } set { this.mSceneFadeOutTime = value; } }

        //========================================
        //      Unity's function
        //------------------------------
        private void Awake()
        {
            instance = this;

            switch (mType)
            {
                case JCS_SwitchSceneType.BLACK_SCREEN:
                    {
                        JCS_UtilityFunctions.PopJCSBlackScreen();
                    }
                    break;

                case JCS_SwitchSceneType.SLIDE_SCREEN:
                    {
                        JCS_UtilityFunctions.PopJCSBlackSlideScreen();
                    }
                    break;
            }

            // Pop white screen depends on game needs.
            if (mPopWhiteScreen)
                JCS_UtilityFunctions.PopJCSWhiteScreen();

#if (UNITY_EDITOR)
            // add the tool in editor mode.
            this.gameObject.AddComponent<ReadSceneNames>();
#endif
        }

        private void Start()
        {
            // NOTE(jenchieh): get the fade out time base on 
            // the scene setting and scene manager specific.
            float fadeoutTime = JCS_SceneSettings.instance.GetSceneFadeInTimeBaseOnSetting();

            switch (mType)
            {
                case JCS_SwitchSceneType.BLACK_SCREEN:
                    {
                        // get the current screen color.
                        mJCSBlackScreen.LocalColor = JCS_SceneSettings.instance.SCREEN_COLOR;

                        mJCSBlackScreen.FadeOut(fadeoutTime);
                    }
                    break;

                case JCS_SwitchSceneType.SLIDE_SCREEN:
                    {
                        mJCSBlackSlideScreen.StartSlideOut(mAlign, fadeoutTime);
                    }
                    break;
            }


            if (JCS_SoundSettings.instance.SMOOTH_SWITCH_SOUND_BETWEEN_SCENE)
            {
                // get the component.
                if (mJCSFadeSound == null)
                    mJCSFadeSound = this.gameObject.AddComponent<JCS_FadeSound>();

                // set the background audio source.
                mJCSFadeSound.SetAudioSource(
                    JCS_SoundManager.instance.GetBGMAudioSource());

                // active the fade sound in effect.
                mJCSFadeSound.FadeIn(
                    JCS_SoundSettings.instance.GetBGM_Volume(),
                    /* Fade in the sound base on the setting. */
                    JCS_SoundSettings.instance.GetSoundFadeInTimeBaseOnSetting());
            }

            // the game is loaded start the game agian
            JCS_GameManager.instance.GAME_PAUSE = false;
        }

        private void Update()
        {
            // check if during the switch scene?
            if (!mSwitchSceneEffect)
                return;


            switch (mType)
            {
                case JCS_SwitchSceneType.BLACK_SCREEN:
                    {
                        if (mJCSBlackScreen.IsFadeIn())
                        {
                            // No need this anymore, since we have the
                            // to clean up everything before we load the scene.
                            // we need this boolean to check weather the event can
                            // spawn new "GameObject" when "OnDestroy" function was 
                            // called in Unity.
                            //mSwitchSceneEffect = false;

                            // load the scene if is ready
                            mAsyncOperation.allowSceneActivation = true;
                        }
                    }
                    break;

                case JCS_SwitchSceneType.SLIDE_SCREEN:
                    {
                        if (mJCSBlackSlideScreen.IsDoneSliding())
                        {
                            // load the scene if is ready
                            mAsyncOperation.allowSceneActivation = true;
                        }
                    }
                    break;
            }
        }

        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        /// <summary>
        /// Load the scene with default fade in time
        /// </summary>
        /// <param name="sceneName"> scene name to load </param>
        public void LoadScene(string sceneName)
        {
            // NOTE(jenchieh): get the fade in time base on 
            // the scene setting and scene manager specific.
            float fadeInTime = JCS_SceneSettings.instance.GetSceneFadeInTimeBaseOnSetting();

            // load scene and pass the value in.
            LoadScene(sceneName, fadeInTime);
        }

        /// <summary>
        /// Load scene with self-define fade in time.
        /// </summary>
        /// <param name="sceneName"> scene name to load </param>
        /// <param name="fadeInTime"> time to fade in </param>
        public void LoadScene(string sceneName, float fadeInTime)
        {
            LoadScene(sceneName, fadeInTime, Color.black);
        }

        /// <summary>
        /// Load scene with self-define fade in time.
        /// </summary>
        /// <param name="sceneName"> scene name to load </param>
        /// /// <param name="screenColor"> screen color </param>
        public void LoadScene(string sceneName, Color screenColor)
        {
            // NOTE(jenchieh): get the fade in time base on 
            // the scene setting and scene manager specific.
            float fadeInTime = JCS_SceneSettings.instance.GetSceneFadeInTimeBaseOnSetting();

            LoadScene(sceneName, fadeInTime, screenColor);
        }

        /// <summary>
        /// Load scene with self-define fade in time.
        /// </summary>
        /// <param name="sceneName"> scene name to load </param>
        /// <param name="fadeInTime"> time to fade in </param>
        /// <param name="screenColor"> screen color </param>
        public void LoadScene(string sceneName, float fadeInTime, Color screenColor)
        {
#if (UNITY_EDITOR)
            // only do this in Editor Mode, 
            // this help level designer to do their job.
            if (!ReadSceneNames.CheckSceneAvailable(sceneName))
            {
                JCS_Debug.LogReminders(
                    "Scene [" + sceneName + "] u want to load is not in the Build Setting...");

                return;
            }
#endif

            // if is loading already, dont load it agian
            if (mSwitchSceneEffect)
                return;

            // set the next scene name
            this.mNextSceneName = sceneName;

            JCS_GameSettings gs = JCS_GameSettings.instance;
            if (gs.SAVE_ON_SWITCH_SCENE &&
                gs.SAVE_GAME_DATA_FUNC != null)
            {
                // do the saving.
                gs.SAVE_GAME_DATA_FUNC.Invoke();
            }

            // preload the scene
            mAsyncOperation = SceneManager.LoadSceneAsync(mNextSceneName);
            mAsyncOperation.allowSceneActivation = false;

            switch (mType)
            {
                case JCS_SwitchSceneType.BLACK_SCREEN:
                    {
                        // move to the last child in order
                        // to render the black screen in front of 
                        // any UI's GUI
                        mJCSBlackScreen.MoveToTheLastChild();

                        // set the screen color.
                        // NOTE(jenchieh): always start with opacity the same 
                        // as previous.
                        screenColor.a = mJCSBlackScreen.LocalColor.a;
                        mJCSBlackScreen.LocalColor = screenColor;

                        // record down the screen color.
                        JCS_SceneSettings.instance.SCREEN_COLOR = screenColor;

                        // start fading in (black screen)
                        mJCSBlackScreen.FadeIn(fadeInTime);
                    }
                    break;

                case JCS_SwitchSceneType.SLIDE_SCREEN:
                    {
                        mJCSBlackSlideScreen.MoveToTheLastChild();

                        mJCSBlackSlideScreen.StartSlideIn(mAlign, fadeInTime);
                    }
                    break;
            }



            // start fading sound
            if (JCS_SoundSettings.instance.SMOOTH_SWITCH_SOUND_BETWEEN_SCENE)
            {
                mJCSFadeSound.SetAudioSource(JCS_SoundManager.instance.GetBGMAudioSource());

                // fade out sound to zero
                mJCSFadeSound.FadeOut(0, fadeInTime);
            }

            // start check to switch scene or not
            mSwitchSceneEffect = true;

            // Pause the game depends on setting...
            JCS_GameManager.instance.GAME_PAUSE = mPauseGameWhileLoadingScene;
        }

        /// <summary>
        /// Check is loading the scene or not.
        /// </summary>
        /// <returns> return result. </returns>
        public bool IsSwitchingScene()
        {
            return mSwitchSceneEffect;
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}
