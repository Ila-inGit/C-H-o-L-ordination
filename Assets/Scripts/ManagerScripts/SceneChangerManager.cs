using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneChangerManager : MonoBehaviour
{
    public Dictionary<SceneNames, string> enumToCurrentScene = new Dictionary<SceneNames, string>();

    private int currentIndexForScene = 0;
    private int currentIndexForDifficulty = 0;
    public List<SceneNames> sceneSequence = new List<SceneNames>();

    private static SceneChangerManager instance;

    public static SceneChangerManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<SceneChangerManager>();
            return instance;
        }
    }

    private void Start()
    {
        ParseQRInfoManager.Instance.ParseJSON("");
    }

    public void init()
    {
        // need to be initilized and modified if some scene is added
        enumToCurrentScene.Add(SceneNames.MY_MANAGER_SCENE, Constants.MY_MANAGER_SCENE);
        enumToCurrentScene.Add(SceneNames.SETTING_SCENE, Constants.SETTING_SCENE);
        enumToCurrentScene.Add(SceneNames.TUTORIAL_FIRST_PART, Constants.TUTORIAL_FIRST_PART);
        enumToCurrentScene.Add(SceneNames.TUTORIAL_SECOND_PART, Constants.TUTORIAL_SECOND_PART);
        enumToCurrentScene.Add(SceneNames.START_ACTIVITIES_SCENE, Constants.START_ACTIVITIES_SCENE);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_CONSTANT, Constants.ACTIVITY_SCENE_CONSTANT);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_NATURAL, Constants.ACTIVITY_SCENE_NATURAL);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_FIGURE_EIGHT, Constants.ACTIVITY_SCENE_FIGURE_EIGHT);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_HARMONIC, Constants.ACTIVITY_SCENE_HARMONIC);
        enumToCurrentScene.Add(SceneNames.TRANSITION_SCENE, Constants.TRANSITION_SCENE);

        if (ParseQRInfoManager.Instance.infoFromJson.doTutorial)
        {
            sceneSequence.Add(SceneNames.TUTORIAL_FIRST_PART);
            sceneSequence.Add(SceneNames.TUTORIAL_SECOND_PART);
            sceneSequence.Add(SceneNames.START_ACTIVITIES_SCENE);
        }
        else
        {
            sceneSequence.Add(SceneNames.START_ACTIVITIES_SCENE);
        }
        foreach (var item in ParseQRInfoManager.Instance.infoFromJson.sceneOrderWithDifficulty)
        {
            sceneSequence.Add(item.name);
        }
        sceneSequence.Add(SceneNames.START_ACTIVITIES_SCENE);
    }

    public string getCurrentName(SceneNames name)
    {
        return enumToCurrentScene[name];
    }

    public Difficulty getDifficulty()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.infoFromJson.sceneOrderWithDifficulty[currentIndexForDifficulty];
        currentIndexForDifficulty++;
        return sceneDifficulty.difficulty;
    }

    public string getNextName()
    {
        Debug.Log(currentIndexForScene);
        Debug.Log("scene sequence:" + sceneSequence[currentIndexForScene]);
        string nextScene = getCurrentName(sceneSequence[currentIndexForScene]);
        currentIndexForScene++;
        return nextScene;
    }

    public void changeScene(SceneNames sceneName)
    {
        string currentName = getCurrentName(sceneName);
        Debug.Log("current name:" + currentName);
        string nextSceneName = getNextName();
        Debug.Log("next name:" + nextSceneName);
        SceneManager.UnloadSceneAsync(currentName);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    }

    public void goToTransitionScene(SceneNames sceneName)
    {
        string currentName = getCurrentName(sceneName);
        Debug.Log("current name:" + currentName);
        SceneManager.UnloadSceneAsync(currentName);
        SceneManager.LoadScene(Constants.TRANSITION_SCENE, LoadSceneMode.Additive);
    }

    public void findNextSceneToLoad()
    {
        string nextSceneName = getNextName();
        Debug.Log("next name:" + nextSceneName);
        SceneManager.UnloadSceneAsync(Constants.TRANSITION_SCENE);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    }
}