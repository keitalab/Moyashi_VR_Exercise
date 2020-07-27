using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class LoadCsvFile
{
    public static List<List<float>> Load(string path)
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
    
    private static string[] LoadCsvData(string path)
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
}
