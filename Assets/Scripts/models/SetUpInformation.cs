using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SetUpInformation
{
    public string sessionID;
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
        sessionID = infoFromJson.p;
        maxTimeForActivity = infoFromJson.m;
        numberTotalAttempts = infoFromJson.t;
        numberRightAttempts = infoFromJson.r;
        doTutorial = infoFromJson.d;

        for (int i = 0; i < infoFromJson.o.Count; i++)
        {
            int difficulty = infoFromJson.sd[i];
            bool isMusicSynch = false, isRhythmSynch = false, isMusicNotSynch = false, isRhythmNotSynch = false;
            Difficulty diffEnum;
            if (difficulty == 3 || difficulty == 4 || difficulty == 5)
            {
                isRhythmSynch = true;
            }
            if (difficulty == 6 || difficulty == 7 || difficulty == 8)
            {
                isMusicSynch = true;
            }
            if (difficulty == 9 || difficulty == 10 || difficulty == 11)
            {
                isRhythmNotSynch = true;
            }
            if (difficulty == 12 || difficulty == 13 || difficulty == 14)
            {
                isMusicNotSynch = true;
            }
            if ((difficulty + 1) % 3 == 0) // 3, 6, 9, 12
            {
                diffEnum = Difficulty.DIFFICULT;
            }
            else if ((difficulty + 1) % 3 == 2) // 2, 5, 8, 11
            {
                diffEnum = Difficulty.MEDIUM;
            }
            else // 1, 4, 7 ,10
            {
                diffEnum = Difficulty.EASY;
            }

            sceneOrderWithDifficulty.Add(new SceneDifficulty((SceneNames)infoFromJson.o[i], diffEnum, isMusicSynch, isRhythmSynch, isMusicNotSynch, isRhythmNotSynch));
        }

    }
}