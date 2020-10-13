using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveCsvFile
{
    
    public static void WritePlayerTransformCsvData(string path, List<List<float>> target)
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

    public static void WritePreExperimentCsvData(string path, string fileName, List<Experiment.TimeAndDistance> timeAndDistanceList)
    {
        string filePath = MultiPathCombine.Combine(path, fileName + ".csv");

        StreamWriter streamWriter = File.CreateText(filePath);
        streamWriter.Close();
        
        var fileInfo = new FileInfo(filePath);
        using (StreamWriter sw = fileInfo.CreateText())
        {
            string saveText = "";

            foreach (var timeAndDistance in timeAndDistanceList)
            {
                saveText += timeAndDistance.GetTime.ToString() + ',' + timeAndDistance.GetDistance.ToString();
                
                saveText += '\n';
            }

            //saveText = saveText.TrimEnd(',');
            sw.Write(saveText);
        }
    }
}
