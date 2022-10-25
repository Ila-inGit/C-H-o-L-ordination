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
}
