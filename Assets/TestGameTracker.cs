using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameTracker : MonoBehaviour
{
    public int playerBeingControlled;
    public TestPlayerScript playerOneScript;
    public TestPlayerScript playerTwoScript;

    List<int> testList = new List<int>();
    List<int> listOfNumbers = new List<int>();
    void Start()
    {
        playerBeingControlled = 1;
        UpdatePlayerControlled();
        playerOneScript.AssignPlayerNum(1);
        playerTwoScript.AssignPlayerNum(2);

        listOfNumbers.Add(1);
        listOfNumbers.Add(2);
        listOfNumbers.Add(3);
        listOfNumbers.Add(4);
        listOfNumbers.Add(5);
        listOfNumbers.Add(6);
        listOfNumbers.Add(7);
        listOfNumbers.Add(8);

        testList = listOfNumbers;
        testList.RemoveAt(1);
        testList.RemoveAt(2);
        listOfNumbers.RemoveAt(3);

        Debug.Log("List of Numbers Count: " + listOfNumbers.Count + ", Test List Count: " + testList.Count);
    }

    void UpdatePlayerControlled()
    {
        if(playerBeingControlled == 1)
        {
            SetPlayerControl(true, false);
        }
        if(playerBeingControlled == 2)
        {
            SetPlayerControl(false, true);
        }
    }

    public void PassUpdatePlayer(int playerNumParam)
    {
        playerBeingControlled = playerNumParam;
        UpdatePlayerControlled();
    }

    void SetPlayerControl(bool one, bool two)
    {
        playerOneScript.beingControlled = one;
        playerTwoScript.beingControlled = two;
    }
}
