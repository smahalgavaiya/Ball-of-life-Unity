using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public Animation bruteImage;
        
    public Animator BruteText;
    public Animator BruteBallAnimator;
    public Animator BruteBallText;
    public Animator alecImage,  AlecBallText, ballOfLifeImage;

    // Start is called before the first frame update
    void Start()
    {
        bruteImage.Play();
        BruteText.Play("introText1");
        StartCoroutine(introOne());
    }

    IEnumerator introOne()
    {
        yield return new WaitForSeconds(5);
        //Play intro 2
        BruteBallAnimator.Play("BruteBall");
        BruteBallText.Play("introText1");
        StartCoroutine(introSecond());
    }
    IEnumerator introSecond()
    {
        yield return new WaitForSeconds(5);
        alecImage.Play("AlecIntoBall");
        AlecBallText.Play("introText1");
        ballOfLifeImage.Play("BallAlphaZeroToOne");
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
