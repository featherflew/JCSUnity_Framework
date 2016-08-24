﻿/**
 * $File: JCS_3DDragDropObject.cs $
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
    /// Source: https://www.youtube.com/watch?v=NMt6Ibxa_XQ
    /// 
    /// Use mouse control the 3d object to drag and drop the object
    /// </summary>
    public class JCS_3DDragDropObject
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables

        [Header("** Runtime Variables **")]
        [Tooltip("Draw the other direction. (Horizontal)")]
        [SerializeField] private bool mFlipX = false;
        [Tooltip("Draw the other direction. (Vertical)")]
        [SerializeField] private bool mFlipY = false;

        private Vector3 mDistance = Vector3.zero;
        private float mPosX = 0;
        private float mPosY = 0;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------

        //========================================
        //      Unity's function
        //------------------------------

        private void OnMouseDown()
        {
            mDistance = Camera.main.WorldToScreenPoint(this.transform.position);
            mPosX = Input.mousePosition.x - mDistance.x;
            mPosY = Input.mousePosition.y - mDistance.y;
        }

        private void OnMouseDrag()
        {
            Vector3 curPos = new Vector3(Input.mousePosition.x - mPosX, Input.mousePosition.y - mPosY, mDistance.z);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

            if (mFlipX)
                worldPos.x = -worldPos.x;
            if (mFlipY)
                worldPos.y = -worldPos.y;

            this.transform.position = worldPos;
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
