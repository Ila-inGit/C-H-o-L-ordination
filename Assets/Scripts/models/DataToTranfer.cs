using System;
[Serializable]
class DataToTranfer
{
    public string body;
    public override string ToString()
    {
        return UnityEngine.JsonUtility.ToJson(this, true);
    }
}