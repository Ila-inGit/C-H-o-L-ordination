using UnityEngine;

public class ParseQRInfoManager : MonoBehaviour
{

    public AudioClip parseCorectlyAudioClip;
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
        // string s = " {\"p\": 1234,\"m\":1121,\"t\": 20,\"r\": 12, \"d\": true,\"o\": [5,5,5,6,6,6,7,7,7,8,8,8],\"sd\": [0,1,2,3,4,5,6,7,8,1,7,0]}";
        SetUpInformationFromJson infoFromJson = SetUpInformationFromJson.CreateFromJSON(jsonString);
        setUpInfo = new SetUpInformation(infoFromJson);
        SceneChangerManager sceneChangerManager = SceneChangerManager.Instance;
        sceneChangerManager.Init();
        SoundManager.Instance.CanPlay(parseCorectlyAudioClip);
    }
}