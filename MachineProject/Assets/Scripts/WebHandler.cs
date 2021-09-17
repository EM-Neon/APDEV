using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;

public class Profile
{
    public string nickname;
    public string name;
    public string email;
    public string contact;
    public string id;

    public Profile(string _nickname, string _name, string _email, string _contact, string _id)
    {
        nickname = _nickname;
        name = _name;
        email = _email;
        contact = _contact;
        id = _id;
    }
}

public class WebHandler : MonoBehaviour
{
    [SerializeField] private InputField[] text;
    [SerializeField] private InputField[] edit;
    [SerializeField] private Dropdown down;

    private List<Profile> profiles = new List<Profile>();
    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("nickname", text[0].text);
        PlayerParams.Add("name", text[1].text);
        PlayerParams.Add("email", text[2].text);
        PlayerParams.Add("contact", text[3].text);


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
            profiles.Clear();
            Debug.Log($"Message: {request.downloadHandler.text}");
            List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(request.downloadHandler.text);
            foreach (Dictionary<string, string> player in playerList)
            {
                Debug.Log($"Got player: {player["nickname"]}");
                Profile profile = new Profile(player["nickname"], player["name"], player["email"], player["contact"], player["id"]);
                profiles.Add(profile);
                Dropdown.OptionData newOptions = new Dropdown.OptionData();
                newOptions.text = player["nickname"];
                down.options.Add(newOptions);

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

        PlayerParams.Add("nickname", edit[0].text);
        PlayerParams.Add("name", edit[1].text);
        PlayerParams.Add("email", edit[2].text);
        PlayerParams.Add("contact", edit[3].text);

        string requestString = JsonConvert.SerializeObject(PlayerParams);

        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/" + profiles[down.value].id, "PUT");

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
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/" + profiles[down.value-1].id, "DELETE");

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
        down.ClearOptions();
        for(int i = 0; i < edit.Length; i++)
        {
            edit[i].text = "";
        }
    }

    public void DeletePlayer()
    {
        StartCoroutine(SampleDeletePlayerRoutine());
        down.ClearOptions();
    }
}
