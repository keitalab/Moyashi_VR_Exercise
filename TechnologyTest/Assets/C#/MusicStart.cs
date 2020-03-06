using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject head; /// <summary>
                                              /// CenterEyeAnchor
                                              /// </summary>
    [SerializeField] private GameObject leftHand;/// <summary>
                                                 /// LeftHandAnchor
                                                 /// </summary>
    [SerializeField] private GameObject rightHand;/// <summary>
                                                  /// RightHandAnchor
                                                  /// </summary>

    private void Start()
    {
        SaveCsvFile.Restart();
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One)) //A
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (OVRInput.Get(OVRInput.Button.Two)) //B
        {
            audioSource.Stop();
            SaveCsvFile.Restart();
        }

        if (OVRInput.Get(OVRInput.Button.Three)) //X
        {
            SaveCsvFile.Write();
        }
        
    }

    private void FixedUpdate()
    {
        if (audioSource.isPlaying)
        {
            SaveCsvFile.SaveTransform(head, leftHand, rightHand);
        }
    }
}
