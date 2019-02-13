using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectBuilder))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectBuilder myScript = (ObjectBuilder)target;
        if (GUILayout.Button("Set Upgrade Title"))
        {
            myScript.AddUpgradeTitle();
            GUILayout.Space(100f);
        }

    }

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        base.OnInteractivePreviewGUI(r, background);
    }
}