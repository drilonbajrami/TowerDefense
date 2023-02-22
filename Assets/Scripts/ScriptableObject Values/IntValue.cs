using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// A simple integer value that extends the ISerializationCallbackReceiver
    /// interface, the OnAfterDeserialize method ensures the value object always
    /// has the initial value when you run the game in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "IntValue", menuName = "Values/IntValue")]
    public class IntValue : ScriptableObject, ISerializationCallbackReceiver
    {
        public int initialValue;
        public int runtimeValue;
        public void OnAfterDeserialize() => runtimeValue = initialValue;
        public void OnBeforeSerialize() { }
    }
}
