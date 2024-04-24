using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    public int playerNumber; // Assign this in the inspector to differentiate between player 1 and player 2's goals.

    private enum ScoringState
    {
        Love,
        Fifteen,
        Thirty,
        Forty,
        Advantage,
        GamePoint
    }

    private ScoringState[] playerScores = new ScoringState[2]; // Scores for each player
    private int[] gamesWon = new int[2]; // Games won per player
    private int[] setsWon = new int[2]; // Sets won per player
    private int currentSet = 0;
    private bool matchOver = false;

    private const int pointsToWinGame = 4;
    private const int gamesToWinSet = 6;
    private const int setsToWinMatch = 3;
    private const int minimumAdvantage = 2;

    void Start()
    {
        // Initialization if needed
        playerScores[0] = ScoringState.Love;
        playerScores[1] = ScoringState.Love;
        Debug.Log("Game started: Both players start at Love");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !matchOver)
        {
            Debug.Log($"Player {playerNumber} scored a goal!");
            PlayerScores(playerNumber - 1);
            BallController ballController = other.GetComponent<BallController>();
            if (ballController != null)
            {
                ballController.ResetBall();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GoalPost")
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.velocity = new Vector3(0, 0, 5); // Optionally change the velocity
            }
        }
    }

    private void PlayerScores(int playerIndex)
    {
        Debug.Log($"Player {playerIndex + 1} scores, updating game score");
        UpdateGameScore(playerIndex, 1 - playerIndex);
    }

    private void UpdateGameScore(int scorerIndex, int opponentIndex)
    {
        ScoringState scorer = playerScores[scorerIndex];
        ScoringState opponent = playerScores[opponentIndex];
        Debug.Log($"Updating game score from {scorer} for scorer and {opponent} for opponent");

        switch (scorer)
        {
            case ScoringState.Love:
                scorer = ScoringState.Fifteen;
                Debug.Log("Scorer moves from Love to Fifteen");
                break;
            case ScoringState.Fifteen:
                scorer = ScoringState.Thirty;
                Debug.Log("Scorer moves from Fifteen to Thirty");
                break;
            case ScoringState.Thirty:
                scorer = ScoringState.Forty;
                Debug.Log("Scorer moves from Thirty to Forty");
                break;
            case ScoringState.Forty:
                if (opponent == ScoringState.Forty || opponent == ScoringState.Advantage)
                {
                    scorer = ScoringState.Advantage;
                    opponent = (opponent == ScoringState.Advantage) ? ScoringState.Forty : opponent;
                    Debug.Log("Deuce: Scorer moves to Advantage");
                }
                else
                {
                    scorer = ScoringState.GamePoint;
                    Debug.Log("Scorer achieves Game Point");
                }
                break;
            case ScoringState.Advantage:
                scorer = ScoringState.GamePoint;
                Debug.Log("Scorer moves from Advantage to Game Point");
                break;
            case ScoringState.GamePoint:
                // Game should be won before this point.
                Debug.Log("Game Point state reached - game logic check needed");
                break;
            default:
                Debug.Log("Unhandled scoring state");
                break;
        }

        playerScores[scorerIndex] = scorer;
        playerScores[opponentIndex] = opponent;

        if (scorer == ScoringState.GamePoint)
        {
            gamesWon[scorerIndex]++;
            Debug.Log($"Player {scorerIndex + 1} wins the game, total games won: {gamesWon[scorerIndex]}");
            CheckGameSetMatch(scorerIndex);
        }
    }

    private void CheckGameSetMatch(int scorerIndex)
    {
        if (gamesWon[scorerIndex] >= gamesToWinSet && gamesWon[scorerIndex] - gamesWon[1 - scorerIndex] >= minimumAdvantage)
        {
            setsWon[scorerIndex]++;
            gamesWon[0] = 0;
            gamesWon[1] = 0;
            Debug.Log($"Set won by player {scorerIndex + 1}, sets won: {setsWon[scorerIndex]}");
            if (setsWon[scorerIndex] == setsToWinMatch)
            {
                Debug.Log($"Match won by player {scorerIndex + 1}");
                matchOver = true;
            }
            else
            {
                Debug.Log($"New set starts for players");
            }
        }
        ResetGameScore();
    }

    private void ResetGameScore()
    {
        playerScores[0] = ScoringState.Love;
        playerScores[1] = ScoringState.Love;
        Debug.Log("Scores reset: Both players return to Love");
    }
}