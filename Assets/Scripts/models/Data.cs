public class MyData
{

    private string constant;
    private string natural;
    private string eight;
    private string harmonical;

    public MyData(string constant, string natural, string eight, string harmonical)
    {
        this.constant = constant;
        this.natural = natural;
        this.eight = eight;
        this.harmonical = harmonical;
    }



    public string identifier;
    private string sessionID;
    private string activityType;
    private string boxType;
    private string angleType;
    private string hitType;
    private float hitTime;
    private float nonHitTime;
    private string counterType;
    private float counter;
    public string timeStamp;
    public Difficulty difficulty;

    public MyData(string identifier, string sessionID, string timeStamp, string activityName, string boxName,
        string angleType, string hitType, float hitTime, float nonHitTime, string counterType, float counter,
        bool isMusicSynch, bool isRhythmSynch, bool isRhythmNotSynch, bool isMusicNotSynch, Difficulty difficulty)
    {
        this.identifier = identifier;
        this.sessionID = sessionID;
        this.timeStamp = timeStamp;
        this.activityType = getActivityType(activityName, isMusicSynch, isRhythmSynch, isRhythmNotSynch, isMusicNotSynch);
        this.boxType = getBoxType(boxName);
        this.angleType = angleType;
        this.hitType = hitType;
        this.hitTime = hitTime;
        this.nonHitTime = nonHitTime;
        this.counterType = counterType;
        this.counter = counter;
        this.difficulty = difficulty;
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
            returnFloat = "00";
        }
        else if (Constants.RIGHT_BOX == name)
        {
            returnFloat = "01";
        }
        else if (Constants.LEFT_BOX == name)
        {
            returnFloat = "10";
        }

        return returnFloat;

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
            "SessionID","Timestamp","Activity type","Difficulty","Box type","Angle type","Hit type","Time hit","Timer non-hit","Counter Type","Counter"
        };
    }

    public object[] mergeData()
    {
        return new string[]
        {
            sessionID, timeStamp, activityType.ToString(), difficulty.ToString(), boxType.ToString(), angleType.ToString(), hitType.ToString(), hitTime.ToString(),nonHitTime.ToString(),counterType.ToString(), counter.ToString()
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
