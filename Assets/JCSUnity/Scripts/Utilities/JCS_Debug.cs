﻿/**
 * $File: JCS_Debug.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System.IO;


namespace JCSUnity
{

    /// <summary>
    /// Debugging class.
    /// </summary>
    public static class JCS_Debug
    {
        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Log)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void Log(
            object msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Log", msg.ToString(), file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Log)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void Log(
            string msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Log", msg, file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Errors)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogError(
            object msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Errors", msg.ToString(), file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Errors)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogError(
            string msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Errors", msg, file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Warnings)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogWarning(
            object msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Warnings", msg.ToString(), file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Warnings)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogWarning(
            string msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Warnings", msg, file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Reminders)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogReminders(
            object msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Reminders", msg.ToString(), file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format. (Reminders)
        /// </summary>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        public static void LogReminders(
            string msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
            JcsLog("Reminders", msg, file, member, line);
        }

        /// <summary>
        /// Print out log by deesign by JCSUnity format.
        /// </summary>
        /// <param name="type">Log type.</param>
        /// <param name="msg"> msg to print out. </param>
        /// <param name="file"> caller file path. </param>
        /// <param name="member"> caller function. </param>
        /// <param name="line"> caller line number. </param>
        private static void JcsLog(
            string type,
            string msg,
            [CallerFilePathAttribute] string file = "",
            [CallerMemberName] string member = "",
            [CallerLineNumberAttribute] int line = 0)
        {
#if (UNITY_EDITOR)
            string filename = Path.GetFileName(file);
            Debug.Log("\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Debug.Log("=-= JCSUnity." + type + " - [" + filename + "](" + line + ")" + msg + " =-=");
            Debug.Log("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
#endif

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void DrawLine(Vector2 from, Vector2 to)
        {
            Vector3 tempFrom = new Vector3(from.x, from.y, 0);
            Vector3 tempTo = new Vector3(to.x, to.y, 0);

            Debug.DrawLine(tempFrom, tempTo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="col"></param>
        public static void DrawLine(Vector2 from, Vector2 to, Color col)
        {
            Vector3 tempFrom = new Vector3(from.x, from.y, 0);
            Vector3 tempTo = new Vector3(to.x, to.y, 0);

            Debug.DrawLine(tempFrom, tempTo, col);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void DrawLine(Vector3 from, Vector3 to)
        {
            Debug.DrawLine(from, to);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="col"></param>
        public static void DrawLine(Vector3 from, Vector3 to, Color col)
        {
            Debug.DrawLine(from, to, col);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topLeft"></param>
        /// <param name="topRight"></param>
        /// <param name="botRight"></param>
        /// <param name="botLeft"></param>
        /// <param name="col"></param>
        public static void DrawRect(Vector3 topLeft, Vector3 topRight, Vector3 botRight, Vector3 botLeft, Color col)
        {
            Debug.DrawLine(topLeft, topRight, col);
            Debug.DrawLine(topRight, botRight, col);
            Debug.DrawLine(botRight, botLeft, col);
            Debug.DrawLine(botLeft, topLeft, col);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topLeft"></param>
        /// <param name="topRight"></param>
        /// <param name="botRight"></param>
        /// <param name="botLeft"></param>
        public static void DrawRect(Vector3 topLeft, Vector3 topRight, Vector3 botRight, Vector3 botLeft)
        {
            // set default color as white
            DrawRect(topLeft, topRight, botRight, botLeft, Color.white);
        }

        /// <summary>
        /// Print out the transform name.
        /// </summary>
        /// <param name="trans"> name of the transform. </param>
        public static void PrintName(Transform trans)
        {
            Debug.Log("Name: " + trans.name);
        }


        /// <summary>
        /// Draw the collider
        /// </summary>
        /// <param name="collider"> Collider u want to draw. </param>
        /// <param name="col"> Color type. </param>
        public static void DrawCollider(BoxCollider2D collider, Color col)
        {
#if (UNITY_EDITOR)
            // get width and height information.
            Vector2 boxInfo = JCS_Physics.GetColliderInfo(collider);

            Vector3 pos = JCS_Physics.GetColliderPosition(collider);

            Vector3 topLeft = new Vector3(
                pos.x - boxInfo.x / 2,
                pos.y + boxInfo.y / 2);
            Vector3 topRight = new Vector3(
                pos.x + boxInfo.x / 2,
                pos.y + boxInfo.y / 2);
            Vector3 botRight = new Vector3(
                pos.x + boxInfo.x / 2,
                pos.y - boxInfo.y / 2);
            Vector3 botLeft = new Vector3(
                pos.x - boxInfo.x / 2,
                pos.y - boxInfo.y / 2);

            DrawRect(topLeft, topRight, botRight, botLeft, col);
#endif
        }

        /// <summary>
        /// Draw the collider, use for check last frame.
        /// </summary>
        /// <param name="collider"> Collider u want to draw. </param>
        /// <param name="col"> Color type. </param>
        /// <param name="origin">Provide the origin position. </param>
        public static void DrawCollider(BoxCollider2D collider, Color col, Vector3 origin)
        {
#if (UNITY_EDITOR)
            // get width and height information.
            Vector2 boxInfo = JCS_Physics.GetColliderInfo(collider);

            Vector3 pos = origin;

            Vector3 topLeft = new Vector3(
                pos.x - boxInfo.x / 2,
                pos.y + boxInfo.y / 2);
            Vector3 topRight = new Vector3(
                pos.x + boxInfo.x / 2,
                pos.y + boxInfo.y / 2);
            Vector3 botRight = new Vector3(
                pos.x + boxInfo.x / 2,
                pos.y - boxInfo.y / 2);
            Vector3 botLeft = new Vector3(
                pos.x - boxInfo.x / 2,
                pos.y - boxInfo.y / 2);

            DrawRect(topLeft, topRight, botRight, botLeft, col);
#endif
        }

    }
}
