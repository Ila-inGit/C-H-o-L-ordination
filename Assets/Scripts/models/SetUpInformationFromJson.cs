using UnityEngine;

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

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }

}