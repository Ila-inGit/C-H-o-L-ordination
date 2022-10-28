using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataCollector : MonoBehaviour
{

    private static DataCollector instance;

    public static DataCollector Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<DataCollector>();
            return instance;
        }
    }

    private static StringBuilder sbOutput = new StringBuilder();

    //private string saved line;
    private static MyData saveInformation;
    private string DATATORETRIVE = "DATATORETRIVE" /*+ ".csv"*/;
    private string CAMERAPOSITION = "CAMERAPOSITION";
    private string CAMERAANGLE = "CAMERAANGLE";

    // private save counter
    private bool firstSave = true;
    private bool firstSaveCameraPosition = true;
    private bool firstSaveCameraAngle = true;

    //Hashtable declaration
    private Dictionary<string, MyData> dataCollection = new Dictionary<string, MyData>();

    public void addToFile(MyData dataToWrite)
    {
        dataCollection[dataToWrite.identifier] = dataToWrite;
        saveInformation = dataCollection[dataToWrite.identifier];
        saveInformation.setTimeStamp();

        string strFilePath = string.Format("{0}/{1}.csv", Application.persistentDataPath, DATATORETRIVE);

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

    public void addCameraPositionToFile(Vector3 dataToWrite)
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERAPOSITION);

        // ----------------------- for windows -----------------------
        if (firstSaveCameraPosition)
        {
            // Create and write the csv file
            File.WriteAllText(strFilePathCamera, dataToWrite.ToString());
            firstSaveCameraPosition = false;
        }
        else
        {
            // To append more lines to the csv file
            File.AppendAllText(strFilePathCamera, dataToWrite.ToString());
        }
    }
    public Vector3 retriveCameraPositionFromFile()
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERAPOSITION);

        // ----------------------- for windows -----------------------
        // Create and write the csv file
        string cameraInitPos = File.ReadAllText(strFilePathCamera);

        return stringToVector3(cameraInitPos);

    }

    public void addCameraAngleToFile(Quaternion dataToWrite)
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERAANGLE);

        // ----------------------- for windows -----------------------
        if (firstSaveCameraAngle)
        {
            // Create and write the csv file
            File.WriteAllText(strFilePathCamera, dataToWrite.ToString());
            firstSaveCameraAngle = false;
        }
        else
        {
            // To append more lines to the csv file
            File.AppendAllText(strFilePathCamera, dataToWrite.ToString());
        }
    }
    public Quaternion retriveCameraAngleFromFile()
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERAANGLE);

        // ----------------------- for windows -----------------------
        // Create and write the csv file
        string cameraInitPos = File.ReadAllText(strFilePathCamera);

        return stringToQuaternion(cameraInitPos);

    }

    public Vector3 stringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        // divido per 10 perchè non vede il punto
        Vector3 result = new Vector3(
            float.Parse(sArray[0]) / 10.0f,
            float.Parse(sArray[1]) / 10.0f,
            float.Parse(sArray[2]) / 10.0f);
        return result;
    }

    public Quaternion stringToQuaternion(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        // divido per 10 perchè non vede il punto
        Quaternion result = new Quaternion(
            float.Parse(sArray[0]) / 10.0f,
            float.Parse(sArray[1]) / 10.0f,
            float.Parse(sArray[2]) / 10.0f,
            float.Parse(sArray[3]) / 10.0f
            );
        return result;
    }

}