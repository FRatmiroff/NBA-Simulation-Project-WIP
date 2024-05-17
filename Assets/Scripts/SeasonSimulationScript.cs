using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonSimulationScript : MonoBehaviour
{
    public MainMenuScript mainMenuScript;
    public static Team[] teamsTemp;
    public Team[] teamsByRecord;
    Team teamOne, teamTwo, teamThree, teamFour, teamFive, teamSix, teamSeven, teamEight, teamNine, teamTen, teamEleven, teamTwelve, teamThirteen, teamFourteen, teamFifteen, teamSixteen, teamSeventeen, teamEighteen, teamNineteen, teamTwenty, teamTwentyOne, teamTwentyTwo, teamTwentyThree, teamTwentyFour, teamTwentyFive, teamTwentySix, teamTwentySeven, teamTwentyEight, teamTwentyNine, teamThirty;
    List<Team> easternConferenceTeamsByRecord = new List<Team>();
    List<Team> westernConferenceTeamsByRecord = new List<Team>();
    public List<int> teamIndexList = new List<int>();
    List<int> playerSeasonScheduleIndexes = new List<int>();
    public string[] numbersToWords = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty", "TwentyOne", "TwentyTwo", "TwentyThree", "TwentyFour", "TwentyFive", "TwentySix", "TwentySeven", "TwentyEight", "TwentyNine", "Thirty"};
    public static int nextTeam;
    int playoffsNextTeam;
    public TMP_Text eastStandingsText;
    public TMP_Text westStandingsText;
    public static bool callSimWeek;
    public static bool playerGameWon;
    public static bool endPostSeason;
    public bool playerMadePostSeason;
    public bool LogTeams;
    public bool winGame;
    public bool loseGame;

    int teamOneWins, teamTwoWins, teamThreeWins, teamFourWins, teamFiveWins, teamSixWins, teamSevenWins, teamEightWins, teamNineWins, teamTenWins, teamElevenWins, teamTwelveWins, teamThirteenWins, teamFourteenWins,teamFifteenWins, teamSixteenWins, teamSeventeenWins, teamEighteenWins, teamNineteenWins, teamTwentyWins, teamTwentyOneWins, teamTwentyTwoWins, teamTwentyThreeWins, teamTwentyFourWins, teamTwentyFiveWins, teamTwentySixWins, teamTwentySevenWins, teamTwentyEightWins, teamTwentyNineWins, teamThirtyWins;
    int teamOneLosses, teamTwoLosses, teamThreeLosses, teamFourLosses, teamFiveLosses, teamSixLosses, teamSevenLosses, teamEightLosses, teamNineLosses, teamTenLosses, teamElevenLosses, teamTwelveLosses, teamThirteenLosses, teamFourteenLosses,teamFifteenLosses, teamSixteenLosses, teamSeventeenLosses, teamEighteenLosses, teamNineteenLosses, teamTwentyLosses, teamTwentyOneLosses, teamTwentyTwoLosses, teamTwentyThreeLosses, teamTwentyFourLosses, teamTwentyFiveLosses, teamTwentySixLosses, teamTwentySevenLosses, teamTwentyEightLosses, teamTwentyNineLosses, teamThirtyLosses;
    int playoffsRoundOneBracketOneWinner, playoffsRoundOneBracketTwoWinner, playoffsRoundOneBracketThreeWinner, playoffsRoundOneBracketFourWinner;
    int playoffsRoundTwoBracketOneWinner, playoffsRoundTwoBracketTwoWinner;
    int playoffsRoundThreeBracketOneWinner;

    public class Team
    {
        string name;
        int id;
        int wins;
        int losses;
        bool playersTeam;
        string conference;
        int seed;
        int rating;

        public Team(string pname, int pid, int pwins, int plosses, int pconference)
        {
            name = pname;
            id = pid;
            wins = pwins;
            losses = plosses;
            playersTeam = false;
            if(pconference == 0)
            {
                conference = "West";
            } else {
                conference = "East";
            }
            rating = 0;
            seed = 0;
        }

        public void SetAsPlayersTeam()
        {
            playersTeam = true;
        }

        public void setSeed(int pseed)
        {
            seed = pseed;
        }

        public string getName()
        {
            return name;
        }

        public string getConference()
        {
            return conference;
        }

        public int getID()
        {
            return id;
        }

        public int getSeed()
        {
            return seed;
        }

        public int getRating()
        {
            return rating;
        }

        public void setRating(int prating)
        {
            rating = prating;
        }

        public bool isPlayersTeam()
        {
            return playersTeam;
        }

        public void resetRecord()
        {
            wins = 0;
            losses = 0;
        }

        public string getRecord()
        {
            return wins + " - " + losses;
        }

        public void SimulateGame(bool wonGame)
        {
            if(wonGame)
            {
                AddWin();
            } else {
                AddLoss();
            }
        }

        public void AddWin()
        {
            wins++;
            PlayerPrefs.SetInt(getTeamWinsPref(), wins);
        }

        public void AddLoss()
        {
            losses++;
            PlayerPrefs.SetInt(getTeamLossesPref(), losses);
        }

        public float getWinLossRatio()
        {
            return (float)wins / ((float)wins + (float)losses);
        }

        string getTeamWinsPref()
        {
            return "Team" + Translate() + "WinsPref";
        }

        string getTeamLossesPref()
        {
            return "Team" + Translate() + "LossesPref";
        }

        public int getLevel()
        {
            if(rating >= 74)
            {
                return 5;
            }
            else if(rating >= 58)
            {
                return 4;
            }
            else if(rating >= 42)
            {
                return 3;
            }
            else if(rating >= 26)
            {
                return 2;
            } else {
                return 1;
            }
        }

        string Translate()
        {
            if(id == 0)
            {
                return "One";
            }
            else if(id == 1)
            {
                return "Two";
            }
            else if(id == 2)
            {
                return "Three";
            }
            else if(id == 3)
            {
                return "Four";
            }
            else if(id == 4)
            {
                return "Five";
            }
            else if(id == 5)
            {
                return "Six";
            }
            else if(id == 6)
            {
                return "Seven";
            }
            else if(id == 7)
            {
                return "Eight";
            }
            else if(id == 8)
            {
                return "Nine";
            }
            else if(id == 9)
            {
                return "Ten";
            }
            else if(id == 10)
            {
                return "Eleven";
            }
            else if(id == 11)
            {
                return "Twelve";
            }
            else if(id == 12)
            {
                return "Thirteen";
            }
            else if(id == 13)
            {
                return "Fourteen";
            }
            else if(id == 14)
            {
                return "Fifteen";
            }
            else if(id == 15)
            {
                return "Sixteen";
            }
            else if(id == 16)
            {
                return "Seventeen";
            }
            else if(id == 17)
            {
                return "Eighteen";
            }
            else if(id == 18)
            {
                return "Nineteen";
            }
            else if(id == 19)
            {
                return "Twenty";
            }
            else if(id == 20)
            {
                return "TwentyOne";
            }
            else if(id == 21)
            {
                return "TwentyTwo";
            }
            else if(id == 22)
            {
                return "TwentyThree";
            }
            else if(id == 23)
            {
                return "TwentyFour";
            }
            else if(id == 24)
            {
                return "TwentyFive";
            }
            else if(id == 25)
            {
                return "TwentySix";
            }
            else if(id == 26)
            {
                return "TwentySeven";
            }
            else if(id == 27)
            {
                return "TwentyEight";
            }
            else if(id == 28)
            {
                return "TwentyNine";
            }
            else
            {
                return "Thirty";
            }
        }
    }
    void Start()
    {
        SetUpPlayerPrefs("NextTeamPref", 0);
        nextTeam = PlayerPrefs.GetInt("NextTeamPref");

        SetUpPlayerPrefsWinsAndLosses("One");
        SetUpPlayerPrefsWinsAndLosses("Two");
        SetUpPlayerPrefsWinsAndLosses("Three");
        SetUpPlayerPrefsWinsAndLosses("Four");
        SetUpPlayerPrefsWinsAndLosses("Five");
        SetUpPlayerPrefsWinsAndLosses("Six");
        SetUpPlayerPrefsWinsAndLosses("Seven");
        SetUpPlayerPrefsWinsAndLosses("Eight");
        SetUpPlayerPrefsWinsAndLosses("Nine");
        SetUpPlayerPrefsWinsAndLosses("Ten");
        SetUpPlayerPrefsWinsAndLosses("Eleven");
        SetUpPlayerPrefsWinsAndLosses("Twelve");
        SetUpPlayerPrefsWinsAndLosses("Thirteen");
        SetUpPlayerPrefsWinsAndLosses("Fourteen");
        SetUpPlayerPrefsWinsAndLosses("Fifteen");
        SetUpPlayerPrefsWinsAndLosses("Sixteen");
        SetUpPlayerPrefsWinsAndLosses("Seventeen");
        SetUpPlayerPrefsWinsAndLosses("Eighteen");
        SetUpPlayerPrefsWinsAndLosses("Nineteen");
        SetUpPlayerPrefsWinsAndLosses("Twenty");
        SetUpPlayerPrefsWinsAndLosses("TwentyOne");
        SetUpPlayerPrefsWinsAndLosses("TwentyTwo");
        SetUpPlayerPrefsWinsAndLosses("TwentyThree");
        SetUpPlayerPrefsWinsAndLosses("TwentyFour");
        SetUpPlayerPrefsWinsAndLosses("TwentyFive");
        SetUpPlayerPrefsWinsAndLosses("TwentySix");
        SetUpPlayerPrefsWinsAndLosses("TwentySeven");
        SetUpPlayerPrefsWinsAndLosses("TwentyEight");
        SetUpPlayerPrefsWinsAndLosses("TwentyNine");
        SetUpPlayerPrefsWinsAndLosses("Thirty");
        teamOneWins = PlayerPrefs.GetInt("TeamOneWinsPref");
        teamTwoWins = PlayerPrefs.GetInt("TeamTwoWinsPref");
        teamThreeWins = PlayerPrefs.GetInt("TeamThreeWinsPref");
        teamFourWins = PlayerPrefs.GetInt("TeamFourWinsPref");
        teamFiveWins = PlayerPrefs.GetInt("TeamFiveWinsPref");
        teamSixWins = PlayerPrefs.GetInt("TeamSixWinsPref");
        teamSevenWins = PlayerPrefs.GetInt("TeamSevenWinsPref");
        teamEightWins = PlayerPrefs.GetInt("TeamEightWinsPref");
        teamNineWins = PlayerPrefs.GetInt("TeamNineWinsPref");
        teamTenWins = PlayerPrefs.GetInt("TeamTenWinsPref");
        teamElevenWins = PlayerPrefs.GetInt("TeamElevenWinsPref");
        teamTwelveWins = PlayerPrefs.GetInt("TeamTwelveWinsPref");
        teamThirteenWins = PlayerPrefs.GetInt("TeamThirteenWinsPref");
        teamFourteenWins = PlayerPrefs.GetInt("TeamFourteenWinsPref");
        teamFifteenWins = PlayerPrefs.GetInt("TeamFifteenWinsPref");
        teamSixteenWins = PlayerPrefs.GetInt("TeamSixteenWinsPref");
        teamSeventeenWins = PlayerPrefs.GetInt("TeamSeventeenWinsPref");
        teamEighteenWins = PlayerPrefs.GetInt("TeamEighteenWinsPref");
        teamNineteenWins = PlayerPrefs.GetInt("TeamNineteenWinsPref");
        teamTwentyWins = PlayerPrefs.GetInt("TeamTwentyWinsPref");
        teamTwentyOneWins = PlayerPrefs.GetInt("TeamTwentyOneWinsPref");
        teamTwentyTwoWins = PlayerPrefs.GetInt("TeamTwentyTwoWinsPref");
        teamTwentyThreeWins = PlayerPrefs.GetInt("TeamTwentyThreeWinsPref");
        teamTwentyFourWins = PlayerPrefs.GetInt("TeamTwentyFourWinsPref");
        teamTwentyFiveWins = PlayerPrefs.GetInt("TeamTwentyFiveWinsPref");
        teamTwentySixWins = PlayerPrefs.GetInt("TeamTwentySixWinsPref");
        teamTwentySevenWins = PlayerPrefs.GetInt("TeamTwentySevenWinsPref");
        teamTwentyEightWins = PlayerPrefs.GetInt("TeamTwentyEightWinsPref");
        teamTwentyNineWins = PlayerPrefs.GetInt("TeamTwentyNineWinsPref");
        teamThirtyWins = PlayerPrefs.GetInt("TeamThirtyWinsPref");
        teamOneLosses = PlayerPrefs.GetInt("TeamOneLossesPref");
        teamTwoLosses = PlayerPrefs.GetInt("TeamTwoLossesPref");
        teamThreeLosses = PlayerPrefs.GetInt("TeamThreeLossesPref");
        teamFourLosses = PlayerPrefs.GetInt("TeamFourLossesPref");
        teamFiveLosses = PlayerPrefs.GetInt("TeamFiveLossesPref");
        teamSixLosses = PlayerPrefs.GetInt("TeamSixLossesPref");
        teamSevenLosses = PlayerPrefs.GetInt("TeamSevenLossesPref");
        teamEightLosses = PlayerPrefs.GetInt("TeamEightLossesPref");
        teamNineLosses = PlayerPrefs.GetInt("TeamNineLossesPref");
        teamTenLosses = PlayerPrefs.GetInt("TeamTenLossesPref");
        teamElevenLosses = PlayerPrefs.GetInt("TeamElevenLossesPref");
        teamTwelveLosses = PlayerPrefs.GetInt("TeamTwelveLossesPref");
        teamThirteenLosses = PlayerPrefs.GetInt("TeamThirteenLossesPref");
        teamFourteenLosses = PlayerPrefs.GetInt("TeamFourteenLossesPref");
        teamFifteenLosses = PlayerPrefs.GetInt("TeamFifteenLossesPref");
        teamSixteenLosses = PlayerPrefs.GetInt("TeamSixteenLossesPref");
        teamSeventeenLosses = PlayerPrefs.GetInt("TeamSeventeenLossesPref");
        teamEighteenLosses = PlayerPrefs.GetInt("TeamEighteenLossesPref");
        teamNineteenLosses = PlayerPrefs.GetInt("TeamNineteenLossesPref");
        teamTwentyLosses = PlayerPrefs.GetInt("TeamTwentyLossesPref");
        teamTwentyOneLosses = PlayerPrefs.GetInt("TeamTwentyOneLossesPref");
        teamTwentyTwoLosses = PlayerPrefs.GetInt("TeamTwentyTwoLossesPref");
        teamTwentyThreeLosses = PlayerPrefs.GetInt("TeamTwentyThreeLossesPref");
        teamTwentyFourLosses = PlayerPrefs.GetInt("TeamTwentyFourLossesPref");
        teamTwentyFiveLosses = PlayerPrefs.GetInt("TeamTwentyFiveLossesPref");
        teamTwentySixLosses = PlayerPrefs.GetInt("TeamTwentySixLossesPref");
        teamTwentySevenLosses = PlayerPrefs.GetInt("TeamTwentySevenLossesPref");
        teamTwentyEightLosses = PlayerPrefs.GetInt("TeamTwentyEightLossesPref");
        teamTwentyNineLosses = PlayerPrefs.GetInt("TeamTwentyNineLossesPref");
        teamThirtyLosses = PlayerPrefs.GetInt("TeamThirtyLossesPref");

        SetUpPlayerPrefs("PlayerMadePostSeasonPref", 0);
        playerMadePostSeason = PlayerPrefs.GetInt("PlayerMadePostSeasonPref") == 1;

        SetUpPlayerPrefs("PlayoffsRoundOneBracketOneWinnerPref", -1);
        SetUpPlayerPrefs("PlayoffsRoundOneBracketTwoWinnerPref", -1);
        SetUpPlayerPrefs("PlayoffsRoundOneBracketThreeWinnerPref", -1);
        SetUpPlayerPrefs("PlayoffsRoundOneBracketFourWinnerPref", -1);
        playoffsRoundOneBracketOneWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketOneWinnerPref");
        playoffsRoundOneBracketTwoWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketTwoWinnerPref");
        playoffsRoundOneBracketThreeWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketThreeWinnerPref");
        playoffsRoundOneBracketFourWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketFourWinnerPref");

        SetUpPlayerPrefs("PlayoffsRoundTwoBracketOneWinnerPref", -1);
        SetUpPlayerPrefs("PlayoffsRoundTwoBracketTwoWinnerPref", -1);
        playoffsRoundTwoBracketOneWinner = PlayerPrefs.GetInt("PlayoffsRoundTwoBracketOneWinnerPref");
        playoffsRoundTwoBracketTwoWinner = PlayerPrefs.GetInt("PlayoffsRoundTwoBracketTwoWinnerPref");

        SetUpPlayerPrefs("PlayoffsRoundThreeBracketOneWinnerPref", -1);
        playoffsRoundThreeBracketOneWinner = PlayerPrefs.GetInt("PlayoffsRoundThreeBracketOneWinnerPref");

        // fill the list of team indexes
        for(int i = 0; i < 30; i++)
        {
            teamIndexList.Add(i);
        }
        teamIndexList.RemoveAt(MainMenuScript.currentTeam - 1);

        if(!PlayerPrefs.HasKey("ResetSeasonSchedulePref"))
        {
            PlayerPrefs.SetInt("ResetSeasonSchedulePref", 1);
        }

        // randomize the list of team indexes
        if(PlayerPrefs.GetInt("ResetSeasonSchedulePref") == 1)
        {
            CreatePlayerSeasonSchedule();
        }
        LoadPlayerSeasonSchedule();

        SetUpPlayerPrefsSeasonSchedule("One", playerSeasonScheduleIndexes[0]);
        SetUpPlayerPrefsSeasonSchedule("Two", playerSeasonScheduleIndexes[1]);
        SetUpPlayerPrefsSeasonSchedule("Three", playerSeasonScheduleIndexes[2]);
        SetUpPlayerPrefsSeasonSchedule("Four", playerSeasonScheduleIndexes[3]);
        SetUpPlayerPrefsSeasonSchedule("Five", playerSeasonScheduleIndexes[4]);
        SetUpPlayerPrefsSeasonSchedule("Six", playerSeasonScheduleIndexes[5]);
        SetUpPlayerPrefsSeasonSchedule("Seven", playerSeasonScheduleIndexes[6]);
        SetUpPlayerPrefsSeasonSchedule("Eight", playerSeasonScheduleIndexes[7]);
        SetUpPlayerPrefsSeasonSchedule("Nine", playerSeasonScheduleIndexes[8]);
        SetUpPlayerPrefsSeasonSchedule("Ten", playerSeasonScheduleIndexes[9]);
        SetUpPlayerPrefsSeasonSchedule("Eleven", playerSeasonScheduleIndexes[10]);
        SetUpPlayerPrefsSeasonSchedule("Twelve", playerSeasonScheduleIndexes[11]);
        SetUpPlayerPrefsSeasonSchedule("Thirteen", playerSeasonScheduleIndexes[12]);
        SetUpPlayerPrefsSeasonSchedule("Fourteen", playerSeasonScheduleIndexes[13]);
        SetUpPlayerPrefsSeasonSchedule("Fifteen", playerSeasonScheduleIndexes[14]);
        SetUpPlayerPrefsSeasonSchedule("Sixteen", playerSeasonScheduleIndexes[15]);
        SetUpPlayerPrefsSeasonSchedule("Seventeen", playerSeasonScheduleIndexes[16]);
        SetUpPlayerPrefsSeasonSchedule("Eighteen", playerSeasonScheduleIndexes[17]);
        SetUpPlayerPrefsSeasonSchedule("Nineteen", playerSeasonScheduleIndexes[18]);
        SetUpPlayerPrefsSeasonSchedule("Twenty", playerSeasonScheduleIndexes[19]);
        SetUpPlayerPrefsSeasonSchedule("TwentyOne", playerSeasonScheduleIndexes[20]);
        SetUpPlayerPrefsSeasonSchedule("TwentyTwo", playerSeasonScheduleIndexes[21]);
        SetUpPlayerPrefsSeasonSchedule("TwentyThree", playerSeasonScheduleIndexes[22]);
        SetUpPlayerPrefsSeasonSchedule("TwentyFour", playerSeasonScheduleIndexes[23]);
        SetUpPlayerPrefsSeasonSchedule("TwentyFive", playerSeasonScheduleIndexes[24]);
        SetUpPlayerPrefsSeasonSchedule("TwentySix", playerSeasonScheduleIndexes[25]);
        SetUpPlayerPrefsSeasonSchedule("TwentySeven", playerSeasonScheduleIndexes[26]);
        SetUpPlayerPrefsSeasonSchedule("TwentyEight", playerSeasonScheduleIndexes[27]);
        SetUpPlayerPrefsSeasonSchedule("TwentyNine", playerSeasonScheduleIndexes[28]);

        teamOne = new Team(mainMenuScript.teamNames[0], 0, teamOneWins, teamOneLosses, 0);
        teamTwo = new Team(mainMenuScript.teamNames[1], 1, teamTwoWins, teamTwoLosses, 0);
        teamThree = new Team(mainMenuScript.teamNames[2], 2, teamThreeWins, teamThreeLosses, 0);
        teamFour = new Team(mainMenuScript.teamNames[3], 3, teamFourWins, teamFourLosses, 0);
        teamFive = new Team(mainMenuScript.teamNames[4], 4, teamFiveWins, teamFiveLosses, 0);
        teamSix = new Team(mainMenuScript.teamNames[5], 5, teamSixWins, teamSixLosses, 0);
        teamSeven = new Team(mainMenuScript.teamNames[6], 6, teamSevenWins, teamSevenLosses, 0);
        teamEight = new Team(mainMenuScript.teamNames[7], 7, teamEightWins, teamEightLosses, 0);
        teamNine = new Team(mainMenuScript.teamNames[8], 8, teamNineWins, teamNineLosses, 0);
        teamTen = new Team(mainMenuScript.teamNames[9], 9, teamTenWins, teamTenLosses, 0);
        teamEleven = new Team(mainMenuScript.teamNames[10], 10, teamElevenWins, teamElevenLosses, 0);
        teamTwelve = new Team(mainMenuScript.teamNames[11], 11, teamTwelveWins, teamTwelveLosses, 0);
        teamThirteen = new Team(mainMenuScript.teamNames[12], 12, teamThirteenWins, teamThirteenLosses, 0);
        teamFourteen = new Team(mainMenuScript.teamNames[13], 13, teamFourteenWins, teamFourteenLosses, 0);
        teamFifteen = new Team(mainMenuScript.teamNames[14], 14, teamFifteenWins, teamFifteenLosses, 0);
        teamSixteen = new Team(mainMenuScript.teamNames[15], 15, teamSixteenWins, teamSixteenLosses, 1);
        teamSeventeen = new Team(mainMenuScript.teamNames[16], 16, teamSeventeenWins, teamSeventeenLosses, 1);
        teamEighteen = new Team(mainMenuScript.teamNames[17], 17, teamEighteenWins, teamEighteenLosses, 1);
        teamNineteen = new Team(mainMenuScript.teamNames[18], 18, teamNineteenWins, teamNineteenLosses, 1);
        teamTwenty = new Team(mainMenuScript.teamNames[19], 19, teamTwentyWins, teamTwentyLosses, 1);
        teamTwentyOne = new Team(mainMenuScript.teamNames[20], 20, teamTwentyOneWins, teamTwentyOneLosses, 1);
        teamTwentyTwo = new Team(mainMenuScript.teamNames[21], 21, teamTwentyTwoWins, teamTwentyTwoLosses, 1);
        teamTwentyThree = new Team(mainMenuScript.teamNames[22], 22, teamTwentyThreeWins, teamTwentyThreeLosses, 1);
        teamTwentyFour = new Team(mainMenuScript.teamNames[23], 23, teamTwentyFourWins, teamTwentyFourLosses, 1);
        teamTwentyFive = new Team(mainMenuScript.teamNames[24], 24, teamTwentyFiveWins, teamTwentyFiveLosses, 1);
        teamTwentySix = new Team(mainMenuScript.teamNames[25], 25, teamTwentySixWins, teamTwentySixLosses, 1);
        teamTwentySeven = new Team(mainMenuScript.teamNames[26], 26, teamTwentySevenWins, teamTwentySevenLosses, 1);
        teamTwentyEight = new Team(mainMenuScript.teamNames[27], 27, teamTwentyEightWins, teamTwentyEightLosses, 1);
        teamTwentyNine = new Team(mainMenuScript.teamNames[28], 28, teamTwentyNineWins, teamTwentyNineLosses, 1);
        teamThirty = new Team(mainMenuScript.teamNames[29], 29, teamThirtyWins, teamThirtyLosses, 1);
        
        Team[] teams = {teamOne, teamTwo, teamThree, teamFour, teamFive,
                 teamSix, teamSeven, teamEight, teamNine, teamTen,
                 teamEleven, teamTwelve, teamThirteen, teamFourteen, teamFifteen,
                 teamSixteen, teamSeventeen, teamEighteen, teamNineteen, teamTwenty,
                 teamTwentyOne, teamTwentyTwo, teamTwentyThree, teamTwentyFour, teamTwentyFive,
                 teamTwentySix, teamTwentySeven, teamTwentyEight, teamTwentyNine, teamThirty};
        teamsTemp = teams;

        SetUpPlayerPrefsTeamRatings();

        if(callSimWeek != true)
        {
            callSimWeek = false;
        }
        
        teams[MainMenuScript.currentTeam - 1].SetAsPlayersTeam();
        if(callSimWeek && !playerMadePostSeason)
        {
            SimulateWeek();
            UpdatePlayersMatchup();
            callSimWeek = false;
        }

        CheckPostSeason();

        mainMenuScript.UpdateNextTeamText();
        UpdateStandingsText();
    }

    void SetUpPlayerPrefs(string playerPrefsName, int set)
    {
        if(!PlayerPrefs.HasKey(playerPrefsName))
        {
            PlayerPrefs.SetInt(playerPrefsName, set);
        }
    }

    void SetUpPlayerPrefsWinsAndLosses(string teamNumber)
    {
        string temp = "Team" + teamNumber + "WinsPref";
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, 0);
        }

        temp = "Team" + teamNumber + "LossesPref";
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, 0);
        }
    }

    void SetUpPlayerPrefsSeasonSchedule(string gameNumber, int teamIndex)
    {
        string temp = "PlayerSeasonScheduleGame" + gameNumber + "OpponentPref";
        if(!PlayerPrefs.HasKey(temp))
        {
            PlayerPrefs.SetInt(temp, teamIndex);
        }
        PlayerPrefs.SetInt(temp, teamIndex);
    }

    void SetUpPlayerPrefsTeamRatings()
    {
        if(!PlayerPrefs.HasKey("TeamOneRatingPref"))
        {
            SetUpTeamRatings();
        }

        for(int i = 0; i < 30; i++)
        {
            if(!PlayerPrefs.HasKey("Team" + numbersToWords[i] + "RatingPref"))
            {
                PlayerPrefs.SetInt("Team" + numbersToWords[i] + "RatingPref", teamsTemp[i].getRating());
            }
            teamsTemp[i].setRating(PlayerPrefs.GetInt("Team"  + numbersToWords[i] + "RatingPref"));
        }
    }

    void SetUpTeamRatings()
    {
        // generate the team ratings, work in progress
        List<int> tempIndexList = new List<int>();
        for(int i = 0; i < 30; i++)
        {
            tempIndexList.Add(i);
        }
        int randomMatchUp = Random.Range(0, tempIndexList.Count);
        teamsTemp[MainMenuScript.currentTeam - 1].setRating(mainMenuScript.GetTeamRating());
        teamsTemp[randomMatchUp].setRating(100 - mainMenuScript.GetTeamRating());
        tempIndexList.Remove(MainMenuScript.currentTeam - 1);
        tempIndexList.Remove(randomMatchUp);

        int count = tempIndexList.Count;
        for(int i = 0; i < count / 2; i++)
        {
            int first = tempIndexList[Random.Range(0, tempIndexList.Count)];
            tempIndexList.Remove(first);
            int second = tempIndexList[Random.Range(0, tempIndexList.Count)];
            tempIndexList.Remove(second);
            GenerateRatingsTwo(first, second);
        }
    }

    void GenerateRatingsTwo(int indexOne, int indexTwo)
    {
        int ratingOne = Random.Range(10, 90);
        int ratingTwo = 100 - ratingOne;
        teamsTemp[indexOne].setRating(ratingOne);
        teamsTemp[indexTwo].setRating(ratingTwo);
    }

    void ResetTeamRatings()
    {
        SetUpTeamRatings();
        for(int i = 0; i < 30; i++)
        {
            PlayerPrefs.SetInt("Team" + numbersToWords[i] + "RatingPref", teamsTemp[i].getRating());
            teamsTemp[i].setRating(PlayerPrefs.GetInt("Team"  + numbersToWords[i] + "RatingPref"));
        }
    }

    public void CreatePlayerSeasonSchedule()
    {
        PlayerPrefs.SetInt("ResetSeasonSchedulePref", 0);
        for(int i = 0; i < playerSeasonScheduleIndexes.Count; i++)
        {
            playerSeasonScheduleIndexes.RemoveAt(0);
        }

        List<int> tempList = new List<int>();
        for(int i = 0; i < teamIndexList.Count; i++)
        {
            tempList.Add(teamIndexList[i]);
        }

        for(int i = 0; i < teamIndexList.Count; i++)
        {
            int random = Random.Range(0, tempList.Count);
            PlayerPrefs.SetInt("PlayerSeasonScheduleGame" + numbersToWords[i] + "OpponentPref", tempList[random]);
            tempList.RemoveAt(random);
        }
    }

    public void LoadPlayerSeasonSchedule()
    {
        for(int i = 0; i < playerSeasonScheduleIndexes.Count; i++)
        {
            playerSeasonScheduleIndexes.RemoveAt(0);
        }

        for(int i = 0; i < 29; i++)
        {
            playerSeasonScheduleIndexes.Add(PlayerPrefs.GetInt("PlayerSeasonScheduleGame" + numbersToWords[i] + "OpponentPref"));
        }
    }

    public string FindTeamForNextGame(Team[] teams)
    {
        if(playerMadePostSeason)
        {
            int playerSeed = teams[MainMenuScript.currentTeam - 1].getSeed();
            string playerConference = teams[MainMenuScript.currentTeam - 1].getConference();
            List<Team> playoffConferenceTeams = new List<Team>();
            List<Team> opposingConferenceTeams = new List<Team>();
            if(playerConference == "West")
            {
                playoffConferenceTeams = westernConferenceTeamsByRecord;
                opposingConferenceTeams = easternConferenceTeamsByRecord;
            }
            else if(playerConference == "East")
            {
                playoffConferenceTeams = easternConferenceTeamsByRecord;
                opposingConferenceTeams = westernConferenceTeamsByRecord;
            }

            if(MainMenuScript.postSeasonGameNumber == 1)
            {
                playoffsNextTeam = playoffConferenceTeams[8 - playerSeed].getID();
                return playoffConferenceTeams[8 - playerSeed].getName();
            }
            else if(MainMenuScript.postSeasonGameNumber == 2)
            {
                bool playerWonRoundOneBracketOne = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundOneBracketOneWinner;
                bool playerWonRoundOneBracketTwo = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundOneBracketTwoWinner;
                bool playerWonRoundOneBracketThree = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundOneBracketThreeWinner;
                bool playerWonRoundOneBracketFour = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundOneBracketFourWinner;

                if(playoffsRoundOneBracketOneWinner == -1 || playoffsRoundOneBracketTwoWinner == -1 || playoffsRoundOneBracketThreeWinner == -1 || playoffsRoundOneBracketFourWinner == -1 )
                {
                    if(!playerWonRoundOneBracketOne)
                    {
                        SimulatePlayoffSeries(playoffConferenceTeams[0].getID(), playoffConferenceTeams[7].getID(), "One", "One");
                        playoffsRoundOneBracketOneWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketOneWinnerPref");
                    }
                    if(!playerWonRoundOneBracketTwo)
                    {
                        SimulatePlayoffSeries(playoffConferenceTeams[3].getID(), playoffConferenceTeams[4].getID(), "One", "Two");
                        playoffsRoundOneBracketTwoWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketTwoWinnerPref");
                    }
                    if(!playerWonRoundOneBracketThree)
                    {
                        SimulatePlayoffSeries(playoffConferenceTeams[2].getID(), playoffConferenceTeams[5].getID(), "One", "Three");
                        playoffsRoundOneBracketThreeWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketThreeWinnerPref");
                    }
                    if(!playerWonRoundOneBracketFour)
                    {
                        SimulatePlayoffSeries(playoffConferenceTeams[1].getID(), playoffConferenceTeams[6].getID(), "One", "Four");
                        playoffsRoundOneBracketFourWinner = PlayerPrefs.GetInt("PlayoffsRoundOneBracketFourWinnerPref");
                    }
                }

                if(playerWonRoundOneBracketOne)
                {
                    playoffsNextTeam = teams[playoffsRoundOneBracketTwoWinner].getID();
                    return teams[playoffsRoundOneBracketTwoWinner].getName();
                }
                else if(playerWonRoundOneBracketTwo)
                {
                    playoffsNextTeam = teams[playoffsRoundOneBracketOneWinner].getID();
                    return teams[playoffsRoundOneBracketOneWinner].getName();
                }
                else if(playerWonRoundOneBracketThree)
                {
                    playoffsNextTeam = teams[playoffsRoundOneBracketFourWinner].getID();
                    return teams[playoffsRoundOneBracketFourWinner].getName();
                }
                else
                {
                    playoffsNextTeam = teams[playoffsRoundOneBracketThreeWinner].getID();
                    return teams[playoffsRoundOneBracketThreeWinner].getName();
                }
            }
            else if(MainMenuScript.postSeasonGameNumber == 3)
            {
                bool playerWonRoundTwoBracketOne = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundTwoBracketOneWinner;
                bool playerWonRoundTwoBracketTwo = teams[MainMenuScript.currentTeam - 1].getID() == playoffsRoundTwoBracketTwoWinner;
                
                if(playoffsRoundTwoBracketOneWinner == -1 || playoffsRoundTwoBracketTwoWinner == -1)
                {
                    if(!playerWonRoundTwoBracketOne)
                    {
                        SimulatePlayoffSeries(playoffsRoundOneBracketOneWinner, playoffsRoundOneBracketTwoWinner, "Two", "One");
                        playoffsRoundTwoBracketOneWinner = PlayerPrefs.GetInt("PlayoffsRoundTwoBracketOneWinnerPref");
                    }
                    if(!playerWonRoundTwoBracketTwo)
                    {
                        SimulatePlayoffSeries(playoffsRoundOneBracketThreeWinner, playoffsRoundOneBracketFourWinner, "Two", "Two");
                        playoffsRoundTwoBracketTwoWinner = PlayerPrefs.GetInt("PlayoffsRoundTwoBracketTwoWinnerPref");
                    }
                }

                if(playerWonRoundTwoBracketOne)
                {
                    playoffsNextTeam = teams[playoffsRoundTwoBracketTwoWinner].getID();
                    return teams[playoffsRoundTwoBracketTwoWinner].getName();
                }
                else
                {
                    playoffsNextTeam = teams[playoffsRoundTwoBracketOneWinner].getID();
                    return teams[playoffsRoundTwoBracketOneWinner].getName();
                }
            }
            else if(MainMenuScript.postSeasonGameNumber == 4)
            {
                int randomTeam = Random.Range(0, opposingConferenceTeams.Count);
                playoffsNextTeam = opposingConferenceTeams[randomTeam].getID();
                return opposingConferenceTeams[randomTeam].getName();
            }
        }

        return teams[playerSeasonScheduleIndexes[nextTeam]].getName() + " (" + teams[playerSeasonScheduleIndexes[nextTeam]].getLevel() + ")";
    }

    public void UpdateStandingsText()
    {
        string westText = "";
        string eastText = "";
        List<Team> westTemp = new List<Team>();
        List<Team> eastTemp = new List<Team>();
        for(int i = 0; i < 30; i++)
        {
            if(i < 15)
            {
                westTemp.Add(teamsTemp[i]);
            } else {
                eastTemp.Add(teamsTemp[i]);
            }
        }
        SortTeamsByRecord(westTemp, westTemp.Count);
        SortTeamsByRecord(eastTemp, eastTemp.Count);
        for(int i = 0; i < 15; i++)
        {
            westText += i + 1 + ". " + westTemp[i].getName() + " (" + westTemp[i].getLevel() + ") : " + westTemp[i].getRecord() + "\n";
        }
        for(int i = 0; i < 15; i++)
        {
            eastText += i + 1 + ". " + eastTemp[i].getName() + " (" + eastTemp[i].getLevel() + ") : " + eastTemp[i].getRecord() + "\n";
        }
        eastStandingsText.text = eastText;
        westStandingsText.text = westText;
    }

    void UpdatePlayersMatchup()
    {
        if(playerGameWon)
        {
            teamsTemp[MainMenuScript.currentTeam - 1].AddWin();
            teamsTemp[playerSeasonScheduleIndexes[nextTeam - 1]].AddLoss();
        }
        else if(!playerGameWon)
        {
            teamsTemp[MainMenuScript.currentTeam - 1].AddLoss();
            teamsTemp[playerSeasonScheduleIndexes[nextTeam - 1]].AddWin();
        }
    }

    public void SimulateWeek()
    {
        // List<bool> recordPool = new List<bool>();
        // int random = Random.Range(1, 3);
        // for(int i = 0; i < 14; i++)
        // {
        //     if(random == 1)
        //     {
        //         recordPool.Add(false);
        //         recordPool.Add(true);
        //     } else {
        //         recordPool.Add(true);
        //         recordPool.Add(false);
        //     }  
        // }

        // int count = 0;
        // for(int i = 0; i < 30; i++)
        // {
        //     if(count < teamsTemp.Length)
        //     {
        //         if(teamsTemp[i].getID() == teamsTemp[playerSeasonScheduleIndexes[nextTeam - 1]].getID() || teamsTemp[count].isPlayersTeam())
        //         {
        //             count++;
        //         } else {
        //             int chance = Random.Range(0, recordPool.Count - 1);
        //             teamsTemp[count].SimulateGame(recordPool[chance]);
        //             recordPool.RemoveAt(chance);
        //             count++;
        //         }
        //     }
        // }

        List<int> tempIndexList = new List<int>();
        for(int i = 0; i < 30; i++)
        {
            tempIndexList.Add(i);
        }
        tempIndexList.Remove(MainMenuScript.currentTeam - 1);
        tempIndexList.Remove(teamsTemp[playerSeasonScheduleIndexes[nextTeam - 1]].getID());

        int count = tempIndexList.Count;
        for(int i = 0; i < count / 2; i++)
        {
            int teamOneIndexTemp = tempIndexList[Random.Range(0, tempIndexList.Count)];
            tempIndexList.Remove(teamOneIndexTemp);
            int teamTwoIndexTemp = tempIndexList[Random.Range(0, tempIndexList.Count)];
            tempIndexList.Remove(teamTwoIndexTemp);
            
            int matchupOdds = teamsTemp[teamOneIndexTemp].getRating() + teamsTemp[teamTwoIndexTemp].getRating();
            int chance = Random.Range (1, matchupOdds + 1);

            if(chance <= teamsTemp[teamOneIndexTemp].getRating())
            {
                teamsTemp[teamOneIndexTemp].SimulateGame(true);
                teamsTemp[teamTwoIndexTemp].SimulateGame(false);
            }
            else if(chance > teamsTemp[teamOneIndexTemp].getRating())
            {
                teamsTemp[teamOneIndexTemp].SimulateGame(false);
                teamsTemp[teamTwoIndexTemp].SimulateGame(true);
            }
        }
    }

    void SimulatePlayoffSeries(int teamOneID, int teamTwoID, string roundNum, string bracketNum)
    {
        int chance = Random.Range(1, 3);
        if(chance == 1)
        {
            PlayerPrefs.SetInt("PlayoffsRound" + roundNum + "Bracket" + bracketNum + "WinnerPref", teamOneID);
        } else {
            PlayerPrefs.SetInt("PlayoffsRound" + roundNum + "Bracket" + bracketNum + "WinnerPref", teamTwoID);
        }
    }
    
    void CheckPostSeason()
    {
        if(MainMenuScript.gameNumber > mainMenuScript.regSeasonGames)
        {
            UpdatePostSeason();
        }
    }

    void UpdatePostSeason()
    {
        playerMadePostSeason = false;
        List<Team> east = new List<Team>();
        List<Team> west = new List<Team>();
        for(int i = 0; i < 15; i++)
        {
            west.Add(teamsTemp[i]);
        }
        for(int i = 15; i < 30; i++)
        {
            east.Add(teamsTemp[i]);
        }
        SortTeamsByRecord(east, east.Count);
        SortTeamsByRecord(west, west.Count);
        for(int i = 0; i < 8; i++)
        {
            easternConferenceTeamsByRecord.Add(east[i]);
            east[i].setSeed(i + 1);
            westernConferenceTeamsByRecord.Add(west[i]);
            west[i].setSeed(i + 1);
            playerMadePostSeason = east[i].isPlayersTeam() || west[i].isPlayersTeam() || playerMadePostSeason;
            if(playerMadePostSeason)
            {
                PlayerPrefs.SetInt("PlayerMadePostSeasonPref", 1);
            }
            Debug.Log(i + 1 + " in East: " + easternConferenceTeamsByRecord[i].getName());
            Debug.Log(i + 1 + " in West: " + westernConferenceTeamsByRecord[i].getName());
        }
        // check if player made playoffs
    }

    void SimulatePlayoffs(int round, int teamsInRound, Team[] teams)
    {
        int playerSeed = teams[MainMenuScript.currentTeam - 1].getSeed();
        string playerConference = teams[MainMenuScript.currentTeam - 1].getConference();
        List<Team> playoffConferenceTeams = new List<Team>();
        if(playerConference == "West")
        {
            playoffConferenceTeams = westernConferenceTeamsByRecord;
        }
        else if(playerConference == "East")
        {
            playoffConferenceTeams = easternConferenceTeamsByRecord;
        }

        int chance;
        for(int i = 1; i <= teamsInRound / 2; i++)
        {
            if(!(playerSeed == i || playerSeed == teamsInRound + 1 - i))
            {
                chance = Random.Range(1, 3);
                if(chance == 1)
                {
                    playoffConferenceTeams.RemoveAt(i - 1);
                } else {
                    playoffConferenceTeams.RemoveAt(teamsInRound - i);
                }
            }
        }
    }

    void SortTeamsByRecord(List<Team> array, int size)
    {
        for(int i = 0; i < size - 1; i++)
        {
            for(int x = 0; x < size - 1; x++)
            {
                if(array[x].getWinLossRatio() < array[x + 1].getWinLossRatio())
                {
                    Team temp = array[x];
                    array[x] = array[x + 1];
                    array[x + 1] = temp;
                }
            }
        }
    }

    public int GetNextTeamLevel()
    {
        int rating;
        if(!playerMadePostSeason)
        {
            rating = teamsTemp[playerSeasonScheduleIndexes[nextTeam]].getRating();
        } else {
            rating = teamsTemp[playoffsNextTeam].getRating();
        }
        
        if(rating >= 74)
        {
            return 5;
        }
        else if(rating >= 58)
        {
            return 4;
        }
        else if(rating >= 42)
        {
            return 3;
        }
        else if(rating >= 26)
        {
            return 2;
        } else {
            return 1;
        }
    }

    public void NewSeason()
    {
        PlayerPrefs.SetInt("NextTeamPref", 0);
        nextTeam = 0;

        PlayerPrefs.SetInt("GameNumberPref", 1);
        MainMenuScript.gameNumber = 1;

        PlayerPrefs.SetInt("TeamOneWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwoWinsPref", 0);
        PlayerPrefs.SetInt("TeamThreeWinsPref", 0);
        PlayerPrefs.SetInt("TeamFourWinsPref", 0);
        PlayerPrefs.SetInt("TeamFiveWinsPref", 0);
        PlayerPrefs.SetInt("TeamSixWinsPref", 0);
        PlayerPrefs.SetInt("TeamSevenWinsPref", 0);
        PlayerPrefs.SetInt("TeamEightWinsPref", 0);
        PlayerPrefs.SetInt("TeamNineWinsPref", 0);
        PlayerPrefs.SetInt("TeamTenWinsPref", 0);
        PlayerPrefs.SetInt("TeamElevenWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwelveWinsPref", 0);
        PlayerPrefs.SetInt("TeamThirteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamFourteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamFifteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamSixteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamSeventeenWinsPref", 0);
        PlayerPrefs.SetInt("TeamEighteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamNineteenWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyOneWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyTwoWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyThreeWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyFourWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyFiveWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentySixWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentySevenWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyEightWinsPref", 0);
        PlayerPrefs.SetInt("TeamTwentyNineWinsPref", 0);
        PlayerPrefs.SetInt("TeamThirtyWinsPref", 0);

        PlayerPrefs.SetInt("TeamOneLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwoLossesPref", 0);
        PlayerPrefs.SetInt("TeamThreeLossesPref", 0);
        PlayerPrefs.SetInt("TeamFourLossesPref", 0);
        PlayerPrefs.SetInt("TeamFiveLossesPref", 0);
        PlayerPrefs.SetInt("TeamSixLossesPref", 0);
        PlayerPrefs.SetInt("TeamSevenLossesPref", 0);
        PlayerPrefs.SetInt("TeamEightLossesPref", 0);
        PlayerPrefs.SetInt("TeamNineLossesPref", 0);
        PlayerPrefs.SetInt("TeamTenLossesPref", 0);
        PlayerPrefs.SetInt("TeamElevenLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwelveLossesPref", 0);
        PlayerPrefs.SetInt("TeamThirteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamFourteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamFifteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamSixteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamSeventeenLossesPref", 0);
        PlayerPrefs.SetInt("TeamEighteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamNineteenLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyOneLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyTwoLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyThreeLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyFourLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyFiveLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentySixLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentySevenLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyEightLossesPref", 0);
        PlayerPrefs.SetInt("TeamTwentyNineLossesPref", 0);
        PlayerPrefs.SetInt("TeamThirtyLossesPref", 0);
        PlayerPrefs.SetInt("PlayerMadePostSeasonPref", 0);

        teamOne.resetRecord();
        teamTwo.resetRecord();
        teamThree.resetRecord();
        teamFour.resetRecord();
        teamFive.resetRecord();
        teamSix.resetRecord();
        teamSeven.resetRecord();
        teamEight.resetRecord();
        teamNine.resetRecord();
        teamTen.resetRecord();
        teamEleven.resetRecord();
        teamTwelve.resetRecord();
        teamThirteen.resetRecord();
        teamFourteen.resetRecord();
        teamFifteen.resetRecord();
        teamSixteen.resetRecord();
        teamSeventeen.resetRecord();
        teamEighteen.resetRecord();
        teamNineteen.resetRecord();
        teamTwenty.resetRecord();
        teamTwentyOne.resetRecord();
        teamTwentyTwo.resetRecord();
        teamTwentyThree.resetRecord();
        teamTwentyFour.resetRecord();
        teamTwentyFive.resetRecord();
        teamTwentySix.resetRecord();
        teamTwentySeven.resetRecord();
        teamTwentyEight.resetRecord();
        teamTwentyNine.resetRecord();
        teamThirty.resetRecord();

        ResetTeamRatings();

        PlayerPrefs.SetInt("PlayoffsRoundOneBracketOneWinnerPref", -1);
        PlayerPrefs.SetInt("PlayoffsRoundOneBracketTwoWinnerPref", -1);
        PlayerPrefs.SetInt("PlayoffsRoundOneBracketThreeWinnerPref", -1);
        playoffsRoundOneBracketOneWinner = -1;
        playoffsRoundOneBracketTwoWinner = -1;
        playoffsRoundOneBracketThreeWinner = -1;
        playoffsRoundOneBracketFourWinner = -1;


        PlayerPrefs.SetInt("PlayoffsRoundTwoBracketOneWinnerPref", -1);
        PlayerPrefs.SetInt("PlayoffsRoundTwoBracketTwoWinnerPref", -1);
        playoffsRoundTwoBracketOneWinner = -1;
        playoffsRoundTwoBracketTwoWinner = -1;

        PlayerPrefs.SetInt("PlayoffsRoundThreeBracketOneWinnerPref", -1);
        playoffsRoundThreeBracketOneWinner = -1;

        mainMenuScript.NewFreeAgents("One");
        mainMenuScript.NewFreeAgents("Two");
        mainMenuScript.NewFreeAgents("Three");

        CreatePlayerSeasonSchedule();
        LoadPlayerSeasonSchedule();

        mainMenuScript.Draft();

        mainMenuScript.UpdateTexts();
    }
}
