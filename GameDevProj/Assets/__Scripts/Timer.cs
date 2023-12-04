using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text elapsedText;
    public float totalTime;
    public float time_elapsed;
    public float initial_value;
    // Start is called before the first frame update
    void Start()
    {
        initial_value = totalTime;
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
            float min = Mathf.FloorToInt(totalTime / 60);
            float sec = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0,00}:{1,00}", min, sec);
            float min_e = Mathf.FloorToInt(time_elapsed / 60);
            float sec_e = Mathf.FloorToInt(time_elapsed % 60);
            elapsedText.text = string.Format("Elapsed Time: {0,00}:{1,00}", min_e, sec_e);
        }
    }
}
