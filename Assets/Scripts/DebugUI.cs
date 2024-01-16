using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class DebugUI : MonoBehaviour
{
    private PlayerDebugDrawingManager PDDM;
    private bool menuOpen;
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
            GUILayout.BeginArea(new Rect(0, 0, 300, 300));
            GUILayout.BeginVertical("Debug Menu", GUI.skin.box);
            GUILayout.Space(10);
            GUILayout.Label("Player Attack:");
            PDDM.raycastDrawEnabled = GUILayout.Toggle(PDDM.raycastDrawEnabled, "Toggle raycast drawing");
            PDDM.raycastPrintEnabled = GUILayout.Toggle(PDDM.raycastPrintEnabled, "Toggle raycast printing");
            GUILayout.Label("Player Movement:");
            PDDM.movementPrintEnabled = GUILayout.Toggle(PDDM.movementPrintEnabled, "Toggle movement printing");
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
