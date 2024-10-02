using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.InfiniteRunnerEngine;
using MF;

public class GamplayController : MonoBehaviour
{
    public GameObject[] popUps;

    void Start()
    {
        MF.SoundManager.Instance.playBgm(SoundClips.GP);

    }

    public void RestartBtnClicked()
    {
        MF.SoundManager.Instance.playSFX(SoundClips.button);

        SceneManager.LoadScene("Gameplay");

    }
    public void HomeBtnClicked()
    {
         MF.SoundManager.Instance.playSFX(SoundClips.button);

        SceneManager.LoadScene("MainMenu");

    }
    public void ResumeBtnClicked()
    {



        Time.timeScale = 1;
        GameManager.Instance.UnPause();

         MF.SoundManager.Instance.playSFX(SoundClips.button);


    }
    public void PauseBtnClicked()
    {
        GameManager.Instance.Pause();
        popUps[0].SetActive(true);

        Time.timeScale = 0;
         MF.SoundManager.Instance.playSFX(SoundClips.button);


    }
    public void onMusicButtonClickEvents()
    {
        if ( MF.SoundManager.Instance.GetComponent<AudioSource>().isPlaying)
             MF.SoundManager.Instance.Stop();
        else
             MF.SoundManager.Instance.playBgm(SoundClips.GP);


    }

}
