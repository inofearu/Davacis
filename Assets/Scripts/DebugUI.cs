using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class DebugUI : MonoBehaviour
{
    private SphereCastVisualiser SCV;
    private SphereOverlapVisualiser SOV;
    private PlayerMovementVisualiser PMV;
    private bool menuOpen;
    [UsedImplicitly]
    private void Awake()
    {
        SCV = GetComponent<SphereCastVisualiser>();
        SOV = GetComponent<SphereOverlapVisualiser>();
        //PMV = GetComponent<PlayerMovementVisualiser>();
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
    void OnGUI()
    {
        if (menuOpen)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 300));
            GUILayout.BeginVertical("Debug Menu", GUI.skin.box);
            GUILayout.Toggle(SOV.enabled, "Toggle SOV drawing");
            GUILayout.Toggle(SCV.enabled, "Toggle SCV drawing");
            GUILayout.EndVertical();
            GUILayout.EndArea();
            //GUILayout.Toggle(PMV.enabled, "Toggle PMV");
        }
    }
}
