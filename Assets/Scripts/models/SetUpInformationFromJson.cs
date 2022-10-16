using UnityEngine;

[System.Serializable]
class SetUpInformationFromJson
{
    public string patientName;
    public int patientID;
    public int sessionID;

    public Difficulty difficulty;

    public int maxTimeFOrActivity;
    public int numberTotalAttempts;
    public int numberRightAttempts;
    public bool doTutorial;

    public static SetUpInformationFromJson CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SetUpInformationFromJson>(jsonString);
    }

}