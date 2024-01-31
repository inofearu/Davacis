/*
FileName : MovementInfo.cs 
Namespace: gDebug.Parameters
FileType : C# Source File
Author : Christopher Huskinson
Created On : 16th January 2023, 13:05
Description: Contains variables for attacks passed out of main files to debug ones.
*/
using UnityEngine;
namespace gDebug.Parameters
{
    public class MovementInfo
    {
        #pragma warning disable IDE1006
        public Vector3 verticalMove { get; set; }
        public bool isGrounded { get; set; }
        public Vector3 playerVelocity { get; set; }
        #pragma warning restore IDE1006
    }
}