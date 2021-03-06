﻿/**
 * $File: JCS_Canvas.cs $
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

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    public class JCS_Canvas 
        : MonoBehaviour
    {

        //----------------------
        // Public Variables
        public static JCS_Canvas instance = null;

        //----------------------
        // Private Variables
        [SerializeField] private Canvas mCanvas = null;
        [SerializeField] private JCS_ResizeUI mResizeUI = null;
        [SerializeField] private string mResizeUI_path = "JCSUnity_Resources/JCS_LevelDesignUI/ResizeUI";

        // Application Rect (Window)
        private RectTransform mAppRect = null;

        //----------------------
        // Protected Variables

        //========================================
        //      setter / getter
        //------------------------------
        public RectTransform GetAppRect() { return this.mAppRect; }
        public Canvas GetCanvas() { return this.mCanvas; }
        public void SetResizeUI(JCS_ResizeUI ui) { this.mResizeUI = ui; }
        public JCS_ResizeUI GetResizeUI() { return this.mResizeUI; }

        //========================================
        //      Unity's function
        //------------------------------
        private void Awake()
        {

            if (instance != null)
            {
                string black_screen_name = JCS_GameSettings.BLACK_SCREEN_NAME;
                string white_screen_name = JCS_GameSettings.WHITE_SCREEN_NAME;

                // cuz the transform list will change while we set the transform to 
                // the transform, 
                List<Transform> readyToSetList = new List<Transform>();

                Transform tempTrans = instance.transform;
                // so record all the transform
                for (int index = 0;
                    index < tempTrans.childCount;
                    ++index)
                {
                    Transform child = tempTrans.GetChild(index);
                    if (child.name == black_screen_name ||
                        child.name == (black_screen_name + "(Clone)"))
                        continue;

                    if (child.name == white_screen_name ||
                        child.name == (white_screen_name + "(Clone)"))
                        continue;

                    if (child.name == "JCS_IgnorePanel")
                        continue;

                    // TODO(JenChieh): optimize this?
                    if (child.GetComponent<JCS_IgnoreDialogueObject>() != null)
                        continue;

                    // add to set list ready to set to the new transform as parent
                    readyToSetList.Add(child);
                }

                // set to the new transform
                foreach (Transform trans in readyToSetList)
                {
                    // set parent to the new canvas in the new scene
                    trans.SetParent(this.transform);
                }

                // Delete the old one
                DestroyImmediate(instance.gameObject);
            }
            

            // attach the new one
            instance = this;

            this.mAppRect = this.GetComponent<RectTransform>();
            this.mCanvas = this.GetComponent<Canvas>();

            if (JCS_GameSettings.instance.RESIZE_UI)
            {
                // resizable UI in order to resize the UI correctly
                GameObject gm = JCS_Utility.SpawnGameObject(mResizeUI_path);
                gm.transform.SetParent(this.transform);
            }
        }

        private void Start()
        {
            if (JCS_GameSettings.instance.RESIZE_UI)
            {
                if (mResizeUI == null)
                    return;

                // get the screen width and height
                Vector2 actualRect = this.GetAppRect().sizeDelta;

                // set it to the right resolution
                mResizeUI.GetResizeRect().sizeDelta = actualRect;
            }
        }


        //========================================
        //      Self-Define
        //------------------------------
        //----------------------
        // Public Functions

        /// <summary>
        /// Add component to resize canvas.
        /// </summary>
        /// <param name="com"> Component add to canvas. </param>
        public void AddComponentToResizeCanvas(Component com)
        {
            if (mResizeUI == null)
            {
                com.transform.SetParent(this.mCanvas.transform);
            }
            else
            {
                com.transform.SetParent(this.mResizeUI.transform);
            }
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}
