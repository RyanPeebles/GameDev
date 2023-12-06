using System;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public playerControl playerInfo;
    public Timer timer;
    public int goldAmount;
    public TMP_Text goldText;
    public TMP_Text totalText;
    public float timeTaken;
    public float totalScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        goldAmount = playerInfo.goldValue;
        goldText.text = "You colledted " + goldAmount.ToString() + " gold!";
        timeTaken = timer.time_elapsed;
        totalScore = goldAmount * (1 / timeTaken) * 10;
        totalText.text = "You scored " + Math.Round(totalScore).ToString() + " points!";

    }
}
