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
            menuOpen = true;
        }
        
    }
    [UsedImplicitly]
    void OnGUI()
    {
        if (menuOpen)
        {
            GUI.Box(new Rect(10, 10, 100, 90), "Debug Menu");
            GUILayout.Toggle(SOV.enabled, "Toggle SOV");
            GUILayout.Toggle(SCV.enabled, "Toggle SCV");
            //GUILayout.Toggle(PMV.enabled, "Toggle PMV");
        }
    }
}
