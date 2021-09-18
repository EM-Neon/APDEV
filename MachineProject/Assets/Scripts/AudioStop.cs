using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioStop : MonoBehaviour
{
    public BGMHandler bgm;
    public AudioSource spraysfx;
    public AnimationSFX sfx;

    private void Awake()
    {
        bgm.isStopped = true;
        spraysfx.Pause();
    }

    private void OnApplicationPause(bool pause)
    {
        bgm.isStopped = true;
        spraysfx.Pause();
        sfx.bgm.Pause();
    }

    public void onResume()
    {
        bgm.isStopped = false;
    }
}
