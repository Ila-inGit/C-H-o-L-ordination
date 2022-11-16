using UnityEngine;

public class ParseQRInfoManager : MonoBehaviour
{
    private static ParseQRInfoManager instance;

    public SetUpInformationFromJson infoFromJson { get; private set; }

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
        //infoFromJson = SetUpInformationFromJson.CreateFromJSON(jsonString);
        // fake init to be deleted
        infoFromJson = new SetUpInformationFromJson();
        SceneChangerManager.Instance.init();
    }

}