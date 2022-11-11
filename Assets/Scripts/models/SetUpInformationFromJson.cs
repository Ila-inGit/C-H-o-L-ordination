using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SetUpInformationFromJson
{
    public int patientID;
    public int sessionID;

    public Difficulty difficulty;

    public int maxTimeForActivity;
    public int numberTotalAttempts;
    public int numberRightAttempts;
    public bool doTutorial;
    public List<SceneDifficulty> sceneOrderWithDifficulty;

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }

    // fake init if a json is not used (for debug)
    public SetUpInformationFromJson()
    {
        maxTimeForActivity = 4;
        numberTotalAttempts = 20;
        numberRightAttempts = 15;
        doTutorial = false;
        SceneDifficulty scena1 = new SceneDifficulty();
        scena1.name = SceneNames.ACTIVITY_SCENE_CONSTANT;
        scena1.difficulty = Difficulty.easy;
        SceneDifficulty scena2 = new SceneDifficulty();
        scena1.name = SceneNames.ACTIVITY_SCENE_FIGURE_EIGHT;
        scena1.difficulty = Difficulty.medium;
        SceneDifficulty scena3 = new SceneDifficulty();
        scena1.name = SceneNames.ACTIVITY_SCENE_HARMONIC;
        scena1.difficulty = Difficulty.easy;
        sceneOrderWithDifficulty.Add(scena1);
        sceneOrderWithDifficulty.Add(scena2);
        sceneOrderWithDifficulty.Add(scena3);
    }

}
