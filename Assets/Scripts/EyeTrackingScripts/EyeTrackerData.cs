using System;
using UnityEngine;

public class MyEyeTrackerData
{
    public string identifier;
    private string sessionID;
    public string timeStamp;
    private string activityType;

    public Difficulty difficulty;

    // Cam / Head tracking
    private Vector3 gazeOrigin, gazeDir;
    private Vector3 headOrigin, headDir;

    // Smoothed eye gaze tracking 
    private Vector3 eyeOrigin, eyeDir, eyeHitPos;

    public MyEyeTrackerData(string identifier, string sessionID, string timeStamp, string activityName, Vector3 headOrigin,
     Vector3 headDir, Vector3 gazeOrigin, Vector3 gazeDir, Vector3 eyeHitPos, bool isMusicSynch, bool isRhythmSynch, 
     bool isRhythmNotSynch, bool isMusicNotSynch, Difficulty difficulty)
    {
        this.identifier = identifier;
        this.sessionID = sessionID;
        this.timeStamp = timeStamp;
        this.activityType = getActivityType(activityName, isMusicSynch, isRhythmSynch, isRhythmNotSynch, isMusicNotSynch);
        this.difficulty = difficulty;
        //this.headOrigin = headOrigin;
        this.headDir = headDir;

        this.gazeOrigin = gazeOrigin;
        this.gazeDir = gazeDir;

        this.eyeHitPos = eyeHitPos;
    }


    public string getActivityType(string name, bool isMusicSynch, bool isRhythmSynch, bool isRhythmNotSynch, bool isMusicNotSynch)
    {

        string returnFloat = "-1";

        if (Constants.ACTIVITY_SCENE_CONSTANT == name)
        {
            if (isRhythmSynch)
            {
                returnFloat = "01000";
            }
            else if (isMusicSynch)
            {
                returnFloat = "00100";
            }
            else if (isRhythmNotSynch)
            {
                returnFloat = "00010";
            }
            else if (isMusicNotSynch)
            {
                returnFloat = "00001";
            }
            else
            {
                returnFloat = "00000";
            }

        }
        else if (Constants.ACTIVITY_SCENE_NATURAL == name)
        {
            if (isRhythmSynch)
            {
                returnFloat = "10000";
            }
            else if (isMusicSynch)
            {
                returnFloat = "10100";
            }
            else if (isRhythmNotSynch)
            {
                returnFloat = "10010";
            }
            else if (isMusicNotSynch)
            {
                returnFloat = "10001";
            }
            else
            {
                returnFloat = "10000";
            }
        }
        else if (Constants.ACTIVITY_SCENE_FIGURE_EIGHT == name)
        {
            if (isRhythmSynch)
            {
                returnFloat = "21000";
            }
            else if (isMusicSynch)
            {
                returnFloat = "20100";
            }
            else if (isRhythmNotSynch)
            {
                returnFloat = "20010";
            }
            else if (isMusicNotSynch)
            {
                returnFloat = "20001";
            }
            else
            {
                returnFloat = "20000";
            }
        }
        else if (Constants.ACTIVITY_SCENE_HARMONIC == name)
        {
            if (isRhythmSynch)
            {
                returnFloat = "31000";
            }
            else if (isMusicSynch)
            {
                returnFloat = "30100";
            }
            else if (isRhythmNotSynch)
            {
                returnFloat = "30010";
            }
            else if (isMusicNotSynch)
            {
                returnFloat = "30001";
            }
            else
            {
                returnFloat = "30000";
            }
        }
        else if (Constants.ACTIVITY_SCENE_HARMONIC_CONSTANT == name)
        {
            if (isRhythmSynch)
            {
                returnFloat = "41000";
            }
            else if (isMusicSynch)
            {
                returnFloat = "40100";
            }
            else if (isRhythmNotSynch)
            {
                returnFloat = "40010";
            }
            else if (isMusicNotSynch)
            {
                returnFloat = "40001";
            }
            else
            {
                returnFloat = "40000";
            }
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
            "SessionID","Timestamp","Activity type","Difficulty","Gaze origin","Gaze direction", "Head origin", "Head direction","Eye hit position"
        };
    }

    public object[] mergeData()
    {
        return new string[]
        {
            sessionID.ToString(), timeStamp, activityType.ToString(), difficulty.ToString(), gazeOrigin.ToString(), gazeDir.ToString(), headOrigin.ToString(), headDir.ToString(),
            eyeHitPos.ToString()
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
