using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GamplayController : MonoBehaviour
{
    public GameObject[] popUps;

    void Start()
    {
        SoundManager.Instance.playBgm(SoundClips.GP);

    }

    public void RestartBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);

        SceneManager.LoadScene("Gameplay");

    }
    public void HomeBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);

        SceneManager.LoadScene("MainMenu");

    }
    public void ResumeBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);


        Time.timeScale = 1;

    }
    public void PauseBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);
        popUps[0].SetActive(true);

        Time.timeScale = 0;

    }
    public void onMusicButtonClickEvents()
    {
        if (SoundManager.Instance.audio.isPlaying)
            SoundManager.Instance.Stop();
        else
            SoundManager.Instance.playBgm(SoundClips.GP);


    }

}
