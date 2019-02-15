using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ValueInterface)), CanEditMultipleObjects]
public class ValueInterfaceEditor : Editor
{
    public SerializedProperty
        velocityZBase,
        velocityZMax,
        velocityZIncreamentAmount,
        player;

    void OnEnable()
    {
        // Setup the SerializedProperties
        velocityZBase = serializedObject.FindProperty("velocityZBase");
        velocityZMax = serializedObject.FindProperty("velocityZMax");
        velocityZIncreamentAmount = serializedObject.FindProperty("velocityZIncreamentAmount");
        player = serializedObject.FindProperty("player");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(velocityZBase);
        EditorGUILayout.PropertyField(velocityZMax);
        EditorGUILayout.PropertyField(velocityZIncreamentAmount);
        EditorGUILayout.PropertyField(player);
        serializedObject.ApplyModifiedProperties();
        ValueInterface valueInterface = (ValueInterface)target;
        if (GUILayout.Button("Değerleri Onayla"))
        {
            valueInterface.SetValues();
        }
    }
}
