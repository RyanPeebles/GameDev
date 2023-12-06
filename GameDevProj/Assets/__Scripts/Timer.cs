using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text elapsedText;
    public float totalTime;
    public float time_elapsed;
    public float initial_value;
    public float min;
    public float sec;
    public float min_e;
    public float sec_e;

    // Start is called before the first frame update
    void Start()
    {
        initial_value = totalTime;
        if (PlayerPrefs.GetFloat("time") != 0) totalTime = PlayerPrefs.GetFloat("time");
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                time_elapsed = initial_value - totalTime;

            }
            min = Mathf.FloorToInt(totalTime / 60);
            sec = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
            min_e = Mathf.FloorToInt(time_elapsed / 60);
            sec_e = Mathf.FloorToInt(time_elapsed % 60);
            elapsedText.text = string.Format("Time taken: {0:00}:{1:00}", min_e, sec_e);

            PlayerPrefs.SetFloat("time", totalTime);
        }

    }
}
