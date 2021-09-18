using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMHandler : MonoBehaviour
{
    private static GameObject audioInstance;
    public AudioClip main;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    public AudioClip shop;
    public AudioSource bgm;
    public AudioClip onEnd;
    public bool isStopped = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (audioInstance == null)
        {
            audioInstance = gameObject;
        }
        else
        {
            Object.Destroy(gameObject);
        }
        main = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "Fire Crackle");
        level1 = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "Level 1");
        level2 = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "Level 2");
        level3 = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "Level 3");
        shop = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "Shop");
        onEnd = AssetBundleManager.assetInstance.GetAsset<AudioClip>("bgm", "End");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            if (SceneManager.GetSceneByName("MainMenu").isLoaded)
            {
                bgm.clip = main;
                bgm.volume = 0.2f;
                if (!bgm.isPlaying)
                    bgm.Play();
            }
            else if (SceneManager.GetSceneByName("level1").isLoaded)
            {
                bgm.clip = level1;
                bgm.volume = 0.1f;
                if (!bgm.isPlaying)
                {
                    bgm.Play();
                }
            }
            else if (SceneManager.GetSceneByName("level2").isLoaded)
            {
                bgm.clip = level2;
                bgm.volume = 0.1f;
                if (!bgm.isPlaying)
                {
                    bgm.Play();
                }
            }
            else if (SceneManager.GetSceneByName("level3").isLoaded)
            {
                bgm.clip = level3;
                bgm.volume = 0.1f;
                if (!bgm.isPlaying)
                {
                    bgm.Play();
                }
            }
            else if (SceneManager.GetSceneByName("ShopScene").isLoaded)
            {
                bgm.clip = shop;
                bgm.volume = 0.1f;
                if (!bgm.isPlaying)
                    bgm.Play();
            }

            else if (SceneManager.GetSceneByName("LevelScene").isLoaded)
            {
                bgm.Pause();
            }
        }
        else
        {
            bgm.Pause();
        }
       
    }
}
