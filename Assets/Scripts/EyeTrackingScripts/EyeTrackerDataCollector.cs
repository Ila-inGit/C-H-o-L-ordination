using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

public class EyeTrackerDataCollector : MonoBehaviour
{

    private static EyeTrackerDataCollector instance;

    public static EyeTrackerDataCollector Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<EyeTrackerDataCollector>();
            return instance;
        }
    }

    //private static string strFilePath = @"C:\Users\utente\Documents\Data.csv";

    private static StringBuilder sbOutput = new StringBuilder();

    //private string saved line;
    private static MyEyeTrackerData saveInformation;

    private string EYEDATATORETRIVE = "EYEDATATORETRIVE" ;


    //private save counter
    private static bool firstSave = true;

    //Hashtable declaration
    private static Dictionary<string, MyEyeTrackerData> dataCollection = new Dictionary<string, MyEyeTrackerData>();

    public void addToFile(MyEyeTrackerData dataToWrite)
    {
        dataCollection[dataToWrite.identifier] = dataToWrite;
        saveInformation = dataCollection[dataToWrite.identifier];
        saveInformation.setTimeStamp();

        string strFilePath = string.Format("{0}/{1}.csv", Application.persistentDataPath, EYEDATATORETRIVE);

        // ----------------------- for windows -----------------------
        if (firstSave)
        {
            // Create and write the csv file
            File.WriteAllText(strFilePath, saveInformation.exportColumnNameforCSV().ToString());
            File.AppendAllText(strFilePath, saveInformation.exportForCSV().ToString());
            firstSave = false;
        }
        else
        {
            // To append more lines to the csv file
            File.AppendAllText(strFilePath, saveInformation.exportForCSV().ToString());
        }
    }

    public static Vector3 stringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]) / 10.0f,
            float.Parse(sArray[1]) / 10.0f,
            float.Parse(sArray[2]) / 10.0f);
        return result;
    }

}