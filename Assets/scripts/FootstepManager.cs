using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public List<AudioClip> stpClips;
    public List<string> tags;
    AudioSource footstepSource;

    bool pitch;

    public LayerMask floor;

    private void Start()
    {
        footstepSource = GetComponentInChildren<AudioSource>();
    }

    void CheckFloor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up,out hit, 2, floor);
        if(hit.collider != null && hit.collider.gameObject.GetComponent<MeshRenderer>())
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if(hit.collider.gameObject.CompareTag(tags[i]))
                {                    
                    footstepSource.clip = stpClips[i];
                }
            }
        }
    }

    public void Step()
    {
        CheckFloor();
        footstepSource.pitch = pitch ? 1 : 1.5f;
        footstepSource.Play();
        pitch = !pitch;
    }
}
