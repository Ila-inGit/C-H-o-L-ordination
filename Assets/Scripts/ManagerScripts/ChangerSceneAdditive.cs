using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ChangerSceneAdditive : MonoBehaviour
{
    public int currentIndex;
    private Constants nameOfScenes = new Constants();

    public void myChangeScene()
    {
        SceneManager.UnloadSceneAsync(currentIndex);
        nameOfScenes.init();
        string nextSceneName = nameOfScenes.getName(currentIndex + 1);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    }
}
