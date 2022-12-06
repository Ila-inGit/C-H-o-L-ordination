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
    private float subject;
    private string activityType;
    private string boxType;
    private string angleType;
    private string hitType;
    private float hitTime;
    private float nonHitTime;
    private string counterType;
    private float counter;
    public string timeStamp;

    public MyData(string identifier, float subject, string timeStamp, string activityName, string boxName, string angleType, string hitType, float hitTime, float nonHitTime, string counterType, float counter, bool isMusicActive, bool isRhythmActive)
    {
        this.identifier = identifier;
        this.subject = subject;
        this.timeStamp = timeStamp;
        this.activityType = getActivityType(activityName, isMusicActive, isRhythmActive);
        this.boxType = getBoxType(boxName);
        this.angleType = angleType;
        this.hitType = hitType;
        this.hitTime = hitTime;
        this.nonHitTime = nonHitTime;
        this.counterType = counterType;
        this.counter = counter;
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

    public string getActivityType(string name, bool isMusicActive, bool isRhythmActive)
    {

        string returnFloat = "-1";

        if (Constants.ACTIVITY_SCENE_CONSTANT == name)
        {
            if (isMusicActive)
            {
                return "_001";

            }
            else if (isRhythmActive)
            {
                return "_010";
            }
            else
            {
                returnFloat = "_000";
            }

        }
        else if (Constants.ACTIVITY_SCENE_NATURAL == name)
        {
            if (isMusicActive)
            {
                return "101";

            }
            else if (isRhythmActive)
            {
                return "110";
            }
            else
            {
                returnFloat = "100";
            }
        }
        else if (Constants.ACTIVITY_SCENE_FIGURE_EIGHT == name)
        {
            if (isMusicActive)
            {
                return "201";

            }
            else if (isRhythmActive)
            {
                return "210";
            }
            else
            {
                returnFloat = "200";
            }
        }
        else if (Constants.ACTIVITY_SCENE_HARMONIC == name)
        {
            if (isMusicActive)
            {
                return "301";

            }
            else if (isRhythmActive)
            {
                return "310";
            }
            else
            {
                returnFloat = "300";
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
            "Subject","Timestamp","Activity type","Box type","Angle type","Hit type","Time hit","Timer non-hit","Counter Type","Counter"
        };
    }

    public object[] mergeData()
    {
        return new string[]
        {
            subject.ToString(), timeStamp, activityType.ToString(), boxType.ToString(), angleType.ToString(), hitType.ToString(), hitTime.ToString(),nonHitTime.ToString(),counterType.ToString(), counter.ToString()
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
