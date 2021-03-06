/**
 * $File: JCS_ButtonSelection.cs $
 * $Date: 2017-10-07 14:41:08 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright (c) 2017 by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace JCSUnity
{
    public delegate void SelectionEnable();
    public delegate void SelectionDisable();
    public delegate void SelectionActive(bool act);

    /// <summary>
    /// One of the button selection for button selection group.
    /// 
    /// NOTE(jenchieh): this must use with JCS_ButtonSelectionGroup
    /// class or else is useless.
    /// </summary>
    public class JCS_ButtonSelection
        : MonoBehaviour
    {

        /*******************************************/
        /*            Public Variables             */
        /*******************************************/
        public SelectionEnable selectionEnable = SelectionEnable;
        public SelectionDisable selectionDisable = SelectionDisable;
        public SelectionActive selectionActive = SelectionActive;

        /// <summary>
        /// Direction the object link to button.
        /// </summary>
        public enum LinkDirection
        {
            ButtonToSelection,
            SelectionToButton,
        };


        /*******************************************/
        /*           Private Variables             */
        /*******************************************/

        private JCS_ButtonSelectionGroup mButtonSelectionGroup = null;

        [Header("** Check Variables (JCS_ButtonSelection) **")]

        [Tooltip("Is this selection got active?")]
        [SerializeField]
        private bool mActive = false;


        [Header("** Initialize Variables (JCS_ButtonSelection) **")]

        [Tooltip("Deactive this button on Awake time?")]
        [SerializeField]
        private bool mDeactiveAtAwake = true;


        [Header("** Runtime Variables (JCS_ButtonSelection) **")]

        [Tooltip("Skip this selection?")]
        [SerializeField]
        private bool mSkip = false;

        [Tooltip("Events when you enter this selection.")]
        [SerializeField]
        private UnityEvent mSelectedEvent = null;

        [Tooltip("List of effect when on this selection.")]
        [SerializeField]
        private JCS_UnityObject[] mEffects = null;


        [Header("- Button (JCS_ButtonSelection)")]

        [Tooltip("Button for selection group to handle.")]
        [SerializeField]
        private JCS_Button mButton = null;

        [Tooltip("This gameobject itself is a button and use this button component.")]
        [SerializeField]
        private bool mSelfAsButton = true;

        [Tooltip("Make skip variable with interactable with the button.")]
        [SerializeField]
        private bool mLinkSkipWithInteractableButton = true;

        [Tooltip("Direction the 'skip' variable connect to button's interactable variable.")]
        [SerializeField]
        private LinkDirection mLinkDirection = LinkDirection.ButtonToSelection;


        [Header("- Full Control (JCS_ButtonSelection)")]

        [Tooltip("What is the selection ontop of this selection? (Press Up)")]
        [SerializeField]
        private JCS_ButtonSelection mUpSelection = null;

        [Tooltip("What is the selection ontop of this selection? (Press Down)")]
        [SerializeField]
        private JCS_ButtonSelection mDownSelection = null;

        [Tooltip("What is the selection ontop of this selection? (Press Right)")]
        [SerializeField]
        private JCS_ButtonSelection mRightSelection = null;

        [Tooltip("What is the selection ontop of this selection? (Press Left)")]
        [SerializeField]
        private JCS_ButtonSelection mLeftSelection = null;

        /*******************************************/
        /*           Protected Variables           */
        /*******************************************/

        /*******************************************/
        /*             setter / getter             */
        /*******************************************/
        public LinkDirection linkDirection { get { return this.mLinkDirection; } set { this.mLinkDirection = value; } }
        public bool LinkSkipWithInteractableButton { get { return this.mLinkSkipWithInteractableButton; } set { this.mLinkSkipWithInteractableButton = value; } }
        public bool DeactiveAtAwake { get { return this.mDeactiveAtAwake; } set { this.mDeactiveAtAwake = value; } }
        public bool SelfAsButton { get { return this.mSelfAsButton; } set { this.mSelfAsButton = value; } }
        public JCS_Button Button { get { return this.mButton; } set { this.mButton = value; } }
        public UnityEvent SelectedEvent { get { return this.mSelectedEvent; } }
        public bool Active
        {
            get { return this.mActive; }
            set
            {
                this.mActive = value;
                DoActive();
            }
        }
        public JCS_ButtonSelectionGroup ButtonSelectionGroup { get { return this.mButtonSelectionGroup; } set { this.mButtonSelectionGroup = value; } }
        public bool Skip
        {
            get { return this.mSkip; }
            set
            {
                this.mSkip = value;

                SetInteractableButton();
            }
        }

        public JCS_ButtonSelection UpSelection { get { return this.mUpSelection; } set { this.mUpSelection = value; } }
        public JCS_ButtonSelection DownSelection { get { return this.mDownSelection; } set { this.mDownSelection = value; } }
        public JCS_ButtonSelection RightSelection { get { return this.mRightSelection; } set { this.mRightSelection = value; } }
        public JCS_ButtonSelection LeftSelection { get { return this.mLeftSelection; } set { this.mLeftSelection = value; } }

        /*******************************************/
        /*            Unity's function             */
        /*******************************************/
        private void Awake()
        {
            if (mDeactiveAtAwake)
            {
                // Deactive every at start
                this.Active = false;
            }

            if (mSelfAsButton)
            {
                if (this.mButton == null)
                    this.mButton = this.GetComponent<JCS_Button>();
            }

            // let the button know this is going to be control in the group.
            if (mButton != null)
                mButton.ButtonSelection = this;
        }

        private void Start()
        {
            SetInteractableButton();
        }


        /*******************************************/
        /*              Self-Define                */
        /*******************************************/
        //----------------------
        // Public Functions

        /// <summary>
        /// Do stuff if this selection been checked.
        /// </summary>
        public void DoSelection()
        {
            if (mButton != null)
                mButton.JCS_ButtonClick();

            if (mSelectedEvent != null)
                mSelectedEvent.Invoke();
        }

        /// <summary>
        /// Check if this selection get selected.
        /// </summary>
        /// <returns>
        /// true: selected.
        /// false: vice versa.
        /// </returns>
        public bool IsSelected()
        {
            return this.mActive;
        }

        /// <summary>
        /// Make this selection selected.
        /// </summary>
        public void MakeSelect()
        {
            mButtonSelectionGroup.SelectSelection(this);
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

        /* Default Function Pointers. */
        private static void SelectionEnable() { }
        private static void SelectionDisable() { }
        private static void SelectionActive(bool act) { }

        /// <summary>
        /// Do active and deactive.
        /// </summary>
        private void DoActive()
        {
            this.selectionActive.Invoke(this.mActive);

            if (this.mActive)
            {
                // Do stuff when active..
                selectionEnable.Invoke();
            }
            else
            {
                // Do stuff when deactive..
                selectionDisable.Invoke();
            }

            ActiveEffects(this.mActive);
        }

        /// <summary>
        /// Active the effects?
        /// </summary>
        /// <param name="act">
        /// true: active effects.
        /// false: deactive effects.
        /// </param>
        private void ActiveEffects(bool act)
        {
            for (int index = 0;
                   index < mEffects.Length;
                   ++index)
            {
                JCS_UnityObject effect = mEffects[index];

                if (effect != null)
                {
                    effect.UpdateUnityData();
                    effect.LocalEnabled = act;
                }
            }
        }

        /// <summary>
        /// Make skip variable connect to interactable with button.
        /// </summary>
        private void SetInteractableButton()
        {
            if (!mLinkSkipWithInteractableButton || mButton == null)
                return;

            switch (mLinkDirection)
            {
                case LinkDirection.ButtonToSelection:
                    {
                        // change itself, button have more control/power
                        // over selection.
                        mSkip = !mButton.Interactable;
                    }
                    return;
                case LinkDirection.SelectionToButton:
                    {
                        // vice versa, this have more control/power
                        // over button.
                        mButton.SetInteractable(!mSkip);
                    }
                    return;
            }

            // this should not happens...
            JCS_Debug.LogWarning("Button selection link failed...");
        }

    }
}
