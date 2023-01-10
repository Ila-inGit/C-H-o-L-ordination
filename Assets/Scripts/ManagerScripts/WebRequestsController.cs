using UnityEngine;
using UnityEditor;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;


public class WebRequestsController : MonoBehaviour
{
    //private readonly string basePath = "https://api.com/endpoint";
    private readonly string basePath = "https://eokt2118w3m9y08.m.pipedream.net";
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public string GetFileContent()
    {
        return DataCollector.Instance.retriveContentFromFile();
    }

    public void Post()
    {
        // We can add default query string params for all requests
        RestClient.DefaultRequestParams["param1"] = "token";
        // RestClient.DefaultRequestParams["param2"] = "My other param";
        // retrive the csv as string and pass in the body of the POST request
        string fileContent = GetFileContent();
        // string fileContent = "ciao, ciao;";

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/endpoint",
            Params = new Dictionary<string, string>{
                {"param1", "sessionTokenValue"}
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
            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();
            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => this.LogMessage("Error", err.Message));
        // currentRequest = new RequestHelper
        // {
        //     Uri = basePath + "/posts",
        //     Params = new Dictionary<string, string> {
        //         { "param1", "value 1" },
        //         { "param2", "value 2" }
        //     },
        //     Body = new Post
        //     {
        //         title = "foo",
        //         body = "bar",
        //         userId = 1
        //     },
        //     EnableDebug = true
        // };
        // RestClient.Post<Post>(currentRequest)
        // .Then(res =>
        // {
        //     // And later we can clear the default query string params for all requests
        //     RestClient.ClearDefaultParams();
        //     this.LogMessage("Success", JsonUtility.ToJson(res, true));
        // })
        // .Catch(err => this.LogMessage("Error", err.Message));
    }
}