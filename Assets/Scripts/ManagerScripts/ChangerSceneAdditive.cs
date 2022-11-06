using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ChangerSceneAdditive : MonoBehaviour
{
    [SerializeField]
    public SceneNames sceneName;
    private Constants nameOfScenes = new Constants();

    public void myChangeScene()
    {
        nameOfScenes.init();
        string currentName = nameOfScenes.getCurrentName(sceneName);
        string nextSceneName = nameOfScenes.getNextName(sceneName);
        SceneManager.UnloadSceneAsync(currentName);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    }

    public void goToTransitionScene()
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
