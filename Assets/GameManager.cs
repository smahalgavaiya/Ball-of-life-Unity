using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager gmInstance { get { return _instance; } }

    public GameObject fristMinion, secondMinion, firstCheif, secondChief, brute;
    public Transform spwanLocation;
    public int generateCount = 10;
    public int fristMinionKillCount, secondMinionKillCount, firstCheifKillCount, secondChiefKillCount;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            fristMinionKillCount = secondMinionKillCount = firstCheifKillCount = secondChiefKillCount = 0;
            loadLevelOne();
        }
    }
    public void updateKillCount(bool isMinion)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_1":
                if(!isMinion)
                    firstCheifKillCount++;
                else 
                    fristMinionKillCount++;
                break;
            case "Level_2":
                 if (!isMinion)
                    secondChiefKillCount++;
                else
                    secondMinionKillCount++;
                break;
            case "Level_3":

                break;
        }
        if (secondMinionKillCount >= 7 && secondChiefKillCount >= 7)
        {
            secondMinionKillCount = secondChiefKillCount = 0;
            if (SceneManager.GetActiveScene().name != "Level_3")
            {
                SceneManager.LoadScene("Level_3");
                loadLevelThree();
            }
        }

        //Level one task done
        else if (fristMinionKillCount >= 5 && firstCheifKillCount >= 5)
        {
            if (SceneManager.GetActiveScene().name != "Level_2" && SceneManager.GetActiveScene().name != "Level_3")
            {
                SceneManager.LoadScene("Level_2");
                loadLevelTwo();
            }
        }
    
    }
    void loadLevelOne()
    {
        //generating first minions
        for (int i = 0; i < 5; ++i)
        {
            GameObject min = GameObject.Instantiate(fristMinion, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
        //generating first chiefs
        for (int i = 0; i < 5; ++i)
        {
            GameObject min = GameObject.Instantiate(firstCheif, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
    }
    void loadLevelTwo()
    {
        //generating first minions
        for (int i = 0; i < 7; ++i)
        {
            GameObject min = GameObject.Instantiate(secondMinion, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
        //generating first chiefs
        for (int i = 0; i < 7; ++i)
        {
            GameObject min = GameObject.Instantiate(secondChief, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
    }
    void loadLevelThree()
    {
        //generating first minions
        for (int i = 0; i < 30; ++i)
        {
            GameObject min = GameObject.Instantiate(secondMinion, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
        //generating first chiefs
        for (int i = 0; i < 2; ++i)
        {
            GameObject min = GameObject.Instantiate(secondChief, spwanLocation);
            min.transform.position = new Vector2(spwanLocation.transform.position.x + i * 1f, spwanLocation.transform.position.y + i * 1f);
        }
        //generating brute
        GameObject min2 = GameObject.Instantiate(brute, spwanLocation);
        min2.transform.position = new Vector2(spwanLocation.transform.position.x, spwanLocation.transform.position.y);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
