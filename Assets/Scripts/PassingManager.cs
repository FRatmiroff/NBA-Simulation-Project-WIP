using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingManager : MonoBehaviour
{
    // number of player with ball
    [Header("Specific Players")]
    public int playerBeingControlled;
    public int lastPlayerToPass;

    // scripts
    [Header("Scripts")]
    public PlayerMovement playerOneScript;
    public PlayerMovement playerTwoScript;
    public PlayerMovement playerThreeScript;
    public PlayerMovement playerFourScript;
    public PlayerMovement playerFiveScript;
    public HUDManager HUDManager;

    // gameobjects
    [Header("GameObjects")]
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    public GameObject playerFive;

    // assist variables
    [Header("Assist Variables")]
    public float assistCooldown;

    void Start()
    {
        playerBeingControlled = 1;
        lastPlayerToPass = 0;
        UpdatePlayerControlled();
        playerOneScript.AssignPlayerNum(1);
        playerTwoScript.AssignPlayerNum(2);
        playerThreeScript.AssignPlayerNum(3);
        playerFourScript.AssignPlayerNum(4);
        playerFiveScript.AssignPlayerNum(5);
    }

    void Update()
    {
        if(assistCooldown > 0f)
        {
            assistCooldown -= Time.deltaTime;
        }
    }

    void UpdatePlayerControlled()
    {
        if(playerBeingControlled == 1)
        {
            SetPlayerControl(true, false, false, false, false);
        }
        if(playerBeingControlled == 2)
        {
            SetPlayerControl(false, true, false, false, false);
        }
        if(playerBeingControlled == 3)
        {
            SetPlayerControl(false, false, true, false, false);
        }
        if(playerBeingControlled == 4)
        {
            SetPlayerControl(false, false, false, true, false);
        }
        if(playerBeingControlled == 5)
        {
            SetPlayerControl(false, false, false, false, true);
        }
        HUDManager.UpdatePlayerName(GetPlayerInControlPM());
    }

    public void PassUpdatePlayer(int playerNumParam)
    {
        assistCooldown = 10.0f;
        lastPlayerToPass = playerBeingControlled;
        playerBeingControlled = playerNumParam;
        UpdatePlayerControlled();
    }

    public void RotatePlayer()
    {
        if(!GetPlayerInControlPM().inASpot)
        {
            GetPlayerInControlPM().rotate = true;
        }
    }

    void SetPlayerControl(bool one, bool two, bool three, bool four, bool five)
    {
        playerOneScript.beingControlled = one;
        playerTwoScript.beingControlled = two;
        playerThreeScript.beingControlled = three;
        playerFourScript.beingControlled = four;
        playerFiveScript.beingControlled = five;
    }

    public GameObject GetPlayerInControlGO()
    {
        if(playerBeingControlled == 1)
        {
            return playerOne;
        } 
        else if(playerBeingControlled == 2)
        {
            return playerTwo;
        }
        else if(playerBeingControlled == 3)
        {
            return playerThree;
        }
        else if(playerBeingControlled == 4)
        {
            return playerFour;
        }
        else
        {
            return playerFive;
        }
    }

    public PlayerMovement GetPlayerInControlPM()
    {
        if(playerBeingControlled == 1)
        {
            return playerOneScript;
        } 
        else if(playerBeingControlled == 2)
        {
            return playerTwoScript;
        }
        else if(playerBeingControlled == 3)
        {
            return playerThreeScript;
        }
        else if(playerBeingControlled == 4)
        {
            return playerFourScript;
        }
        else
        {
            return playerFiveScript;
        }
    }
}
