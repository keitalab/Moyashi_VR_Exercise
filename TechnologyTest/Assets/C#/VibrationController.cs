using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    private const string leftHand = "LeftHand";
    private const string rightHand = "RightHand";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == leftHand)
        {
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
            StartCoroutine(VibrationStop(true));
        }
        else if(collision.gameObject.tag == rightHand)
        {
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            StartCoroutine(VibrationStop(false));
        }
    }

    private IEnumerator VibrationStop(bool isLeft)
    {
        yield return new WaitForSeconds(0.2f);
        if (isLeft)
        {
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }
    }
}
