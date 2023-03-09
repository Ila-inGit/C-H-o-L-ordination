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
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null) instance = GameObject.FindObjectOfType<SceneChangerManager>();
    }

    public void Init()
    {
        // need to be initilized and modified if some scene is added
        if (!isInitialized())
        {
            enumToCurrentScene.Add(SceneNames.MY_MANAGER_SCENE, Constants.MY_MANAGER_SCENE);
            enumToCurrentScene.Add(SceneNames.SETTING_SCENE, Constants.SETTING_SCENE);
            enumToCurrentScene.Add(SceneNames.TUTORIAL_FIRST_PART, Constants.TUTORIAL_FIRST_PART);
            enumToCurrentScene.Add(SceneNames.TUTORIAL_SECOND_PART, Constants.TUTORIAL_SECOND_PART);
            enumToCurrentScene.Add(SceneNames.START_ACTIVITIES_SCENE, Constants.START_ACTIVITIES_SCENE);
            enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_CONSTANT, Constants.ACTIVITY_SCENE_CONSTANT);
            enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_NATURAL, Constants.ACTIVITY_SCENE_NATURAL);
            enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_FIGURE_EIGHT, Constants.ACTIVITY_SCENE_FIGURE_EIGHT);
            enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_HARMONIC, Constants.ACTIVITY_SCENE_HARMONIC);
            enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_HARMONIC_CONSTANT, Constants.ACTIVITY_SCENE_HARMONIC_CONSTANT);
            enumToCurrentScene.Add(SceneNames.TRANSITION_SCENE, Constants.TRANSITION_SCENE);
            enumToCurrentScene.Add(SceneNames.FINAL_SCENE, Constants.FINAL_SCENE);
        }

        sceneSequence.Clear();

        if (ParseQRInfoManager.Instance.setUpInfo.doTutorial)
        {
            sceneSequence.Add(SceneNames.TUTORIAL_FIRST_PART);
            sceneSequence.Add(SceneNames.TUTORIAL_SECOND_PART);
            sceneSequence.Add(SceneNames.START_ACTIVITIES_SCENE);
        }
        else
        {
            sceneSequence.Add(SceneNames.START_ACTIVITIES_SCENE);
        }
        foreach (var item in ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty)
        {
            sceneSequence.Add(item.name);
        }
        sceneSequence.Add(SceneNames.FINAL_SCENE);
    }

    public bool isInitialized()
    {
        return enumToCurrentScene.Count != 0;
    }

    public string getCurrentName(SceneNames name)
    {
        if (enumToCurrentScene.Count != 0)
        {
            return enumToCurrentScene[name];
        }
        else
        {
            return "";
        }

    }

    public Difficulty getDifficulty()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty];
        currentIndexForDifficulty++;
        return sceneDifficulty.difficulty;
    }
    public Difficulty getDifficultyForFile()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty];
        return sceneDifficulty.difficulty;
    }
    public bool isMusicSynch()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty - 1];
        return sceneDifficulty.isMusicSynch;
    }
    public bool isRhythmSynch()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty - 1];
        return sceneDifficulty.isRhythmSynch;
    }
    public bool isMusicNotSynch()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty - 1];
        return sceneDifficulty.isMusicNotSynch;
    }
    public bool isRhythmNotSynch()
    {
        SceneDifficulty sceneDifficulty =
            ParseQRInfoManager.Instance.setUpInfo.sceneOrderWithDifficulty[currentIndexForDifficulty - 1];
        return sceneDifficulty.isRhythmNotSynch;
    }

    public string getNextName()
    {
        string nextScene = getCurrentName(sceneSequence[currentIndexForScene]);
        if (nextScene != "")
        {
            currentIndexForScene++;
            return nextScene;
        }
        else
        {
            return "";
        }

    }

    public void changeScene(SceneNames sceneName)
    {
        string currentName = getCurrentName(sceneName);
        if (currentName != "")
        {
            string nextSceneName = getNextName();
            if (nextSceneName != null)
            {
                SceneManager.UnloadSceneAsync(currentName);
                SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            }
        }
    }

    public void goToTransitionScene(SceneNames sceneName)
    {
        string currentName = getCurrentName(sceneName);
        if (currentName != "")
        {
            SceneManager.UnloadSceneAsync(currentName);
            SceneManager.LoadScene(Constants.TRANSITION_SCENE, LoadSceneMode.Additive);
        }

    }

    public void findNextSceneToLoad()
    {
        string nextSceneName = getNextName();
        if (nextSceneName != "")
        {
            SceneManager.UnloadSceneAsync(Constants.TRANSITION_SCENE);
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        }

    }
}