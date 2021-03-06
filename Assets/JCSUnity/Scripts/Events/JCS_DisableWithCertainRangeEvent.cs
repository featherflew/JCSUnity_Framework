/**
 * $File: JCS_DisableWithCertainRangeEvent.cs $
 * $Date: 2016-11-12 21:30:00 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;


namespace JCSUnity
{

    /// <summary>
    /// Disable the the current game object 
    /// with in the certain range.
    /// </summary>
    [RequireComponent(typeof(JCS_FadeObject))]
    public class JCS_DisableWithCertainRangeEvent
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables

        private JCS_FadeObject mJCSFadeObject = null;

        [Header("** Runtime Variables (JCS_DisableWithCertainRangeEvent) **")]

        [Tooltip("")]
        [SerializeField]
        private bool mUseLocal = false;

        [Tooltip("Target with in the range.")]
        [SerializeField]
        private Transform mTargetTransform = null;

        [Tooltip("Target position, do not have to pass in transform.")]
        [SerializeField]
        private Vector3 mTargetPosition = Vector3.zero;

        [Tooltip("Disable with in this range.")]
        [SerializeField] [Range(0, 1000)]
        private float mRange = 0;

        [Header("** Optional Variables (JCS_DisableWithCertainRangeEvent) **")]

        [Tooltip("")]
        [SerializeField]
        private bool mFadeEffect = false;

        [Tooltip("When do the object start to fade.")]
        [SerializeField] [Range(0, 1000)]
        private float mFadeDistance = 0;

        private bool mFaded = false;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public bool FadeEffect { get { return this.mFadeEffect; } set { this.mFadeEffect = value; } }
        public Vector3 TargetPosition { get { return this.mTargetPosition; } set { this.mTargetPosition = value; } }
        public void SetTargetTransfrom(Transform trans)
        {
            // update target position.
            this.mTargetTransform = trans;

            // update the target position too.
            this.mTargetPosition = this.transform.position;
        }
        public float Range { get { return this.mRange; } set { this.mRange = value; } }

        //========================================
        //      Unity's function
        //------------------------------
        private void Awake()
        {
            mJCSFadeObject = this.GetComponent<JCS_FadeObject>();
        }

        private void Update()
        {
            UpdateTargetPosition();

            DisableWithInRange();
        }

        private void OnEnable()
        {
            mFaded = false;
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
        /// Disable the gameobject when whith in the range.
        /// </summary>
        private void DisableWithInRange()
        {
            float distance = 0;
            if (mUseLocal)
            {
                distance = Vector3.Distance(this.transform.localPosition, this.mTargetPosition);
            }
            else
            {
                // get the distance between self's and target's.
                distance = Vector3.Distance(this.transform.position, this.mTargetPosition);
            }

            if (distance < this.mRange)
            {
                // disable the game object.
                this.gameObject.SetActive(false);
            }

            // check fade effect enable?
            if (!mFadeEffect || mFaded)
                return;

            if (distance < this.mFadeDistance)
            {
                mJCSFadeObject.FadeOut();
                mFaded = true;
            }
        }

        /// <summary>
        /// Get the tartget's transform position / local position.
        /// </summary>
        private void UpdateTargetPosition()
        {
            if (mTargetTransform == null)
            {
                if (JCS_GameSettings.instance.DEBUG_MODE)
                {
                    JCS_Debug.LogError(
                        "Cannot set the position without target transform.");
                }

                return;
            }

            if (mUseLocal)
            {
                mTargetPosition = mTargetTransform.localPosition;
            }
            else
            {
                // get the target position.
                mTargetPosition = mTargetTransform.position;
            }
        }

    }
}
