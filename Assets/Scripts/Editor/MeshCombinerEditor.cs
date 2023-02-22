using TowerDefense.Map;
using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    [CustomEditor(typeof(MeshCombiner))]
    public class MeshCombinerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Combine"))
                ((MeshCombiner)target).CombineMeshes(false);
        }
    }
}
