using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] private Text timerObject;
    private float timer;
    private int clearTaskCount;
    [SerializeField] private GameObject taskObject;
    private IEnumerator activeTaskCoroutine;

    private void Start()
    {
        timer = 4f;
        clearTaskCount = 0;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                timerObject.text = Mathf.FloorToInt(timer).ToString();
                yield return null;
            }
            else
            {
                timer = 0f;
                activeTaskCoroutine = Task();
                StartCoroutine(activeTaskCoroutine);
                yield break;
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
            timerObject.text = timer.ToString("f2");
            yield return null;
        }
    }

    public void ClearTask()
    {
        if (clearTaskCount < 10)
        {
            clearTaskCount++;
            StopCoroutine(activeTaskCoroutine);
            activeTaskCoroutine = null;
            activeTaskCoroutine = Task();
            StartCoroutine(activeTaskCoroutine);
        }
        else
        {
            StopCoroutine(activeTaskCoroutine);
            activeTaskCoroutine = null;
            activeTaskCoroutine = Task();
            timerObject.text = timer.ToString("f2");
        }
    }
}
