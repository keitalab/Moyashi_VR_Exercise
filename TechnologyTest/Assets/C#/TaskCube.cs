using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaskCube : MonoBehaviour
{
    private Experience experience;
    private Material material;
    private bool isExist;
    
    private void Start()
    {
        isExist = false;
        experience = Experience.Instance;
        material = this.gameObject.GetComponent<Renderer>().material;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (experience.GetTaskStatus == Experience.TaskStatus.LeftPreTasking ||
            experience.GetTaskStatus == Experience.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
                isExist = true;
                Color color = material.color;
                color.a = 0.5f;
                material.color = color;
            }
        }
        else if (experience.GetTaskStatus == Experience.TaskStatus.RightPreTasking ||
                  experience.GetTaskStatus == Experience.TaskStatus.RightRealTasking)
        {
            if (other.gameObject.name == "hand.R_end")
            {
                isExist = true;
                Color color = material.color;
                color.a = 0.5f;
                material.color = color;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (experience.GetTaskStatus == Experience.TaskStatus.LeftPreTasking ||
            experience.GetTaskStatus == Experience.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
                isExist = false;
                Color color = material.color;
                color.a = 1f;
                material.color = color;
            }
        }
        else if (experience.GetTaskStatus == Experience.TaskStatus.RightPreTasking ||
                 experience.GetTaskStatus == Experience.TaskStatus.RightRealTasking)
        {
            if (other.gameObject.name == "hand.R_end")
            {
                isExist = false;
                Color color = material.color;
                color.a = 1f;
                material.color = color;
            }
        }
    }

    private void Update()
    {
        if (isExist)
        {
            if (experience.GetTaskStatus == Experience.TaskStatus.LeftPreTasking ||
                experience.GetTaskStatus == Experience.TaskStatus.LeftRealTasking)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                {
                    experience.ClearTask();
                    Destroy(this.gameObject);
                }
            }
            else if (experience.GetTaskStatus == Experience.TaskStatus.RightPreTasking ||
                     experience.GetTaskStatus == Experience.TaskStatus.RightRealTasking)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                {
                    experience.ClearTask();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
