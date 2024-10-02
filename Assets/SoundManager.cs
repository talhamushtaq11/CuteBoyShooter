using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MF{
[SerializeField]
public enum SoundClips
{
    button,
    MM,
    GP,
    play,
    gameOver,
    GameWin,
    Pause,
    Fire,
    Kill,
    EnemyDeath,
    CharacterJump,
    CharacterSkid,
    CharacterRun,
    CharacterIdle
};
public class SoundManager : MonoBehaviour
{
    
    public AudioClip[] allSfx;
    public AudioSource audio;

    public static SoundManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
               
    }

    public void playSFX(SoundClips clips)
    {
        audio.PlayOneShot(allSfx[((int)clips)]);
    }
    public void playSFXDelayedGO(float time)
    {
        Invoke("playGO", time);
    }
    void playGO()
    {
        Stop();
         audio.loop = true;
        audio.PlayOneShot(allSfx[((int)SoundClips.gameOver)]);
    }

    public void Stop()
    {
        audio.Stop();
    }
    public void playBgm(SoundClips clips)
    {
        AudioClip clip = (allSfx[((int)clips)]);
        audio.loop = true;
        audio.clip = clip;
        audio.Play();
    }
    public void StopBgm(SoundClips clips)
    {
        AudioClip clip = (allSfx[((int)clips)]);
        audio.Stop();
    }
}


};
