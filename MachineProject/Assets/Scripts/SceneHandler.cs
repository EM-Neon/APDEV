using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public void onStart()
    {
        SceneManager.LoadScene("level1");
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
        SceneManager.LoadScene("level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("level3");
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

    public void onEnd()
    {
        SceneManager.LoadScene("GameOverScene");
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
