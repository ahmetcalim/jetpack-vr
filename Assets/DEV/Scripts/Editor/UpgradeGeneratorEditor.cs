using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UpgradeGenerator)), CanEditMultipleObjects]
public class UpgradeGeneratorEditor : Editor
{
    public SerializedProperty
        upgradeTitle,
        upgradeFeaturePowerup,
        upgradeFeatureMovement,
        movement_x,
        movement_y,
        movement_z,
        phase_time,
        bullettime_time,
        rocket_area_lenght,
        upgradeButton,
        upgradeButtonParent,
        cost,
        index;

    void OnEnable()
    {
        
           // Setup the SerializedProperties
        upgradeFeaturePowerup = serializedObject.FindProperty("upgradeFeaturePowerup");
        upgradeFeatureMovement = serializedObject.FindProperty("upgradeFeatureMovement");
        upgradeTitle = serializedObject.FindProperty("upgradeTitle");
        movement_x = serializedObject.FindProperty("movement_x");
        movement_y = serializedObject.FindProperty("movement_y");
        movement_z = serializedObject.FindProperty("movement_z");
        phase_time = serializedObject.FindProperty("phase_time");
        bullettime_time = serializedObject.FindProperty("bullettime_time");
        rocket_area_lenght = serializedObject.FindProperty("rocket_area_lenght");
        upgradeButton = serializedObject.FindProperty("upgradeButton");
        upgradeButtonParent = serializedObject.FindProperty("upgradeButtonParent");
        cost = serializedObject.FindProperty("cost");
        index = serializedObject.FindProperty("index");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(upgradeTitle);

        UpgradeGenerator.UpgradeTitle title = (UpgradeGenerator.UpgradeTitle)upgradeTitle.enumValueIndex;

        switch (title)
        {
            case UpgradeGenerator.UpgradeTitle.POWERUP:
                EditorGUILayout.PropertyField(upgradeFeaturePowerup);
                UpgradeGenerator.UpgradeFeaturePowerup powerUpType = (UpgradeGenerator.UpgradeFeaturePowerup)upgradeFeaturePowerup.enumValueIndex;
                switch (powerUpType)
                {
                    case UpgradeGenerator.UpgradeFeaturePowerup.PHASE:
                        EditorGUILayout.PropertyField(phase_time, new GUIContent("Phase During Time"));
                        break;
                    case UpgradeGenerator.UpgradeFeaturePowerup.ROCKET:
                        EditorGUILayout.PropertyField(rocket_area_lenght, new GUIContent("Rocket Square Size"));
                        break;
                    case UpgradeGenerator.UpgradeFeaturePowerup.BULLET_TIME:
                        EditorGUILayout.PropertyField(bullettime_time, new GUIContent("Bullet Time During Time"));
                        break;
                    default:
                        break;
                }
                break;
            case UpgradeGenerator.UpgradeTitle.MOVEMENT:
                EditorGUILayout.PropertyField(upgradeFeatureMovement);
                UpgradeGenerator.UpgradeFeatureMovement movementType = (UpgradeGenerator.UpgradeFeatureMovement)upgradeFeatureMovement.enumValueIndex;
                switch (movementType)
                {
                    case UpgradeGenerator.UpgradeFeatureMovement.MOVEMENT_X:
                        EditorGUILayout.PropertyField(movement_x, new GUIContent("Movement Speed X"));
                        break;
                    case UpgradeGenerator.UpgradeFeatureMovement.MOVEMENT_Y:
                        EditorGUILayout.PropertyField(movement_y, new GUIContent("Movement Speed Y"));
                        break;
                    case UpgradeGenerator.UpgradeFeatureMovement.MOVEMENT_Z:
                        EditorGUILayout.PropertyField(movement_z, new GUIContent("Movement Speed Z"));
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        EditorGUILayout.PropertyField(cost);
        EditorGUILayout.PropertyField(upgradeButton);
        EditorGUILayout.PropertyField(upgradeButtonParent);
        EditorGUILayout.PropertyField(index);
        serializedObject.ApplyModifiedProperties();
        
        UpgradeGenerator upgradeGenerator = (UpgradeGenerator)target;
        if (GUILayout.Button("Create Upgrade Button"))
        {
            upgradeGenerator.CreateUpgrade();
        }
      
    }

}
