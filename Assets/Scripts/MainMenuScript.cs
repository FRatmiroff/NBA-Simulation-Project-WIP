using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // panels
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject StatsPanel;
    public GameObject StartMenuPanel;
    public GameObject PostGameStatsPanel;
    public GameObject PostGameXPPanel;
    public GameObject LevelUpPanel;
    public GameObject CareerPanel;
    public GameObject StandingsPanel;
    public GameObject StatsNoPlayersPanel;
    public GameObject FreeAgentsPanel;
    public GameObject TradePanel;
    public GameObject InjuryPanel;
    public GameObject DraftPanel;
    public GameObject SocialMediaPanel;
    GameObject currentScreen;

    [Header("GameObjects")]
    public GameObject FreeAgentOneElement;
    public GameObject FreeAgentTwoElement;
    public GameObject FreeAgentThreeElement;

    // game checks
    public static bool backFromGame;
    public static bool championshipGameWon;

    // social media feed
    // figure out how to add one of those posts where the commenter says "[player name] tonight" lists the players stats
    // and then says like "tough" or "light work"
    [Header("Social Media Comments")]
    public List<string> prePlayerGoodComments = new List<string>();
    public List<string> prePlayerBadComments = new List<string>();
    string socialMediaCommentOne, socialMediaCommentTwo, socialMediaCommentThree;

    [Header("Names")]
    string[] lastNames = {"Butler", "Adebayo", "Haliburton", "James", "Curry", "Fox", "Tatum", 
                                "Banchero", "Young", "Durant", "Booker", "Antetekounmpo", "Lillard", 
                                "Mitchell", "Wembanyama", "Leonard", "Doncic", "Martin", "Vincent",
                                "Irving", "Kuzma", "Poole", "Brown", "Brunson", "Edwards", "Ball", 
                                "Robinson", "Smith", "Wilson", "Thompson", "Green", "Johnson",
                                "Williams", "Garcia", "Miller", "Davis", "Lopez", "Thomas",
                                "Taylor", "Moore", "Jackson", "Lee", "Perez", "White", "Sanchez",
                                "Clark", "Hill", "Adams", "Baker", "Hall", "Campbell", "Parker",
                                "Morris", "Cooper", "Reed", "Watson", "Brooks", "Wood", "Russell",
                                "Morant", "Barnes", "Williamson", "Embiid", "Jokic"};
    string[] firstNames = {"Jimmy", "Leonard", "Tyrese", "Sebastian", "Steph", "De'Aaron", "Jayson",
                                "James", "Kevin", "Devin", "Theo", "Damian", "Donovan", "Michael",
                                "Rafael", "Victor", "TJ", "Luka", "Kai", "Kyle", "Jordan", "Jalen",
                                "Anthony", "Melo", "CJ", "Scottie", "AJ", "Joe", "Nick", "Tyler", 
                                "Ryan", "Jacob", "Austin", "Brandon", "Blake", "Allen", "Grayson", 
                                "Adrian", "Lewis", "Andre", "Dominic", "Dylan", "Ethan", "Collin", 
                                "Graham", "Zach", "Emilio", "George"};
    public List<string> teamNames = new List<string>();
    
    // text mesh pro texts
    [Header("Text Mesh Pro Texts")]
    public TMP_Text GameNumberText;
    public TMP_Text strengthValueText;
    public TMP_Text speedValueText;
    public TMP_Text accuracyValueText;
    public TMP_Text CreditsText;
    public TMP_Text DraftPicksText;
    public TMP_Text HeightWeightConditionText;
    public TMP_Text PlayerNumberText;
    public TMP_Text PlayerInfluenceText;
    public TMP_Text PostGameStatsText;
    public TMP_Text PostGameXPText;
    public TMP_Text careerPlayerNameText;
    public TMP_Text careerDurationText;
    public TMP_Text careerAveragesText;
    public TMP_Text careerAchievementsText;
    public TMP_Text teamNameText;
    public TMP_Text nextTeamText;
    public TMP_Text ConditionText;
    public TMP_Text freeAgentOneText;
    public TMP_Text freeAgentTwoText;
    public TMP_Text freeAgentThreeText;
    public TMP_Text tradeHeadlineText;
    public TMP_Text traderReceivesText;
    public TMP_Text playerReceivesText;
    public TMP_Text InjuryText;
    public TMP_Text DraftRoundText;
    public TMP_Text DraftOptionOneText;
    public TMP_Text DraftOptionTwoText;
    public TMP_Text DraftOptionThreeText;
    public TMP_Text DraftOptionFourText;
    public TMP_Text DraftOptionFiveText;
    public TMP_Text DraftOptionSixText;
    public TMP_Text FreeAgentCreditsText;
    public TMP_Text LevelUpPlayerNameText;
    public TMP_Text LevelUpStrengthValueText;
    public TMP_Text LevelUpSpeedValueText;
    public TMP_Text LevelUpAccuracyValueText;
    public TMP_Text SocialMediaFeedText;
    public TMP_Text SocialMediaInfluenceText;

    // game number variables
    [Header("Regular Season Games")]
    public static int gameNumber;
    public static int postSeasonGameNumber;
    public int regSeasonGames;
    
    // player attributes
    int P1StrengthValue;
    int P1SpeedValue;
    int P1AccuracyValue;
    int P2StrengthValue;
    int P2SpeedValue;
    int P2AccuracyValue;
    int P3StrengthValue;
    int P3SpeedValue;
    int P3AccuracyValue;
    int P4StrengthValue;
    int P4SpeedValue;
    int P4AccuracyValue;
    int P5StrengthValue;
    int P5SpeedValue;
    int P5AccuracyValue;
    string P1Name, P2Name, P3Name, P4Name, P5Name;
    int P1Height, P2Height, P3Height, P4Height, P5Height;
    int P1Weight, P2Weight, P3Weight, P4Weight, P5Weight;
    int P1XP, P2XP, P3XP, P4XP, P5XP;
    int P1Condition, P2Condition, P3Condition, P4Condition, P5Condition;
    int P1Influence, P2Influence, P3Influence, P4Influence, P5Influence;
    int P1CareerGames, P2CareerGames, P3CareerGames, P4CareerGames, P5CareerGames;
    int P1CareerPoints, P2CareerPoints, P3CareerPoints, P4CareerPoints, P5CareerPoints;
    int P1CareerAssists, P2CareerAssists, P3CareerAssists, P4CareerAssists, P5CareerAssists;
    int P1CareerRebounds, P2CareerRebounds, P3CareerRebounds, P4CareerRebounds, P5CareerRebounds;
    int P1Chips, P2Chips, P3Chips, P4Chips, P5Chips;
    public static int P1SpotFilled, P2SpotFilled, P3SpotFilled, P4SpotFilled, P5SpotFilled;
    int rosterSpotsFilled;
    int P1InjuryTime, P2InjuryTime, P3InjuryTime, P4InjuryTime, P5InjuryTime;

    bool P1LevelUp, P2LevelUp, P3LevelUp, P4LevelUp, P5LevelUp;
    int PlayerLevelUp;
    [SerializeField] int levelUpBase;

    // defense random min/max
    int defensiveValueMin;
    int defensiveValueMax;

    // free agents
    string FA1Name, FA2Name, FA3Name;
    int FA1Strength, FA2Strength, FA3Strength;
    int FA1Speed, FA2Speed, FA3Speed;
    int FA1Accuracy, FA2Accuracy, FA3Accuracy;
    int FA1Height, FA2Height, FA3Height;
    int FA1Weight, FA2Weight, FA3Weight;
    int FA1Signed, FA2Signed, FA3Signed;
    int FA1Cost, FA2Cost, FA3Cost;

    // trade player
    string tradeName;
    int tradeStrength;
    int tradeSpeed;
    int tradeAccuracy;
    int tradeHeight;
    int tradeWeight;
    int playerTradedIndex;
    int tradeType;

    // post game stats
    public static int playerOnePoints, playerTwoPoints, playerThreePoints, playerFourPoints, playerFivePoints;
    public static int playerOneReb, playerTwoReb, playerThreeReb, playerFourReb, playerFiveReb;
    public static int playerOneAst, playerTwoAst, playerThreeAst, playerFourAst, playerFiveAst;

    // default player attributes on start
    [Header("Default Starting Attribute Values")]
    [SerializeField] int startingStrengthValue;
    [SerializeField] int startingSpeedValue;
    [SerializeField] int startingAccuracyValue;

    // credits
    public static int Credits;
    // career variables
    public static int currentTeam;

    // draft
    bool drafting;
    int draftPicks;

    string draftOptionOneName, draftOptionTwoName, draftOptionThreeName, draftOptionFourName, draftOptionFiveName, draftOptionSixName;
    int draftOptionOneStrength, draftOptionTwoStrength, draftOptionThreeStrength, draftOptionFourStrength, draftOptionFiveStrength, draftOptionSixStrength;
    int draftOptionOneSpeed, draftOptionTwoSpeed, draftOptionThreeSpeed, draftOptionFourSpeed, draftOptionFiveSpeed, draftOptionSixSpeed;
    int draftOptionOneAccuracy, draftOptionTwoAccuracy, draftOptionThreeAccuracy, draftOptionFourAccuracy, draftOptionFiveAccuracy, draftOptionSixAccuracy;
    int draftOptionOneHeight, draftOptionTwoHeight, draftOptionThreeHeight, draftOptionFourHeight, draftOptionFiveHeight, draftOptionSixHeight;
    int draftOptionOneWeight, draftOptionTwoWeight, draftOptionThreeWeight, draftOptionFourWeight, draftOptionFiveWeight, draftOptionSixWeight;

    // reset player prefs
    [Header("Player Prefs")]
    [SerializeField] int currentPlayerStatsScreenIndex;
    List<int> playerStatsScreens = new List<int>();
    public bool resetPrefsOption;
    public bool createTradeOption;
    public bool draftOption;

    [Header("Scripts")]
    public SeasonSimulationScript seasonSimulationScript;

    void Start()
    {
        teamNames.Add("Dallas Mavericks");
        teamNames.Add("Denver Nuggets");
        teamNames.Add("Golden State Warriors");
        teamNames.Add("Houston Rockets");
        teamNames.Add("Los Angeles Clippers");
        teamNames.Add("Los Angeles Lakers");
        teamNames.Add("Memphis Grizzlies");
        teamNames.Add("Minnesota Timberwolves");
        teamNames.Add("New Orleans Pelicans");
        teamNames.Add("Oklahoma City Thunder");
        teamNames.Add("Phoenix Suns");
        teamNames.Add("Portland Trailblazers");
        teamNames.Add("Sacramento Kings");
        teamNames.Add("San Antonio Spurs");
        teamNames.Add("Utah Jazz");
        teamNames.Add("Atlanta Hawks");
        teamNames.Add("Boston Celtics");
        teamNames.Add("Brooklyn Nets");
        teamNames.Add("Charlotte Hornets");
        teamNames.Add("Chicago Bulls");
        teamNames.Add("Cleveland Cavaliers");
        teamNames.Add("Detroit Pistons");
        teamNames.Add("Indiana Pacers");
        teamNames.Add("Miami Heat");
        teamNames.Add("Milwaukee Bucks");
        teamNames.Add("New York Knicks");
        teamNames.Add("Orlando Magic");
        teamNames.Add("Philadelphia 76ers");
        teamNames.Add("Toronto Raptors");
        teamNames.Add("Washington Wizards");
        
        StartMenuPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        StatsPanel.SetActive(false);
        PostGameStatsPanel.SetActive(false);
        PostGameXPPanel.SetActive(false);
        LevelUpPanel.SetActive(false);
        CareerPanel.SetActive(false);
        StandingsPanel.SetActive(false);
        StatsNoPlayersPanel.SetActive(false);
        FreeAgentsPanel.SetActive(false);
        TradePanel.SetActive(false);
        InjuryPanel.SetActive(false);
        DraftPanel.SetActive(false);
        SocialMediaPanel.SetActive(false);

        resetPrefsOption = false;
        drafting = false;
        
        // set player prefs
        if(!PlayerPrefs.HasKey("GameNumberPref"))
        {
            PlayerPrefs.SetInt("GameNumberPref", 1);
        }
        gameNumber = PlayerPrefs.GetInt("GameNumberPref");

        // player one prefs
        if(!PlayerPrefs.HasKey("PlayerOneNamePref"))
        {
            PlayerPrefs.SetString("PlayerOneNamePref", GenerateName());
        }
        if(!PlayerPrefs.HasKey("PlayerOneStrengthPref"))
        {
            PlayerPrefs.SetInt("PlayerOneStrengthPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerOneSpeedPref"))
        {
            PlayerPrefs.SetInt("PlayerOneSpeedPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerOneAccuracyPref"))
        {
            PlayerPrefs.SetInt("PlayerOneAccuracyPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerOneHeightPref"))
        {
            PlayerPrefs.SetInt("PlayerOneHeightPref", GenerateHeight());
        }
        if(!PlayerPrefs.HasKey("PlayerOneWeightPref"))
        {
            PlayerPrefs.SetInt("PlayerOneWeightPref", GenerateWeight());
        }
        P1Name = PlayerPrefs.GetString("PlayerOneNamePref");
        P1StrengthValue = PlayerPrefs.GetInt("PlayerOneStrengthPref");
        P1SpeedValue = PlayerPrefs.GetInt("PlayerOneSpeedPref");
        P1AccuracyValue = PlayerPrefs.GetInt("PlayerOneAccuracyPref");
        P1Height = PlayerPrefs.GetInt("PlayerOneHeightPref");
        P1Weight = PlayerPrefs.GetInt("PlayerOneWeightPref");

        // player two prefs
        if(!PlayerPrefs.HasKey("PlayerTwoNamePref"))
        {
            PlayerPrefs.SetString("PlayerTwoNamePref", GenerateName());
        }
        if(!PlayerPrefs.HasKey("PlayerTwoStrengthPref"))
        {
            PlayerPrefs.SetInt("PlayerTwoStrengthPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerTwoSpeedPref"))
        {
            PlayerPrefs.SetInt("PlayerTwoSpeedPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerTwoAccuracyPref"))
        {
            PlayerPrefs.SetInt("PlayerTwoAccuracyPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerTwoHeightPref"))
        {
            PlayerPrefs.SetInt("PlayerTwoHeightPref", GenerateHeight());
        }
        if(!PlayerPrefs.HasKey("PlayerTwoWeightPref"))
        {
            PlayerPrefs.SetInt("PlayerTwoWeightPref", GenerateWeight());
        }
        P2Name = PlayerPrefs.GetString("PlayerTwoNamePref");
        P2StrengthValue = PlayerPrefs.GetInt("PlayerTwoStrengthPref");
        P2SpeedValue = PlayerPrefs.GetInt("PlayerTwoSpeedPref");
        P2AccuracyValue = PlayerPrefs.GetInt("PlayerTwoAccuracyPref");
        P2Height = PlayerPrefs.GetInt("PlayerTwoHeightPref");
        P2Weight = PlayerPrefs.GetInt("PlayerTwoWeightPref");

        // player three prefs
        if(!PlayerPrefs.HasKey("PlayerThreeNamePref"))
        {
            PlayerPrefs.SetString("PlayerThreeNamePref", GenerateName());
        }
        if(!PlayerPrefs.HasKey("PlayerThreeStrengthPref"))
        {
            PlayerPrefs.SetInt("PlayerThreeStrengthPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerThreeSpeedPref"))
        {
            PlayerPrefs.SetInt("PlayerThreeSpeedPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerThreeAccuracyPref"))
        {
            PlayerPrefs.SetInt("PlayerThreeAccuracyPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerThreeHeightPref"))
        {
            PlayerPrefs.SetInt("PlayerThreeHeightPref", GenerateHeight());
        }
        if(!PlayerPrefs.HasKey("PlayerThreeWeightPref"))
        {
            PlayerPrefs.SetInt("PlayerThreeWeightPref", GenerateWeight());
        }
        P3Name = PlayerPrefs.GetString("PlayerThreeNamePref");
        P3StrengthValue = PlayerPrefs.GetInt("PlayerThreeStrengthPref");
        P3SpeedValue = PlayerPrefs.GetInt("PlayerThreeSpeedPref");
        P3AccuracyValue = PlayerPrefs.GetInt("PlayerThreeAccuracyPref");
        P3Height = PlayerPrefs.GetInt("PlayerThreeHeightPref");
        P3Weight = PlayerPrefs.GetInt("PlayerThreeWeightPref");

        // player four prefs
        if(!PlayerPrefs.HasKey("PlayerFourNamePref"))
        {
            PlayerPrefs.SetString("PlayerFourNamePref", GenerateName());
        }
        if(!PlayerPrefs.HasKey("PlayerFourStrengthPref"))
        {
            PlayerPrefs.SetInt("PlayerFourStrengthPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFourSpeedPref"))
        {
            PlayerPrefs.SetInt("PlayerFourSpeedPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFourAccuracyPref"))
        {
            PlayerPrefs.SetInt("PlayerFourAccuracyPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFourHeightPref"))
        {
            PlayerPrefs.SetInt("PlayerFourHeightPref", GenerateHeight());
        }
        if(!PlayerPrefs.HasKey("PlayerFourWeightPref"))
        {
            PlayerPrefs.SetInt("PlayerFourWeightPref", GenerateWeight());
        }
        P4Name = PlayerPrefs.GetString("PlayerFourNamePref");
        P4StrengthValue = PlayerPrefs.GetInt("PlayerFourStrengthPref");
        P4SpeedValue = PlayerPrefs.GetInt("PlayerFourSpeedPref");
        P4AccuracyValue = PlayerPrefs.GetInt("PlayerFourAccuracyPref");
        P4Height = PlayerPrefs.GetInt("PlayerFourHeightPref");
        P4Weight = PlayerPrefs.GetInt("PlayerFourWeightPref");

        // player five prefs
        if(!PlayerPrefs.HasKey("PlayerFiveNamePref"))
        {
            PlayerPrefs.SetString("PlayerFiveNamePref", GenerateName());
        }
        if(!PlayerPrefs.HasKey("PlayerFiveStrengthPref"))
        {
            PlayerPrefs.SetInt("PlayerFiveStrengthPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFiveSpeedPref"))
        {
            PlayerPrefs.SetInt("PlayerFiveSpeedPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFiveAccuracyPref"))
        {
            PlayerPrefs.SetInt("PlayerFiveAccuracyPref", returnRandom(2, 7));
        }
        if(!PlayerPrefs.HasKey("PlayerFiveHeightPref"))
        {
            PlayerPrefs.SetInt("PlayerFiveHeightPref", GenerateHeight());
        }
        if(!PlayerPrefs.HasKey("PlayerFiveWeightPref"))
        {
            PlayerPrefs.SetInt("PlayerFiveWeightPref", GenerateWeight());
        }
        P5Name = PlayerPrefs.GetString("PlayerFiveNamePref");
        P5StrengthValue = PlayerPrefs.GetInt("PlayerFiveStrengthPref");
        P5SpeedValue = PlayerPrefs.GetInt("PlayerFiveSpeedPref");
        P5AccuracyValue = PlayerPrefs.GetInt("PlayerFiveAccuracyPref");
        P5Height = PlayerPrefs.GetInt("PlayerFiveHeightPref");
        P5Weight = PlayerPrefs.GetInt("PlayerFiveWeightPref");

        // career player prefs
        SetUpPlayerPrefsString("SocialMediaCommentOnePref", "Jacob Black\nThis team has potential. Don't let the doubters have you believe otherwise\n\n");
        SetUpPlayerPrefsString("SocialMediaCommentTwoPref", "JC Perez\nWhy would we hire this guy? Sell the team at this point.\n\n");
        SetUpPlayerPrefsString("SocialMediaCommentThreePref", "Dylan J.\nSomeone has to make this team better. This might be the guy. Don't give up yet\n\n");
        socialMediaCommentOne = PlayerPrefs.GetString("SocialMediaCommentOnePref");
        socialMediaCommentTwo = PlayerPrefs.GetString("SocialMediaCommentTwoPref");
        socialMediaCommentThree = PlayerPrefs.GetString("SocialMediaCommentThreePref");

        SetUpPlayerPrefsRoster("Player", "XPPref", 0);
        P1XP = PlayerPrefs.GetInt("PlayerOneXPPref");
        P2XP = PlayerPrefs.GetInt("PlayerTwoXPPref");
        P3XP = PlayerPrefs.GetInt("PlayerThreeXPPref");
        P4XP = PlayerPrefs.GetInt("PlayerFourXPPref");
        P5XP = PlayerPrefs.GetInt("PlayerFiveXPPref");

        SetUpPlayerPrefsInt("PlayerOneConditionPref", GenerateCondition());
        SetUpPlayerPrefsInt("PlayerTwoConditionPref", GenerateCondition());
        SetUpPlayerPrefsInt("PlayerThreeConditionPref", GenerateCondition());
        SetUpPlayerPrefsInt("PlayerFourConditionPref", GenerateCondition());
        SetUpPlayerPrefsInt("PlayerFiveConditionPref", GenerateCondition());
        P1Condition = PlayerPrefs.GetInt("PlayerOneConditionPref");
        P2Condition = PlayerPrefs.GetInt("PlayerTwoConditionPref");
        P3Condition = PlayerPrefs.GetInt("PlayerThreeConditionPref");
        P4Condition = PlayerPrefs.GetInt("PlayerFourConditionPref");
        P5Condition = PlayerPrefs.GetInt("PlayerFiveConditionPref");

        SetUpPlayerPrefsInt("PlayerOneInfluencePref", returnRandom(0, 101));
        SetUpPlayerPrefsInt("PlayerTwoInfluencePref", returnRandom(0, 101));
        SetUpPlayerPrefsInt("PlayerThreeInfluencePref", returnRandom(0, 101));
        SetUpPlayerPrefsInt("PlayerFourInfluencePref", returnRandom(0, 101));
        SetUpPlayerPrefsInt("PlayerFiveInfluencePref", returnRandom(0, 101));
        P1Influence = PlayerPrefs.GetInt("PlayerOneInfluencePref");
        P2Influence = PlayerPrefs.GetInt("PlayerTwoInfluencePref");
        P3Influence = PlayerPrefs.GetInt("PlayerThreeInfluencePref");
        P4Influence = PlayerPrefs.GetInt("PlayerFourInfluencePref");
        P5Influence = PlayerPrefs.GetInt("PlayerFiveInfluencePref");

        SetUpPlayerPrefsRoster("Player", "GamesPref", 0);
        P1CareerGames = PlayerPrefs.GetInt("PlayerOneGamesPref");
        P2CareerGames = PlayerPrefs.GetInt("PlayerTwoGamesPref");
        P3CareerGames = PlayerPrefs.GetInt("PlayerThreeGamesPref");
        P4CareerGames = PlayerPrefs.GetInt("PlayerFourGamesPref");
        P5CareerGames = PlayerPrefs.GetInt("PlayerFiveGamesPref");

        SetUpPlayerPrefsRoster("Player", "TotalPointsPref", 0);
        SetUpPlayerPrefsRoster("Player", "TotalAssistsPref", 0);
        SetUpPlayerPrefsRoster("Player", "TotalReboundsPref", 0);
        P1CareerPoints = PlayerPrefs.GetInt("PlayerOneTotalPointsPref");
        P1CareerAssists = PlayerPrefs.GetInt("PlayerOneTotalAssistsPref");
        P1CareerRebounds = PlayerPrefs.GetInt("PlayerOneTotalReboundsPref");
        
        P2CareerPoints = PlayerPrefs.GetInt("PlayerTwoTotalPointsPref");
        P2CareerAssists = PlayerPrefs.GetInt("PlayerTwoTotalAssistsPref");
        P2CareerRebounds = PlayerPrefs.GetInt("PlayerTwoTotalReboundsPref");

        P3CareerPoints = PlayerPrefs.GetInt("PlayerThreeTotalPointsPref");
        P3CareerAssists = PlayerPrefs.GetInt("PlayerThreeTotalAssistsPref");
        P3CareerRebounds = PlayerPrefs.GetInt("PlayerThreeTotalReboundsPref");

        P4CareerPoints = PlayerPrefs.GetInt("PlayerFourTotalPointsPref");
        P4CareerAssists = PlayerPrefs.GetInt("PlayerFourTotalAssistsPref");
        P4CareerRebounds = PlayerPrefs.GetInt("PlayerFourTotalReboundsPref");

        P5CareerPoints = PlayerPrefs.GetInt("PlayerFiveTotalPointsPref");
        P5CareerAssists = PlayerPrefs.GetInt("PlayerFiveTotalAssistsPref");
        P5CareerRebounds = PlayerPrefs.GetInt("PlayerFiveTotalReboundsPref");

        SetUpPlayerPrefsRoster("Player", "ChampionshipsPref", 0);
        P1Chips = PlayerPrefs.GetInt("PlayerOneChampionshipsPref");
        P2Chips = PlayerPrefs.GetInt("PlayerTwoChampionshipsPref");
        P3Chips = PlayerPrefs.GetInt("PlayerThreeChampionshipsPref");
        P4Chips = PlayerPrefs.GetInt("PlayerFourChampionshipsPref");
        P5Chips = PlayerPrefs.GetInt("PlayerFiveChampionshipsPref");

        // free agents set up
        SetUpPlayerPrefsFreeAgent("One");
        FA1Name = PlayerPrefs.GetString("FreeAgentOneNamePref");
        FA1Strength = PlayerPrefs.GetInt("FreeAgentOneStrengthPref");
        FA1Speed = PlayerPrefs.GetInt("FreeAgentOneSpeedPref");
        FA1Accuracy = PlayerPrefs.GetInt("FreeAgentOneAccuracyPref");
        FA1Height = PlayerPrefs.GetInt("FreeAgentOneHeightPref");
        FA1Weight = PlayerPrefs.GetInt("FreeAgentOneWeightPref");
        FA1Signed = PlayerPrefs.GetInt("FreeAgentOneSignedPref");
        FA1Cost = PlayerPrefs.GetInt("FreeAgentOneCostPref");
        SetUpPlayerPrefsFreeAgent("Two");
        FA2Name = PlayerPrefs.GetString("FreeAgentTwoNamePref");
        FA2Strength = PlayerPrefs.GetInt("FreeAgentTwoStrengthPref");
        FA2Speed = PlayerPrefs.GetInt("FreeAgentTwoSpeedPref");
        FA2Accuracy = PlayerPrefs.GetInt("FreeAgentTwoAccuracyPref");
        FA2Height = PlayerPrefs.GetInt("FreeAgentTwoHeightPref");
        FA2Weight = PlayerPrefs.GetInt("FreeAgentTwoWeightPref");
        FA2Signed = PlayerPrefs.GetInt("FreeAgentTwoSignedPref");
        FA2Cost = PlayerPrefs.GetInt("FreeAgentTwoCostPref");
        SetUpPlayerPrefsFreeAgent("Three");
        FA3Name = PlayerPrefs.GetString("FreeAgentThreeNamePref");
        FA3Strength = PlayerPrefs.GetInt("FreeAgentThreeStrengthPref");
        FA3Speed = PlayerPrefs.GetInt("FreeAgentThreeSpeedPref");
        FA3Accuracy = PlayerPrefs.GetInt("FreeAgentThreeAccuracyPref");
        FA3Height = PlayerPrefs.GetInt("FreeAgentThreeHeightPref");
        FA3Weight = PlayerPrefs.GetInt("FreeAgentThreeWeightPref");
        FA3Signed = PlayerPrefs.GetInt("FreeAgentThreeSignedPref");
        FA3Cost = PlayerPrefs.GetInt("FreeAgentThreeCostPref");

        if(!PlayerPrefs.HasKey("CurrentTeamPref"))
        {
            PlayerPrefs.SetInt("CurrentTeamPref", Random.Range(1, 31));
        }
        currentTeam = PlayerPrefs.GetInt("CurrentTeamPref");

        if(!PlayerPrefs.HasKey("CreditsPref"))
        {
            PlayerPrefs.SetInt("CreditsPref", 0);
        }
        Credits = PlayerPrefs.GetInt("CreditsPref");

        SetUpPlayerPrefsInt("DraftPicksPref", 1);
        draftPicks = PlayerPrefs.GetInt("DraftPicksPref");

        SetUpPlayerPrefsRoster("Player", "InjuryTimePref", 0);
        P1InjuryTime = PlayerPrefs.GetInt("PlayerOneInjuryTimePref");
        P2InjuryTime = PlayerPrefs.GetInt("PlayerTwoInjuryTimePref");
        P3InjuryTime = PlayerPrefs.GetInt("PlayerThreeInjuryTimePref");
        P4InjuryTime = PlayerPrefs.GetInt("PlayerFourInjuryTimePref");
        P5InjuryTime = PlayerPrefs.GetInt("PlayerFiveInjuryTimePref");

        SetUpPlayerPrefsRoster("Player", "RosterSpotFilledPref", 1);
        P1SpotFilled = PlayerPrefs.GetInt("PlayerOneRosterSpotFilledPref");
        P2SpotFilled = PlayerPrefs.GetInt("PlayerTwoRosterSpotFilledPref");
        P3SpotFilled = PlayerPrefs.GetInt("PlayerThreeRosterSpotFilledPref");
        P4SpotFilled = PlayerPrefs.GetInt("PlayerFourRosterSpotFilledPref");
        P5SpotFilled = PlayerPrefs.GetInt("PlayerFiveRosterSpotFilledPref");

        if(!PlayerPrefs.HasKey("RosterSpotsFilledPref"))
        {
            PlayerPrefs.SetInt("RosterSpotsFilledPref", 5);
        }
        rosterSpotsFilled = PlayerPrefs.GetInt("RosterSpotsFilledPref");

        if(P1SpotFilled == 1)
        {
            playerStatsScreens.Add(1);
        }
        if(P2SpotFilled == 1)
        {
            playerStatsScreens.Add(2);
        }
        if(P3SpotFilled == 1)
        {
            playerStatsScreens.Add(3);
        }
        if(P4SpotFilled == 1)
        {
            playerStatsScreens.Add(4);
        }
        if(P5SpotFilled == 1)
        {
            playerStatsScreens.Add(5);
        }
        

        // back from game check
        if(backFromGame != true)
        {
            backFromGame = false;
        }
        if(championshipGameWon != true)
        {
            championshipGameWon = false;
        }

        // set menu on start/after game
        if(backFromGame)
        {
            float playerOneStatsDiff = 0f;
            float playerTwoStatsDiff = 0f;
            float playerThreeStatsDiff = 0f;
            float playerFourStatsDiff = 0f;
            float playerFiveStatsDiff = 0f;
            if(P1SpotFilled == 1)
            playerOneStatsDiff = GetStatDiff(playerOnePoints, playerOneAst, playerOneReb, P1CareerPoints, P1CareerAssists, P1CareerRebounds, P1CareerGames);
            if(P2SpotFilled == 1)
            playerTwoStatsDiff = GetStatDiff(playerTwoPoints, playerTwoAst, playerTwoReb, P2CareerPoints, P2CareerAssists, P2CareerRebounds, P2CareerGames);
            if(P3SpotFilled == 1)
            playerThreeStatsDiff = GetStatDiff(playerThreePoints, playerThreeAst, playerThreeReb, P3CareerPoints, P3CareerAssists, P3CareerRebounds, P3CareerGames);
            if(P4SpotFilled == 1)
            playerFourStatsDiff = GetStatDiff(playerFourPoints, playerFourAst, playerFourReb, P4CareerPoints, P4CareerAssists, P4CareerRebounds, P4CareerGames);
            if(P5SpotFilled == 1)
            playerFiveStatsDiff = GetStatDiff(playerFivePoints, playerFiveAst, playerFiveReb, P5CareerPoints, P5CareerAssists, P5CareerRebounds, P5CareerGames);

            int conditionFactor = GetConditionFactor(playerOneStatsDiff);
            int P1Total = playerOnePoints + playerOneAst + playerOneReb;
            int P2Total = playerTwoPoints + playerTwoAst + playerTwoReb;
            int P3Total = playerThreePoints + playerThreeAst + playerThreeReb;
            int P4Total = playerFourPoints + playerFourAst + playerFourReb;
            int P5Total = playerFivePoints + playerFiveAst + playerFiveReb;
            Debug.Log("1: " + playerOneStatsDiff);
            Debug.Log("2: " + playerTwoStatsDiff);
            Debug.Log("3: " + playerThreeStatsDiff);
            Debug.Log("4: " + playerFourStatsDiff);
            Debug.Log("5: " + playerFiveStatsDiff);
            if(P1SpotFilled == 1)
            {
                P1Condition += (int)(playerOneStatsDiff * conditionFactor);
                if(P1Total == 0)
                {
                    P1Condition -= Random.Range(2, 6);
                }
                PlayerPrefs.SetInt("PlayerOneConditionPref", P1Condition);
            }
            conditionFactor = GetConditionFactor(playerTwoStatsDiff);
            if(P2SpotFilled == 1)
            {
                P2Condition += (int)(playerTwoStatsDiff * conditionFactor);
                if(P2Total == 0)
                {
                    P2Condition -= Random.Range(2, 6);
                }
                PlayerPrefs.SetInt("PlayerTwoConditionPref", P2Condition);
            }
            conditionFactor = GetConditionFactor(playerThreeStatsDiff);
            if(P3SpotFilled == 1)
            {
                P3Condition += (int)(playerThreeStatsDiff * conditionFactor);
                if(P3Total == 0)
                {
                    P3Condition -= Random.Range(2, 6);
                }
                PlayerPrefs.SetInt("PlayerThreeConditionPref", P3Condition);
            }
            conditionFactor = GetConditionFactor(playerFourStatsDiff);
            if(P4SpotFilled == 1)
            {
                P4Condition += (int)(playerFourStatsDiff * conditionFactor);
                if(P4Total == 0)
                {
                    P4Condition -= Random.Range(2, 6);
                }
                PlayerPrefs.SetInt("PlayerFourConditionPref", P4Condition);
            }
            conditionFactor = GetConditionFactor(playerFiveStatsDiff);
            if(P5SpotFilled == 1)
            {
                P5Condition += (int)(playerFiveStatsDiff * conditionFactor);
                if(P5Total == 0)
                {
                    P5Condition -= Random.Range(2, 6);
                }
                PlayerPrefs.SetInt("PlayerFiveConditionPref", P5Condition);
            }
            CheckConditionMinimum();

            AddGamePlayedToPlayersCareers();
            AddPlayerTotalStats();
            UpdatePostGameStatsText();
            PostGameSocialMediaFeed();
            SwitchPanel(PostGameStatsPanel);
        } else {
            SwitchPanel(MainMenuPanel);
        }

        if(championshipGameWon)
        {
            AddChampionshipWonToPlayersCareers();
            championshipGameWon = false;
        }

        Debug.Log(PlayerPrefs.GetInt("FreeAgentOneCostPref") + "!");

        // update texts
        UpdateSocialMediaFeed();
        UpdateTexts();
    }

    void SetUpPlayerPrefsInt(string playerPref, int set)
    {
        if(!PlayerPrefs.HasKey(playerPref))
        {
            PlayerPrefs.SetInt(playerPref, set);
        }
    }

    void SetUpPlayerPrefsString(string playerPref, string set)
    {
        if(!PlayerPrefs.HasKey(playerPref))
        {
            PlayerPrefs.SetString(playerPref, set);
        }
    }

    void SetUpPlayerPrefsRoster(string playerPrefsNameBeginning, string playerPrefsNameEnding, int set)
    {
        string temp = playerPrefsNameBeginning + "One" + playerPrefsNameEnding;
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, set);
        }
        temp = playerPrefsNameBeginning + "Two" + playerPrefsNameEnding;
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, set);
        }
        temp = playerPrefsNameBeginning + "Three" + playerPrefsNameEnding;
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, set);
        }
        temp = playerPrefsNameBeginning + "Four" + playerPrefsNameEnding;
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, set);
        }
        temp = playerPrefsNameBeginning + "Five" + playerPrefsNameEnding;
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, set);
        }
    }

    void SetUpPlayerPrefsFreeAgent(string agentNum)
    {
        SetUpPlayerPrefsString("FreeAgent" + agentNum + "NamePref", GenerateName());
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "StrengthPref", returnRandom(1, 9));
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "SpeedPref", returnRandom(1, 9));
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "AccuracyPref", returnRandom(1, 9));
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "HeightPref", GenerateHeight());
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "WeightPref", GenerateWeight());
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "SignedPref", 0);
        SetUpPlayerPrefsInt("FreeAgent" + agentNum + "CostPref", GenerateFreeAgentCost(agentNum));
    }

    public void NewFreeAgents(string agentNum)
    {
        PlayerPrefs.SetString("FreeAgent" + agentNum + "NamePref", GenerateName());
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "StrengthPref", returnRandom(1, 9));
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "SpeedPref", returnRandom(1, 9));
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "AccuracyPref", returnRandom(1, 9));
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "HeightPref", GenerateHeight());
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "WeightPref", GenerateWeight());
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "SignedPref", 0);
        PlayerPrefs.SetInt("FreeAgent" + agentNum + "CostPref", GenerateFreeAgentCost(agentNum));

        if(agentNum == "One")
        {
            FA1Name = PlayerPrefs.GetString("FreeAgentOneNamePref");
            FA1Strength = PlayerPrefs.GetInt("FreeAgentOneStrengthPref");
            FA1Speed = PlayerPrefs.GetInt("FreeAgentOneSpeedPref");
            FA1Accuracy = PlayerPrefs.GetInt("FreeAgentOneAccuracyPref");
            FA1Height = PlayerPrefs.GetInt("FreeAgentOneHeightPref");
            FA1Weight = PlayerPrefs.GetInt("FreeAgentOneWeightPref");
            FA1Signed = PlayerPrefs.GetInt("FreeAgentOneSignedPref");
            FA1Cost = PlayerPrefs.GetInt("FreeAgentOneCostPref");
        }
        else if(agentNum == "Two")
        {
            FA2Name = PlayerPrefs.GetString("FreeAgentTwoNamePref");
            FA2Strength = PlayerPrefs.GetInt("FreeAgentTwoStrengthPref");
            FA2Speed = PlayerPrefs.GetInt("FreeAgentTwoSpeedPref");
            FA2Accuracy = PlayerPrefs.GetInt("FreeAgentTwoAccuracyPref");
            FA2Height = PlayerPrefs.GetInt("FreeAgentTwoHeightPref");
            FA2Weight = PlayerPrefs.GetInt("FreeAgentTwoWeightPref");
            FA2Signed = PlayerPrefs.GetInt("FreeAgentTwoSignedPref");
            FA2Cost = PlayerPrefs.GetInt("FreeAgentTwoCostPref");
        }
        else if(agentNum == "Three")
        {
            FA3Name = PlayerPrefs.GetString("FreeAgentThreeNamePref");
            FA3Strength = PlayerPrefs.GetInt("FreeAgentThreeStrengthPref");
            FA3Speed = PlayerPrefs.GetInt("FreeAgentThreeSpeedPref");
            FA3Accuracy = PlayerPrefs.GetInt("FreeAgentThreeAccuracyPref");
            FA3Height = PlayerPrefs.GetInt("FreeAgentThreeHeightPref");
            FA3Weight = PlayerPrefs.GetInt("FreeAgentThreeWeightPref");
            FA3Signed = PlayerPrefs.GetInt("FreeAgentThreeSignedPref");
            FA3Cost = PlayerPrefs.GetInt("FreeAgentThreeCostPref");
        }
    }

    void Update()
    {
        // reset prefs
        if(resetPrefsOption)
        {
            ResetPrefs();
            resetPrefsOption = false;
        }

        if(createTradeOption)
        {
            CreateTrade();
            createTradeOption = false;
        }

        if(draftOption)
        {
            Draft();
            draftOption = false;
        }
    }

    // menu button functions
    public void Play()
    {
        SwitchPanel(StartMenuPanel);
    }

    public void Stats()
    {
        currentPlayerStatsScreenIndex = 0;
        StatsScreenUpdateTexts();
        if(rosterSpotsFilled > 0)
        {
            SwitchPanel(StatsPanel);
        } else {
            SwitchPanel(StatsNoPlayersPanel);
        }
    }

    public void BackToStatsFromCareer()
    {
        StatsScreenUpdateTexts();
        SwitchPanel(StatsPanel);
    }

    public void StartGame()
    {
        if(P1InjuryTime > 0)
        {
            P1Name = "";
            P1StrengthValue = Random.Range(1, 5);
            P1SpeedValue = Random.Range(1, 5);
            P1AccuracyValue = Random.Range(1, 5);
            P1Height = Random.Range(70, 76);
            P1Weight = Random.Range(36, 40) * 5;
        }
        if(P2InjuryTime > 0)
        {
            P2Name = "";
            P2StrengthValue = Random.Range(1, 5);
            P2SpeedValue = Random.Range(1, 5);
            P2AccuracyValue = Random.Range(1, 5);
            P2Height = Random.Range(70, 76);
            P2Weight = Random.Range(36, 40) * 5;
        }
        if(P3InjuryTime > 0)
        {
            P3Name = "";
            P3StrengthValue = Random.Range(1, 5);
            P3SpeedValue = Random.Range(1, 5);
            P3AccuracyValue = Random.Range(1, 5);
            P3Height = Random.Range(70, 76);
            P3Weight = Random.Range(36, 40) * 5;
        }
        if(P4InjuryTime > 0)
        {
            P4Name = "";
            P4StrengthValue = Random.Range(1, 5);
            P4SpeedValue = Random.Range(1, 5);
            P4AccuracyValue = Random.Range(1, 5);
            P4Height = Random.Range(70, 76);
            P4Weight = Random.Range(36, 40) * 5;
        }
        if(P5InjuryTime > 0)
        {
            P5Name = "";
            P5StrengthValue = Random.Range(1, 5);
            P5SpeedValue = Random.Range(1, 5);
            P5AccuracyValue = Random.Range(1, 5);
            P5Height = Random.Range(70, 76);
            P5Weight = Random.Range(36, 40) * 5;
        }

        ControlManager.playerOneSpeed = ((float)P1SpeedValue / 10f) + 1.5f;
        ControlManager.playerTwoSpeed = ((float)P2SpeedValue / 10f) + 1.5f;
        ControlManager.playerThreeSpeed = ((float)P3SpeedValue / 10f) + 1.5f;
        ControlManager.playerFourSpeed = ((float)P4SpeedValue / 10f) + 1.5f;
        ControlManager.playerFiveSpeed = ((float)P5SpeedValue / 10f) + 1.5f;

        ControlManager.playerOneMaxForce = ((float)P1StrengthValue / 2f) + 8f;
        ControlManager.playerTwoMaxForce = ((float)P2StrengthValue / 2f) + 8f;
        ControlManager.playerThreeMaxForce = ((float)P3StrengthValue / 2f) + 8f;
        ControlManager.playerFourMaxForce = ((float)P4StrengthValue / 2f) + 8f;
        ControlManager.playerFiveMaxForce = ((float)P5StrengthValue / 2f) + 8f;

        ControlManager.playerOneAccuracy = (float)P1AccuracyValue;
        ControlManager.playerTwoAccuracy = (float)P2AccuracyValue;
        ControlManager.playerThreeAccuracy = (float)P3AccuracyValue;
        ControlManager.playerFourAccuracy = (float)P4AccuracyValue;
        ControlManager.playerFiveAccuracy = (float)P5AccuracyValue;

        ControlManager.playerOneName = P1Name;
        ControlManager.playerTwoName = P2Name;
        ControlManager.playerThreeName = P3Name;
        ControlManager.playerFourName = P4Name;
        ControlManager.playerFiveName = P5Name;

        GameManager.playerOneHeight = P1Height;
        GameManager.playerTwoHeight = P2Height;
        GameManager.playerThreeHeight = P3Height;
        GameManager.playerFourHeight = P4Height;
        GameManager.playerFiveHeight = P5Height;

        GameManager.playerOneCondition = P1Condition;
        GameManager.playerTwoCondition = P2Condition;
        GameManager.playerThreeCondition = P3Condition;
        GameManager.playerFourCondition = P4Condition;
        GameManager.playerFiveCondition = P5Condition;

        // defense
        GameManager.defenderOneCloseOutSpeedMultiplier = GetCloseOutSpeedMultiplierValues();
        GameManager.defenderTwoCloseOutSpeedMultiplier = GetCloseOutSpeedMultiplierValues();
        GameManager.defenderThreeCloseOutSpeedMultiplier = GetCloseOutSpeedMultiplierValues();
        GameManager.defenderFourCloseOutSpeedMultiplier = GetCloseOutSpeedMultiplierValues();
        GameManager.defenderFiveCloseOutSpeedMultiplier = GetCloseOutSpeedMultiplierValues();

        GameManager.defenderOneSmoothTime = GetSmoothTimeValues();
        GameManager.defenderTwoSmoothTime = GetSmoothTimeValues();
        GameManager.defenderThreeSmoothTime = GetSmoothTimeValues();
        GameManager.defenderFourSmoothTime = GetSmoothTimeValues();
        GameManager.defenderFiveSmoothTime = GetSmoothTimeValues();

        GameManager.defenderOneDistScaleFactor = GetDistScaleFactorValues();
        GameManager.defenderTwoDistScaleFactor = GetDistScaleFactorValues();
        GameManager.defenderThreeDistScaleFactor = GetDistScaleFactorValues();
        GameManager.defenderFourDistScaleFactor = GetDistScaleFactorValues();
        GameManager.defenderFiveDistScaleFactor = GetDistScaleFactorValues();

        GameManager.defenderOneMaintainDistanceBase = GetMaintainDistanceBaseValues();
        GameManager.defenderTwoMaintainDistanceBase = GetMaintainDistanceBaseValues();
        GameManager.defenderThreeMaintainDistanceBase = GetMaintainDistanceBaseValues();
        GameManager.defenderFourMaintainDistanceBase = GetMaintainDistanceBaseValues();
        GameManager.defenderFiveMaintainDistanceBase = GetMaintainDistanceBaseValues();

        SceneManager.LoadScene("OneVersusOne");
    }

    public void BackToMenu()
    {
        UpdateTexts();
        SwitchPanel(StartMenuPanel);
    }

    public void BackToMenuFromStats()
    {
        UpdateTexts();
        if(drafting)
        {
            SwitchPanel(DraftPanel);
        } else {
            SwitchPanel(StartMenuPanel);
        }
    }

    public void Career()
    {
        SwitchPanel(CareerPanel);
    }

    public void Standings()
    {
        SwitchPanel(StandingsPanel);
    }

    public void FreeAgents()
    {
        UpdateFreeAgentsText();
        SwitchPanel(FreeAgentsPanel);
    }

    public void SocialMedia()
    {
        UpdateSocialMediaInfluenceText();
        SwitchPanel(SocialMediaPanel);
    }

    public void PostGameStatsContinue()
    {
        UpdatePlayersXP();
        SwitchPanel(PostGameXPPanel);
    }

    public void PostGameXPContinue()
    {
        if(rosterSpotsFilled > 0 && (P1LevelUp || P2LevelUp || P3LevelUp || P4LevelUp || P5LevelUp))
        {
            LevelUp();
        } else {
            PostGameContinue();
        }
    }

    void LevelUp()
    {
        SwitchPanel(LevelUpPanel);
        if(P1LevelUp)
        {
            UpdateLevelUpText(P1Name, P1StrengthValue, P1SpeedValue, P1AccuracyValue);
            PlayerLevelUp = 1;
        }
        else if(P2LevelUp)
        {
            UpdateLevelUpText(P2Name, P2StrengthValue, P2SpeedValue, P2AccuracyValue);
            PlayerLevelUp = 2;
        }
        else if(P3LevelUp)
        {
            UpdateLevelUpText(P3Name, P3StrengthValue, P3SpeedValue, P3AccuracyValue);
            PlayerLevelUp = 3;
        }
        else if(P4LevelUp)
        {
            UpdateLevelUpText(P4Name, P4StrengthValue, P4SpeedValue, P4AccuracyValue);
            PlayerLevelUp = 4;
        }
        else if(P5LevelUp)
        {
            UpdateLevelUpText(P5Name, P5StrengthValue, P5SpeedValue, P5AccuracyValue);
            PlayerLevelUp = 5;
        } else {
            PostGameContinue();
        }
    }

    public void LevelUpStrength()
    {
        if(PlayerLevelUp == 1 && P1StrengthValue < 10)
        {
            P1StrengthValue++;
            PlayerPrefs.SetInt("PlayerOneStrengthValue", P1StrengthValue);
            P1LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 2 && P2StrengthValue < 10)
        {
            P2StrengthValue++;
            PlayerPrefs.SetInt("PlayerTwoStrengthValue", P2StrengthValue);
            P2LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 3 && P3StrengthValue < 10)
        {
            P3StrengthValue++;
            PlayerPrefs.SetInt("PlayerThreeStrengthValue", P3StrengthValue);
            P3LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 4 && P4StrengthValue < 10)
        {
            P4StrengthValue++;
            PlayerPrefs.SetInt("PlayerFourStrengthValue", P4StrengthValue);
            P4LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 5 && P5StrengthValue < 10)
        {
            P5StrengthValue++;
            PlayerPrefs.SetInt("PlayerFiveStrengthValue", P5StrengthValue);
            P5LevelUp = false;
            LevelUp();
        }
    }

    public void LevelUpSpeed()
    {
        if(PlayerLevelUp == 1 && P1SpeedValue < 10)
        {
            P1SpeedValue++;
            PlayerPrefs.SetInt("PlayerOneSpeedValue", P1SpeedValue);
            P1LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 2 && P2SpeedValue < 10)
        {
            P2SpeedValue++;
            PlayerPrefs.SetInt("PlayerTwoSpeedValue", P2SpeedValue);
            P2LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 3 && P3SpeedValue < 10)
        {
            P3SpeedValue++;
            PlayerPrefs.SetInt("PlayerThreeSpeedValue", P3SpeedValue);
            P3LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 4 && P4SpeedValue < 10)
        {
            P4SpeedValue++;
            PlayerPrefs.SetInt("PlayerFourSpeedValue", P4SpeedValue);
            P4LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 5 && P5SpeedValue < 10)
        {
            P5SpeedValue++;
            PlayerPrefs.SetInt("PlayerFiveSpeedValue", P5SpeedValue);
            P5LevelUp = false;
            LevelUp();
        }
    }

    public void LevelUpAccuracy()
    {
        if(PlayerLevelUp == 1 && P1AccuracyValue < 10)
        {
            P1AccuracyValue++;
            PlayerPrefs.SetInt("PlayerOneAccuracyValue", P1AccuracyValue);
            P1LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 2 && P2AccuracyValue < 10)
        {
            P2AccuracyValue++;
            PlayerPrefs.SetInt("PlayerTwoAccuracyValue", P2AccuracyValue);
            P2LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 3 && P3AccuracyValue < 10)
        {
            P3AccuracyValue++;
            PlayerPrefs.SetInt("PlayerThreeAccuracyValue", P3AccuracyValue);
            P3LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 4 && P4AccuracyValue < 10)
        {
            P4AccuracyValue++;
            PlayerPrefs.SetInt("PlayerFourAccuracyValue", P4AccuracyValue);
            P4LevelUp = false;
            LevelUp();
        }
        else if(PlayerLevelUp == 5 && P5AccuracyValue < 10)
        {
            P5AccuracyValue++;
            PlayerPrefs.SetInt("PlayerFiveAccuracyValue", P5AccuracyValue);
            P5LevelUp = false;
            LevelUp();
        }
    }

    void PostGameContinue()
    {
        if(P1InjuryTime > 0)
        {
            P1InjuryTime--;
            PlayerPrefs.SetInt("PlayerOneInjuryTimePref", P1InjuryTime);
        }
        if(P2InjuryTime > 0)
        {
            P2InjuryTime--;
            PlayerPrefs.SetInt("PlayerTwoInjuryTimePref", P2InjuryTime);
        }
        if(P3InjuryTime > 0)
        {
            P3InjuryTime--;
            PlayerPrefs.SetInt("PlayerThreeInjuryTimePref", P3InjuryTime);
        }
        if(P4InjuryTime > 0)
        {
            P4InjuryTime--;
            PlayerPrefs.SetInt("PlayerFourInjuryTimePref", P4InjuryTime);
        }
        if(P5InjuryTime > 0)
        {
            P5InjuryTime--;
            PlayerPrefs.SetInt("PlayerFiveInjuryTimePref", P5InjuryTime);
        }

        if(SeasonSimulationScript.endPostSeason || (!seasonSimulationScript.playerMadePostSeason && gameNumber > regSeasonGames))
        {
            seasonSimulationScript.NewSeason();
            SeasonSimulationScript.endPostSeason = false;
            seasonSimulationScript.UpdateStandingsText();
        } else {
            int random = Random.Range(1, 4);
            if(random == 1 && (rosterSpotsFilled > 0 || draftPicks > 0) && !seasonSimulationScript.playerMadePostSeason)
            {
                CreateTrade();
            } 
            else if(random == 2 && rosterSpotsFilled > 0 && P1InjuryTime == 0 && P2InjuryTime == 0 && P3InjuryTime == 0 && P4InjuryTime == 0 && P5InjuryTime == 0)
            {
                Injury();
            } else {
                BackToMenu();
            }
        }
    }

    public void NextPlayer()
    {
        if(rosterSpotsFilled > 1 && currentPlayerStatsScreenIndex < playerStatsScreens.Count - 1)
        {
            currentPlayerStatsScreenIndex++;
        }
        
        StatsScreenUpdateTexts();
    }

    public void PreviousPlayer()
    {
        if(rosterSpotsFilled > 1 && currentPlayerStatsScreenIndex > 0)
        {
            currentPlayerStatsScreenIndex--;
        }
        StatsScreenUpdateTexts();
    }

    public void CutPlayerStats()
    {
        CutPlayer(playerStatsScreens[currentPlayerStatsScreenIndex]);

        Debug.Log(currentPlayerStatsScreenIndex + ", " + playerStatsScreens.Count);
        if(currentPlayerStatsScreenIndex == playerStatsScreens.Count)
        {
            currentPlayerStatsScreenIndex--;
        }
        StatsScreenUpdateTexts();
    }

    void CutPlayer(int condition)
    {
        if(condition == 1)
        {
            P1SpotFilled = 0;
            PlayerPrefs.SetInt("PlayerOneRosterSpotFilledPref", P1SpotFilled);
            playerStatsScreens.Remove(1);

            P1StrengthValue = Random.Range(1, 5);
            P1SpeedValue = Random.Range(1, 5);
            P1AccuracyValue = Random.Range(1, 5);
            P1Name = "";
            P1Height = Random.Range(70, 76);
            P1Weight = Random.Range(36, 40) * 5;
            P1XP = 0;
            P1Condition = 0;
            P1Influence = 0;
            AssignNewPlayerPrefs("One", P1Name, P1StrengthValue, P1SpeedValue, P1AccuracyValue, P1Height, P1Weight, P1Condition, P1Influence);
        }
        else if(condition == 2)
        {
            P2SpotFilled = 0;
            PlayerPrefs.SetInt("PlayerTwoRosterSpotFilledPref", P2SpotFilled);
            playerStatsScreens.Remove(2);

            P2StrengthValue = Random.Range(1, 5);
            P2SpeedValue = Random.Range(1, 5);
            P2AccuracyValue = Random.Range(1, 5);
            P2Name = "";
            P2Height = Random.Range(70, 76);
            P2Weight = Random.Range(36, 40) * 5;
            P2XP = 0;
            P2Condition = 0;
            P2Influence = 0;
            AssignNewPlayerPrefs("Two", P2Name, P2StrengthValue, P2SpeedValue, P2AccuracyValue, P2Height, P2Weight, P2Condition, P2Influence);
        }
        else if(condition == 3)
        {
            P3SpotFilled = 0;
            PlayerPrefs.SetInt("PlayerThreeRosterSpotFilledPref", P3SpotFilled);
            playerStatsScreens.Remove(3);

            P3StrengthValue = Random.Range(1, 5);
            P3SpeedValue = Random.Range(1, 5);
            P3AccuracyValue = Random.Range(1, 5);
            P3Name = "";
            P3Height = Random.Range(70, 76);
            P3Weight = Random.Range(36, 40) * 5;
            P3XP = 0;
            P3Condition = 0;
            P3Influence = 0;
            AssignNewPlayerPrefs("Three", P3Name, P3StrengthValue, P3SpeedValue, P3AccuracyValue, P3Height, P3Weight, P3Condition, P3Influence);
        }
        else if(condition == 4)
        {
            P4SpotFilled = 0;
            PlayerPrefs.SetInt("PlayerFourRosterSpotFilledPref", P4SpotFilled);
            playerStatsScreens.Remove(4);

            P4StrengthValue = Random.Range(1, 5);
            P4SpeedValue = Random.Range(1, 5);
            P4AccuracyValue = Random.Range(1, 5);
            P4Name = "";
            P4Height = Random.Range(70, 76);
            P4Weight = Random.Range(36, 40) * 5;
            P4XP = 0;
            P4Condition = 0;
            P4Influence = 0;
            AssignNewPlayerPrefs("Four", P4Name, P4StrengthValue, P4SpeedValue, P4AccuracyValue, P4Height, P4Weight, P4Condition, P4Influence);
        }
        else if(condition == 5)
        {
            P5SpotFilled = 0;
            PlayerPrefs.SetInt("PlayerFiveRosterSpotFilledPref", P5SpotFilled);
            playerStatsScreens.Remove(5);

            P5StrengthValue = Random.Range(1, 5);
            P5SpeedValue = Random.Range(1, 5);
            P5AccuracyValue = Random.Range(1, 5);
            P5Name = "";
            P5Height = Random.Range(70, 76);
            P5Weight = Random.Range(36, 40) * 5;
            P5XP = 0;
            P5Condition = 0;
            P5Influence = 0;
            AssignNewPlayerPrefs("Five", P5Name, P5StrengthValue, P5SpeedValue, P5AccuracyValue, P5Height, P5Weight, P5Condition, P5Influence);
        }
        rosterSpotsFilled -= 1;
        PlayerPrefs.SetInt("RosterSpotsFilledPref", rosterSpotsFilled);
    }

    void StatsScreenUpdateTexts()
    {
        if(rosterSpotsFilled > 0)
        {
            UpdatePlayerNumberText();
            UpdateStrengthText();
            UpdateSpeedText();
            UpdateAccuracyText();
            UpdateCareerText();
            UpdateCreditsText();
            UpdateDraftPicksText();
            UpdatePlayerInfluenceText();
        } else {
            SwitchPanel(StatsNoPlayersPanel);
        }
    }

    void AssignNewPlayerPrefs(string playerNum, string name, int strength, int speed, int accuracy, int height, int weight, int condition, int influence)
    {
        PlayerPrefs.SetString("Player" + playerNum + "NamePref", name);
        PlayerPrefs.SetInt("Player" + playerNum + "StrengthPref", strength);
        PlayerPrefs.SetInt("Player" + playerNum + "SpeedPref", speed);
        PlayerPrefs.SetInt("Player" + playerNum + "AccuracyPref", accuracy);
        PlayerPrefs.SetInt("Player" + playerNum + "HeightPref", height);
        PlayerPrefs.SetInt("Player" + playerNum + "WeightPref", weight);
        PlayerPrefs.SetInt("Player" + playerNum + "InjuryTimePref", 0);
        PlayerPrefs.SetInt("Player" + playerNum + "XPPref", 0);
        PlayerPrefs.SetInt("Player" + playerNum + "ConditionPref", condition);
        PlayerPrefs.SetInt("Player" + playerNum + "InfluencePref", influence);
    }

    // attribute setting button functions
    public void AddStrength()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1StrengthValue < 10 && Credits > 0)
        {
            P1StrengthValue++; 
            PlayerPrefs.SetInt("PlayerOneStrengthPref", P1StrengthValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateStrengthText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2StrengthValue < 10 && Credits > 0)
        {
            P2StrengthValue++; 
            PlayerPrefs.SetInt("PlayerTwoStrengthPref", P2StrengthValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateStrengthText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3StrengthValue < 10 && Credits > 0)
        {
            P3StrengthValue++; 
            PlayerPrefs.SetInt("PlayerThreeStrengthPref", P3StrengthValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateStrengthText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4StrengthValue < 10 && Credits > 0)
        {
            P4StrengthValue++; 
            PlayerPrefs.SetInt("PlayerFourStrengthPref", P4StrengthValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateStrengthText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5StrengthValue < 10 && Credits > 0)
        {
            P5StrengthValue++; 
            PlayerPrefs.SetInt("PlayerFiveStrengthPref", P5StrengthValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateStrengthText();
            UpdateCreditsText();
        }
    }

    public void SubtractStrength()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1StrengthValue > 1)
        {
            P1StrengthValue--;
            UpdateStrengthText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2StrengthValue > 1)
        {
            P2StrengthValue--;
            UpdateStrengthText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3StrengthValue > 1)
        {
            P3StrengthValue--;
            UpdateStrengthText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4StrengthValue > 1)
        {
            P4StrengthValue--;
            UpdateStrengthText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5StrengthValue > 1)
        {
            P5StrengthValue--;
            UpdateStrengthText();
        }
    }

    public void AddSpeed()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1SpeedValue < 10 && Credits > 0)
        {
            P1SpeedValue++; 
            PlayerPrefs.SetInt("PlayerOneSpeedPref", P1SpeedValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateSpeedText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2SpeedValue < 10 && Credits > 0)
        {
            P2SpeedValue++; 
            PlayerPrefs.SetInt("PlayerTwoSpeedPref", P2SpeedValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateSpeedText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3SpeedValue < 10 && Credits > 0)
        {
            P3SpeedValue++; 
            PlayerPrefs.SetInt("PlayerThreeSpeedPref", P3SpeedValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateSpeedText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4SpeedValue < 10 && Credits > 0)
        {
            P4SpeedValue++; 
            PlayerPrefs.SetInt("PlayerFourSpeedPref", P4SpeedValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateSpeedText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5SpeedValue < 10 && Credits > 0)
        {
            P5SpeedValue++; 
            PlayerPrefs.SetInt("PlayerFiveSpeedPref", P5SpeedValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateSpeedText();
            UpdateCreditsText();
        }
    }

    public void SubtractSpeed()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1SpeedValue > 1)
        {
            P1SpeedValue--;
            UpdateSpeedText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2SpeedValue > 1)
        {
            P2SpeedValue--;
            UpdateSpeedText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3SpeedValue > 1)
        {
            P3SpeedValue--;
            UpdateSpeedText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4SpeedValue > 1)
        {
            P4SpeedValue--;
            UpdateSpeedText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5SpeedValue > 1)
        {
            P5SpeedValue--;
            UpdateSpeedText();
        }
    }

    public void AddAccuracy()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1AccuracyValue < 10 && Credits > 0)
        {
            P1AccuracyValue++; 
            PlayerPrefs.SetInt("PlayerOneAccuracyPref", P1AccuracyValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateAccuracyText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2AccuracyValue < 10 && Credits > 0)
        {
            P2AccuracyValue++; 
            PlayerPrefs.SetInt("PlayerTwoAccuracyPref", P2AccuracyValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateAccuracyText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3AccuracyValue < 10 && Credits > 0)
        {
            P3AccuracyValue++; 
            PlayerPrefs.SetInt("PlayerThreeAccuracyPref", P3AccuracyValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateAccuracyText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4AccuracyValue < 10 && Credits > 0)
        {
            P4AccuracyValue++; 
            PlayerPrefs.SetInt("PlayerFourAccuracyPref", P4AccuracyValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateAccuracyText();
            UpdateCreditsText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5AccuracyValue < 10 && Credits > 0)
        {
            P5AccuracyValue++; 
            PlayerPrefs.SetInt("PlayerFiveAccuracyPref", P5AccuracyValue);
            Credits--;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateAccuracyText();
            UpdateCreditsText();
        }
    }

    public void SubtractAccuracy()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1 && P1AccuracyValue > 1)
        {
            P1AccuracyValue--;
            UpdateAccuracyText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2 && P2AccuracyValue > 1)
        {
            P2AccuracyValue--;
            UpdateAccuracyText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3 && P3AccuracyValue > 1)
        {
            P3AccuracyValue--;
            UpdateAccuracyText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4 && P4AccuracyValue > 1)
        {
            P4AccuracyValue--;
            UpdateAccuracyText();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5 && P5AccuracyValue > 1)
        {
            P5AccuracyValue--;
            UpdateAccuracyText();
        }
    }

    void UpdatePlayersXP()
    {
        int PreP1XP = P1XP;
        int PreP2XP = P2XP;
        int PreP3XP = P3XP;
        int PreP4XP = P4XP;
        int PreP5XP = P5XP;
        if(P1SpotFilled == 1)
        P1XP += (playerOnePoints + playerOneAst + playerOneReb + 1);
        if(P2SpotFilled == 1)
        P2XP += (playerTwoPoints + playerTwoAst + playerTwoReb + 1);
        if(P3SpotFilled == 1)
        P3XP += (playerThreePoints + playerThreeAst + playerThreeReb + 1);
        if(P4SpotFilled == 1)
        P4XP += (playerFourPoints + playerFourAst + playerFourReb + 1);
        if(P5SpotFilled == 1)
        P5XP += (playerFivePoints + playerFiveAst + playerFiveReb + 1);

        PlayerPrefs.SetInt("PlayerOneXPPref", P1XP);
        PlayerPrefs.SetInt("PlayerTwoXPPref", P2XP);
        PlayerPrefs.SetInt("PlayerThreeXPPref", P3XP);
        PlayerPrefs.SetInt("PlayerFourXPPref", P4XP);
        PlayerPrefs.SetInt("PlayerFiveXPPref", P5XP);

        P1LevelUp = getLevelUp(PreP1XP, P1XP);
        P2LevelUp = getLevelUp(PreP2XP, P2XP);
        P3LevelUp = getLevelUp(PreP3XP, P3XP);
        P4LevelUp = getLevelUp(PreP4XP, P4XP);
        P5LevelUp = getLevelUp(PreP5XP, P5XP);

        string pgxptext = "";
        string xp;

        xp = getXPText(P1XP);
        if(P1SpotFilled == 1)
        pgxptext += P1Name + " XP - " + xp + "\n";

        xp = getXPText(P2XP);
        if(P2SpotFilled == 1)
        pgxptext += P2Name + " XP - " + xp + "\n";

        xp = getXPText(P3XP);
        if(P3SpotFilled == 1)
        pgxptext += P3Name + " XP - " + xp + "\n";

        xp = getXPText(P4XP);
        if(P4SpotFilled == 1)
        pgxptext += P4Name + " XP - " + xp + "\n";

        xp = getXPText(P5XP);
        if(P5SpotFilled == 1)
        pgxptext += P5Name + " XP - " + xp + "\n";

        PostGameXPText.text = pgxptext;
    }

    string getXPText(int playerXP)
    {
        // int playerLevel = playerXP / levelUpBase + (levelUpBase * (playerXP / levelUpBase));
        int playerLevel = CalculatePlayerLevel(playerXP);
        // return ((playerXP % (levelUpBase * (playerLevel + 1))) - levelUpBase * playerLevel) + " / " + (levelUpBase * (playerLevel + 1));
        int xpForCurrentLevel = (playerLevel * (playerLevel + 1) / 2) * levelUpBase;
        int xpForNextLevel = ((playerLevel + 1) * (playerLevel + 2) / 2) * levelUpBase;
        int xpToNextLevel = xpForNextLevel - playerXP;
        int currentLevelXP = playerXP - xpForCurrentLevel;
        return currentLevelXP + " / " + (xpForNextLevel - (levelUpBase * playerLevel));
    }

    bool getLevelUp(int preXP, int XP)
    {
        // return preXP < ((preXP / levelUpBase) + 1) * levelUpBase && XP >= ((preXP / levelUpBase) + 1) * levelUpBase;
        int levelBefore = CalculatePlayerLevel(preXP);
        int levelAfter = CalculatePlayerLevel(XP);
        return levelAfter > levelBefore;
    }

    int CalculatePlayerLevel(int playerXP)
    {
        float n = (-1f + Mathf.Sqrt(1f + 8f * (float)playerXP / (float)levelUpBase)) / 2f;
        return (int)Mathf.Floor(n);
    }
    
    public void SignFreeAgentOne()
    {
        SignFreeAgent(FA1Name, FA1Strength, FA1Speed, FA1Accuracy, FA1Height, FA1Weight, FA1Cost, 1);
    }
    public void SignFreeAgentTwo()
    {
        SignFreeAgent(FA2Name, FA2Strength, FA2Speed, FA2Accuracy, FA2Height, FA2Weight, FA2Cost, 2);
    }
    public void SignFreeAgentThree()
    {
        SignFreeAgent(FA3Name, FA3Strength, FA3Speed, FA3Accuracy, FA3Height, FA3Weight, FA3Cost, 3);
    }

    void SignFreeAgent(string freeAgentName, int freeAgentStrength, int freeAgentSpeed, int freeAgentAccuracy, int freeAgentHeight, int freeAgentWeight, int freeAgentCost, int freeAgentNum)
    {
        if(rosterSpotsFilled < 5 && Credits >= freeAgentCost)
        {
            if(P1SpotFilled == 0)
            {
                P1Name = freeAgentName;
                P1StrengthValue = freeAgentStrength;
                P1SpeedValue = freeAgentSpeed;
                P1AccuracyValue = freeAgentAccuracy;
                P1Height = freeAgentHeight;
                P1Weight = freeAgentWeight;
                P1InjuryTime = 0;
                P1SpotFilled = 1;
                P1XP = 0;
                P1Condition = GenerateCondition();
                P1Influence = returnRandom(0, 101);
                SignFreeAgentPrefs("One", P1Name, P1StrengthValue, P1SpeedValue, P1AccuracyValue, P1Height, P1Weight, P1Condition, P1Influence);
                PlayerPrefs.SetInt("PlayerOneRosterSpotFilledPref", P1SpotFilled);
                playerStatsScreens.Add(1);
            }
            else if(P2SpotFilled == 0)
            {
                P2Name = freeAgentName;
                P2StrengthValue = freeAgentStrength;
                P2SpeedValue = freeAgentSpeed;
                P2AccuracyValue = freeAgentAccuracy;
                P2Height = freeAgentHeight;
                P2Weight = freeAgentWeight;
                P2InjuryTime = 0;
                P2SpotFilled = 1;
                P2XP = 0;
                P2Influence = returnRandom(0, 101);
                P2Condition = GenerateCondition();
                SignFreeAgentPrefs("Two", P2Name, P2StrengthValue, P2SpeedValue, P2AccuracyValue, P2Height, P2Weight, P2Condition, P2Influence);
                PlayerPrefs.SetInt("PlayerTwoRosterSpotFilledPref", P2SpotFilled);
                playerStatsScreens.Add(2);
            }
            else if(P3SpotFilled == 0)
            {
                P3Name = freeAgentName;
                P3StrengthValue = freeAgentStrength;
                P3SpeedValue = freeAgentSpeed;
                P3AccuracyValue = freeAgentAccuracy;
                P3Height = freeAgentHeight;
                P3Weight = freeAgentWeight;
                P3InjuryTime = 0;
                P3SpotFilled = 1;
                P3XP = 0;
                P3Condition = GenerateCondition();
                P3Influence = returnRandom(0, 101);
                SignFreeAgentPrefs("Three", P3Name, P3StrengthValue, P3SpeedValue, P3AccuracyValue, P3Height, P3Weight, P3Condition, P3Influence);
                PlayerPrefs.SetInt("PlayerThreeRosterSpotFilledPref", P3SpotFilled);
                playerStatsScreens.Add(3);
            }
            else if(P4SpotFilled == 0)
            {
                P4Name = freeAgentName;
                P4StrengthValue = freeAgentStrength;
                P4SpeedValue = freeAgentSpeed;
                P4AccuracyValue = freeAgentAccuracy;
                P4Height = freeAgentHeight;
                P4Weight = freeAgentWeight;
                P4InjuryTime = 0;
                P4SpotFilled = 1;
                P4XP = 0;
                P4Condition = GenerateCondition();
                P4Influence = returnRandom(0, 101);
                SignFreeAgentPrefs("Four", P4Name, P4StrengthValue, P4SpeedValue, P4AccuracyValue, P4Height, P4Weight, P4Condition, P4Influence);
                PlayerPrefs.SetInt("PlayerFourRosterSpotFilledPref", P4SpotFilled);
                playerStatsScreens.Add(4);
            }
            else if(P5SpotFilled == 0)
            {
                P5Name = freeAgentName;
                P5StrengthValue = freeAgentStrength;
                P5SpeedValue = freeAgentSpeed;
                P5AccuracyValue = freeAgentAccuracy;
                P5Height = freeAgentHeight;
                P5Weight = freeAgentWeight;
                P5InjuryTime = 0;
                P5SpotFilled = 1;
                P5XP = 0;
                P5Condition = GenerateCondition();
                P5Influence = returnRandom(0, 101);
                SignFreeAgentPrefs("Five", P5Name, P5StrengthValue, P5SpeedValue, P5AccuracyValue, P5Height, P5Weight, P5Condition, P5Influence);
                PlayerPrefs.SetInt("PlayerFiveRosterSpotFilledPref", P5SpotFilled);
                playerStatsScreens.Add(5);
            }
            rosterSpotsFilled++;
            PlayerPrefs.SetInt("RosterSpotsFilledPref", rosterSpotsFilled);
            Credits -= freeAgentCost;
            PlayerPrefs.SetInt("CreditsPref", Credits);
            UpdateFreeAgentsText();

            if(freeAgentNum == 1)
            {
                FreeAgentOneElement.SetActive(false);
                FA1Signed = 1;
                PlayerPrefs.SetInt("FreeAgentOneSignedPref", FA1Signed);
            }
            else if(freeAgentNum == 2)
            {
                FreeAgentTwoElement.SetActive(false);
                FA2Signed = 1;
                PlayerPrefs.SetInt("FreeAgentTwoSignedPref", FA2Signed);
            }
            else if(freeAgentNum == 3)
            {
                FreeAgentThreeElement.SetActive(false);
                FA3Signed = 1;
                PlayerPrefs.SetInt("FreeAgentThreeSignedPref", FA3Signed);
            }
            playerStatsScreens.Sort();
        }
    }

    void SignFreeAgentPrefs(string playerNum, string name, int strength, int speed, int accuracy, int height, int weight, int condition, int influence)
    {
        PlayerPrefs.SetString("Player" + playerNum + "NamePref", name);
        PlayerPrefs.SetInt("Player" + playerNum + "StrengthPref", strength);
        PlayerPrefs.SetInt("Player" + playerNum + "SpeedPref", speed);
        PlayerPrefs.SetInt("Player" + playerNum + "AccuracyPref", accuracy);
        PlayerPrefs.SetInt("Player" + playerNum + "HeightPref", height);
        PlayerPrefs.SetInt("Player" + playerNum + "WeightPref", weight);
        PlayerPrefs.SetInt("Player" + playerNum + "XPPref", 0);
        PlayerPrefs.SetInt("Player" + playerNum + "ConditionPref", condition);
        PlayerPrefs.SetInt("Player" + playerNum + "InfluencePref", influence);
        ResetPlayerCareer(playerNum);
    }

    // cool idea for trades might be if a player is assigned like a "value" score
    // which is determined by how good all their attributes are, and the user can
    // see the value score of who they might receive but they don't get the exact
    // specifics of all their attributes
    void CreateTrade()
    {
        int traderIndex = seasonSimulationScript.teamIndexList[Random.Range(0, seasonSimulationScript.teamIndexList.Count)];
        tradeHeadlineText.text = "The " + SeasonSimulationScript.teamsTemp[traderIndex].getName() + " are offering a trade";
        tradeName = GenerateName();
        tradeStrength = returnRandom(1, 9);
        tradeSpeed = returnRandom(1, 9);
        tradeAccuracy = returnRandom(1, 9);
        tradeHeight = GenerateHeight();
        tradeWeight = GenerateWeight();
        tradeType = Random.Range(1, 4);
        // trade type one = user receives draft pick, trader receives player
        // trade type two = user receives player, trader receives draft pick
        // trade type three = user receives player, trader receives player
        Debug.Log("RSF: " + rosterSpotsFilled);
        if(tradeType == 1 && rosterSpotsFilled > 0)
        {
            tradeType = 1;
            playerReceivesText.text = "YOU RECEIVE\n\n" + SeasonSimulationScript.teamsTemp[traderIndex].getName() + " 1st Round Pick";
            playerTradedIndex = playerStatsScreens[Random.Range(0, playerStatsScreens.Count)] - 1;
            string playerNumString = seasonSimulationScript.numbersToWords[playerTradedIndex];
            traderReceivesText.text = "TRADER RECEIVES\n\n" + PlayerPrefs.GetString("Player" + playerNumString + "NamePref") + "\nStrength: " + PlayerPrefs.GetInt("Player" + playerNumString + "StrengthPref") + "\nSpeed: " + PlayerPrefs.GetInt("Player" + playerNumString + "SpeedPref") + "\nAccuracy: " + PlayerPrefs.GetInt("Player" + playerNumString + "AccuracyPref") + "\n" + (int)(PlayerPrefs.GetInt("Player" + playerNumString + "HeightPref")/12) + "'" + (PlayerPrefs.GetInt("Player" + playerNumString + "HeightPref")%12) + " " + PlayerPrefs.GetInt("Player" + playerNumString + "WeightPref") + " lbs"; 
        }
        else if(tradeType == 2 && draftPicks > 0 && rosterSpotsFilled < 5)
        {
            tradeType = 2;
            playerReceivesText.text = "YOU RECEIVE\n\n" + tradeName + "\nStrength: " + tradeStrength + "\nSpeed: " + tradeSpeed + "\nAccuracy: " + tradeAccuracy + "\n" + (int)(tradeHeight/12) + "'" + (tradeHeight%12) + " " + tradeWeight + " lbs";
            traderReceivesText.text = "TRADER RECEIVES\n\n" + SeasonSimulationScript.teamsTemp[currentTeam - 1].getName() + " 1st Round Pick"; 
        } else {
            tradeType = 3;
            playerReceivesText.text = "YOU RECEIVE\n\n" + tradeName + "\nStrength: " + tradeStrength + "\nSpeed: " + tradeSpeed + "\nAccuracy: " + tradeAccuracy + "\n" + (int)(tradeHeight/12) + "'" + (tradeHeight%12) + " " + tradeWeight + " lbs";
            playerTradedIndex = playerStatsScreens[Random.Range(0, playerStatsScreens.Count)] - 1;
            string playerNumString = seasonSimulationScript.numbersToWords[playerTradedIndex];
            traderReceivesText.text = "TRADER RECEIVES\n\n" + PlayerPrefs.GetString("Player" + playerNumString + "NamePref") + "\nStrength: " + PlayerPrefs.GetInt("Player" + playerNumString + "StrengthPref") + "\nSpeed: " + PlayerPrefs.GetInt("Player" + playerNumString + "SpeedPref") + "\nAccuracy: " + PlayerPrefs.GetInt("Player" + playerNumString + "AccuracyPref") + "\n" + (int)(PlayerPrefs.GetInt("Player" + playerNumString + "HeightPref")/12) + "'" + (PlayerPrefs.GetInt("Player" + playerNumString + "HeightPref")%12) + " " + PlayerPrefs.GetInt("Player" + playerNumString + "WeightPref") + " lbs"; 
        }
        SwitchPanel(TradePanel);
    }

    public void AcceptTrade()
    {
        string playerNumString = seasonSimulationScript.numbersToWords[playerTradedIndex];

        if(tradeType == 1)
        {
            draftPicks++;
            PlayerPrefs.SetInt("DraftPicksPref", draftPicks);

            CutPlayer(playerTradedIndex + 1);
        }
        else if(tradeType == 2)
        {
            draftPicks--;
            PlayerPrefs.SetInt("DraftPicksPref", draftPicks);
        }

        if(tradeType == 2 || tradeType == 3)
        {
            PlayerPrefs.SetString("Player" + playerNumString + "NamePref", tradeName);
            PlayerPrefs.SetInt("Player" + playerNumString + "StrengthPref", tradeStrength);
            PlayerPrefs.SetInt("Player" + playerNumString + "SpeedPref", tradeSpeed);
            PlayerPrefs.SetInt("Player" + playerNumString + "AccuracyPref", tradeAccuracy);
            PlayerPrefs.SetInt("Player" + playerNumString + "HeightPref", tradeHeight);
            PlayerPrefs.SetInt("Player" + playerNumString + "WeightPref", tradeWeight);
            PlayerPrefs.SetInt("Player" + playerNumString + "InjuryTimePref", 0);
            PlayerPrefs.SetInt("Player" + playerNumString + "RosterSpotFilled", 1);
            PlayerPrefs.SetInt("Player" + playerNumString + "XPPref", 0);
            PlayerPrefs.SetInt("Player" + playerNumString + "ConditionPref", GenerateCondition());
            PlayerPrefs.SetInt("Player" + playerNumString + "InfluencePref", returnRandom(0, 101));
            ResetPlayerCareer(playerNumString);

            if(((P1SpotFilled == 0 && tradeType == 2) || playerTradedIndex + 1 == 1 && tradeType == 3))
            {
                P1Name = tradeName;
                P1StrengthValue = tradeStrength;
                P1SpeedValue = tradeSpeed;
                P1AccuracyValue = tradeAccuracy;
                P1Height = tradeHeight;
                P1Weight = tradeWeight;
                P1InjuryTime = 0;
                P1SpotFilled = 1;
                P1XP = 0;
                P1Condition = PlayerPrefs.GetInt("PlayerOneConditionPref");
                P1Influence = PlayerPrefs.GetInt("PlayerOneInfluencePref");
                if(tradeType == 2)
                {
                    playerStatsScreens.Add(1);
                }
            }
            else if((P2SpotFilled == 0 && tradeType == 2) || (playerTradedIndex + 1 == 2 && tradeType == 3))
            {
                P2Name = tradeName;
                P2StrengthValue = tradeStrength;
                P2SpeedValue = tradeSpeed;
                P2AccuracyValue = tradeAccuracy;
                P2Height = tradeHeight;
                P2Weight = tradeWeight;
                P2InjuryTime = 0;
                P2SpotFilled = 0;
                P2XP = 0;
                P2Condition = PlayerPrefs.GetInt("PlayerTwoConditionPref");
                P2Influence = PlayerPrefs.GetInt("PlayerTwoInfluencePref");
                if(tradeType == 2)
                {
                    playerStatsScreens.Add(2);
                }
            }
            else if((P3SpotFilled == 0 && tradeType == 2) || playerTradedIndex + 1 == 3 && tradeType == 3)
            {
                P3Name = tradeName;
                P3StrengthValue = tradeStrength;
                P3SpeedValue = tradeSpeed;
                P3AccuracyValue = tradeAccuracy;
                P3Height = tradeHeight;
                P3Weight = tradeWeight;
                P3InjuryTime = 0;
                P3SpotFilled = 0;
                P3XP = 0;
                P3Condition = PlayerPrefs.GetInt("PlayerThreeConditionPref");
                P3Influence = PlayerPrefs.GetInt("PlayerThreeInfluencePref");
                if(tradeType == 2)
                {
                    playerStatsScreens.Add(3);
                }
            }
            else if((P4SpotFilled == 0 && tradeType == 2) || playerTradedIndex + 1 == 4 && tradeType == 3)
            {
                P4Name = tradeName;
                P4StrengthValue = tradeStrength;
                P4SpeedValue = tradeSpeed;
                P4AccuracyValue = tradeAccuracy;
                P4Height = tradeHeight;
                P4Weight = tradeWeight;
                P4InjuryTime = 0;
                P4SpotFilled = 0;
                P4XP = 0;
                P4Condition = PlayerPrefs.GetInt("PlayerFourConditionPref");
                P4Influence = PlayerPrefs.GetInt("PlayerFourInfluencePref");
                if(tradeType == 2)
                {
                    playerStatsScreens.Add(4);
                }
            }
            else if((P5SpotFilled == 0 && tradeType == 2) || playerTradedIndex + 1 == 5 && tradeType == 3)
            {
                P5Name = tradeName;
                P5StrengthValue = tradeStrength;
                P5SpeedValue = tradeSpeed;
                P5AccuracyValue = tradeAccuracy;
                P5Height = tradeHeight;
                P5Weight = tradeWeight;
                P5InjuryTime = 0;
                P5SpotFilled = 0;
                P5XP = 0;
                P5Condition = PlayerPrefs.GetInt("PlayerFiveConditionPref");
                P5Influence = PlayerPrefs.GetInt("PlayerFiveInfluencePref");
                if(tradeType == 2)
                {
                    playerStatsScreens.Add(5);
                }
            }
        }
        
        SwitchPanel(StartMenuPanel);
    }

    void Injury()
    {
        int injuredPlayer = playerStatsScreens[Random.Range(0, playerStatsScreens.Count)];
        int injuredTime = Random.Range(1, 4);
        if(injuredPlayer == 1)
        {
            P1InjuryTime = injuredTime;
            PlayerPrefs.SetInt("PlayerOneInjuryTimePref", P1InjuryTime);
        }
        else if(injuredPlayer == 2)
        {
            P2InjuryTime = injuredTime;
            PlayerPrefs.SetInt("PlayerTwoInjuryTimePref", P2InjuryTime);
        }
        else if(injuredPlayer == 3)
        {
            P3InjuryTime = injuredTime;
            PlayerPrefs.SetInt("PlayerThreeInjuryTimePref", P3InjuryTime);
        }
        else if(injuredPlayer == 4)
        {
            P4InjuryTime = injuredTime;
            PlayerPrefs.SetInt("PlayerFourInjuryTimePref", P4InjuryTime);
        }
        else if(injuredPlayer == 5)
        {
            P5InjuryTime = injuredTime;
            PlayerPrefs.SetInt("PlayerFiveInjuryTimePref", P5InjuryTime);
        }

        UpdateInjuryText(injuredPlayer);
        SwitchPanel(InjuryPanel);
    }

    public void Draft()
    {
        if(draftPicks > 0)
        {
            drafting = true;
            DraftRoundText.text = "DRAFT ROUND ONE";
            GenerateDraftOptions(4, 9);
            DraftOptionOneText.text = draftOptionOneName + "\nStrength: " + draftOptionOneStrength + "\nSpeed: " + draftOptionOneSpeed + "\nAccuracy: " + draftOptionOneAccuracy + "\n" + (int)(draftOptionOneHeight/12) + "'" + (draftOptionOneHeight%12) + " " + draftOptionOneWeight + " lbs";
            DraftOptionTwoText.text = draftOptionTwoName + "\nStrength: " + draftOptionTwoStrength + "\nSpeed: " + draftOptionTwoSpeed + "\nAccuracy: " + draftOptionTwoAccuracy + "\n" + (int)(draftOptionTwoHeight/12) + "'" + (draftOptionTwoHeight%12) + " " + draftOptionTwoWeight + " lbs";
            DraftOptionThreeText.text = draftOptionThreeName + "\nStrength: " + draftOptionThreeStrength + "\nSpeed: " + draftOptionThreeSpeed + "\nAccuracy: " + draftOptionThreeAccuracy + "\n" + (int)(draftOptionThreeHeight/12) + "'" + (draftOptionThreeHeight%12) + " " + draftOptionThreeWeight + " lbs";
            DraftOptionFourText.text = draftOptionFourName + "\nStrength: " + draftOptionFourStrength + "\nSpeed: " + draftOptionFourSpeed + "\nAccuracy: " + draftOptionFourAccuracy + "\n" + (int)(draftOptionFourHeight/12) + "'" + (draftOptionFourHeight%12) + " " + draftOptionFourWeight + " lbs";
            DraftOptionFiveText.text = draftOptionFiveName + "\nStrength: " + draftOptionFiveStrength + "\nSpeed: " + draftOptionFiveSpeed + "\nAccuracy: " + draftOptionFiveAccuracy + "\n" + (int)(draftOptionFiveHeight/12) + "'" + (draftOptionFiveHeight%12) + " " + draftOptionFiveWeight + " lbs";
            DraftOptionSixText.text = draftOptionSixName + "\nStrength: " + draftOptionSixStrength + "\nSpeed: " + draftOptionSixSpeed + "\nAccuracy: " + draftOptionSixAccuracy + "\n" + (int)(draftOptionSixHeight/12) + "'" + (draftOptionSixHeight%12) + " " + draftOptionSixWeight + " lbs";

            SwitchPanel(DraftPanel);
        } else {
            EndDraft();
        }
    }

    public void EndDraft()
    {
        drafting = false;
        SwitchPanel(StartMenuPanel);
        draftPicks = 1;
        PlayerPrefs.SetInt("DraftPicksPref", draftPicks);
    }

    public void DraftOptionOne()
    {
        DraftPlayer(draftOptionOneName, draftOptionOneStrength, draftOptionOneSpeed, draftOptionOneAccuracy, draftOptionOneHeight, draftOptionOneWeight);
    }
    public void DraftOptionTwo()
    {
        DraftPlayer(draftOptionTwoName, draftOptionTwoStrength, draftOptionTwoSpeed, draftOptionTwoAccuracy, draftOptionTwoHeight, draftOptionTwoWeight);
    }
    public void DraftOptionThree()
    {
        DraftPlayer(draftOptionThreeName, draftOptionThreeStrength, draftOptionThreeSpeed, draftOptionThreeAccuracy, draftOptionThreeHeight, draftOptionThreeWeight);
    }
    public void DraftOptionFour()
    {
        DraftPlayer(draftOptionFourName, draftOptionFourStrength, draftOptionFourSpeed, draftOptionFourAccuracy, draftOptionFourHeight, draftOptionFourWeight);
    }
    public void DraftOptionFive()
    {
        DraftPlayer(draftOptionFiveName, draftOptionFiveStrength, draftOptionFiveSpeed, draftOptionFiveAccuracy, draftOptionFiveHeight, draftOptionFiveWeight);
    }
    public void DraftOptionSix()
    {
        DraftPlayer(draftOptionSixName, draftOptionSixStrength, draftOptionSixSpeed, draftOptionSixAccuracy, draftOptionSixHeight, draftOptionSixWeight);
    }

    void DraftPlayer(string name, int strength, int speed, int accuracy, int height, int weight)
    {
        if(rosterSpotsFilled < 5)
        {
            if(P1SpotFilled == 0)
            {
                P1Name = name;
                P1StrengthValue = strength;
                P1SpeedValue = speed;
                P1AccuracyValue = accuracy;
                P1Height = height;
                P1Weight = weight;
                P1InjuryTime = 0;
                P1SpotFilled = 1;
                P1XP = 0;
                P1Condition = GenerateCondition();
                P1Influence = returnRandom(0, 101);
                AssignNewPlayerPrefs("One", name, strength, speed, accuracy, height, weight, P1Condition, P1Influence);
                PlayerPrefs.SetInt("PlayerOneRosterSpotFilledPref", P1SpotFilled);
                playerStatsScreens.Add(1);
            }
            else if(P2SpotFilled == 0)
            {
                P2Name = name;
                P2StrengthValue = strength;
                P2SpeedValue = speed;
                P2AccuracyValue = accuracy;
                P2Height = height;
                P2Weight = weight;
                P2InjuryTime = 0;
                P2SpotFilled = 1;
                P2XP = 0;
                P2Condition = GenerateCondition();
                P2Influence = returnRandom(0, 101);
                AssignNewPlayerPrefs("Two", name, strength, speed, accuracy, height, weight, P2Condition, P2Influence);
                PlayerPrefs.SetInt("PlayerTwoRosterSpotFilledPref", P2SpotFilled);
                playerStatsScreens.Add(2);
            }
            else if(P3SpotFilled == 0)
            {
                P3Name = name;
                P3StrengthValue = strength;
                P3SpeedValue = speed;
                P3AccuracyValue = accuracy;
                P3Height = height;
                P3Weight = weight;
                P3InjuryTime = 0;
                P3SpotFilled = 1;
                P3XP = 0;
                P3Condition = GenerateCondition();
                P3Influence = returnRandom(0, 101);
                AssignNewPlayerPrefs("Three", name, strength, speed, accuracy, height, weight, P3Condition, P3Influence);
                PlayerPrefs.SetInt("PlayerThreeRosterSpotFilledPref", P3SpotFilled);
                playerStatsScreens.Add(3);
            }
            else if(P4SpotFilled == 0)
            {
                P4Name = name;
                P4StrengthValue = strength;
                P4SpeedValue = speed;
                P4AccuracyValue = accuracy;
                P4Height = height;
                P4Weight = weight;
                P4InjuryTime = 0;
                P4SpotFilled = 1;
                P4XP = 0;
                P4Condition = GenerateCondition();
                P4Influence = returnRandom(0, 101);
                AssignNewPlayerPrefs("Four", name, strength, speed, accuracy, height, weight, P4Condition, P4Influence);
                PlayerPrefs.SetInt("PlayerFourRosterSpotFilledPref", P4SpotFilled);
                playerStatsScreens.Add(4);
            }
            else if(P5SpotFilled == 0)
            {
                P5Name = name;
                P5StrengthValue = strength;
                P5SpeedValue = speed;
                P5AccuracyValue = accuracy;
                P5Height = height;
                P5Weight = weight;
                P5InjuryTime = 0;
                P5SpotFilled = 1;
                P5XP = 0;
                P5Condition = GenerateCondition();
                P5Influence = returnRandom(0, 101);
                AssignNewPlayerPrefs("Five", name, strength, speed, accuracy, height, weight, P5Condition, P5Influence);
                PlayerPrefs.SetInt("PlayerFiveRosterSpotFilledPref", P5SpotFilled);
                playerStatsScreens.Add(5);
            }
            rosterSpotsFilled++;
            PlayerPrefs.SetInt("RosterSpotsFilledPref", rosterSpotsFilled);
            playerStatsScreens.Sort();

            draftPicks--;
            Draft();
        }
    }

    void GenerateDraftOptions(int min, int max)
    {
        draftOptionOneName = GenerateName();
        draftOptionTwoName = GenerateName();
        draftOptionThreeName = GenerateName();
        draftOptionFourName = GenerateName();
        draftOptionFiveName = GenerateName();
        draftOptionSixName = GenerateName();
        
        draftOptionOneStrength = returnRandom(min, max + 1);
        draftOptionTwoStrength = returnRandom(min, max + 1);
        draftOptionThreeStrength = returnRandom(min, max + 1);
        draftOptionFourStrength = returnRandom(min, max + 1);
        draftOptionFiveStrength = returnRandom(min, max + 1);
        draftOptionSixStrength = returnRandom(min, max + 1);

        draftOptionOneSpeed = returnRandom(min, max + 1);
        draftOptionTwoSpeed = returnRandom(min, max + 1);
        draftOptionThreeSpeed = returnRandom(min, max + 1);
        draftOptionFourSpeed = returnRandom(min, max + 1);
        draftOptionFiveSpeed = returnRandom(min, max + 1);
        draftOptionSixSpeed = returnRandom(min, max + 1);

        draftOptionOneAccuracy = returnRandom(min, max + 1);
        draftOptionTwoAccuracy = returnRandom(min, max + 1);
        draftOptionThreeAccuracy = returnRandom(min, max + 1);
        draftOptionFourAccuracy = returnRandom(min, max + 1);
        draftOptionFiveAccuracy = returnRandom(min, max + 1);
        draftOptionSixAccuracy = returnRandom(min, max + 1);

        draftOptionOneHeight = GenerateHeight();
        draftOptionTwoHeight = GenerateHeight();
        draftOptionThreeHeight = GenerateHeight();
        draftOptionFourHeight = GenerateHeight();
        draftOptionFiveHeight = GenerateHeight();
        draftOptionSixHeight = GenerateHeight();

        draftOptionOneWeight = GenerateWeight();
        draftOptionTwoWeight = GenerateWeight();
        draftOptionThreeWeight = GenerateWeight();
        draftOptionFourWeight = GenerateWeight();
        draftOptionFiveWeight = GenerateWeight();
        draftOptionSixWeight = GenerateWeight();
    }

    // text update functions
    void UpdateStrengthText()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            strengthValueText.text = P1StrengthValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            strengthValueText.text = P2StrengthValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            strengthValueText.text = P3StrengthValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            strengthValueText.text = P4StrengthValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            strengthValueText.text = P5StrengthValue.ToString();
        }
    }

    void UpdateSpeedText()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            speedValueText.text = P1SpeedValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            speedValueText.text = P2SpeedValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            speedValueText.text = P3SpeedValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            speedValueText.text = P4SpeedValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            speedValueText.text = P5SpeedValue.ToString();
        }
    }

    void UpdateAccuracyText()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            accuracyValueText.text = P1AccuracyValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            accuracyValueText.text = P2AccuracyValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            accuracyValueText.text = P3AccuracyValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            accuracyValueText.text = P4AccuracyValue.ToString();
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            accuracyValueText.text = P5AccuracyValue.ToString();
        }
    }

    void UpdateLevelUpText(string playerName, int strength, int speed, int accuracy)
    {
        LevelUpPlayerNameText.text = playerName;
        LevelUpStrengthValueText.text = strength.ToString();
        LevelUpSpeedValueText.text = speed.ToString();
        LevelUpAccuracyValueText.text = accuracy.ToString();
    }

    public void UpdateTexts()
    {
        // Debug.Log("Index: " + currentPlayerStatsScreenIndex + ", " + playerStatsScreens[currentPlayerStatsScreenIndex]);
        Debug.Log("Index: " + currentPlayerStatsScreenIndex + ", Count: " + playerStatsScreens.Count + ", RSF: " + rosterSpotsFilled);
        if(rosterSpotsFilled > 0)
        {
            UpdateStrengthText();
            UpdateSpeedText();
            UpdateAccuracyText();
            UpdatePlayerNumberText();
            UpdateCareerText();
        }
        
        UpdateCreditsText();
        UpdateGameNumberText();
        UpdateTeamNameText();
        UpdateFreeAgentsText();
        UpdateConditionText();
    }

    void UpdatePlayerNumberText()
    {
        string injured = "";
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            if(P1InjuryTime > 0)
            {
                injured = " (X)";
            }
            PlayerNumberText.text = P1Name + injured;
            HeightWeightConditionText.text = "" + (int)(P1Height/12) + "'" + (P1Height%12) + " " + P1Weight + " lbs   " + P1Condition + "% Condition";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            if(P2InjuryTime > 0)
            {
                injured = " (X)";
            }
            PlayerNumberText.text = P2Name + injured;
            HeightWeightConditionText.text = "" + (int)(P2Height/12) + "'" + (P2Height%12) + " " + P2Weight + " lbs   " + P2Condition + "% Condition";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            if(P3InjuryTime > 0)
            {
                injured = " (X)";
            }
            PlayerNumberText.text = P3Name + injured;
            HeightWeightConditionText.text = "" + (int)(P3Height/12) + "'" + (P3Height%12) + " " + P3Weight + " lbs   " + P3Condition + "% Condition";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            if(P4InjuryTime > 0)
            {
                injured = " (X)";
            }
            PlayerNumberText.text = P4Name + injured;
            HeightWeightConditionText.text = "" + (int)(P4Height/12) + "'" + (P4Height%12) + " " + P4Weight + " lbs   " + P4Condition + "% Condition";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            if(P5InjuryTime > 0)
            {
                injured = " (X)";
            }
            PlayerNumberText.text = P5Name + injured;
            HeightWeightConditionText.text = "" + (int)(P5Height/12) + "'" + (P5Height%12) + " " + P5Weight + " lbs   " + P5Condition + "% Condition";
        }
    }

    void UpdatePlayerInfluenceText()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            PlayerInfluenceText.text = P1Influence + "% Influence";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            PlayerInfluenceText.text = P2Influence + "% Influence";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            PlayerInfluenceText.text = P3Influence + "% Influence";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            PlayerInfluenceText.text = P4Influence + "% Influence";
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            PlayerInfluenceText.text = P5Influence + "% Influence";
        }
    }

    void UpdateInjuryText(int injuredPlayer)
    {
        string playerName = "";
        int injuryTime = 0;
        if(injuredPlayer == 1)
        {
            playerName = P1Name;
            injuryTime = P1InjuryTime;
        }
        else if(injuredPlayer == 2)
        {
            playerName = P2Name;
            injuryTime = P2InjuryTime;
        }
        else if(injuredPlayer == 3)
        {
            playerName = P3Name;
            injuryTime = P3InjuryTime;
        }
        else if(injuredPlayer == 4)
        {
            playerName = P4Name;
            injuryTime = P4InjuryTime;
        }
        else if(injuredPlayer == 5)
        {
            playerName = P5Name;
            injuryTime = P5InjuryTime;
        }
        InjuryText.text = playerName + " has sustained an injury. He will be out for " + injuryTime + " games.";
    }

    void UpdateCreditsText()
    {
        CreditsText.text = Credits + " Credits";
    }

    void UpdateDraftPicksText()
    {
        DraftPicksText.text = "Draft Picks: " + draftPicks;
    }

    void UpdateGameNumberText()
    {
        Debug.Log("Game Number: " + gameNumber);
        if(gameNumber <= regSeasonGames) 
        {
            GameNumberText.text = "GAME #" + gameNumber;
            postSeasonGameNumber = 0;
        } 
        if(gameNumber == regSeasonGames + 1)
        {
            GameNumberText.text = "PLAYOFF ROUND 1";
            postSeasonGameNumber = 1;
        }
        else if(gameNumber == regSeasonGames + 2)
        {
            GameNumberText.text = "CONFERENCE SEMIFINALS";
            postSeasonGameNumber = 2;
        }
        else if(gameNumber == regSeasonGames + 3)
        {
            GameNumberText.text = "CONFERENCE FINALS";
            postSeasonGameNumber = 3;
        }
        else if(gameNumber == regSeasonGames + 4)
        {
            GameNumberText.text = "RETROBALL CHAMPIONSHIP";
            postSeasonGameNumber = 4;
        }
    }

    void UpdateConditionText()
    {
        int conditionAVG = (int)((P1Condition + P2Condition + P3Condition + P4Condition + P5Condition) / rosterSpotsFilled);
        ConditionText.text = conditionAVG + "% Condition";
    }

    void UpdatePostGameStatsText()
    {
        string pgstatstext = "";
        if(P1SpotFilled == 1)
        pgstatstext += P1Name + " - " + playerOnePoints + " PTS " + playerOneAst + " AST " + playerOneReb + " REB\n";
        if(P2SpotFilled == 1)
        pgstatstext += P2Name + " - " + playerTwoPoints + " PTS " + playerTwoAst + " AST " + playerTwoReb + " REB\n";
        if(P3SpotFilled == 1)
        pgstatstext += P3Name + " - " + playerThreePoints + " PTS " + playerThreeAst + " AST " + playerThreeReb + " REB\n";
        if(P4SpotFilled == 1)
        pgstatstext += P4Name + " - " + playerFourPoints + " PTS " + playerFourAst + " AST " + playerFourReb + " REB\n";
        if(P5SpotFilled == 1)
        pgstatstext += P5Name + " - " + playerFivePoints + " PTS " + playerFiveAst + " AST " + playerFiveReb + " REB";

        PostGameStatsText.text = pgstatstext;
    }

    void UpdateCareerText()
    {
        if(playerStatsScreens[currentPlayerStatsScreenIndex] == 1)
        {
            float P1AvgPts = GetAverageStat(P1CareerPoints, P1CareerGames);
            float P1AvgAst = GetAverageStat(P1CareerAssists, P1CareerGames);
            float P1AvgReb = GetAverageStat(P1CareerRebounds, P1CareerGames);

            careerPlayerNameText.text = P1Name;
            careerDurationText.text = "Games Played: " + P1CareerGames;
            careerAveragesText.text = "PTS: " + P1AvgPts + " AST: " + P1AvgAst + " REB: " + P1AvgReb;

            careerAchievementsText.text = "Championships: " + P1Chips;
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 2)
        {
            float P2AvgPts = GetAverageStat(P2CareerPoints, P2CareerGames); 
            float P2AvgAst = GetAverageStat(P2CareerAssists, P2CareerGames); 
            float P2AvgReb = GetAverageStat(P2CareerRebounds, P2CareerGames);  

            careerPlayerNameText.text = P2Name;
            careerDurationText.text = "Games Played: " + P2CareerGames;
            careerAveragesText.text = "PTS: " + P2AvgPts + " AST: " + P2AvgAst + " REB: " + P2AvgReb;

            careerAchievementsText.text = "Championships: " + P2Chips;
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 3)
        {
            float P3AvgPts = GetAverageStat(P3CareerPoints, P3CareerGames); 
            float P3AvgAst = GetAverageStat(P3CareerAssists, P3CareerGames);   
            float P3AvgReb = GetAverageStat(P3CareerRebounds, P3CareerGames);

            careerPlayerNameText.text = P3Name;
            careerDurationText.text = "Games Played: " + P3CareerGames;
            careerAveragesText.text = "PTS: " + P3AvgPts + " AST: " + P3AvgAst + " REB: " + P3AvgReb;

            careerAchievementsText.text = "Championships: " + P3Chips;
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 4)
        {
            float P4AvgPts = GetAverageStat(P4CareerPoints, P4CareerGames); 
            float P4AvgAst = GetAverageStat(P4CareerAssists, P4CareerGames); 
            float P4AvgReb = GetAverageStat(P4CareerRebounds, P4CareerGames);  

            careerPlayerNameText.text = P4Name;
            careerDurationText.text = "Games Played: " + P4CareerGames;
            careerAveragesText.text = "PTS: " + P4AvgPts + " AST: " + P4AvgAst + " REB: " + P4AvgReb;

            careerAchievementsText.text = "Championships: " + P4Chips;
        }
        else if(playerStatsScreens[currentPlayerStatsScreenIndex] == 5)
        {
            float P5AvgPts = GetAverageStat(P5CareerPoints, P5CareerGames);
            float P5AvgAst = GetAverageStat(P5CareerAssists, P5CareerGames);
            float P5AvgReb = GetAverageStat(P5CareerRebounds, P5CareerGames);    

            careerPlayerNameText.text = P5Name;
            careerDurationText.text = "Games Played: " + P5CareerGames;
            careerAveragesText.text = "PTS: " + P5AvgPts + " AST: " + P5AvgAst + " REB: " + P5AvgReb;

            careerAchievementsText.text = "Championships: " + P5Chips;
        }
    }

    void UpdateFreeAgentsText()
    {
        freeAgentOneText.text = GetFreeAgentText(FA1Name, FA1Strength, FA1Speed, FA1Accuracy, FA1Height, FA1Weight, FA1Cost);
        freeAgentTwoText.text = GetFreeAgentText(FA2Name, FA2Strength, FA2Speed, FA2Accuracy, FA2Height, FA2Weight, FA2Cost);
        freeAgentThreeText.text = GetFreeAgentText(FA3Name, FA3Strength, FA3Speed, FA3Accuracy, FA3Height, FA3Weight, FA3Cost);
        FreeAgentCreditsText.text = Credits + " Credits";
        Debug.Log("1: " + FA1Signed + ", 2: " + FA2Signed + ", 3: " + FA3Signed);
        if(FA1Signed == 0)
        {
            FreeAgentOneElement.SetActive(true);
        } else {
            FreeAgentOneElement.SetActive(false);
        }
        if(FA2Signed == 0)
        {
            FreeAgentTwoElement.SetActive(true);
        } else {
            FreeAgentTwoElement.SetActive(false);
        }
        if(FA3Signed == 0)
        {
            FreeAgentThreeElement.SetActive(true);
        } else {
            FreeAgentThreeElement.SetActive(false);
        }
    }

    string GetFreeAgentText(string name, int strength, int speed, int accuracy, int height, int weight, int cost)
    {
        return name + "\nStrength: " + strength + "\nSpeed: " + speed + "\nAccuracy: " + accuracy + "\n" + (int)(height/12) + "'" + (height%12) + " " + weight + " lbs\nCost: " + cost;
    }

    float GetAverageStat(int stat, int games)
    {
        float num = (float)stat / (float)games;
        num *= 10f;
        num = (int)num;
        num /= 10f;
        if(num < 0.1f)
        {
            return 0f;
        }
        else
        {
            return num;
        }
    }

    float GetStatDiff(int pts, int ast, int reb, int carpts, int carast, int carreb, int cargames)
    {
        return (float)(pts + ast + reb) - (GetAverageStat(carpts, cargames) + GetAverageStat(carast, cargames) + GetAverageStat(carreb, cargames));
    }

    int GetConditionFactor(float statdiff)
    {
        if(statdiff > 0)
        {
            return 2;
        } else {
            return 5;
        }
    }

    void SwitchPanel(GameObject Panel)
    {
        if(currentScreen != null)
        currentScreen.SetActive(false);

        Panel.SetActive(true);
        currentScreen = Panel;
    }

    void AddGamePlayedToPlayersCareers()
    {
        P1CareerGames++;
        P2CareerGames++;
        P3CareerGames++;
        P4CareerGames++;
        P5CareerGames++;

        PlayerPrefs.SetInt("PlayerOneGamesPref", P1CareerGames);
        PlayerPrefs.SetInt("PlayerTwoGamesPref", P2CareerGames);
        PlayerPrefs.SetInt("PlayerThreeGamesPref", P3CareerGames);
        PlayerPrefs.SetInt("PlayerFourGamesPref", P4CareerGames);
        PlayerPrefs.SetInt("PlayerFiveGamesPref", P5CareerGames);
    }

     void AddChampionshipWonToPlayersCareers()
    {
        P1Chips++;
        P2Chips++;
        P3Chips++;
        P4Chips++;
        P5Chips++;

        PlayerPrefs.SetInt("PlayerOneChampionshipsPref", P1Chips);
        PlayerPrefs.SetInt("PlayerTwoChampionshipsPref", P2Chips);
        PlayerPrefs.SetInt("PlayerThreeChampionshipsPref", P3Chips);
        PlayerPrefs.SetInt("PlayerFourChampionshipsPref", P4Chips);
        PlayerPrefs.SetInt("PlayerFiveChampionshipsPref", P5Chips);
    }

    void AddPlayerTotalStats()
    {
        P1CareerPoints += playerOnePoints;
        P1CareerAssists += playerOneAst;
        P1CareerRebounds += playerOneReb;

        P2CareerPoints += playerTwoPoints;
        P2CareerAssists += playerTwoAst;
        P2CareerRebounds += playerTwoReb;

        P3CareerPoints += playerThreePoints;
        P3CareerAssists += playerThreeAst;
        P3CareerRebounds += playerThreeReb;

        P4CareerPoints += playerFourPoints;
        P4CareerAssists += playerFourAst;
        P4CareerRebounds += playerFourReb;

        P5CareerPoints += playerFivePoints;
        P5CareerAssists += playerFiveAst;
        P5CareerRebounds += playerFiveReb;

        PlayerPrefs.SetInt("PlayerOneTotalPointsPref", P1CareerPoints);
        PlayerPrefs.SetInt("PlayerOneTotalAssistsPref", P1CareerAssists);
        PlayerPrefs.SetInt("PlayerOneTotalReboundsPref", P1CareerRebounds);

        PlayerPrefs.SetInt("PlayerTwoTotalPointsPref", P2CareerPoints);
        PlayerPrefs.SetInt("PlayerTwoTotalAssistsPref", P2CareerAssists);
        PlayerPrefs.SetInt("PlayerTwoTotalReboundsPref", P2CareerRebounds);
        
        PlayerPrefs.SetInt("PlayerThreeTotalPointsPref", P3CareerPoints);
        PlayerPrefs.SetInt("PlayerThreeTotalAssistsPref", P3CareerAssists);
        PlayerPrefs.SetInt("PlayerThreeTotalReboundsPref", P3CareerRebounds);

        PlayerPrefs.SetInt("PlayerFourTotalPointsPref", P4CareerPoints);
        PlayerPrefs.SetInt("PlayerFourTotalAssistsPref", P4CareerAssists);
        PlayerPrefs.SetInt("PlayerFourTotalReboundsPref", P4CareerRebounds);

        PlayerPrefs.SetInt("PlayerFiveTotalPointsPref", P5CareerPoints);
        PlayerPrefs.SetInt("PlayerFiveTotalAssistsPref", P5CareerAssists);
        PlayerPrefs.SetInt("PlayerFiveTotalReboundsPref", P5CareerRebounds);
    }

    void UpdateTeamNameText()
    {
        teamNameText.text = teamNames[currentTeam - 1];
    }

    public void UpdateNextTeamText()
    {
        nextTeamText.text = seasonSimulationScript.FindTeamForNextGame(SeasonSimulationScript.teamsTemp);
    }

    float GetCloseOutSpeedMultiplierValues()
    {
        return GetRandomDefensiveValues(0.35f, 0.5f, 0.3f, 0.4f, 0.2f, 0.3f, 0.15f, 0.25f, 0.1f, 0.2f);
    }

    float GetSmoothTimeValues()
    {
        return GetRandomDefensiveValues(0.1f, 0.2f, 0.15f, 0.2f, 0.15f, 0.25f, 0.2f, 0.25f, 0.25f, 0.3f);
    }

    float GetDistScaleFactorValues()
    {
        return GetRandomDefensiveValues(12f, 13f, 11f, 12f, 10f, 11f, 8.5f, 10f, 7f, 9f);
    }

    float GetMaintainDistanceBaseValues()
    {
        return GetRandomDefensiveValues(1.5f, 1.6f, 1.5f, 1.7f, 1.5f, 1.8f, 1.5f, 1.9f, 1.5f, 2f);
    }

    float GetRandomDefensiveValues(float minOne, float maxOne, float minTwo, float maxTwo, float minThree, float maxThree, float minFour, float maxFour, float minFive, float maxFive)
    {
        int level = seasonSimulationScript.GetNextTeamLevel();
        if(level == 5)
        {
            return Random.Range(minOne, maxOne);
        }
        else if(level == 4)
        {
            return Random.Range(minTwo, maxTwo);
        }
        else if(level == 3)
        {
            return Random.Range(minThree, maxThree);
        }
        else if(level == 2)
        {
            return Random.Range(minFour, maxFour);
        } else {
            return Random.Range(minFive, maxFive);
        }
    }

    // generate name
    string GenerateName()
    {
        string first = firstNames[Random.Range(0, firstNames.Length - 1)];
        string last = lastNames[Random.Range(0, lastNames.Length - 1)];
        return first + " " + last;
    }

    int GenerateHeight()
    {
        return Random.Range(70, 87);
    }

    int GenerateWeight()
    {
        return Random.Range(36, 61) * 5;
    }

    public int GetTeamRating()
    {
        float avgStats = (float)(P1StrengthValue + P2StrengthValue + P3StrengthValue + P4StrengthValue + P5StrengthValue + P1SpeedValue + P2SpeedValue + P3SpeedValue + P4SpeedValue + P5SpeedValue + P1AccuracyValue + P2AccuracyValue + P3AccuracyValue + P4AccuracyValue + P5AccuracyValue) / 15.0f;
        return (int)(avgStats * 10);
    }

    public int GenerateCondition()
    {
        return Random.Range(30, 61);
    }

    void CheckConditionMinimum()
    {
        if(P1Condition < 0)
        {
            P1Condition = 0;
        }
        if(P2Condition < 0)
        {
            P2Condition = 0;
        }
        if(P3Condition < 0)
        {
            P3Condition = 0;
        }
        if(P4Condition < 0)
        {
            P4Condition = 0;
        }
        if(P5Condition < 0)
        {
            P5Condition = 0;
        }
        PlayerPrefs.SetInt("PlayerOneConditionPref", P1Condition);
        PlayerPrefs.SetInt("PlayerTwoConditionPref", P1Condition);
        PlayerPrefs.SetInt("PlayerThreeConditionPref", P1Condition);
        PlayerPrefs.SetInt("PlayerFourConditionPref", P1Condition);
        PlayerPrefs.SetInt("PlayerFiveConditionPref", P1Condition);
    }

    public int GenerateFreeAgentCost(string agentNum)
    {
        int strength = PlayerPrefs.GetInt("FreeAgent" + agentNum + "StrengthPref");
        int speed = PlayerPrefs.GetInt("FreeAgent" + agentNum + "SpeedPref");
        int accuracy = PlayerPrefs.GetInt("FreeAgent" + agentNum + "AccuracyPref");

        return (int)((strength + speed + accuracy) * 2f / 3f);
        
    }

    void ResetPlayerCareer(string playerNumString)
    {
        PlayerPrefs.SetInt("Player" + playerNumString + "GamesPref", 0);
        PlayerPrefs.SetInt("Player" + playerNumString + "TotalPointsPref", 0);
        PlayerPrefs.SetInt("Player" + playerNumString + "TotalAssistsPref", 0);
        PlayerPrefs.SetInt("Player" + playerNumString + "TotalReboundsPref", 0);
        PlayerPrefs.SetInt("Player" + playerNumString + "ChampionshipsPref", 0);

        if(playerNumString == "One")
        {
            P1CareerGames = 0;
            P1CareerPoints = 0;
            P1CareerAssists = 0;
            P1CareerRebounds = 0;
            P1Chips = 0;
        }
        else if(playerNumString == "Two")
        {
            P2CareerGames = 0;
            P2CareerPoints = 0;
            P2CareerAssists = 0;
            P2CareerRebounds = 0;
            P2Chips = 0;
        }
        else if(playerNumString == "Three")
        {
            P3CareerGames = 0;
            P3CareerPoints = 0;
            P3CareerAssists = 0;
            P3CareerRebounds = 0;
            P3Chips = 0;
        }
        else if(playerNumString == "Four")
        {
            P4CareerGames = 0;
            P4CareerPoints = 0;
            P4CareerAssists = 0;
            P4CareerRebounds = 0;
            P4Chips = 0;
        }
        else if(playerNumString == "Five")
        {
            P5CareerGames = 0;
            P5CareerPoints = 0;
            P5CareerAssists = 0;
            P5CareerRebounds = 0;
            P5Chips = 0;
        }
    }

    // reset prefs function
    void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    int returnRandom(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    // social media
    void UpdateSocialMediaInfluenceText()
    {
        int influenceAVG = (P1Influence + P2Influence + P3Influence + P4Influence + P5Influence) / rosterSpotsFilled;
        SocialMediaInfluenceText.text = influenceAVG + "% Influence";
    }

    void UpdateSocialMediaFeed()
    {
        string feed = "";
        feed += socialMediaCommentOne;
        feed += socialMediaCommentTwo;
        feed += socialMediaCommentThree;
        SocialMediaFeedText.text = feed;
    }

    void PostGameSocialMediaFeed()
    {
        List<int> players = new List<int>();
        for(int i = 1; i <= 5; i++)
        {
            players.Add(i);
        }
        int commentSpotOne, commentSpotTwo, commentSpotThree;
        int random = Random.Range(0, players.Count);
        commentSpotOne = players[random];
        players.RemoveAt(random);
        random = Random.Range(0, players.Count);
        commentSpotTwo = players[random];
        players.RemoveAt(random);
        random = Random.Range(0, players.Count);
        commentSpotThree = players[random];
        players.RemoveAt(random);

        socialMediaCommentOne = GetPlayerComment(commentSpotOne);
        socialMediaCommentTwo = GetPlayerComment(commentSpotTwo);
        socialMediaCommentThree = GetPlayerComment(commentSpotThree);
        PlayerPrefs.SetString("SocialMediaCommentOnePref", socialMediaCommentOne);
        PlayerPrefs.SetString("SocialMediaCommentTwoPref", socialMediaCommentTwo);
        PlayerPrefs.SetString("SocialMediaCommentThreePref", socialMediaCommentThree);
    }

    string GetPlayerComment(int player)
    {
        string feed = "";
        if(player == 1)
        feed += AddToSocialMediaFeed(GetPlayerSocialMediaStatus(playerOnePoints, playerOneAst, playerOneReb), P1Name);
        if(player == 2)
        feed += AddToSocialMediaFeed(GetPlayerSocialMediaStatus(playerTwoPoints, playerTwoAst, playerTwoReb), P2Name);
        if(player == 3)
        feed += AddToSocialMediaFeed(GetPlayerSocialMediaStatus(playerThreePoints, playerThreeAst, playerThreeReb), P3Name);
        if(player == 4)
        feed += AddToSocialMediaFeed(GetPlayerSocialMediaStatus(playerFourPoints, playerFourAst, playerFourReb), P4Name);
        if(player == 5)
        feed += AddToSocialMediaFeed(GetPlayerSocialMediaStatus(playerFivePoints, playerFiveAst, playerFiveReb), P5Name);
        return feed;
    }

    string AddToSocialMediaFeed(bool status, string name)
    {
        string feed = "";
        feed += firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)] + "\n";
        if(status)
        {
            int randomIndex = Random.Range(0, prePlayerGoodComments.Count);
            feed += name + prePlayerGoodComments[randomIndex] + "\n\n";
            Debug.Log("RI: " + randomIndex);
            prePlayerGoodComments.RemoveAt(randomIndex);
        } else {
            int randomIndex = Random.Range(0, prePlayerBadComments.Count);
            feed += name + prePlayerBadComments[randomIndex] + "\n\n";
            Debug.Log("RI: " + randomIndex);
            prePlayerBadComments.RemoveAt(randomIndex);
        }
        return feed;
    }

    bool GetPlayerSocialMediaStatus(int pts, int ast, int reb)
    {
        return pts + ast + reb >= 3;
    }
}
