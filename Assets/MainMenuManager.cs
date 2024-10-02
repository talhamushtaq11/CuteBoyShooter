using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MF;

public class MainMenuManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.playBgm(SoundClips.MM);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onButtonClickEvents(int i)
    {
        
        switch (i)
        {
            case 1: Application.OpenURL("https://play.google.com/store/apps/details?id=com.aenstudios.cuteboyshooter");
                SoundManager.Instance.playSFX(SoundClips.button);
                break;
            case 2:
                Debug.Log("achievements");
                SoundManager.Instance.playSFX(SoundClips.button);
                break;
            case 3:
                SoundManager.Instance.playSFX(SoundClips.button);
                SceneManager.LoadScene("characterSelection");
                break;
            case 4:
                if (SoundManager.Instance.audio.isPlaying)
                    SoundManager.Instance.Stop();
                else
                    SoundManager.Instance.playBgm(SoundClips.GP);
                break;
        }
        
    }
   
}
