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
    public List<int> o = new List<int>();
    // --- sceneDifficulty
    public List<int> sd = new List<int>();

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }
}
