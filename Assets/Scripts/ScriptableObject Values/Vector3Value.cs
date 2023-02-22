using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// A simple Vector3 value that extends the ISerializationCallbackReceiver
    /// interface, the OnAfterDeserialize method ensures the value object always
    /// has the initial value when you run the game in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "Vector3Value", menuName = "Values/Vector3Value")]
    public class Vector3Value : ScriptableObject, ISerializationCallbackReceiver
    {
        public Vector3 initialValue;
        public Vector3 runtimeValue;
        public void OnAfterDeserialize() => runtimeValue = initialValue;
        public void OnBeforeSerialize() { }
    }
}