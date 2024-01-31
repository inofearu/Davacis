/*
FileName : UI.cs 
Namespace : gDebug
FileType : C# Source File
Author : Christopher Huskinson
Created On : 1st January 2024, 13:23
Description : Creates the debug UI.
*/
using UnityEngine;
using JetBrains.Annotations;
    
namespace gDebug
{
    public class DebugUI : MonoBehaviour
    {
        private PlayerManager playerManager;
        private bool menuOpen;
        private int windowId = 0000;
        private Rect windowRect = new Rect(20, 20, 600, 600);
        [UsedImplicitly]
        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
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
            GUILayout.Label("Level:");
            if (GUILayout.Button("Level up"))
            {
                playerManager.levelUp = true;
            }
            else
            {
                playerManager.levelUp = false;
            }
            if (GUILayout.Button("Level down"))
            {
                playerManager.levelDown = true;
            }
            else
            {
                playerManager.levelDown = false;
            }
                GUI.DragWindow();
        }
    }
}