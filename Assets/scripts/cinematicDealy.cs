using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematicDealy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        SceneManager.LoadScene("MainMenu");
    }
}
