using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// A simple float value that extends the ISerializationCallbackReceiver
    /// interface, the OnAfterDeserialize method ensures the value object always
    /// has the initial value when you run the game in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "FloatValue", menuName = "Values/FloatValue")]
    public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public int initialValue;
        public int runtimeValue;
        public void OnAfterDeserialize() => runtimeValue = initialValue;
        public void OnBeforeSerialize() { }
    }
}
