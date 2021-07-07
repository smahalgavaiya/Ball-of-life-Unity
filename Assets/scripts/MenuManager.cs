using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject howToPlayGO;
    public AudioSource AudioSource;
    public Toggle soundToggle;
    // Start is called before the first frame update
    void Start()
    {
        howToPlayGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        //Todo scenemanager load level 1
        SceneManager.LoadScene("Level_1");
    }

    public void setSound()
    {
        //Todo set sound on and off vice versa
        if (AudioSource.volume == 1)
        {
            soundToggle.isOn = false;
            AudioSource.volume = 0;
        }

        else
        {
            soundToggle.isOn = true;
            AudioSource.volume = 1;
        }
           

    }

    public void showHowToPlayGO() 
    {
        howToPlayGO.SetActive(true);
    }
    public void hideHowToPlayGO()
    {
        howToPlayGO.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
