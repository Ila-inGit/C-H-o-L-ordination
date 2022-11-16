using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SetUpInformation
{
    public int patientID;
    public int maxTimeForActivity;
    public int numberTotalAttempts;
    public int numberRightAttempts;
    public bool doTutorial;
    public List<SceneDifficulty> sceneOrderWithDifficulty = new List<SceneDifficulty>();

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }
    
    public SetUpInformation(SetUpInformationFromJson infoFromJson)
    {
        patientID = infoFromJson.p;
        maxTimeForActivity = infoFromJson.m;
        numberTotalAttempts = infoFromJson.t;
        numberRightAttempts = infoFromJson.r;
        doTutorial = infoFromJson.d;

        for (int i = 0; i < infoFromJson.o.Count; i++)
        {
            sceneOrderWithDifficulty.Add(new SceneDifficulty((SceneNames)infoFromJson.o[i], (Difficulty)infoFromJson.sd[i]));
        }

    }
}