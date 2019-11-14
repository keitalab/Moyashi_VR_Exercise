using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VibrationController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private bool leftRight;
    private static readonly string sphereTag = "CheckDistanceSphere";
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag(sphereTag))
        {
            if (leftRight)
            {
                OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
            }
            else
            {
                OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            }
            StartCoroutine(VibrationStop(0.2f));
        }
    }
    private IEnumerator VibrationStop(float time)
    {
        yield return new WaitForSeconds(time);
        if (leftRight)
        {
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }
    }
}