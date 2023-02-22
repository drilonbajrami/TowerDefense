using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Map
{
    // Taken from Unity forum https://answers.unity.com/questions/196649/combinemeshes-with-different-materials.html
    // from user (Bunzanga) answer.

    /// <summary>
    /// Combines all children game object's mesh components into a single one.
    /// </summary>
    public class MeshCombiner : MonoBehaviour
    {
        /// <summary>
        /// Combines all children meshes by material type.
        /// </summary>
        public void CombineMeshes(bool destroyChildrenGameObjects)
        {
            List<Material> materials = new();

            // Grouped combine instances into different lists for every material found.
            List<List<CombineInstance>> combineInstanceList = new();

            // Actual mesh filter components (data)
            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

            // Go through each mesh filter from meshFilters
            foreach (MeshFilter meshFilter in meshFilters) {

                // Get the mesh renderer component.
                MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

                // Skip if meshRenderer is null or meshFileter's shared mesh.
                if (!meshRenderer ||
                    !meshFilter.sharedMesh ||
                    meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount) {
                    continue;
                }

                // Go through each subMesh of the current mesh filter.
                for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++) {

                    // Get the index of the material in the material's list. (-1) if not found yet.
                    int materialIndex = Contains(materials, meshRenderer.sharedMaterials[s].name);
                    if (materialIndex == -1) {
                        materials.Add(meshRenderer.sharedMaterials[s]);
                        materialIndex = materials.Count - 1;
                    }

                    combineInstanceList.Add(new List<CombineInstance>());

                    CombineInstance combineInstance = new() {
                        transform = meshRenderer.transform.localToWorldMatrix,
                        subMeshIndex = s,
                        mesh = meshFilter.sharedMesh
                    };

                    // Add combine instance to the list of combine instances with the same material.
                    combineInstanceList[materialIndex].Add(combineInstance);
                }
            }

            // Get or create mesh filter, renderer and collider.
            if (!gameObject.TryGetComponent<MeshFilter>(out var meshFilterCombine))
                meshFilterCombine = gameObject.AddComponent<MeshFilter>();
            if (!gameObject.TryGetComponent<MeshRenderer>(out var meshRendererCombine))
                meshRendererCombine = gameObject.AddComponent<MeshRenderer>();
            if (!gameObject.TryGetComponent<MeshCollider>(out var meshColliderCombine))
                meshColliderCombine = gameObject.AddComponent<MeshCollider>();

            // Combine by material index into per-material meshes
            // also, Create CombineInstance array for next step
            Mesh[] meshes = new Mesh[materials.Count];
            CombineInstance[] combineInstances = new CombineInstance[materials.Count];

            for (int m = 0; m < materials.Count; m++) {
                CombineInstance[] combineInstanceArray = combineInstanceList[m].ToArray();
                meshes[m] = new Mesh();
                meshes[m].CombineMeshes(combineInstanceArray, true, true);

                combineInstances[m] = new CombineInstance {
                    mesh = meshes[m],
                    subMeshIndex = 0
                };
            }

            // Combine into one
            meshFilterCombine.sharedMesh = new Mesh() { name = "Mesh" };
            meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);
            meshColliderCombine.sharedMesh = meshFilterCombine.sharedMesh;

            // Destroy other meshes
            foreach (Mesh oldMesh in meshes) {
                oldMesh.Clear();
                DestroyImmediate(oldMesh);
            }

            // Assign materials
            Material[] materialsArray = materials.ToArray();
            meshRendererCombine.materials = materialsArray;

            if (destroyChildrenGameObjects)
                foreach (MeshFilter meshFilter in meshFilters)
                    DestroyImmediate(meshFilter.gameObject);
        }

        /// <summary>
        /// Returns the index of the material in the list of materials, if found.
        /// </summary>
        /// <param name="searchList">The list of materials to search in.</param>
        /// <param name="searchName">The name of the material to search for.</param>
        /// <returns>The index of the material if found in in the given list of materials, otherwise return -1.</returns>
        private int Contains(List<Material> searchList, string searchName)
        {
            for (int i = 0; i < searchList.Count; i++)
                if (searchList[i].name == searchName)
                    return i;

            return -1;
        }
    }
}