using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibrary : MonoBehaviour
{
    public static Material Grass;
    public static Material Path;

    public static Material Selected;
    public static Material Hover;

    void Awake()
    {
        Grass = Resources.Load<Material>("Materials/CellMaterials/Grass");
        Path = Resources.Load<Material>("Materials/CellMaterials/Path");

        Selected = Resources.Load<Material>("Materials/CellInteractionMaterials/Selected");
        Hover = Resources.Load<Material>("Materials/CellInteractionMaterials/Hover");
    }
}
