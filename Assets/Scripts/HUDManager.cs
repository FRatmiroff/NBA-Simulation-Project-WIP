using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // text mesh pro texts
    [Header("Text Mesh Pro Texts")]
    public TMP_Text playerScoreText;
    public TMP_Text opponentScoreText;
    public TMP_Text playerNameText;

    // team scores
    [Header("Team Scores")]
    public int playerScore;
    public int opponentScore;

    // scripts
    [Header("Scripts")]
    public GameManager gameManager;
    public PlayerMovement playerOneMovement;

    void Start()
    {
        // set team scores
        playerScore = 0;
        opponentScore = 0;

        // update team scores
        UpdatePlayerScore(0);
        UpdateOpponentScore(0);
        UpdatePlayerName(playerOneMovement);
    }

    // update score functions
    // add score parameter to score variable
    // update text
    // check if player/opponent won
    public void UpdatePlayerScore(int score)
    {
        playerScore += score;
        playerScoreText.text = "HOME: " + playerScore;
        gameManager.GameOverCheck();
    }

    public void UpdateOpponentScore(int score)
    {
        opponentScore += score;
        opponentScoreText.text = "AWAY: " + opponentScore;
    }

    public void UpdatePlayerName(PlayerMovement playerMovement)
    {
        playerNameText.text = playerMovement.playerName;
    }
}
