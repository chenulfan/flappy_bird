using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    private const string PLAY_GAME_SCENE_NAME = "GameScene";
    private const string TOP_SCORE_TITLE = "Top Results";
    private const int AMOUNT_OF_TOP_SCORES_TO_DISPLAY = 5;
    private const string ROUND_AMOUNT_KEY = "RoundAmount";
    public GameObject m_Menu;
    public GameObject m_PlayButton;    
    public GameObject m_Canvas;
    public GameObject m_HighScoreButton;
    public GameObject m_BackButton;
    public GameObject m_HighScoreResultText;
    public GameObject m_InfoText;
    public GameObject m_InfoButton;
    void HideMenu()
    {
        m_Menu.SetActive(false);
        m_PlayButton.SetActive(false);
        m_HighScoreButton.SetActive(false);
        m_InfoButton.SetActive(false);
    }
    void ShowMenu()
    {
        m_HighScoreResultText.SetActive(false);
        m_InfoText.SetActive(false);
        m_Menu.SetActive(true);
        m_PlayButton.SetActive(true);
        m_HighScoreButton.SetActive(true);
        m_InfoButton.SetActive(true);
    }

    public void PlayGame()
    {
        m_Canvas.SetActive(false);
        SceneManager.LoadScene(PLAY_GAME_SCENE_NAME);
    }
    public void GoBack()
    {
        ShowMenu();
        m_BackButton.SetActive(false);
    }
    private int[] calcTopScores()
    {
        int[] topScores = new int[AMOUNT_OF_TOP_SCORES_TO_DISPLAY];
        int amountOfRounds = PlayerPrefs.GetInt(ROUND_AMOUNT_KEY, 0);
        for (int j = 0; j < amountOfRounds + 1; j++) 
        {
            int roundJScore = PlayerPrefs.GetInt(j.ToString(), 0);
            for (int i = AMOUNT_OF_TOP_SCORES_TO_DISPLAY - 1; i >= 0; i--)
            {
                if (roundJScore > topScores[i]) 
                {
                    if (i == AMOUNT_OF_TOP_SCORES_TO_DISPLAY - 1) 
                    {
                        topScores[i] = roundJScore;
                    }
                    else
                    {
                        int temp = topScores[i];
                        topScores[i] = roundJScore;
                        topScores[i+1] = temp;
                    }
                }
            }

        }
        return topScores;
    }
    void updateTopScoresText(int[] topScores)
    {
        Text highScoreTextRef = m_HighScoreResultText.GetComponent<Text>();
        highScoreTextRef.text = $"{TOP_SCORE_TITLE}\n";
        string highScoreRawText = highScoreTextRef.text;
        for (int i = 0; i < AMOUNT_OF_TOP_SCORES_TO_DISPLAY; i++)
        {
            string currentScoreString = topScores[i] == 0 ? "-" : topScores[i].ToString();
            highScoreRawText += $"#{i+1}. {currentScoreString}\n";
        }
        highScoreTextRef.text = highScoreRawText;
    }
    void formatHighScore()
    {
        int[] highScores = calcTopScores();
        updateTopScoresText(highScores);
    }
    public void ShowHighScore()
    {
        m_BackButton.SetActive(true);
        formatHighScore();
        m_HighScoreResultText.SetActive(true);
        HideMenu();
    }
    public void ShowInfo()
    {
        m_BackButton.SetActive(true);
        m_InfoText.SetActive(true);
        HideMenu();
    }

    public void MainMenuButton()
    {
        m_Menu.SetActive(true);
        m_Menu.SetActive(false);
    }
}