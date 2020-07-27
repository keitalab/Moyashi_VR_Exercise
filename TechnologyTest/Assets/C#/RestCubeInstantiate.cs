using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestCubeInstantiate : MonoBehaviour
{
    private Experience experience;
    private Material material;
    
    private void Start()
    {
        experience = Experience.Instance;
        material = this.gameObject.GetComponent<Material>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (experience.GetTaskStatus == Experience.TaskStatus.LeftPreTasking ||
            experience.GetTaskStatus == Experience.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
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
                Color color = material.color;
                color.a = 1f;
                material.color = color;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (experience.GetTaskStatus == Experience.TaskStatus.LeftPreTasking ||
            experience.GetTaskStatus == Experience.TaskStatus.LeftRealTasking)
        {
            if (other.gameObject.name == "hand.L_end")
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                {
                    
                }
            }
        }
        else if (experience.GetTaskStatus == Experience.TaskStatus.RightPreTasking ||
                 experience.GetTaskStatus == Experience.TaskStatus.RightRealTasking)
        {
            if (other.gameObject.name == "hand.R_end")
            {
                Color color = material.color;
                color.a = 0.5f;
                material.color = color;
            }
        }
    }
}
