using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicStartAndSceneChange : SingletonMonoBehaviour<MusicStartAndSceneChange>
{
    public const int musicTime = 195;

    private bool doingExercise;

    private float musicElapsedTime = 0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doingExercise)
        {
            return;
        }
        
        musicElapsedTime += Time.deltaTime;
        if (musicElapsedTime > musicTime)
        {
            SceneManager.LoadScene("LongHandExperiment");
        }
    }

    public void MusicMeasureStart(bool doExercise)
    {
        if (doExercise)
        {
            audioSource.UnPause();
        }

        doingExercise = true;
    }

}
