using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileTexture : MonoBehaviour
{
    public float scaleFactor;
    public bool update;

    void Update()
    {

        if (update && Application.isEditor && !Application.isPlaying)
        {
            update = false;
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x / scaleFactor, transform.localScale.z / scaleFactor);
            transform.hasChanged = false;
        }

    }
}
