using UnityEngine;

public class Timer : MonoBehaviour {

    public int timeLimit;
    public float maxWidth;
    public GameObject timerBar;
    public TextMesh timeText;

    private float widthPerSecond;
    private float startTime;
    private bool expired;
    private bool pause;

    
    public void Begin()
    {
        expired = false;
        pause = false;
        startTime = Time.time;
    }

    public void Pause()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
        var remainingTime = int.Parse(timeText.text);
        var elapsedTime = timeLimit - remainingTime;
        startTime = Time.time - elapsedTime;
    }

    public bool Expired()
    {
        return expired;
    }

    private void Start()
    {
        widthPerSecond = (float)maxWidth / timeLimit;
        expired = false;
        pause = true;
    }

    private void Update() {
        if (!pause)
        {
            var currentSecond = Time.time - startTime;
            if (currentSecond > timeLimit)
            {
                if (!expired)
                {
                    timeText.text = "0";
                    expired = true;
                }
            }
            else
            {
                UpdateTimer(currentSecond);
            }
        }
	}

    private void UpdateTimer(float currentSecond)
    {
        timeText.text = (timeLimit - (int)currentSecond).ToString();
        var timerBarScale = timerBar.transform.localScale;
        timerBar.transform.localScale = new Vector3(
            timerBarScale.x,
            (currentSecond * widthPerSecond),
            timerBarScale.z
        ); ;
    }
}
