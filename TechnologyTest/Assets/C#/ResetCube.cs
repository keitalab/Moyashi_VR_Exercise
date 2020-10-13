using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResetCube : MonoBehaviour
{
    private Experiment experiment;
    private bool isExist;
    
    private void Start()
    {
        experiment = Experiment.Instance;
        isExist = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (experiment.GetTaskStatus == Experiment.TaskStatus.LeftPreTasking ||
            experiment.GetTaskStatus == Experiment.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
                isExist = true;
            }
        }
        else if (experiment.GetTaskStatus == Experiment.TaskStatus.RightPreTasking ||
                  experiment.GetTaskStatus == Experiment.TaskStatus.RightRealTasking)
        {
            if (other.gameObject.name == "hand.R_end")
            {
                isExist = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (experiment.GetTaskStatus == Experiment.TaskStatus.LeftPreTasking ||
            experiment.GetTaskStatus == Experiment.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
                isExist = true;
            }
        }
        else if (experiment.GetTaskStatus == Experiment.TaskStatus.RightPreTasking ||
                 experiment.GetTaskStatus == Experiment.TaskStatus.RightRealTasking)
        {
            if (other.gameObject.name == "hand.R_end")
            {
                isExist = true;
            }
        }
    }

    private void Update()
    {
        if (isExist)
        {
            if (experiment.GetTaskStatus == Experiment.TaskStatus.LeftPreTasking ||
                experiment.GetTaskStatus == Experiment.TaskStatus.LeftRealTasking)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                {
                    experiment.InstantiateTaskCube();
                    Destroy(this.gameObject);
                }
            }
            else if (experiment.GetTaskStatus == Experiment.TaskStatus.RightPreTasking ||
                     experiment.GetTaskStatus == Experiment.TaskStatus.RightRealTasking)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                {
                    experiment.InstantiateTaskCube();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
