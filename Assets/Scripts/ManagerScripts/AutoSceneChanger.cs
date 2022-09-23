using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class AutoSceneChanger : MonoBehaviour
{
    private float timerBeforeChange = 0.0f;
    private int index = 0;
    private int totalTouches = 0;
    private Scene currentScene;
    private TouchesCounter touchesCounter;
    private static Dictionary<string, int> parameters;

    private void Start()
    {
        if (SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_CONSTANT).isLoaded)
        {
            currentScene = SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_CONSTANT);

        }
        else if (SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_NATURAL).isLoaded)
        {
            currentScene = SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_NATURAL);

        }
        else if (SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_FIGURE_EIGHT).isLoaded)
        {
            currentScene = SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_FIGURE_EIGHT);

        }
        else if (SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_HARMONIC).isLoaded)
        {
            currentScene = SceneManager.GetSceneByName(Constants.ACTIVITY_SCENE_HARMONIC);
        }

        index = currentScene.buildIndex;
    }


    private void FixedUpdate()
    {
        touchesCounter = gameObject.GetComponent<TouchesCounter>();
        totalTouches = touchesCounter.totalTouches;

        timerBeforeChange += Time.deltaTime;
        if (timerBeforeChange >= 240)
        {
            // Debug.Log("4 min expired");
            NeedToChange();
        }
        else if (totalTouches == 15)
        {
            // Debug.Log("15 total touches done");
            NeedToChange();
        }
    }


    private void NeedToChange()
    {
        if (currentScene.name == Constants.ACTIVITY_SCENE_HARMONIC)
            PlayerPrefs.SetInt("nextSceneBuildIndex", 3);
        else
            PlayerPrefs.SetInt("nextSceneBuildIndex", index + 1); //saving the index of the next scene to load

        SceneManager.UnloadSceneAsync(index);
        SceneManager.LoadScene(Constants.TRANSITION_SCENE, LoadSceneMode.Additive);

    }

}