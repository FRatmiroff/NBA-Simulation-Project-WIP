using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defenderLogic : MonoBehaviour
{
    // scripts
    [Header("Scripts")]
    public ControlManager controlManager;
    public PassingManager passingManager;
    public DefenderColliderScript defenderColliderScript;
    public GameManager gameManager;

    // gameobjects
    [Header("GameObjects")]
    public GameObject myPlayer;
    public GameObject defenderCollider; // to prevent collision glitching, separate game object as collider

    // gameobject components
    [Header("GameObject Components")]
    public Transform player;
    public Transform hoop;

    // defensive attributes variables
    [Header("Defender Difficulty Settings")]
    public float maintainDistance = 1.5f; // Desired distance from the player
    public float closeOutSpeedMultiplier = 0.25f; // Speed multiplier for closing out on the shooter
    public float closeOutStopDistance = 0.9f; // Distance to stop from the player when closing out
    public float smoothTime = 0.2f; // Time taken to smooth the movement
    public float distScaleFactor = 10f; // factor by which the defender gets closer to the player as the player gets closer to the basket
    public float maintainDistanceBase = 1.5f;

    // defensive logic variables
    public int defenderNum;
    private Queue<Vector2> playerPositions = new Queue<Vector2>(); // Stores player positions for delayed following
    private float timeSinceLastPosition = 0f;
    private float positionRecordInterval = 0.1f; // Interval to record player's position
    private Vector3 currentVelocity = Vector3.zero; // Current velocity for SmoothDamp

    void Start()
    {
        // get stats
        closeOutSpeedMultiplier = gameManager.GetCloseOutSpeedMultiplierValue(defenderNum);
        smoothTime = gameManager.GetSmoothTimeValue(defenderNum);
        distScaleFactor = gameManager.GetDistScaleFactorValue(defenderNum);
        maintainDistanceBase = gameManager.GetMaintainDistanceBaseValue(defenderNum);
    }

    private void Update()
    {
        timeSinceLastPosition += Time.deltaTime;
        if(transform.position.x > 0)
        {
            transform.localScale = new Vector2(1.5f, 1.5f);
        } else {
            transform.localScale = new Vector2(-1.5f, 1.5f);
        }

        if (timeSinceLastPosition >= positionRecordInterval)
        {
            playerPositions.Enqueue(player.position);
            timeSinceLastPosition = 0;
        }

        while (playerPositions.Count > 0 && playerPositions.Count > (smoothTime / positionRecordInterval))
        {
            playerPositions.Dequeue(); // removes and returns the object next in queue
        }

        // close out on shot
        if (controlManager.shooting && myPlayer == passingManager.GetPlayerInControlGO())
        {
            CloseOut();
        }
        else // defensive logic, stay between player and basket
        {
            MoveDefender();
        }

        // make collider follow defender's movement
        defenderCollider.transform.position = transform.position;
    }

    void MoveDefender()
    {
        if (playerPositions.Count == 0) return; // if no new player positions, don't move defender

        // get necessary positions
        Vector2 delayedPlayerPosition = playerPositions.Peek(); // returns object next in queue without removing it
        Vector2 defenderPosition = transform.position; // set defender position
        Vector2 hoopPosition = hoop.position; // set position of hoop

        // direction from player to hoop
        Vector2 playerToHoopDirection = (hoopPosition - delayedPlayerPosition).normalized; // when normalized, a vector keeps the same direction but its length is 1.0
        // position for defender to move to
        Vector2 idealDefenderPosition = delayedPlayerPosition + playerToHoopDirection * maintainDistance;

        Vector2 playerToHoop = hoopPosition - delayedPlayerPosition;

        // scales distance so defender gets closer to player as player gets closer to hoop
        float distScale = (Mathf.Abs(playerToHoop.x) + Mathf.Abs(playerToHoop.y)) / distScaleFactor;
        maintainDistance = maintainDistanceBase * (distScale + 0.3f);

        // Ensure defender is always between player and hoop
        Vector2 defenderToHoopDirection = (hoopPosition - defenderPosition).normalized;
        float angleBetweenPlayerAndHoop = Vector2.Angle(playerToHoopDirection, defenderToHoopDirection);
        if (angleBetweenPlayerAndHoop > 90)
        {
            idealDefenderPosition = delayedPlayerPosition - playerToHoopDirection * maintainDistance;
        }

        // Apply smoothing to the movement
        Vector3 targetPosition = new Vector3(idealDefenderPosition.x, idealDefenderPosition.y, transform.position.z);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        transform.position = smoothPosition;
    }

    void CloseOut()
    {
        if(!defenderColliderScript.touchingOffensivePlayer)
        {
            Vector2 playerPosition = player.position;
            Vector2 directionToPlayer = (playerPosition - (Vector2)transform.position).normalized;

            Vector2 closeOutTargetPosition = playerPosition - directionToPlayer * closeOutStopDistance;
            Vector3 targetPosition = new Vector3(closeOutTargetPosition.x, closeOutTargetPosition.y, transform.position.z);

            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime / closeOutSpeedMultiplier);
            transform.position = smoothPosition;
        }  
    }

    public void AssignDefenderNum(int defenderNumParam)
    {
        defenderNum = defenderNumParam;
    }
}