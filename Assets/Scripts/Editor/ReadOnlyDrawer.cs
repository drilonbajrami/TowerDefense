using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            => EditorGUI.LabelField(position, label, new GUIContent(property.intValue.ToString()));
    }
}