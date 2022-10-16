using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

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

    //private static string strFilePath = @"C:\Users\utente\Documents\Data.csv";
    //private static string strFilePath = @"C:\Users\Ilaria\Desktop\Data.csv"; //da cambiare

    private static StringBuilder sbOutput = new StringBuilder();

    //private string saved line;
    private static MyData saveInformation;

    // private static string timeStamp = System.DateTime.Now.ToString();
    private string DATATORETRIVE = "DATATORETRIVE" /*+ ".csv"*/;
    private string CAMERA = "CAMERA";


    //private save counter
    private static bool firstSave = true;
    private static bool firstSaveCamera = true;

    //Hashtable declaration
    private static Dictionary<string, MyData> dataCollection = new Dictionary<string, MyData>();


    //static async void WriteData()
    //{
    //    if (firstSave){
    //        //StorageFile sampleFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
    //        StorageFile sampleFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

    //        await FileIO.AppendTextAsync(sampleFile, saveInformation.exportColumnNameforCSV().ToString() 
    //                                                  + saveInformation.exportForCSV().ToString());

    //        firstSave = false;
    //    }
    //else{
    //    //StorageFile sampleFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
    //    StorageFile sampleFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
    //    await FileIO.AppendTextAsync(sampleFile, saveInformation.exportForCSV().ToString());
    //}
    //}

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

    public void addToFileCamera(Vector3 dataToWrite)
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERA);

        // ----------------------- for windows -----------------------
        if (firstSaveCamera)
        {
            // Create and write the csv file
            File.WriteAllText(strFilePathCamera, dataToWrite.ToString());
            firstSave = false;
        }
        else
        {
            // To append more lines to the csv file
            File.AppendAllText(strFilePathCamera, dataToWrite.ToString());
        }
    }
    public Vector3 retriveCameraFromFile()
    {

        string strFilePathCamera = string.Format("{0}/{1}.txt", Application.persistentDataPath, CAMERA);

        // ----------------------- for windows -----------------------

        // Create and write the csv file
        string cameraInitPos = File.ReadAllText(strFilePathCamera);

        return stringToVector3(cameraInitPos);

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
        // divido per 10 perchè non vede il punto
        Vector3 result = new Vector3(
            float.Parse(sArray[0]) / 10.0f,
            float.Parse(sArray[1]) / 10.0f,
            float.Parse(sArray[2]) / 10.0f);
        return result;
    }

}