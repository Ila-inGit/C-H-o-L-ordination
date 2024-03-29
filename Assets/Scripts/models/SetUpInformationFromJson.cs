using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SetUpInformationFromJson
{
    // --- sessionID
    public string p;
    // --- maxTimeForActivity
    public int m;
    // --- numberTotalAttempts
    public int t;
    // --- numberRightAttempts
    public int r;
    // --- doTutorial
    public bool d;
    // --- sceneOrder 
    // --- (5 - elliptic constant, 6 - elliptic natural, 7 - lemniscate, 8 - harmonical natural, 9 - harmonical constant)
    public List<int> o = new List<int>();
    // --- sceneDifficulty
    // --- (0- easy no music, 1- medium no music, 2- difficult no music, 3- easy rhythm, 4- medium rhythm, 5- difficult rhythm,
    // ---   6- easy music, 7- medium music, 8- difficult music, 9- easy rhythm asynch, 10- medium rhythm asynch, 11- difficult rhythm asynch, 
    // ---   12- easy music asynch, 13- medium music asynch, 14- difficult music asynch)
    public List<int> sd = new List<int>();

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }
}
