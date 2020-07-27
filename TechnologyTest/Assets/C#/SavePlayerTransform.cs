using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerTransform : MonoBehaviour
{
    [SerializeField] private GameObject head; /// <summary>
    /// CenterEyeAnchor
    /// </summary>
    [SerializeField] private GameObject leftHand;/// <summary>
    /// LeftHandAnchor
    /// </summary>
    [SerializeField] private GameObject rightHand;/// <summary>
    /// RightHandAnchor
    /// </summary>
    
    private List<List<float>> headTransform = new List<List<float>>();
    private List<List<float>> leftHandTransform = new List<List<float>>();
    private List<List<float>> rightHandTransform = new List<List<float>>();

    // Update is called once per frame
    void FixedUpdate()
    {
        SaveTransform();
        if (OVRInput.Get(OVRInput.Button.Two)) //B
        {
            SaveCsvFile.WritePlayerTransformCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "Head.csv"), headTransform);
            SaveCsvFile.WritePlayerTransformCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "LeftHand.csv"), leftHandTransform);
            SaveCsvFile.WritePlayerTransformCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "RightHand.csv"), rightHandTransform);
        }
    }
    
    private void SaveTransform()
    {
        headTransform.Add(new List<float>()
        {
            head.transform.localPosition.x, head.transform.localPosition.y,
            head.transform.localPosition.z, head.transform.localEulerAngles.x, head.transform.localEulerAngles.y,
            head.transform.localEulerAngles.z
        });
        leftHandTransform.Add(new List<float>()
        {
            leftHand.transform.localPosition.x, leftHand.transform.localPosition.y,
            leftHand.transform.localPosition.z, leftHand.transform.localEulerAngles.x, leftHand.transform.localEulerAngles.y,
            leftHand.transform.localEulerAngles.z
        });
        rightHandTransform.Add(new List<float>()
        {
            rightHand.transform.localPosition.x, rightHand.transform.localPosition.y,
            rightHand.transform.localPosition.z, rightHand.transform.localEulerAngles.x, rightHand.transform.localEulerAngles.y,
            rightHand.transform.localEulerAngles.z
        });
    }
}
