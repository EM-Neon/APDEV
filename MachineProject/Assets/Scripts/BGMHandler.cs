using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMHandler : MonoBehaviour
{
    private static GameObject audioInstance;
    public AudioClip main;
/*    public AudioClip level1;*/
    public AudioClip shop;
    [SerializeField] private AudioSource bgm;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            bgm.clip = main;
            bgm.volume = 0.5f;
            if (!bgm.isPlaying)
                bgm.Play();
        }
/*        else if (SceneManager.GetSceneByName("Test").isLoaded)
        {
            bgm.clip = level1;
            bgm.volume = 0.1f;
        }*/
        else if (SceneManager.GetSceneByName("ShopScene").isLoaded)
        {
            bgm.clip = shop;
            bgm.volume = 0.1f;
            if (!bgm.isPlaying)
                bgm.Play();
        }

        else if(SceneManager.GetSceneByName("LevelScene").isLoaded)
        {
            bgm.Pause();
        }

    }
}
