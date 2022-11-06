using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class AutoSceneChanger : MonoBehaviour
{
    [SerializeField]
    public SceneNames sceneName;
    private Constants nameOfScenes = new Constants();

    private float timerBeforeChange = 0.0f;
    private int totalTouches = 0;
    private TouchesCounter touchesCounter;
    private static Dictionary<string, int> parameters;

    private void Start()
    {
        nameOfScenes.init();
    }

    private void FixedUpdate()
    {
        touchesCounter = gameObject.GetComponent<TouchesCounter>();
        totalTouches = touchesCounter.totalTouches;

        timerBeforeChange += Time.deltaTime;
        if (timerBeforeChange >= ParseQRInfoManager.Instance.infoFromJson.maxTimeForActivity)
        {
            // Debug.Log("4 min expired");
            NeedToChange();
        }
        else if (totalTouches == ParseQRInfoManager.Instance.infoFromJson.numberRightAttempts)
        {
            // Debug.Log("15 total touches done");
            NeedToChange();
        }
        // else if (totalTouches == ParseQRInfoManager.Instance.infoFromJson.numberTotalAttempts)
        // {
        //     // Debug.Log("Max total touches done");
        //     NeedToChange();
        // }
    }


    private void NeedToChange()
    {
        string currentName = nameOfScenes.getCurrentName(sceneName);

        if (sceneName == SceneNames.ACTIVITY_SCENE_HARMONIC)
        {
            int enumCorrespondingInt = (int)SceneNames.SETTING_SCENE;
            PlayerPrefs.SetInt("nextSceneEnum", enumCorrespondingInt);
        }
        else
        {
            int enumCorrespondingNextInt = ((int)sceneName) + 1;
            PlayerPrefs.SetInt("nextSceneEnum", enumCorrespondingNextInt); //saving the index of the next scene to load
        }

        SceneManager.UnloadSceneAsync(currentName);
        SceneManager.LoadScene(Constants.TRANSITION_SCENE, LoadSceneMode.Additive);

    }

}