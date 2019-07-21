using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lowerHeadPosition : MonoBehaviour
{
    [SerializeField] private float lowerRate = 0.2f;
    [SerializeField] private GameObject trackingSpace;
    [SerializeField] Text text;
    private float playerHeight;
    private const float waitTime = 2f;
    private float elapsedTime = 0f;

    private void Start()
    {
        text.text = "0";
        playerHeight = 1.7f;
    }

    void LateUpdate()
    {
        //text.text = this.gameObject.transform.position.y + ", "+ (playerHeight + trackingSpace.transform.position.y) / 2;
        if (elapsedTime > waitTime)
        {
            if (this.gameObject.transform.position.y < (playerHeight + trackingSpace.transform.position.y) / 2)
            {
                //headPositionPressure -= lowerRate;
                text.text = (int.Parse(text.text) + 1).ToString();
                StartCoroutine(lowerCoroutine());
                elapsedTime = 0f;
            }
        }
        /*
        Vector3 centerPos = this.gameObject.transform.position;
        centerPos.y *= headPositionPressure;
        this.gameObject.transform.position = centerPos;
        Vector3 rightPos = rightEye.transform.position;
        rightPos.y *= headPositionPressure;
        rightEye.transform.position = rightPos;
        Vector3 leftPos = leftEye.transform.position;
        leftPos.y *= headPositionPressure;
        leftEye.transform.position = leftPos;
        */
        elapsedTime += Time.deltaTime;
    }

    private IEnumerator lowerCoroutine()
    {
        Vector3 pos = trackingSpace.transform.position;
        float onetimeLowerRate = 0.01f;

        for(int i = 0; i < lowerRate / onetimeLowerRate; i++) {
            pos.y -= onetimeLowerRate;
            trackingSpace.transform.position = pos;
            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }
}
