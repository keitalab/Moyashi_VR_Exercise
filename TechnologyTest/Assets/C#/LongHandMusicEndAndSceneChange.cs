using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LongHandMusicEndAndSceneChange : MonoBehaviour
{
    private float elapsedTime = 0f;
    private int musicTime = 240;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > musicTime)
        {
            SceneManager.LoadScene("LongHandExperience");
        }
    }
}
