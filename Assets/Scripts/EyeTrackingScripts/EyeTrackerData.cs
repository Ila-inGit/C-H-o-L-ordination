using System;
using UnityEngine;
public class MyEyeTrackerData
{
    private string scene;
    public string identifier;
    private string sessionID;
    private string activityType;
    //private string boxType;
    //private string angleType;

    // Cam / Head tracking
    private Vector3 gazeOrigin, gazeDir;
    private Vector3 headOrigin, headDir;

    // Smoothed eye gaze tracking 
    private Vector3 eyeOrigin; // (EyeOrigin.x, EyeOrigin.y, EyeOrigin.z)
    private Vector3 eyeDir; // (EyeDir.x, EyeDir.y, EyeDir.z)   
    private Vector3 eyeHitPos; // (EyeHitPos.x, EyeHitPos.y, EyeHitPos.z)            

    public string timeStamp;

    //public MyEyeTrackerData(string identifier, float sessionID, string timeStamp, string activityName, string? boxName, string angleType, Vector3 headOrigin, Vector3 headDir, Vector3 eyeOrigin, Vector3 eyeDir, Vector3 eyeHitPos)
    public MyEyeTrackerData(string identifier, string sessionID, string timeStamp, string activityName, Vector3 headOrigin, Vector3 headDir, Vector3 gazeOrigin, Vector3 gazeDir, Vector3 eyeOrigin, Vector3 eyeDir, Vector3 eyeHitPos)
    {
        this.identifier = identifier;
        this.sessionID = sessionID;
        this.timeStamp = timeStamp;
        this.activityType = getActivityType(activityName);
        //this.boxType = getBoxType(boxName);
        //this.angleType = angleType;
        this.headOrigin = headOrigin;
        this.headDir = headDir;

        this.gazeOrigin = gazeOrigin;
        this.gazeDir = gazeDir;

        this.eyeOrigin = eyeOrigin;
        this.eyeDir = eyeDir;
        this.eyeHitPos = eyeHitPos;
    }

    public string getBoxType(string name)
    {

        string returnFloat = name;

        if (Constants.TOP_BOX == name)
        {
            returnFloat = "11";
        }
        else if (Constants.BOTTOM_BOX == name)
        {
            returnFloat = "_00";
        }
        else if (Constants.RIGHT_BOX == name)
        {
            returnFloat = "_01";
        }
        else if (Constants.LEFT_BOX == name)
        {
            returnFloat = "10";
        }

        return returnFloat;

    }

    public string getActivityType(string name)
    {

        string returnFloat = "-1";

        if (Constants.ACTIVITY_SCENE_CONSTANT == name)
        {
            returnFloat = "0";
        }
        else if (Constants.ACTIVITY_SCENE_NATURAL == name)
        {
            returnFloat = "1";
        }
        else if (Constants.ACTIVITY_SCENE_FIGURE_EIGHT == name)
        {
            returnFloat = "2";
        }
        else if (Constants.ACTIVITY_SCENE_HARMONIC == name)
        {
            returnFloat = "3";
        }

        return returnFloat;

    }


    public void setTimeStamp()
    {
        timeStamp = System.DateTime.Now.ToString();
    }


    public string exportForCSV()
    {
        return GetStringFormat(mergeData());
    }


    public string exportColumnNameforCSV()
    {
        return GetStringFormat(getColumnsName());
    }

    public object[] getColumnsName()
    {
        return new string[]
        {
            //TODO adapted for eyetracking
            "SessionID","Timestamp","Activity type","Head origin","Head direction","Eye origin","Eye direction","Eye hit position"
        };
    }

    public object[] mergeData()
    {
        return new string[]
        {
            sessionID.ToString(), timeStamp, activityType.ToString(), gazeOrigin.ToString(), gazeDir.ToString(), headDir.ToString(),
            eyeOrigin.ToString(), eyeDir.ToString(), eyeHitPos.ToString()
            //ToString("F3") in order to show 3 decimale
        };
    }

    public static string GetStringFormat(object[] data)
    {
        string strFormat = "";
        for (int i = 0; i < data.Length; i++)
        {
            strFormat += (data[i] + System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + " ");
        }
        strFormat += ("\n");
        return strFormat;
    }

}
