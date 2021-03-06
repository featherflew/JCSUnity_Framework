﻿/**
 * $File: JCS_PanelType.cs $
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
    public enum JCS_PanelType
    {
        // reset the scale, position and rotation 
        // every time opens it
        RESET_PANEL,            
        RECORD_PANEL,           // single player

        RECORD_PANEL_TO_DATABASE      // for online game
    }
}
