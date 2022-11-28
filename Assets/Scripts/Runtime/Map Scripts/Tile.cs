using System.Text;
using UnityEngine;

namespace TowerDefense.Runtime
{
    public class Tile : MonoBehaviour
    {
        public Renderer renderer;

        public void SetColor(int i)
        {
            if (i == 0) renderer.material.color = Color.green;
            if (i == 1) renderer.material.color = Color.yellow;
            if (i == 2) renderer.material.color = Color.white;
            if (i == 3) renderer.material.color = Color.blue;
        }
    }
}