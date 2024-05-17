using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallPassScript : MonoBehaviour
{
    private bool isPassing = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timeToReachTarget;
    private float startTime;

    TestPlayerScript playerCurrentlyBeingPassedToScript;
    public TestGameTracker testGameTracker;

    public Transform playerOneTransform;
    public Transform playerTwoTransform;

    public void Pass(GameObject teammate)
    {
        transform.position = GetPlayerInControlPos(testGameTracker.playerBeingControlled);
        startPosition = transform.position;
        targetPosition = teammate.transform.position;
        startTime = Time.time;
        
        // Adjust the time to reach the target based on distance and desired speed
        timeToReachTarget = Vector3.Distance(startPosition, targetPosition) / 10f; // Example speed
        isPassing = true;
        playerCurrentlyBeingPassedToScript = teammate.GetComponent<TestPlayerScript>();
    }

    private void Update()
    {
        if (isPassing)
        {
            float timeSinceStarted = Time.time - startTime;
            float percentageComplete = timeSinceStarted / timeToReachTarget;

            transform.position = Vector3.Lerp(startPosition, targetPosition, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isPassing = false;
                testGameTracker.PassUpdatePlayer(playerCurrentlyBeingPassedToScript.playerNum);
            }
        } else {
            transform.position = GetPlayerInControlPos(testGameTracker.playerBeingControlled);
        }
    }

    Vector3 GetPlayerInControlPos(int playerNumParam)
    {
        if(playerNumParam == 1)
        {
            return playerOneTransform.position;
        } else {
            return playerTwoTransform.position;
        }
    }
}
