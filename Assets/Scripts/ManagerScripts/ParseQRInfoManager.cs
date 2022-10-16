using UnityEngine;

public class ParseQRInfoManager : MonoBehaviour
{
    private static ParseQRInfoManager instance;

    public static ParseQRInfoManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<ParseQRInfoManager>();
            return instance;
        }
    }
}