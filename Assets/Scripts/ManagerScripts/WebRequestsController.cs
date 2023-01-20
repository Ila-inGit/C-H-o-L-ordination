using UnityEngine;
using UnityEditor;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;


public class WebRequestsController : MonoBehaviour
{
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    private void Start()
    {
        Post();
    }

    public string GetFileContent()
    {
        return DataCollector.Instance.retriveContentFromFile();
    }

    public void Post()
    {
        // We can add default query string params for all requests
        // RestClient.DefaultRequestParams["param1"] = "My param";
        // retrive the csv as string and pass in the body of the POST request
        string fileContent = GetFileContent();
        //string fileContent = "{'ciao', 'ciao'}";

        currentRequest = new RequestHelper
        {
            Uri = Constants.BASEURL + Constants.UPLOADFILE,
            Params = new Dictionary<string, string>{
                {"sessionToken", ParseQRInfoManager.Instance.setUpInfo.sessionID}// "abd-kur09-76859-hejd-poi" }
            },
            Body = new DataToTranfer
            {
                body = fileContent
            },
            EnableDebug = true,
        };
        RestClient.Post<string>(currentRequest)
        .Then(res =>
        {
            // Ad later we can clear the default query string params for all requestsn
            RestClient.ClearDefaultParams();
            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => this.LogMessage("Error", err.Message));
    }
}