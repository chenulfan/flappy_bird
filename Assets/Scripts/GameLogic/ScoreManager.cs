using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{ 
    public GameObject m_CurrentScoreText;
    private const int SCORE_ACHIEVED_PER_INCREASE = 1;
    private const string SCORE_TEXT = "Score: ";
    private const string ROUND_AMOUNT_KEY = "RoundAmount";

    private int ExtractScoreFromText(string i_RawText) {
        string numbersInText = string.Empty;
        foreach (char letter in i_RawText)
        {
            if (Char.IsDigit(letter))
            {
                numbersInText += letter;           
            }
        }
        return int.Parse(numbersInText);
    }
    private string FormatScore(int i_Score) 
    {
        return SCORE_TEXT + i_Score;
    }
    private int GetCurrentScore()
    {
        return ExtractScoreFromText(m_CurrentScoreText.GetComponent<Text>().text);
    }

    private int getRoundAmount()
    {
        int currentRoundAmount = PlayerPrefs.GetInt(ROUND_AMOUNT_KEY,0);
        return currentRoundAmount;
    }
    private void updateRoundNumber() 
    {
        int currentRoundAmount = getRoundAmount();
        PlayerPrefs.SetInt(ROUND_AMOUNT_KEY, currentRoundAmount + 1);
    }
    public void SaveScore () 
    {
        updateRoundNumber();
        int roundAmount = getRoundAmount();
        int currentScore = GetCurrentScore();
        PlayerPrefs.SetInt(roundAmount.ToString(), currentScore);
    }
    public void IncreaseScore()
    {
        Text currentDisplayTextRef = m_CurrentScoreText.GetComponent<Text>();
        string scoreText = currentDisplayTextRef.text;
        int currentScoreValue = ExtractScoreFromText(scoreText);
        currentScoreValue += SCORE_ACHIEVED_PER_INCREASE;
        currentDisplayTextRef.text = FormatScore(currentScoreValue);
    }
}
