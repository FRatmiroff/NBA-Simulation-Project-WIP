using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    // object components
    [Header("Object Components")]
    public Rigidbody2D rb;
    public Collider2D col;

    // gameobjects
    [Header("GameObjects")]
    public GameObject player;

    // return ball position (for drag and shoot functionality)
    public Vector3 pos { get { return transform.position; } }

    // gameobject components
    [Header("GameObject Components")]
    public Transform playerOneTransform;
    public Transform playerTwoTransform;
    public Transform playerThreeTransform;
    public Transform playerFourTransform;
    public Transform playerFiveTransform;

    // scripts
    [Header("Scripts")]
    public ControlManager CM;
    public GameManager GM;
    public HUDManager HM;
    PlayerMovement playerCurrentlyBeingPassedToScript;
    public PassingManager passingManager;    

    // checks
    [Header("Checks")]
    public bool possessionOver; // check if possession ends
    public bool isPassing = false; // check if ball is currently being passed
    public bool isDribbling = true;

    // highest point while shooting (to check if ball can go in)
    [Header("Highest Point During Shot")]
    public float highestPoint;

    // passing variables
    [Header("Passing Settings")]
    public float passSpeed;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timeToReachTarget;
    private float startTime;

    void Start()
    {
        possessionOver = false;
        highestPoint = 0f;
    }

    void Update()
    {
        if (isPassing)
        {
            float timeSinceStarted = Time.time - startTime;
            float percentageComplete = timeSinceStarted / timeToReachTarget;

            transform.position = Vector3.Lerp(startPosition, targetPosition, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isPassing = false;
                isDribbling = true;
                passingManager.PassUpdatePlayer(playerCurrentlyBeingPassedToScript.playerNum);
            }
        } else if(isDribbling) {
            transform.position = GetPlayerInControlPos(passingManager.playerBeingControlled);
        }
    }

    void FixedUpdate()
    {
        // track highest point during shot
        if(CM.shotInAir)
        {
            if(transform.position.y > highestPoint)
            {
                highestPoint = transform.position.y;
            }
        }
    }

    public void Pass(GameObject teammate)
    {
        transform.position = GetPlayerInControlPos(passingManager.playerBeingControlled);
        startPosition = transform.position;
        targetPosition = teammate.transform.position;
        startTime = Time.time;
        passingManager.RotatePlayer();
        
        // Adjust the time to reach the target based on distance and desired speed
        timeToReachTarget = Vector3.Distance(startPosition, targetPosition) / passSpeed; // Example speed
        isPassing = true;
        isDribbling = false;
        playerCurrentlyBeingPassedToScript = teammate.GetComponent<PlayerMovement>();
    }

    public void Push(Vector2 force)
    {
        // shoot ball
        isDribbling = false;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public Vector3 GetPlayerInControlPos(int playerNumParam)
    {
        if(playerNumParam == 1)
        {
            return playerOneTransform.position;
        } 
        else if(playerNumParam == 2) 
        {
            return playerTwoTransform.position;
        }
        else if(playerNumParam == 3)
        {
            return playerThreeTransform.position;
        }
        else if(playerNumParam == 4)
        {
            return playerFourTransform.position;
        }
        else
        {
            return playerFiveTransform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if ball is on basket
        if(collision.gameObject.name == "BasketTrigger")
        {
            CM.onHoop = true;
        }
        // check if ball is scored
        if(collision.gameObject.name == "ScoreTrigger" && rb.velocity.y < 0f && !possessionOver && highestPoint > 3.5f)
        {
            Debug.Log("Score");
            possessionOver = true;
            GM.AddPointsToPlayer(CM.shotValue);
            GM.AddAssistToPlayer(passingManager.lastPlayerToPass);
            HM.UpdatePlayerScore(CM.shotValue);
            CM.shotMade = true;
            GM.PossessionOver();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // check if ball is on basket
        if(collision.gameObject.name == "BasketTrigger")
        {
            CM.onHoop = false;
        }
    }
}
