using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TennisScoreManager : MonoBehaviour
{
    public Text player1GameScoreText;
    public Text player2GameScoreText;
    public Text player1SetScoreText;
    public Text player2SetScoreText;
    public Text matchResultText;

    // Scoring variables
    private int player1GameScore = 0;
    private int player2GameScore = 0;
    private int[] player1SetScores;
    private int[] player2SetScores;
    private int currentSet = 0;
    private bool isMatchOver = false;

    private const int gamesToWinSet = 6;
    private const int minimumAdvantage = 2;
    private const int setsToWinMatch = 3;
    private const int tiebreakGame = 7;

    void Start()
    {
        player1SetScores = new int[setsToWinMatch];
        player2SetScores = new int[setsToWinMatch];
        UpdateUI();
    }

    public void PlayerScored(int playerNumber)
    {
        if (isMatchOver) return;

        if (playerNumber == 1)
        {
            player1GameScore++;
        }
        else
        {
            player2GameScore++;
        }

        CheckGameScore();
    }

    private void CheckGameScore()
    {
        // Check for a tiebreak scenario
        if (player1SetScores[currentSet] == gamesToWinSet - 1 &&
            player2SetScores[currentSet] == gamesToWinSet - 1 &&
            currentSet == setsToWinMatch - 1)
        {
            if (player1GameScore >= tiebreakGame && player1GameScore - player2GameScore >= minimumAdvantage)
            {
                PlayerWinsGame(1);
            }
            else if (player2GameScore >= tiebreakGame && player2GameScore - player1GameScore >= minimumAdvantage)
            {
                PlayerWinsGame(2);
            }
        }
        else
        {
            if (player1GameScore >= gamesToWinSet && player1GameScore - player2GameScore >= minimumAdvantage)
            {
                PlayerWinsGame(1);
            }
            else if (player2GameScore >= gamesToWinSet && player2GameScore - player1GameScore >= minimumAdvantage)
            {
                PlayerWinsGame(2);
            }
        }

        UpdateUI();
    }

    private void PlayerWinsGame(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1SetScores[currentSet]++;
            player1GameScore = 0;
            player2GameScore = 0;
            if (player1SetScores[currentSet] >= gamesToWinSet)
            {
                PlayerWinsSet(1);
            }
        }
        else
        {
            player2SetScores[currentSet]++;
            player1GameScore = 0;
            player2GameScore = 0;
            if (player2SetScores[currentSet] >= gamesToWinSet)
            {
                PlayerWinsSet(2);
            }
        }
    }

    private void PlayerWinsSet(int playerNumber)
    {
        int playerSetScore = playerNumber == 1 ? player1SetScores[currentSet] : player2SetScores[currentSet];

        if (playerSetScore >= setsToWinMatch)
        {
            PlayerWinsMatch(playerNumber);
        }
        else
        {
            // Prepare for next set
            currentSet++;
            player1GameScore = 0;
            player2GameScore = 0;
        }

        UpdateUI();
    }

    private void PlayerWinsMatch(int playerNumber)
    {
        isMatchOver = true;
        matchResultText.text = "Player " + playerNumber + " Wins the Match!";
    }

    private void UpdateUI()
    {
        player1GameScoreText.text = TennisScoreToText(player1GameScore);
        player2GameScoreText.text = TennisScoreToText(player2GameScore);

        player1SetScoreText.text = "Sets: " + string.Join("-", player1SetScores);
        player2SetScoreText.text = "Sets: " + string.Join("-", player2SetScores);

        if (isMatchOver)
        {
            player1GameScoreText.enabled = false;
            player2GameScoreText.enabled = false;
        }
    }

    private string TennisScoreToText(int score)
    {
        switch (score)
        {
            case 0: return "Love";
            case 1: return "15";
            case 2: return "30";
            case 3: return "40";
            default: return score.ToString(); // For tiebreak scores
        }
    }
}
   
