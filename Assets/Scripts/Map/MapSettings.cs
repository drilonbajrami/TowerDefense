using UnityEngine;

namespace TowerDefense.Map
{
    /// <summary>
    /// Map settings.
    /// </summary>
    [System.Serializable]
    public class MapSettings : IMapSettings
    {
        [field: SerializeField, Range(7, 31)] public int Width { get; private set; }
        [field: SerializeField, Range(7, 31)] public int Height { get; private set; }
        [field: SerializeField] public float TileSize { get; private set; }

        public void ValidateSettings()
        {
            Width = Width % 2 == 0 ? Width + 1 : Width;
            Height = Height % 2 == 0 ? Height + 1 : Height;
            TileSize = Mathf.Clamp(TileSize, 1f, TileSize);
        }
    }
}