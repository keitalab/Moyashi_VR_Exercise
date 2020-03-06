﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveCsvFile
{
    private static List<List<float>> headTransform = new List<List<float>>();
    private static List<List<float>> leftHandTransform = new List<List<float>>();
    private static List<List<float>> rightHandTransform = new List<List<float>>();
    
    private static void WriteCsvData(string path, List<List<float>> target)
    {
        var fileInfo = new FileInfo(path);
        using (StreamWriter sw = fileInfo.CreateText())
        {
            string saveText = "";
            foreach (var oneLine in target)
            {
                foreach (var one in oneLine)
                {
                    saveText += one.ToString() + ',';
                }

                saveText += '\n';
            }
            sw.Write(saveText);
        }
    }

    public static void Restart()
    {
        headTransform = new List<List<float>>();
        leftHandTransform = new List<List<float>>();
        rightHandTransform = new List<List<float>>();
    }

    public static void SaveTransform(GameObject head, GameObject leftHand, GameObject rightHand)
    {
        headTransform.Add(new List<float>()
        {
            head.transform.position.x, head.transform.position.y,
            head.transform.position.z, head.transform.localEulerAngles.x, head.transform.localEulerAngles.y,
            head.transform.localEulerAngles.z
        });
        leftHandTransform.Add(new List<float>()
        {
            leftHand.transform.position.x, leftHand.transform.position.y,
            leftHand.transform.position.z, leftHand.transform.localEulerAngles.x, leftHand.transform.localEulerAngles.y,
            leftHand.transform.localEulerAngles.z
        });
        rightHandTransform.Add(new List<float>()
        {
            rightHand.transform.position.x, rightHand.transform.position.y,
            rightHand.transform.position.z, rightHand.transform.localEulerAngles.x, rightHand.transform.localEulerAngles.y,
            rightHand.transform.localEulerAngles.z
        });
    }

    public static void Write()
    {
        WriteCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "Head.csv"),  headTransform);
        WriteCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "LeftHand.csv"), leftHandTransform);
        WriteCsvData(MultiPathCombine.Combine(Application.dataPath, "C#", "RightHand.csv"), rightHandTransform);
    }
}
