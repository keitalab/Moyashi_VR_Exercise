using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVisible : MonoBehaviour
{
    private bool isVisible = true;
    [SerializeField] private List<GameObject> visibleObject;

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (isVisible)
            {
                foreach(GameObject obj in visibleObject)
                {
                    if(obj.GetComponent<MeshRenderer>() != null)
                    {
                        obj.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
                isVisible = false;
            }
            else
            {
                foreach (GameObject obj in visibleObject)
                {
                    if (obj.GetComponent<MeshRenderer>() != null)
                    {
                        obj.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                isVisible = true;
            }
        }
    }
}
