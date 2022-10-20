public class MyEyeTrackerData
{

    private string constant;
    private string natural;
    private string eight;
    private string harmonical;

    public MyEyeTrackerData(string constant, string natural, string eight, string harmonical)
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

    public MyEyeTrackerData(string identifier, float subject, string timeStamp, string activityName, string boxName, string angleType, string hitType, float hitTime, float nonHitTime, string counterType, float counter)
    {
        this.identifier = identifier;
        this.subject = subject;
        this.timeStamp = timeStamp;
        this.activityType = getActivityType(activityName);
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
            //TODO adapt it for eyetracking
            "Subject","Timestamp","Activity type","Box type","Angle type","Hit type","Time hit","Timer non-hit","Counter Type","Counter"
        };
    }

    public object[] mergeData()
    {
        return new string[]
        {
            //TODO adapt it for eyetracking
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