#if (UNITY_ANDRIOD || UNITY_IOS)
/**
 * $File: JCS_AdvertisementManager.cs $
 * $Date: 2017-04-28 21:48:08 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright (c) 2017 by Shen, Jen-Chieh $
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


namespace JCSUnity
{

    /// <summary>
    /// Handle Advertisment provide by Unity Technologies company.
    /// 
    /// NOTE(jenchieh): If using this manager, you need to do these 
    /// two thing.
    /// 
    ///     1) Work Online not offline on Unity.
    ///     2) Enable window->service->Ads service provided by Unity 
    ///       Technologies.
    /// 
    /// </summary>
    public class JCS_AdvertisementManager
        : MonoBehaviour
    {

        //----------------------
        // Public Variables
        public static JCS_AdvertisementManager instance = null;

#if (UNITY_ANDROID)
        [Tooltip("Andriod game id provided by Unity Server window.")]
        public string ANDRIOD_GAME_ID = "";
#elif (UNITY_IOS)
        [Tooltip("iOS game id provided by Unity Server window.")]
        public string IOS_GAME_ID = "";
#endif

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

#if (UNITY_ANDROID)
        Advertisement.Initialize(ANDRIOD_GAME_ID);
#elif (UNITY_IOS)
        Advertisement.Initialize(IOS_GAME_ID);
#endif
        }

        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        /// <summary>
        /// Start the reward ads video. This will pop out the video screen.
        /// </summary>
        /// <param name="delayTime"> time delay. </param>
        /// <param name="result"> result callback </param>
        public void StartDelayRewardVideo(float delayTime, Action<ShowResult> result)
        {
            StartCoroutine(DelayedRewardVideo(delayTime, result));
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

        /// <summary>
        /// Main function delay and show reward ads.
        /// </summary>
        /// <param name="delayTime"> time delay. </param>
        /// <param name="result"> result callback </param>
        /// <returns> result of the coroutine. </returns>
        private IEnumerator DelayedRewardVideo(float delayTime, Action<ShowResult> result)
        {
            yield return new WaitForSeconds(delayTime);

            ShowRewardedAd(result);
        }

        /// <summary>
        /// Show reward ads.
        /// </summary>
        /// <param name="callback"> function accept the result. </param>
        public void ShowRewardedAd(Action<ShowResult> callback)
        {
            if (Advertisement.IsReady())
            {
                ShowOptions options = new ShowOptions();
                options.resultCallback = callback;
                Advertisement.Show("rewardedVideo", options);
            }
        }
    }
}
#endif
