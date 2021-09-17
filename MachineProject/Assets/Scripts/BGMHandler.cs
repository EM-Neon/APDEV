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
    /*public AudioClip level3;*/
    public AudioClip shop;
    public AudioClip spawnSFX;
    public AudioClip throwSFX;
    private bool sfxPlaying = false;
    private float timer = 0;
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
        if (sfxPlaying)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 60.0f)
            {
                sfxPlaying = false;
                timer = 0;
            }
        }
        else
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
                if(!bgm.isPlaying)
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

    public void onSpawn()
    {
        bgm.Stop();
        bgm.clip = spawnSFX;
        sfxPlaying = true;
        bgm.Play();
    }

    public void onThrow()
    {
        bgm.Pause();
        bgm.clip = throwSFX;
        sfxPlaying = true;
        bgm.Play();
        
    }
}
