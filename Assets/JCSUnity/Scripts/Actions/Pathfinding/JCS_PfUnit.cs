﻿/**
 * $File: JCS_PfUnit.cs $
 * $Date: $
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
    /// 
    /// </summary>
    public class JCS_PfUnit
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables

        [Header("** Check Variables (JCS_PfUnit) **")]

        [Tooltip("Target we want to track.")]
        [SerializeField]
        private Transform mTarget = null;

        [SerializeField]
        private Vector3[] mPath = null;

        [SerializeField]
        private int mTargetIndex = 0;


        [Header("** Runtime Variables (JCS_PfUnit) **")]

        [SerializeField]
        private float mSpeed = 20;

        

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public Transform Target { get { return this.mTarget; } set { this.mTarget = value; } }

        //========================================
        //      Unity's function
        //------------------------------

#if (UNITY_EDITOR)
        private void Update()
        {
            if (JCS_Input.GetKeyDown(KeyCode.A))
                ActivePathfinding();
            if (JCS_Input.GetKeyDown(KeyCode.S))
                ActivePathfinding(new Vector3(0, 0, 32));
            if (JCS_Input.GetKeyDown(KeyCode.D))
                ActivePathfinding(new Vector3(0, 0, 32.2f));
        }
#endif

        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        /// <summary>
        /// Make the path finding request and 
        /// start doing the track movement.
        /// </summary>
        public void ActivePathfinding()
        {
            ActivePathfinding(this.mTarget);
        }
        /// <summary>
        /// Make the path finding request and 
        /// start doing the track movement.
        /// </summary>
        /// <param name="trans"> Move to this transform. </param>
        public void ActivePathfinding(Transform trans)
        {
            ActivePathfinding(trans.position);
        }
        /// <summary>
        /// Make the path finding request and 
        /// start doing the track movement.
        /// </summary>
        /// <param name="pos"> Move to this position. </param>
        public void ActivePathfinding(Vector3 pos)
        {
            // clear the array
            mPath = null;

            // reset target index
            mTargetIndex = 0;

            // make path finding request
            JCS_PathRequestManager.RequestPath(
                this.transform.position,
                pos,
                OnPathFound);
        }

        /// <summary>
        /// Delay the request function call.
        /// </summary>
        /// <param name="newPath"></param>
        /// <param name="pathSuccessful"></param>
        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                mPath = newPath;
                mTargetIndex = 0;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
        
        /// <summary>
        /// Draw out the path.
        /// </summary>
        public void OnDrawGizmos()
        {
            if (mPath != null)
            {
                for (int i = mTargetIndex; i < mPath.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(mPath[i], Vector3.one);

                    if (i == mTargetIndex)
                    {
                        Gizmos.DrawLine(transform.position, mPath[i]);
                    }
                    else {
                        Gizmos.DrawLine(mPath[i - 1], mPath[i]);
                    }
                }
            }
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator FollowPath()
        {
            // do nothing if the length is zero
            if (mPath == null || mPath.Length == 0)
                yield break;

            Vector3 currentWaypoint = mPath[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    ++mTargetIndex;

                    if (mPath == null || 
                        mTargetIndex >= mPath.Length)
                        yield break;

                    currentWaypoint = mPath[mTargetIndex];
                }

                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    currentWaypoint, 
                    mSpeed * Time.deltaTime);

                yield return null;
            }
        }

    }
}
