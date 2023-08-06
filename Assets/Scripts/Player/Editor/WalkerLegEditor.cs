using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WalkerLegEditor : Editor
{

    private WalkerLeg c;

    public void OnSceneGUI()
    {
        c = this.target as WalkerLeg;
        Handles.color = Color.red;
        Handles.DrawWireDisc(c.transform.position, Vector3.up, float.Parse(serializedObject.FindProperty("_stepZone").stringValue));
    }
}
