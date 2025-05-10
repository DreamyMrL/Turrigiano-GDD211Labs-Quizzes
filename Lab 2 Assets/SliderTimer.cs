using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SliderTimer : MonoBehaviour
{
    public Slider fillSlider;
    public Button stopButton;
    public TMP_Text Indicator;
    public TMP_Text scoreText;
    public TMP_Text roundText;
    public int score;
    public int rounds;

    private bool isFilling = true;
    private bool hasStopped = false;
    public float fillSpeed = 50f;
    private float resetDelay = 1.0f;

    void Start()
    {
        StartCoroutine(Startup());
        score = 0;
        rounds = 1;
        roundText.text = "Round: " + rounds;
        Indicator.text = "Wait for it...";
        if (fillSlider != null)
        {
            fillSlider.value = 0f;
        }
        if (stopButton != null)
        {
            stopButton.onClick.AddListener(StopAndResetFilling);
        }
    }

    IEnumerator Startup()
    {
        yield return new WaitForSeconds(2);
    }

    void Update()
    {
        if (isFilling && fillSlider.value < fillSlider.maxValue)
        {
            fillSlider.value += fillSpeed * Time.deltaTime;
        }
        if (fillSlider.value >= fillSlider.maxValue && !hasStopped)
        {
            StopAndResetFilling();
        }
    }

    // Function to stop and reset the slider after a delay
    private void StopAndResetFilling()
    {
        if (hasStopped) return;

        hasStopped = true;
        isFilling = false;
        PointGive();
        rounds += 1;
        roundText.text = "Round: " + rounds;
        if (rounds <= 10)
        {
            StartCoroutine(ResetSliderAfterDelay());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    // Coroutine to reset the slider after a delay
    private IEnumerator ResetSliderAfterDelay()
    {
        fillSpeed = Random.Range(40, 70);
        scoreText.text = "Score: " + score;
        yield return new WaitForSeconds(resetDelay);
        Indicator.text = "Wait for it...";

        // Reset the slider and start filling again
        fillSlider.value = 0f;
        isFilling = true;
        hasStopped = false;
    }

    void PointGive()
    {
        if (fillSlider.value >= 98f)
        {
            if(fillSlider.value >= 100f)
            {
                Indicator.text = "Too late!";
            }
            else
            {
                Indicator.text = "Excellent! +5";
                score += 5;
            }
        }
        else if (fillSlider.value >= 95f)
        {
            Indicator.text = "Great! +3";
            score += 3;
        }
        else if (fillSlider.value >= 90f)
        {
            Indicator.text = "Good! +2";
            score += 2;
        }
        else if (fillSlider.value >= 85f)
        {
            Indicator.text = "Ok! +1";
            score += 1;
        }
        else if (fillSlider.value < 85f)
        {
            Indicator.text = "Too early!";
        }
    }

    IEnumerator GameOver()
    {
        Indicator.text = "GAME OVER";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }
}
