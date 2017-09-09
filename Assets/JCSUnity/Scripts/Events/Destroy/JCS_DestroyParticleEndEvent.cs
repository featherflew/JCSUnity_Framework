/**
 * $File: JCS_DestroyParticleEndEvent.cs $
 * $Date: 2017-04-10 20:56:24 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright (c) 2017 by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JCSUnity
{

    /// <summary>
    /// Destroy if the particle system ends.
    /// </summary>
    [RequireComponent(typeof(JCS_ParticleSystem))]
    public class JCS_DestroyParticleEndEvent
        : MonoBehaviour
    {

        //----------------------
        // Public Variables

        //----------------------
        // Private Variables
        private JCS_ParticleSystem mParticleSystem = null;

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
            this.mParticleSystem = this.GetComponent<JCS_ParticleSystem>();
        }

        private void Update()
        {
            if (mParticleSystem.IsParticleEnd())
                Destroy(this.gameObject);
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