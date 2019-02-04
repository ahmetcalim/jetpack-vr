using UnityEngine;
using System.Collections;
using UnityEditor;

public class ValueInterface : EditorWindow
{
    [MenuItem("Window/ValueInterface")]
    public static void ShowWindow()
    {
        GetWindow<ValueInterface>("Value Interface");
    }
    private void OnGUI()
    {

        Player player = FindObjectOfType<Player>();
        Player.resourceMultipleValue = EditorGUILayout.FloatField("Resource Multiple Value", Player.resourceMultipleValue);

        PlayerMovementController.constant1 = EditorGUILayout.FloatField("Constant 1", PlayerMovementController.constant1);
        PlayerMovementController.constant2 = EditorGUILayout.FloatField("Constant 2", PlayerMovementController.constant2);
        PlayerMovementController.constant3 = EditorGUILayout.FloatField("Constant 3", PlayerMovementController.constant3);
        Physics.gravity = EditorGUILayout.Vector3Field("Gravity", Physics.gravity);
       
       

    }
}

