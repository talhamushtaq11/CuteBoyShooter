using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] CharacterBtns;
    public Sprite selected;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("price", 90000);
    }

    public void onCharacterSelection(int i)
    {
        foreach (GameObject gm in CharacterBtns)
        {
            gm.transform.GetChild(0).gameObject.SetActive(false);

        }
        CharacterBtns[i - 1].transform.GetChild(0).gameObject.SetActive(true);

        
        SoundManager.Instance.playSFX(SoundClips.button);

    }
    public void onCharacterPurchase(int i)
    {
        if (PlayerPrefs.GetInt("price") > 7000)
        {
            PlayerPrefs.SetInt("price", (PlayerPrefs.GetInt("price") - 7000));
            CharacterBtns[i].GetComponent<Button>().interactable = true;
            CharacterBtns[i].transform.GetChild(1).gameObject.SetActive(false);
            SoundManager.Instance.playSFX(SoundClips.button);

        }
        else
        {
            Debug.Log("NoAmmount");
            SoundManager.Instance.playSFX(SoundClips.button);

        }

    }

    public void playBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);
        SoundManager.Instance.Stop();

        SceneManager.LoadScene("Gameplay");

    }
    public void backBtnClicked()
    {
        SoundManager.Instance.playSFX(SoundClips.button);

        SceneManager.LoadScene("MainMenu");

    }
}
