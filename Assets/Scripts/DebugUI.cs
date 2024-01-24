/*
FileName : UI.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 9th January 2023, 13:23:00
Description : Creates the debug UI.
*/
using UnityEngine;
using JetBrains.Annotations;
using gDebug.Player;
    
namespace gDebug
{
    public class DebugUI : MonoBehaviour
    {
        private Manager playerManager;
        private bool menuOpen;
        private int windowId = 0000;
        private Rect windowRect = new Rect(20, 20, 600, 600);
        [UsedImplicitly]
        private void Awake()
        {
            playerManager = GetComponent<Manager>();
        }
        [UsedImplicitly]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (menuOpen)
                {
                    menuOpen = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    menuOpen = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        [UsedImplicitly]
        private void OnGUI()
        {
            if (menuOpen)
            {
                windowRect = GUILayout.Window(windowId, windowRect, DrawDebugWindow, "Debug Menu");
                windowRect.width = Screen.width * 0.01f;
                windowRect.height = Screen.height * 0.01f;
            }
        }
        private void DrawDebugWindow(int id)
        {
            GUILayout.Label("Player Attack:");
            playerManager.raycastDrawEnabled = GUILayout.Toggle(playerManager.raycastDrawEnabled, "Toggle raycast drawing");
            playerManager.raycastPrintEnabled = GUILayout.Toggle(playerManager.raycastPrintEnabled, "Toggle raycast printing");
            GUILayout.Label("Player Movement:");
            playerManager.movementDrawEnabled = GUILayout.Toggle(playerManager.movementDrawEnabled, "Toggle movement drawing");
            playerManager.movementPrintEnabled = GUILayout.Toggle(playerManager.movementPrintEnabled, "Toggle movement printing");
            GUI.DragWindow();
        }
    }
}