﻿/**
 * $File: JCS_3DLookAtAction.cs $
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
    /// 
    /// </summary>
    public class JCS_3DLookAtAction
        : MonoBehaviour
        , JCS_Action
    {

        //----------------------
        // Public Variables

        public enum State
        {
            POSITIVE = 1,
            NEGATIVE = -1
        };

        //----------------------
        // Private Variables

        [Header("** Runtime Variables (JCS_3DLookAtAction) **")]

        [Tooltip("Do the action avaliable?")]
        [SerializeField]
        private bool mLookAction = true;

        [Tooltip("Target we are going to look at.")]
        [SerializeField]
        private Transform mTargetTransform = null;

        [Tooltip("Direction the object looking at.")]
        [SerializeField]
        private JCS_Vector3Direction mLookDirection = JCS_Vector3Direction.UP;

        [Tooltip("Is the direction positive or negative value?")]
        [SerializeField]
        private State mState = State.POSITIVE;

        [Tooltip("Some reason the formula isn't seem correct?")]
        [SerializeField]
        private bool mRotateBack90 = false;

        [Tooltip("Freeze x axis?")]
        [SerializeField]
        private bool mFreezeX = false;
        [Tooltip("Freeze y axis?")]
        [SerializeField]
        private bool mFreezeY = false;
        [Tooltip("Freeze z axis?")]
        [SerializeField]
        private bool mFreezeZ = false;

        [Tooltip("Offset of each angle.")]
        [SerializeField]
        private Vector3 mAngleOffset = Vector3.zero;


        [Header("- Asymptotic Look (JCS_3DLookAtAction)")]

        [Tooltip("Did the object gradually look at the target?")]
        [SerializeField]
        private bool mAsympLook = false;

        [Tooltip("How it look at the target?")]
        [SerializeField]
        [Range(0.01f, 10.0f)]
        private float mLookFriction = 0.4f;

        // record down the last euler angles.
        private Vector3 mLastEulerAngles = Vector3.zero;

        // the target eular angles we are targeting to approach.
        private Vector3 mTargetEulerAngles = Vector3.zero;

        // record the current rotation every frame, in order to set it back
        // to original rotation which is the freezing effect.
        private Vector3 mCurrentRotation = Vector3.zero;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public Transform GetTargetTransform() { return this.mTargetTransform; }
        public void SetTargetTransform(Transform t) { this.mTargetTransform = t; }
        public bool RotateBack90 { get { return this.mRotateBack90; } set { this.mRotateBack90 = value; } }

        public bool LookAction { get { return this.mLookAction; } set { this.mLookAction = value; } }

        public bool AsympLook { get { return this.mAsympLook; } set { this.mAsympLook = value; } }
        public float LookFriction { get { return this.mLookFriction; } set { this.mLookFriction = value; } }

        //========================================
        //      Unity's function
        //------------------------------

        private void LateUpdate()
        {
            // record the current rotation.
            mCurrentRotation = this.transform.eulerAngles;

            DoLookAt();
            DoAsympLook();
            DoFreeze();
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
        /// Do the look at algorithm here...
        /// </summary>
        private void DoLookAt()
        {
            // check if the effect and target avaliable?
            if (mTargetTransform == null || !mLookAction)
                return;

            // record down the euler angle before we changes.
            mLastEulerAngles = this.transform.localEulerAngles;


            Vector3 lookPoint = mTargetTransform.position;
            Vector3 direction = Vector3.up;

            // get direction according to the type.
            direction = JCS_Utility.VectorDirection(mLookDirection);

            transform.LookAt(lookPoint, direction * (int)mState);

            // apply offset angle
            this.transform.eulerAngles += mAngleOffset;

            // TODO(JenChieh): study the rotation going on in 
            //                Unity lower level archietecture.
            // rotate back to X-axis.
            if (mRotateBack90)
                transform.Rotate(0, -90, 0);
        }

        /// <summary>
        /// Do the asymptotic look algorithm 
        /// here...
        /// </summary>
        private void DoAsympLook()
        {
            // check if the effect active?
            if (!mAsympLook)
                return;

            // NOTE(jenchieh): once we get here, mean that 
            // we already look at the target cuz of the last 
            // function call is "DoLookAt()".


            // set the target eular angles.
            mTargetEulerAngles = this.transform.localEulerAngles;

            // set the angle to the angle before 
            // we do the "DoLookAt action".
            this.transform.localEulerAngles = mLastEulerAngles;

            // precalculate the angle in order to have negative effect.
            Vector3 deltaAngles = this.transform.localEulerAngles;
            deltaAngles = (this.mTargetEulerAngles - this.transform.localEulerAngles) / mLookFriction * Time.deltaTime;

            // IMPORTANT(jenchieh): here is how u deal with the
            // Unity Engine 0 ~ 360 degree range euler angle.
            {
                // find the diff of the two rotation.
                Vector3 difVec = JCS_Mathf.ToPositive(this.mTargetEulerAngles - mLastEulerAngles);

                if (difVec.x > 180)
                {
                    if (mTargetEulerAngles.x < mLastEulerAngles.x)
                        deltaAngles.x = ((360 + mTargetEulerAngles.x) - mLastEulerAngles.x) / mLookFriction * Time.deltaTime;
                    else
                        deltaAngles.x = -((360 - mTargetEulerAngles.x) + mLastEulerAngles.x) / mLookFriction * Time.deltaTime;
                }

                if (difVec.y >= 180)
                {
                    // Current euler angle y 在左邊.
                    if (mTargetEulerAngles.y < mLastEulerAngles.y)
                        deltaAngles.y = ((360 + mTargetEulerAngles.y) - mLastEulerAngles.y) / mLookFriction * Time.deltaTime;
                    // Current euler angle y 在右邊.
                    else
                        deltaAngles.y = -((360 - mTargetEulerAngles.y) + mLastEulerAngles.y) / mLookFriction * Time.deltaTime;
                }

                if (difVec.z > 180)
                {
                    if (mTargetEulerAngles.z < mLastEulerAngles.z)
                        deltaAngles.z = ((360 + mTargetEulerAngles.z) - mLastEulerAngles.z) / mLookFriction * Time.deltaTime;
                    else
                        deltaAngles.z = -((360 - mTargetEulerAngles.z) + mLastEulerAngles.z) / mLookFriction * Time.deltaTime;
                }
            }

            // apply precalculate angle.
            this.transform.localEulerAngles += deltaAngles;
        }

        /// <summary>
        /// Do the freeze rotation?
        /// </summary>
        private void DoFreeze()
        {
            Vector3 newRotation = this.transform.eulerAngles;

            // if freeze, set to previous rotation.
            if (mFreezeX)
                newRotation.x = mCurrentRotation.x;

            if (mFreezeY)
                newRotation.y = mCurrentRotation.y;

            if (mFreezeZ)
                newRotation.z = mCurrentRotation.z;

            this.transform.eulerAngles = newRotation;
        }

    }
}