using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstractorMove : MonoBehaviour
{
    private int progress;
    
    private List<List<float>> headTransform;
    private List<List<float>> leftHandTransform;
    private List<List<float>> rightHandTransform;
    
    [SerializeField] private GameObject head; /// <summary>
    /// CenterEyeAnchor
    /// </summary>
    [SerializeField] private GameObject leftHand;/// <summary>
    /// LeftHandAnchor
    /// </summary>
    [SerializeField] private GameObject rightHand;/// <summary>
    /// RightHandAnchor
    /// </summary>
    private void Awake()
    {
        headTransform = LoadCsvFile.Load(MultiPathCombine.Combine(Application.dataPath, "C#", "Head.csv"));
        leftHandTransform = LoadCsvFile.Load(MultiPathCombine.Combine(Application.dataPath, "C#", "LeftHand.csv"));
        rightHandTransform = LoadCsvFile.Load(MultiPathCombine.Combine(Application.dataPath, "C#", "RightHand.csv"));
        progress = 0;
    }
    
    private void FixedUpdate()
    {
        if (progress >= headTransform.Count - 1)
        {
            return;
        }
        
        int yRotation = 0;
        head.transform.localPosition = new Vector3(headTransform[progress][0], headTransform[progress][1], headTransform[progress][2]);
        head.transform.localEulerAngles = new Vector3(headTransform[progress][3], headTransform[progress][4] + yRotation, headTransform[progress][5]);
        leftHand.transform.localPosition = new Vector3((leftHandTransform[progress][0]), leftHandTransform[progress][1], leftHandTransform[progress][2]);
        leftHand.transform.localEulerAngles = new Vector3(leftHandTransform[progress][3], leftHandTransform[progress][4] + yRotation, leftHandTransform[progress][5]);
        rightHand.transform.localPosition = new Vector3((rightHandTransform[progress][0]), rightHandTransform[progress][1], rightHandTransform[progress][2]);
        rightHand.transform.localEulerAngles = new Vector3(rightHandTransform[progress][3], rightHandTransform[progress][4] + yRotation, rightHandTransform[progress][5]);
        progress++;
    }
}
