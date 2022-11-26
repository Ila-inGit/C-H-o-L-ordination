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
            int difficulty = infoFromJson.sd[i];
            bool isMusicActive = false, isRhythmActive = false;
            Difficulty diffEnum;
            if (difficulty == 3 || difficulty == 4 || difficulty == 5)
            {
                isRhythmActive = true;
            }
            if (difficulty == 6 || difficulty == 7 || difficulty == 8)
            {
                isMusicActive = true;
            }
            if ((difficulty + 1) % 3 == 0) // 3, 6, 9
            {
                diffEnum = Difficulty.DIFFICULT;
            }
            else if ((difficulty + 1) % 3 == 2) // 2, 5, 8 
            {
                diffEnum = Difficulty.MEDIUM;
            }
            else // 1, 4, 7
            {
                diffEnum = Difficulty.EASY;
            }

            sceneOrderWithDifficulty.Add(new SceneDifficulty((SceneNames)infoFromJson.o[i], diffEnum, isMusicActive, isRhythmActive));
        }

    }
}