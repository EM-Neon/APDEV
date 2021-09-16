using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;

public class WebHandler : MonoBehaviour
{
    private Text nickname;
    private Text playerName;
    private Text email;
    private Text contact;
    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }
    public void setNickname(InputField text)
    {
        nickname.text = text.text;
    }
    public void playerNickname(InputField text)
    {
        playerName.text = text.text;
    }
    public void setEmail(InputField text)
    {
        email.text = text.text;
    }
    public void setContact(InputField text)
    {
        contact.text = text.text;
    }
    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("nickname", nickname.text);
        PlayerParams.Add("name", playerName.text);
        PlayerParams.Add("email", email.text);
        PlayerParams.Add("contact", contact.text);

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players" , "POST");

        request.SetRequestHeader("Content-Type", "application/json");

        request.uploadHandler = new UploadHandlerRaw(requestData);

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
        }
        else{
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleGetPlayersRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
            List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(request.downloadHandler.text);
            foreach(Dictionary<string, string> player in playerList)
            {
                Debug.Log($"Got player: {player["nickname"]}");
            }
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleGetPlayerRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
            Dictionary<string, string> player = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);

            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleEditPlayerRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("nickname", "6");
        PlayerParams.Add("name", "Shad");
        PlayerParams.Add("email", "100");
        PlayerParams.Add("contact", "100");

        string requestString = JsonConvert.SerializeObject(PlayerParams);

        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "PUT");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
            Dictionary<string, string> player = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);

            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleDeletePlayerRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores/10", "DELETE");

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

    public void CreatePlayer()
    {
        StartCoroutine(SamplePostRoutine());
    }

    public void GetPlayers()
    {
        StartCoroutine(SampleGetPlayersRoutine());
    }

    public void GetPlayer()
    {
        StartCoroutine(SampleGetPlayerRoutine());
    }

    public void EditPlayer()
    {
        StartCoroutine(SampleEditPlayerRoutine());
    }

    public void DeletePlayer()
    {
        StartCoroutine(SampleDeletePlayerRoutine());
    }
}
