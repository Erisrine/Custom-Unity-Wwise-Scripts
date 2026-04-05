using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomRoom))]
public class CustomRoomInspector : AkRoomInspector
{
    SerializedProperty roomSwitchProp;

    public new void OnEnable()
    {
        base.OnEnable();
        roomSwitchProp = serializedObject.FindProperty("roomSwitch");
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        // Add a header label
        EditorGUILayout.Space(); // optional spacing
        EditorGUILayout.LabelField("Custom Room Settings", EditorStyles.boldLabel);

        // Draw a boxed area for your property
        //TODO This shit fucking sucks
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.PropertyField(roomSwitchProp, new GUIContent("Room Switch"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}