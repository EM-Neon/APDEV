using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text text;
    public InputField user_name;
    public Offline offlines;
    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("group_num", "6");
        PlayerParams.Add("user_name", user_name.text);
        PlayerParams.Add("score",  text.text);

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST");

        request.SetRequestHeader("Content-Type", "application/json");

        request.uploadHandler = new UploadHandlerRaw(requestData);

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleGetPlayersRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/6", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
            text.text = "";
            List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(request.downloadHandler.text);
            foreach (Dictionary<string, string> player in playerList)
            {
                Debug.Log($"Got player: {player["user_name"]}");
                text.text += $"{player["user_name"]} - {player["score"]}\n";
            }
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    public void CreatePlayer()
    {
        if (!offlines.hasInternet)
        {
            return;
        }
        StartCoroutine(SamplePostRoutine());
    }

    public void GetPlayers()
    {
        if (!offlines.hasInternet)
        {
            return;
        }
        StartCoroutine(SampleGetPlayersRoutine());
    }
}
