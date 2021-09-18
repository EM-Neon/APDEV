using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AnimationSFX : MonoBehaviour
{
    public AudioClip spawnSFX;
    public AudioClip throwSFX;
    public AudioSource bgm;

    private void Start()
    {
        bgm.volume = 0.5f;
    }

    public void onSpawn()
    {
        bgm.clip = spawnSFX;
        bgm.Play();
    }

    public void onThrow()
    {
        bgm.clip = throwSFX;
        bgm.Play();
    }
}
