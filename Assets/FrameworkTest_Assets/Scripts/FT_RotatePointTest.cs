/**
 * $File: FT_RotatePointTest.cs $
 * $Date: 2017-09-11 19:00:53 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright (c) 2017 by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JCSUnity;


/// <summary>
/// 
/// </summary>
public class FT_RotatePointTest 
    : MonoBehaviour 
{

    /*******************************************/
    /*            Public Variables             */
    /*******************************************/

    /*******************************************/
    /*           Private Variables             */
    /*******************************************/
    public Transform origin = null;
    public float angleX = 10;
    public float angleY = 10;
    public float angleZ = 10;

    public float radius = 10;

    /*******************************************/
    /*           Protected Variables           */
    /*******************************************/

    /*******************************************/
    /*             setter / getter             */
    /*******************************************/

    /*******************************************/
    /*            Unity's function             */
    /*******************************************/
    private void Awake()
    {

    }

    private void Update()
    {
        if (JCS_Input.GetKeyDown(KeyCode.C))
        {
            //this.transform.position = JCS_Mathf.RotatePointY(this.transform.position, mOrigin.transform.position, mAngle);
        }

        if (JCS_Input.GetKey(KeyCode.C))
        {
            --angleY;
            this.transform.position = JCS_Mathf.CirclePositionY(
                origin.transform.position, 
                angleY, 
                radius, 
                this.transform.position);
        }

        if (JCS_Input.GetKey(KeyCode.V))
        {
            ++angleZ;
            this.transform.position = JCS_Mathf.CirclePositionZ(
                origin.transform.position, 
                angleZ,
                radius, 
                this.transform.position);
        }
    }
    
    /*******************************************/
    /*              Self-Define                */
    /*******************************************/
    //----------------------
    // Public Functions
    
    //----------------------
    // Protected Functions
    
    //----------------------
    // Private Functions
    
}
