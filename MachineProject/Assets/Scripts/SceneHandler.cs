using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public void onStart()
    {
        SceneManager.LoadScene("Test");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Level1()
    {
        /*SceneManager.LoadScene("GameScene");*/
        Debug.Log("Does Not Exist");
    }
    public void Level2()
    {
        Debug.Log("Does Not Exist");
    }
    public void Level3()
    {
        Debug.Log("Does Not Exist");
    }

    public void onShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionScene");
    }
    
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void fromNotif(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void toNotif(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void toAds(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
