using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVisibility : MonoBehaviour
{
    private void LateUpdate()
    {
        ViewObstructed();
    }


    public List<MeshRenderer> meshes = new List<MeshRenderer>();
    void ViewObstructed()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 4f);
        if(hits.Length > 0)
        {
            foreach(var hit in hits)
            {
                if (hit.collider.GetComponent<MeshRenderer>())
                {
                    if (!meshes.Contains(hit.collider.GetComponent<MeshRenderer>()))
                    {
                        meshes.Add(hit.collider.GetComponent<MeshRenderer>());
                        Color nC = meshes[meshes.Count - 1].material.color;
                        nC.a = 0.1f;
                        MaterialPropertyBlock block = new MaterialPropertyBlock();
                        block.SetColor("_Color", nC);
                        meshes[meshes.Count - 1].SetPropertyBlock(block);
                    }
                }
            }
        }
        else
        {
            if (meshes.Count > 0)
            {
                foreach (var mesh in meshes)
                {
                    mesh.SetPropertyBlock(new MaterialPropertyBlock());
                }
                meshes.Clear();
            }
        }
    }
}
