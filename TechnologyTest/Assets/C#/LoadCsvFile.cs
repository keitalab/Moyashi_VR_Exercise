using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LoadCsvFile : MonoBehaviour
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
        headTransform = Load(MultiPathCombine.Combine(Application.dataPath, "C#", "Head.csv"));
        leftHandTransform = Load(MultiPathCombine.Combine(Application.dataPath, "C#", "LeftHand.csv"));
        rightHandTransform = Load(MultiPathCombine.Combine(Application.dataPath, "C#", "RightHand.csv"));
        progress = 0;
    }

    private List<List<float>> Load(string path)
    {
        var rowData = LoadCsvData(path);

        var returnData = new List<List<float>>();

        foreach (var data in rowData)
        {
            if (data == "")
            {
                continue;
            }

            var oneData = data.Split(',');

            var lineData = new List<float>()
            {
                float.Parse(oneData[0]), float.Parse(oneData[1]), float.Parse(oneData[2]), float.Parse(oneData[3]),
                float.Parse(oneData[4]), float.Parse(oneData[5])
            };
            returnData.Add(lineData);
        }

        return returnData;
    }
    
    private string[] LoadCsvData(string path)
    {
        var fileInfo = new FileInfo(path);

        string readText = "";
        
        try {
            using (StreamReader sr = new StreamReader(fileInfo.OpenRead(), Encoding.UTF8)) {
                readText = sr.ReadToEnd();
            }
        } catch (Exception e) {
            Debug.Log(e);
        }
        
        var rowData = readText.Split('\n');
        return rowData;
    }
    
    private void FixedUpdate()
    {
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
