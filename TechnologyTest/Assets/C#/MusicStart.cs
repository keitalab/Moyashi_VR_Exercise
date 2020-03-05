using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One)) //A
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Pause();
            }
        }

        if (OVRInput.Get(OVRInput.Button.Two)) //B
        {
            audioSource.Stop();
        }
        
    }
}
