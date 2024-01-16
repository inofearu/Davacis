using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class DebugUI : MonoBehaviour
{
    private PlayerDebugDrawingManager PDDM;
    private bool menuOpen;
    private int windowId = 0000;
    private Rect windowRect = new Rect(20, 20, 600, 600);
    [UsedImplicitly]
    private void Awake()
    {
        PDDM = GetComponent<PlayerDebugDrawingManager>();
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
        PDDM.raycastDrawEnabled = GUILayout.Toggle(PDDM.raycastDrawEnabled, "Toggle raycast drawing");
        PDDM.raycastPrintEnabled = GUILayout.Toggle(PDDM.raycastPrintEnabled, "Toggle raycast printing");
        GUILayout.Label("Player Movement:");
        PDDM.movementPrintEnabled = GUILayout.Toggle(PDDM.movementPrintEnabled, "Toggle movement printing");
        GUI.DragWindow();
    }
}