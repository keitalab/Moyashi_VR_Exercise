﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Experience : SingletonMonoBehaviour<Experience>
{
    private const string aButtonClick = "\n右手のAボタンクリックで進む";
    private const string xButtonClick = "\n左手のXボタンクリックで進む";

    private List<String> explanationTextList = new List<string>()
    {
        "このタスクでは、黒いキューブと赤いキューブが出現します。" + aButtonClick,
        "本実験では、右手と左手に分けてタスクを行います。" + aButtonClick,
        "本実験では、黒いキューブを触れてから赤いキューブを触れるまでの時間を計測します" + aButtonClick,
        "まず、黒いキューブに右手を合わせ、右手のトリガーを引いてください" + aButtonClick,
        "次に、赤いキューブが出現するので、赤いキューブに右手を合わせ、右手のトリガーを引いてください。" + aButtonClick,
        "右手が赤いキューブ内にある際には、赤いキューブの色が半透明になります。" + aButtonClick,
        "それでは、1度右手での練習タスクを試行します。" + aButtonClick,

        "本タスクでは、この練習タスクを10回連続で施行されます。" + aButtonClick,
        "それでは、右手での本タスクを試行します。" + aButtonClick,

        "続いて、左手で同様のタスクを行います。" + xButtonClick,
        "まず、黒いキューブに左手を合わせ、左手のトリガーを引いてください" + xButtonClick,
        "次に、赤いキューブが出現するので、赤いキューブに右手を合わせ、左手のトリガーを引いてください。" + xButtonClick,
        "左手が赤いキューブ内にある際には、赤いキューブの色が半透明になります。" + xButtonClick,
        "それでは、1度左手での練習タスクを試行します。" + xButtonClick,

        "本タスクでは、この練習タスクを10回連続で施行されます。" + xButtonClick,
        "それでは、左手での本タスクを試行します。" + xButtonClick,

        "実験は以上になります。ありがとうございました。" + xButtonClick,
    };

    private TaskStatus nowTaskStatus;

    public Experience.TaskStatus GetTaskStatus
    {
        get { return nowTaskStatus; }
    }

    private Vector3 resetCubePosition = new Vector3(0.5f, 1f, 0f);

    private float timer;
    private List<float> timeList;

    private const int AmountTaskCount = 10;
    private int clearTaskCount;
    private int readExplanationCount;
    [SerializeField] private GameObject taskCube;
    [SerializeField] private GameObject resetCube;
    [SerializeField] private Text explanationText;
    private IEnumerator activeTaskCoroutine;

    private void Start()
    {
        clearTaskCount = 0;
        readExplanationCount = 0;
        explanationText.text = explanationTextList[0];
    }

    private void Update()
    {
        if (nowTaskStatus != TaskStatus.None)
        {
            timer += Time.deltaTime;
            return;
        }

        if (0 <= readExplanationCount && readExplanationCount <= 5)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                explanationText.text = explanationTextList[readExplanationCount];
            }
        }
        else if (readExplanationCount == 6)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                StartTask();
                nowTaskStatus = TaskStatus.LeftPreTasking;
            }
        }
        else if (readExplanationCount == 7)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                explanationText.text = explanationTextList[readExplanationCount];
            }
        }
        else if (readExplanationCount == 8)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                StartTask();
                nowTaskStatus = TaskStatus.LeftRealTasking;
            }
        }
        else if (9 <= readExplanationCount && readExplanationCount <= 12)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                explanationText.text = explanationTextList[readExplanationCount];
            }
        }
        else if (readExplanationCount == 13)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                StartTask();
                nowTaskStatus = TaskStatus.RightPreTasking;
            }
        }
        else if (readExplanationCount == 14)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                explanationText.text = explanationTextList[readExplanationCount];
            }
        }
        else if (readExplanationCount == 15)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                StartTask();
                nowTaskStatus = TaskStatus.RightRealTasking;
            }
        }
        else if (readExplanationCount == 16)
        {
            if ((OVRInput.Get(OVRInput.Button.One)))
            {
                readExplanationCount++;
                explanationText.text = explanationTextList[readExplanationCount];
            }
        }
    }

    private void StartTask()
    {
        timer = 0;
        timeList = new List<float>();

        Instantiate(resetCube, resetCubePosition, Quaternion.identity);
    }

    public void ClearTask()
    {
        if (nowTaskStatus == TaskStatus.LeftPreTasking || nowTaskStatus == TaskStatus.RightPreTasking)
        {
            nowTaskStatus = TaskStatus.None;
            return;
        }
        else
        {
            if (clearTaskCount < AmountTaskCount)
            {
                timeList.Add(timer);
                timer = 0;
                Instantiate(resetCube, resetCubePosition, Quaternion.identity);
                clearTaskCount++;
            }
            else
            {
                if (nowTaskStatus == TaskStatus.LeftRealTasking)
                {
                    string path = MultiPathCombine.Combine(Application.dataPath, "Experience");
                    SaveCsvFile.WriteExperienceCsvData(path, nowTaskStatus.ToString() + DateTime.Now.ToString(), timeList);
                    nowTaskStatus = TaskStatus.None;
                }
            }
        }
    }

    private IEnumerator Task()
    {
        Vector3 instantiatePosition = new Vector3(
            Random.Range(0f, 0.5f),
            Random.Range(0f, 1f),
            Random.Range(-0.5f, 0.5f)
        );
        Quaternion instantiateQuaternion = Quaternion.identity;
        Instantiate(taskObject, instantiatePosition, instantiateQuaternion);

        while (true)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public enum TaskStatus
    {
        LeftPreTasking,
        LeftRealTasking,
        RightPreTasking,
        RightRealTasking,
        None
    }
}