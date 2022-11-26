using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SetUpInformationFromJson
{
    // --- patientID
    public int p;
    // --- maxTimeForActivity
    public int m;
    // --- numberTotalAttempts
    public int t;
    // --- numberRightAttempts
    public int r;
    // --- doTutorial
    public bool d;
    // --- sceneOrder 
    // --- (5- constant, 6- natural, 7- eight, 8- harmonical)
    public List<int> o = new List<int>();
    // --- sceneDifficulty
    // --- (0- easy no music, 1- medium no music, 2- difficult no music, 3- easy rhythm, 4- medium rhythm, 5- difficult rhythm,
    // ---   6- easy music, 7- medium music, 8- difficult music)
    public List<int> sd = new List<int>();

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }
}
