using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    private Experience experience;

    private void Start()
    {
        experience = GameObject.Find("Experience").GetComponent<Experience>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.name == "hand.L_end" || other.gameObject.name == "hand.R_end")
        {
            experience.ClearTask();
            Destroy(this.gameObject);
        }
    }
}
