using UnityEngine;

public class ParseQRInfoManager : MonoBehaviour
{
    [SerializeField]
    AudioClip QRcodeParsedCorrectly;
    private static ParseQRInfoManager instance;

    public SetUpInformation setUpInfo { get; private set; }

    public static ParseQRInfoManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = GameObject.FindObjectOfType<ParseQRInfoManager>();
    }



    public void ParseJSON(string jsonString)
    {
        // string s = " {\"p\": 1234,\"m\":1121,\"t\": 20,\"r\": 12, \"d\": false,\"o\": [5,6,7,8,5,6,7,8,5,7,8,5],\"sd\": [0,1,2,0,0,2,1,2,0,1,2,0]}";
        SetUpInformationFromJson infoFromJson = SetUpInformationFromJson.CreateFromJSON(jsonString);
        setUpInfo = new SetUpInformation(infoFromJson);
        SceneChangerManager sceneChangerManager = SceneChangerManager.Instance;
        sceneChangerManager.Init();
        SoundManager.Instance.Playsound(QRcodeParsedCorrectly);
    }

}