using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Panels
    [Header("Panels")]
    public GameObject defensePanel;
    public GameObject gameOverPanel;

    // text mesh pro texts
    [Header("Text Mesh Pro Texts")]
    public TMP_Text defenseText;
    public TMP_Text gameOverText;

    // name of defender
    [Header("Name")]
    string[] names = {"Butler", "Adebayo", "Haliburton", "James", "Curry", "Fox", "Tatum", 
                      "Banchero", "Young", "Durant", "Booker", "Antetekounmpo", "Lillard", 
                      "Mitchell", "Gilgeous-Alexander", "Wembanyama", "Leonard", "Doncic", 
                      "Irving", "Kuzma", "Poole", "Brown", "Brunson", "Edwards", "Ball", 
                      "Morant", "Barnes", "Williamson", "Embiid", "Jokic"};
    [SerializeField] string defenderName;

    // final score
    [Header("Final Game Score")]
    [SerializeField] int finalScore;
    
    // scripts
    [Header("Scripts")]
    public PlayerMovement playerOneMovement;
    public PlayerMovement playerTwoMovement;
    public PlayerMovement playerThreeMovement;
    public PlayerMovement playerFourMovement;
    public PlayerMovement playerFiveMovement;
    public defenderLogic defenderOneLogic;
    public defenderLogic defenderTwoLogic;
    public defenderLogic defenderThreeLogic;
    public defenderLogic defenderFourLogic;
    public defenderLogic defenderFiveLogic;
    public ControlManager controlManager;
    public ballScript ballScript;
    public HUDManager HUDManager;
    public PassingManager passingManager;
    public OffensivePlayerLogic offensivePlayerLogic;

    // gameobjects
    [Header("GameObjects")]
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    public GameObject playerFive;
    public GameObject defenderOne;
    public GameObject defenderTwo;
    public GameObject defenderThree;
    public GameObject defenderFour;
    public GameObject defenderFive;
    public GameObject basketball;

    [Header("Starting Positions")]
    public Vector2 PlayerOneStartingPos;
    public Vector2 PlayerTwoStartingPos;
    public Vector2 PlayerThreeStartingPos;
    public Vector2 PlayerFourStartingPos;
    public Vector2 PlayerFiveStartingPos;
    public Vector2 DefenderOneStartingPos;
    public Vector2 DefenderTwoStartingPos;
    public Vector2 DefenderThreeStartingPos;
    public Vector2 DefenderFourStartingPos;
    public Vector2 DefenderFiveStartingPos;

    // game stats
    [Header("Player In-Game Stats")]
    public int P1Pts, P2Pts, P3Pts, P4Pts, P5Pts;
    public int P1Reb, P2Reb, P3Reb, P4Reb, P5Reb;
    public int P1Ast, P2Ast, P3Ast, P4Ast, P5Ast;
    int playersWithRankFive;
    int playersWithRankFour;
    int playersWithRankThree;
    int playersWithRankTwo;
    int playersWithRankOne;
    int randomPlayer;

    // gameover check
    [Header("Game Over Check")]
    public bool gameOver;

    // player heights and conditions
    public static int playerOneHeight, playerTwoHeight, playerThreeHeight, playerFourHeight, playerFiveHeight;
    public static int playerOneCondition, playerTwoCondition, playerThreeCondition, playerFourCondition, playerFiveCondition;

    // defender attributes
    public static float defenderOneCloseOutSpeedMultiplier, defenderTwoCloseOutSpeedMultiplier, defenderThreeCloseOutSpeedMultiplier, defenderFourCloseOutSpeedMultiplier, defenderFiveCloseOutSpeedMultiplier;
    public static float defenderOneSmoothTime, defenderTwoSmoothTime, defenderThreeSmoothTime, defenderFourSmoothTime, defenderFiveSmoothTime;
    public static float defenderOneDistScaleFactor, defenderTwoDistScaleFactor, defenderThreeDistScaleFactor, defenderFourDistScaleFactor, defenderFiveDistScaleFactor;
    public static float defenderOneMaintainDistanceBase, defenderTwoMaintainDistanceBase, defenderThreeMaintainDistanceBase, defenderFourMaintainDistanceBase, defenderFiveMaintainDistanceBase;
    
    void Start()
    {
        // choose defender name randomly from list
        defenderName = names[Random.Range(0, names.Length - 1)];

        // turn off panels
        defensePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // set game over to false
        gameOver = false;

        defenderOneLogic.AssignDefenderNum(1);
        defenderTwoLogic.AssignDefenderNum(2);
        defenderThreeLogic.AssignDefenderNum(3);
        defenderFourLogic.AssignDefenderNum(4);
        defenderFiveLogic.AssignDefenderNum(5);

        // set in game stats to zero
        P1Pts = 0;
        P2Pts = 0;
        P3Pts = 0;
        P4Pts = 0;
        P5Pts = 0;
        
        P1Reb = 0;
        P2Reb = 0;
        P3Reb = 0;
        P4Reb = 0;
        P5Reb = 0;

        P1Ast = 0;
        P2Ast = 0;
        P3Ast = 0;
        P4Ast = 0;
        P5Ast = 0;

        // starting positions
        // idea for starting positions to add different "sets"
        // where the players dont just have to be in 5 out but also
        // have like a set where theres two guys in the post or
        // some type of iso
        PlayerOneStartingPos = new Vector2(0f, -4f);
        PlayerTwoStartingPos = new Vector2(4.5f, -3.5f);
        PlayerThreeStartingPos = new Vector2(-4.5f, -3.5f);
        PlayerFourStartingPos = new Vector2(-6.5f, 1.5f);
        PlayerFiveStartingPos = new Vector2(6.5f, 1.5f);

        DefenderOneStartingPos = new Vector2(0f, -2f);
        DefenderTwoStartingPos = new Vector2(3.5f, -1.5f);
        DefenderThreeStartingPos = new Vector2(-3.5f, -1.5f);
        DefenderFourStartingPos = new Vector2(-4.5f, 1.5f);
        DefenderFiveStartingPos = new Vector2(4.5f, 1.5f);
    }

    // game state functions
    public void DefenseScreen()
    {
        Debug.Log("Stop");
        StopCoroutine(delay());
        defensePanel.SetActive(true);
        DefenseText();
        playerOneMovement.defense = true;
        controlManager.defense = true;
    }

    public void Continue()
    {
        GameOverCheck();
        defensePanel.SetActive(false);
        if(!gameOver)
        {
            playerOneMovement.defense = false;
            controlManager.defense = false;
            ballScript.possessionOver = false;
            controlManager.shotMade = false;
            Reset();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PossessionOver()
    {
        StartCoroutine(delay());
    }

    // add delay before starting defense
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.0f);
        if(!gameOver)
        {
            DefenseScreen();
        }
    }

    // set events for defense
    void DefenseText()
    {
        controlManager.isDragging = false;
        int rand = Random.Range(1, 7);

        if(rand == 1)
        {
            defenseText.text = defenderName + " drove to the paint and layed it up";
            HUDManager.UpdateOpponentScore(2);
            Debug.Log("drove");
        }
        else if(rand == 2)
        {
            defenseText.text = defenderName + " sank the midrange dribble pull-up";
            HUDManager.UpdateOpponentScore(2);
            Debug.Log("pull up");
        }
        else if(rand == 3)
        {
            defenseText.text = defenderName + " hits a step back three";
            HUDManager.UpdateOpponentScore(3);
            Debug.Log("three");
        }
        else
        {
            defenseText.text = "Miss from " + defenderName;
            AddReboundToPlayer();
        }
    }

    // reset offensive possession
    void Reset()
    {
        randomPlayer = Random.Range(1, 6);
        playerOne.transform.position = PlayerOneStartingPos;
        playerTwo.transform.position = PlayerTwoStartingPos;
        playerThree.transform.position = PlayerThreeStartingPos;
        playerFour.transform.position = PlayerFourStartingPos;
        playerFive.transform.position = PlayerFiveStartingPos;
        defenderOne.transform.position = DefenderOneStartingPos;
        defenderTwo.transform.position = DefenderTwoStartingPos;
        defenderThree.transform.position = DefenderThreeStartingPos;
        defenderFour.transform.position = DefenderFourStartingPos;
        defenderFive.transform.position = DefenderFiveStartingPos;

        RecoverStamina(1, playerOneMovement);
        RecoverStamina(2, playerTwoMovement);
        RecoverStamina(3, playerThreeMovement);
        RecoverStamina(4, playerFourMovement);
        RecoverStamina(5, playerFiveMovement);

        passingManager.playerBeingControlled = randomPlayer;
        playerOneMovement.beingControlled = randomPlayer == 1;
        playerTwoMovement.beingControlled = randomPlayer == 2;
        playerThreeMovement.beingControlled = randomPlayer == 3;
        playerFourMovement.beingControlled = randomPlayer == 4;
        playerFiveMovement.beingControlled = randomPlayer == 5;
        if(randomPlayer == 1)
        {
            HUDManager.UpdatePlayerName(playerOneMovement);
        }
        else if(randomPlayer == 2)
        {
            HUDManager.UpdatePlayerName(playerTwoMovement);
        }
        else if(randomPlayer == 3)
        {
            HUDManager.UpdatePlayerName(playerThreeMovement);
        }
        else if(randomPlayer == 4)
        {
            HUDManager.UpdatePlayerName(playerFourMovement);
        }
        else if(randomPlayer == 5)
        {
            HUDManager.UpdatePlayerName(playerFiveMovement);
        }
        
        ballScript.isDribbling = true;
        controlManager.possession = true;
        controlManager.ballGrounded = false;
    }

    void RecoverStamina(int playerNum, PlayerMovement player)
    {
        if(passingManager.playerBeingControlled != playerNum && player.stamina <= 0.97f)
        {
            player.stamina += 0.03f;
        }
    }

    // check if game is over
    public void GameOverCheck()
    {
        if(HUDManager.playerScore >= finalScore)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "You Win";
            MainMenuScript.championshipGameWon = false;
            if(MainMenuScript.postSeasonGameNumber == 4)
            {
                PlayerPrefs.SetInt("CreditsPref", MainMenuScript.Credits + 3);
                MainMenuScript.championshipGameWon = true;
                SeasonSimulationScript.endPostSeason = true;
            } else {
                PlayerPrefs.SetInt("CreditsPref", MainMenuScript.Credits + GetProfit(true));
                PlayerPrefs.SetInt("GameNumberPref", MainMenuScript.gameNumber + 1);
            }

            if(MainMenuScript.postSeasonGameNumber > 0)
            {
                UpdatePlayerPositionInPlayoffs();
            }
            
            SetPostGameStats();
            SeasonSimulationScript.playerGameWon = true;
            SeasonSimulationScript.callSimWeek = true;
            PlayerPrefs.SetInt("NextTeamPref", SeasonSimulationScript.nextTeam + 1);
            MainMenuScript.backFromGame = true;
            gameOver = true;
        }
        else if(HUDManager.opponentScore >= finalScore)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "You Lose";
            if(MainMenuScript.postSeasonGameNumber > 0)
            {
                SeasonSimulationScript.endPostSeason = true;
            } else {
                PlayerPrefs.SetInt("GameNumberPref", MainMenuScript.gameNumber + 1);
            }
            
            SetPostGameStats();
            SeasonSimulationScript.playerGameWon = false;
            SeasonSimulationScript.callSimWeek = true;
            PlayerPrefs.SetInt("CreditsPref", MainMenuScript.Credits + GetProfit(false));
            PlayerPrefs.SetInt("NextTeamPref", SeasonSimulationScript.nextTeam + 1);
            MainMenuScript.backFromGame = true;
            gameOver = true;
        }
    }

    void UpdatePlayerPositionInPlayoffs()
    {
        int playerSeed = SeasonSimulationScript.teamsTemp[MainMenuScript.currentTeam - 1].getSeed();
        int playerID = SeasonSimulationScript.teamsTemp[MainMenuScript.currentTeam - 1].getID();

        if(MainMenuScript.postSeasonGameNumber == 1)
        {
            if(playerSeed == 1 || playerSeed == 8)
            {
                Debug.Log("Bracket One");
                PlayerPrefs.SetInt("PlayoffsRoundOneBracketOneWinnerPref", playerID);
            }
            else if(playerSeed == 4 || playerSeed == 5)
            {
                Debug.Log("Bracket Two");
                PlayerPrefs.SetInt("PlayoffsRoundOneBracketTwoWinnerPref", playerID);
            }
            else if(playerSeed == 3 || playerSeed == 6)
            {
                Debug.Log("Bracket Three");
                PlayerPrefs.SetInt("PlayoffsRoundOneBracketThreeWinnerPref", playerID);
            }
            else
            {
                Debug.Log("Bracket Four");
                PlayerPrefs.SetInt("PlayoffsRoundOneBracketFourWinnerPref", playerID);
            }
            Debug.Log("One: " + PlayerPrefs.GetInt("PlayoffsRoundOneBracketOneWinnerPref"));
            Debug.Log("Two: " + PlayerPrefs.GetInt("PlayoffsRoundOneBracketTwoWinnerPref"));
            Debug.Log("Three: " + PlayerPrefs.GetInt("PlayoffsRoundOneBracketThreeWinnerPref"));
            Debug.Log("Four: " + PlayerPrefs.GetInt("PlayoffsRoundOneBracketFourWinnerPref"));
        }
        else if(MainMenuScript.postSeasonGameNumber == 2)
        {
            if(PlayerPrefs.GetInt("PlayoffsRoundOneBracketOneWinnerPref") == playerID || PlayerPrefs.GetInt("PlayoffsRoundOneBracketTwoWinnerPref") == playerID)
            {
                PlayerPrefs.SetInt("PlayoffsRoundTwoBracketOneWinnerPref", playerID);
            }
            else if(PlayerPrefs.GetInt("PlayoffsRoundOneBracketThreeWinnerPref") == playerID || PlayerPrefs.GetInt("PlayoffsRoundOneBracketFourWinnerPref") == playerID)
            {
                PlayerPrefs.SetInt("PlayoffsRoundTwoBracketTwoWinnerPref", playerID);
            }
        }
    }

    public GameObject GetDefenderOnBallGO()
    {
        if(passingManager.playerBeingControlled == defenderOneLogic.defenderNum)
        {
            return defenderOne;
        } 
        else if(passingManager.playerBeingControlled == defenderTwoLogic.defenderNum)
        {
            return defenderTwo;
        }
        else if(passingManager.playerBeingControlled == defenderThreeLogic.defenderNum)
        {
            return defenderThree;
        }
        else if(passingManager.playerBeingControlled == defenderFourLogic.defenderNum)
        {
            return defenderFour;
        }
        else
        {
            return defenderFive;
        }
    }

    public void AddPointsToPlayer(int pts)
    {
        if(passingManager.playerBeingControlled == 1)
        {
            P1Pts += pts;
        }
        else if(passingManager.playerBeingControlled == 2)
        {
            P2Pts += pts;
        }
        else if(passingManager.playerBeingControlled == 3)
        {
            P3Pts += pts;
        }
        else if(passingManager.playerBeingControlled == 4)
        {
            P4Pts += pts;
        }
        else if(passingManager.playerBeingControlled == 5)
        {
            P5Pts += pts;
        }
    }

    public void AddAssistToPlayer(int playerThatAssisted)
    {
        if(playerThatAssisted == 1 &&  passingManager.assistCooldown > 0f)
        {
            P1Ast++;
        }
        else if(playerThatAssisted == 2 &&  passingManager.assistCooldown > 0f)
        {
            P2Ast++;
        }
        else if(playerThatAssisted == 3 &&  passingManager.assistCooldown > 0f)
        {
            P3Ast++;
        }
        else if(playerThatAssisted == 4 &&  passingManager.assistCooldown > 0f)
        {
            P4Ast++;
        }
        else if(playerThatAssisted == 5 &&  passingManager.assistCooldown > 0f)
        {
            P5Ast++;
        }
    }

    void AddReboundToPlayer()
    {
        int playerOneRanking = DetermineReboundRanking(playerOneHeight);
        int playerTwoRanking = DetermineReboundRanking(playerTwoHeight);
        int playerThreeRanking = DetermineReboundRanking(playerThreeHeight);
        int playerFourRanking = DetermineReboundRanking(playerFourHeight);
        int playerFiveRanking = DetermineReboundRanking(playerFiveHeight);
        Debug.Log("1: " + playerOneRanking + ", 2: " + playerTwoRanking + ", 3: " + playerThreeRanking + ", 4: " + playerFourRanking + ", 5: " + playerFiveRanking); 

        int ranksTotal = playerOneRanking + playerTwoRanking + playerThreeRanking + playerFourRanking + playerFiveRanking;
        int chance = Random.Range(1, ranksTotal + 1);
        Debug.Log("rankstotal: " + ranksTotal + ", chance: " + chance);

        if(chance <= playerOneRanking)
        {
            P1Reb++;
        }
        else if(chance <= playerOneRanking + playerTwoRanking && chance > playerOneRanking)
        {
            P2Reb++;
        }
        else if(chance <= playerOneRanking + playerTwoRanking + playerThreeRanking && chance > playerOneRanking + playerTwoRanking)
        {
            P3Reb++;
        }
        else if(chance <= playerOneRanking + playerTwoRanking + playerThreeRanking + playerFourRanking && chance > playerOneRanking + playerTwoRanking + playerThreeRanking)
        {
            P4Reb++;
        }
        else
        {
            P5Reb++;
        }
    }

    int DetermineReboundRanking(int height)
    {
        if(height >= 84)
        {
            return 10;
        }
        else if(height >= 81)
        {
            return 8;
        }
        else if(height >= 78)
        {
            return 6;
        }
        else if(height >= 75)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }

    void SetPostGameStats()
    {
        if(MainMenuScript.P1SpotFilled == 1)
        {
            MainMenuScript.playerOnePoints = P1Pts;
            MainMenuScript.playerOneReb = P1Reb;
            MainMenuScript.playerOneAst = P1Ast;
        }
        if(MainMenuScript.P2SpotFilled == 1)
        {
            MainMenuScript.playerTwoPoints = P2Pts;
            MainMenuScript.playerTwoReb = P2Reb;
            MainMenuScript.playerTwoAst = P2Ast;
        }
        if(MainMenuScript.P3SpotFilled == 1)
        {
            MainMenuScript.playerThreePoints = P3Pts;
            MainMenuScript.playerThreeReb = P3Reb;
            MainMenuScript.playerThreeAst = P3Ast;
        }
        if(MainMenuScript.P4SpotFilled == 1)
        {
            MainMenuScript.playerFourPoints = P4Pts;
            MainMenuScript.playerFourReb = P4Reb;
            MainMenuScript.playerFourAst = P4Ast;
        }
        if(MainMenuScript.P5SpotFilled == 1)
        {
            MainMenuScript.playerFivePoints = P5Pts;
            MainMenuScript.playerFiveReb = P5Reb;
            MainMenuScript.playerFiveAst = P5Ast;
        }
    }

    public float GetCloseOutSpeedMultiplierValue(int defenderNum)
    {
        if(defenderNum == 1)
        {
            return defenderOneCloseOutSpeedMultiplier;
        }
        else if(defenderNum == 2)
        {
            return defenderTwoCloseOutSpeedMultiplier;
        }
        else if(defenderNum == 3)
        {
            return defenderThreeCloseOutSpeedMultiplier;
        }
        else if(defenderNum == 4)
        {
            return defenderFourCloseOutSpeedMultiplier;
        }
        else{
            return defenderFiveCloseOutSpeedMultiplier;
        }
    }

    public float GetSmoothTimeValue(int defenderNum)
    {
        if(defenderNum == 1)
        {
            return defenderOneSmoothTime;
        }
        else if(defenderNum == 2)
        {
            return defenderTwoSmoothTime;
        }
        else if(defenderNum == 3)
        {
            return defenderThreeSmoothTime;
        }
        else if(defenderNum == 4)
        {
            return defenderFourSmoothTime;
        }
        else{
            return defenderFiveSmoothTime;
        }
    }

    public float GetDistScaleFactorValue(int defenderNum)
    {
        if(defenderNum == 1)
        {
            return defenderOneDistScaleFactor;
        }
        else if(defenderNum == 2)
        {
            return defenderTwoDistScaleFactor;
        }
        else if(defenderNum == 3)
        {
            return defenderThreeDistScaleFactor;
        }
        else if(defenderNum == 4)
        {
            return defenderFourDistScaleFactor;
        }
        else{
            return defenderFiveDistScaleFactor;
        }
    }

    public float GetMaintainDistanceBaseValue(int defenderNum)
    {
        if(defenderNum == 1)
        {
            return defenderOneMaintainDistanceBase;
        }
        else if(defenderNum == 2)
        {
            return defenderTwoMaintainDistanceBase;
        }
        else if(defenderNum == 3)
        {
            return defenderThreeMaintainDistanceBase;
        }
        else if(defenderNum == 4)
        {
            return defenderFourMaintainDistanceBase;
        }
        else{
            return defenderFiveMaintainDistanceBase;
        }
    }

    public int GetProfit(bool win)
    {
        float conditionEffect = 0f;
        int conditionSum = playerOneCondition + playerTwoCondition + playerThreeCondition + playerFourCondition + playerFiveCondition;
        if(conditionSum > 400)
        {
            conditionEffect = 1.0f;
        }
        else if(conditionSum > 300)
        {
            conditionEffect = 0.8f;
        }
        else if(conditionSum > 200)
        {
            conditionEffect = 0.6f;
        }
        else if(conditionSum > 100)
        {
            conditionEffect = 0.4f;
        }
        else if(conditionSum >= 0)
        {
            conditionEffect = 0.2f;
        }

        if(win)
        {
            return (int)(5f * conditionEffect);
        } else {
            return (int)(3f * conditionEffect);
        }
    }
}
