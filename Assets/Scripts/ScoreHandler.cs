using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text leftScoreText;
    [SerializeField] private TMP_Text rightScoreText;

    private int leftScore = 0;
    private int rightScore = 0;
    


    private void Update()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();

    }
    public enum Players
    {
        Left,
        Right
    }

    public void IncreaseScore(Players player)
    {
        if (player == Players.Right)
        {
            rightScore += 1;
        }
        else if (player == Players.Left)
        {
            leftScore += 1;
        }
    }
}
