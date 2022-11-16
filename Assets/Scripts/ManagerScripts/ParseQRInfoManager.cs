using UnityEngine;

public class ParseQRInfoManager : MonoBehaviour
{
    private static ParseQRInfoManager instance;

    public SetUpInformation setUpInfo { get; private set; }

    public static ParseQRInfoManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<ParseQRInfoManager>();
            return instance;
        }
    }



    public void ParseJSON(string jsonString)
    {
        string s = " {\"p\": 1234,\"m\":1121,\"t\": 20,\"r\": 12, \"d\": false,\"o\": [5,6,7,8,5,6,7,8,5,7,8,5],\"sd\": [0,1,2,0,0,2,1,2,0,1,2,0]}";
        SetUpInformationFromJson infoFromJson = SetUpInformationFromJson.CreateFromJSON(s);
        setUpInfo = new SetUpInformation(infoFromJson);
        SceneChangerManager.Instance.init();
    }

}