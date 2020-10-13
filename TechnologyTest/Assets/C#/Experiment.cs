using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Experiment : SingletonMonoBehaviour<Experiment>
{
    private const string aButtonClick = "\n右手のAボタンクリックで進む";
    private const string xButtonClick = "\n左手のXボタンクリックで進む";
    [SerializeField] private GameObject handL_end;
    [SerializeField] private GameObject handR_end;

    private List<String> beforeRightPreTaskTextList = new List<string>()
    {
        "このタスクでは、黒いキューブと赤いキューブが出現します" + aButtonClick,
        "本実験では、右手と左手に分けてタスクを行います" + aButtonClick,
        "本実験では、黒いキューブを触れてから赤いキューブを触れるまでの時間を計測します" + aButtonClick,
        "まず、黒いキューブに右手を合わせ、右手のトリガーを引いてください" + aButtonClick,
        "次に、赤いキューブが出現するので、赤いキューブに右手を合わせ、右手のトリガーを引いてください" + aButtonClick,
        "右手が赤いキューブ内にある際には、赤いキューブの色が半透明になります" + aButtonClick,
        "それでは、1度右手での練習タスクを試行します" + aButtonClick,
    };
    
    private List<String> beforeRightRealTaskTextList = new List<string>()
    {
        "本タスクでは、この練習タスクを10回連続で施行されます。" + aButtonClick,
        "それでは、右手での本タスクを試行します。" + aButtonClick,
    };
    
    private List<String> beforeLeftPreTaskTextList = new List<string>()
    {
        "続いて、左手で同様のタスクを行います。" + xButtonClick,
        "まず、黒いキューブに左手を合わせ、左手のトリガーを引いてください" + xButtonClick,
        "次に、赤いキューブが出現するので、赤いキューブに右手を合わせ、左手のトリガーを引いてください。" + xButtonClick,
        "左手が赤いキューブ内にある際には、赤いキューブの色が半透明になります。" + xButtonClick,
        "それでは、1度左手での練習タスクを試行します。" + xButtonClick,
    };
    
    private List<String> beforeLeftRealTaskTextList = new List<string>()
    {
        "本タスクでは、この練習タスクを10回連続で施行されます。" + xButtonClick,
        "それでは、左手での本タスクを試行します。" + xButtonClick,
    };
    
    private List<String> afterExperimentTextList = new List<string>()
    {
        "実験は以上になります。ありがとうございました。" + xButtonClick,
    };

    private TaskStatus nowTaskStatus;

    public Experiment.TaskStatus GetTaskStatus
    {
        get { return nowTaskStatus; }
    }

    private Vector3 resetCubePosition;

    private float timer;
    private bool isTimerMoving = false;
    private List<TimeAndDistance> timeAndDistanceList;

    public class TimeAndDistance
    {
        public TimeAndDistance(float time, float distance)
        {
            this.time = time;
            this.distance = distance;
        }
        
        private float time;
        public float GetTime
        {
            get
            {
                return time;
            }
        }
        private float distance;

        public float GetDistance
        {
            get { return distance; }
        }
    }

    private const int AmountTaskCount = 10;
    private int clearTaskCount;
    private int readExplanationCount;
    public GameObject taskCube;
    [SerializeField] private GameObject resetCube;
    [SerializeField] private Text explanationText;
    private IEnumerator activeTaskCoroutine;

    private void Start()
    {
        clearTaskCount = 0;
        readExplanationCount = 0;
        explanationText.text = beforeRightPreTaskTextList[readExplanationCount];
        //nowTaskStatus = TaskStatus.BeforeExperiment;
        nowTaskStatus = TaskStatus.RightPreDescription;
        resetCubePosition = new Vector3(this.gameObject.transform.position.x + 0.5f,
            this.gameObject.transform.position.y + 1f, this.gameObject.transform.position.z);
    }

    private void Update()
    {
        if ((nowTaskStatus == TaskStatus.LeftRealTasking || nowTaskStatus == TaskStatus.RightRealTasking) && isTimerMoving)
        {
            timer += Time.deltaTime;
            return;
        }
        
        if (nowTaskStatus == TaskStatus.RightPreDescription)
        {
            if ((OVRInput.GetDown(OVRInput.Button.One)))
            {
                if (readExplanationCount < beforeRightPreTaskTextList.Count - 1)
                {
                    readExplanationCount++;
                    explanationText.text = beforeRightPreTaskTextList[readExplanationCount];
                }
                else if (readExplanationCount == beforeRightPreTaskTextList.Count - 1)
                {
                    explanationText.text = "";
                    StartTask();
                    nowTaskStatus = TaskStatus.RightPreTasking;
                }
            }
        }
        else if (nowTaskStatus == TaskStatus.RightRealDescription)
        {
            if ((OVRInput.GetDown(OVRInput.Button.One)))
            {
                if (readExplanationCount < beforeRightRealTaskTextList.Count - 1)
                {
                    readExplanationCount++;
                    explanationText.text = beforeRightRealTaskTextList[readExplanationCount];
                }
                else if (readExplanationCount == beforeRightRealTaskTextList.Count - 1)
                {
                    explanationText.text = "";
                    StartTask();
                    nowTaskStatus = TaskStatus.RightRealTasking;
                }
            }
        }
        else if (nowTaskStatus == TaskStatus.LeftPreDescription)
        {
            if ((OVRInput.GetDown(OVRInput.Button.Three)))
            {
                if (readExplanationCount < beforeLeftPreTaskTextList.Count - 1)
                {
                    readExplanationCount++;
                    explanationText.text = beforeLeftPreTaskTextList[readExplanationCount];
                }
                else if (readExplanationCount == beforeLeftPreTaskTextList.Count - 1)
                {
                    explanationText.text = "";
                    StartTask();
                    nowTaskStatus = TaskStatus.LeftPreTasking;
                }
            }
        }
        else if (nowTaskStatus == TaskStatus.LeftRealDescription)
        {
            if ((OVRInput.GetDown(OVRInput.Button.Three)))
            {
                if (readExplanationCount < beforeLeftRealTaskTextList.Count - 1)
                {
                    readExplanationCount++;
                    explanationText.text = beforeLeftRealTaskTextList[readExplanationCount];
                }
                else if (readExplanationCount == beforeLeftRealTaskTextList.Count - 1)
                {
                    explanationText.text = "";
                    StartTask();
                    nowTaskStatus = TaskStatus.LeftRealTasking;
                }
            }
        }
    }

    private void StartTask()
    {
        timer = 0;
        clearTaskCount = 0;
        isTimerMoving = false;
        timeAndDistanceList = new List<TimeAndDistance>();

        Instantiate(resetCube, resetCubePosition, Quaternion.identity);
    }

    public void InstantiateTaskCube()
    {
        Vector3 instantiatePosition;
        if (nowTaskStatus == TaskStatus.LeftPreTasking || nowTaskStatus == TaskStatus.LeftRealTasking)
        {
            instantiatePosition = new Vector3(
                Random.Range(0.2f, 0.8f) + this.gameObject.transform.position.x,
                Random.Range(0.5f, 1.2f) + this.gameObject.transform.position.y,
                Random.Range(0f, 0.2f) + this.gameObject.transform.position.z
            );
        }
        else
        {
            instantiatePosition = new Vector3(
                Random.Range(0.2f, 0.8f) + this.gameObject.transform.position.x,
                Random.Range(0.5f, 1.2f) + this.gameObject.transform.position.y,
                Random.Range(-0.2f, 0f) + this.gameObject.transform.position.z
            );
        }

        Instantiate(taskCube, instantiatePosition, Quaternion.identity);
        isTimerMoving = true;
    }

    public void ClearTask(Vector3 taskCubePosition)
    {
        if (nowTaskStatus == TaskStatus.LeftPreTasking)
        {
            nowTaskStatus = TaskStatus.LeftRealDescription;
            isTimerMoving = false;
            readExplanationCount = 0;
            explanationText.text = beforeLeftRealTaskTextList[0];
            return;
        }
        else if (nowTaskStatus == TaskStatus.RightPreTasking)
        {
            nowTaskStatus = TaskStatus.RightRealDescription;
            isTimerMoving = false;
            readExplanationCount = 0;
            explanationText.text = beforeRightRealTaskTextList[readExplanationCount];
            return;
        }
        else
        {
            clearTaskCount++;
            if (clearTaskCount < AmountTaskCount)
            {
                if (nowTaskStatus == TaskStatus.RightRealTasking)
                {
                    timeAndDistanceList.Add(new TimeAndDistance(timer,
                        Vector3.Distance(taskCubePosition, handR_end.transform.position)));
                }
                else if (nowTaskStatus == TaskStatus.LeftRealTasking)
                {
                    timeAndDistanceList.Add(new TimeAndDistance(timer,
                        Vector3.Distance(taskCubePosition, handL_end.transform.position)));
                }
                timer = 0;
                isTimerMoving = false;
                Instantiate(resetCube, resetCubePosition, Quaternion.identity);
            }
            else
            {
                string path = MultiPathCombine.Combine(Application.dataPath, "ExperimentData");
                string dateText = DateTime.Now.Year.ToString() +"_"+ DateTime.Now.Month.ToString() +"_" + DateTime.Now.Day.ToString() +"_"+
                    DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
                SaveCsvFile.WritePreExperimentCsvData(path, nowTaskStatus.ToString() + "_" + dateText, timeAndDistanceList);
                timer = 0f;
                isTimerMoving = false;
                clearTaskCount = 0;
                readExplanationCount = 0;
                if (nowTaskStatus == TaskStatus.RightRealTasking)
                {
                    nowTaskStatus = TaskStatus.LeftPreDescription;
                    explanationText.text = beforeLeftPreTaskTextList[readExplanationCount];
                }
                else if (nowTaskStatus == TaskStatus.LeftRealTasking)
                {
                    nowTaskStatus = TaskStatus.AfterExperiment;
                    explanationText.text = afterExperimentTextList[readExplanationCount];
                }
            }
        }
    }

    public enum TaskStatus
    {
        BeforeExperiment,
        RightPreDescription,
        RightPreTasking,
        RightRealDescription,
        RightRealTasking,
        LeftPreDescription,
        LeftPreTasking,
        LeftRealDescription,
        LeftRealTasking,
        AfterExperiment,
    }
}