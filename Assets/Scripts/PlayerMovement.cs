using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    // player attributes
    [Header("Offensive Player Stats")]
    public float moveSpeed;
    public float walkSpeed;
    float sprintSpeed;
    public float strength;
    public float accuracy;
    public float stamina;
    public string playerName;
    public int playerNum;

    // object components
    [Header("Object Components")]
    private Rigidbody2D rb;
    public Collider2D col;
    public Transform LeftCorner;
    public Transform LeftWing;
    public Transform TopOfKey;
    public Transform RightWing;
    public Transform RightCorner;
    Transform courtSpotTarget;
    
    // movement variables
    Vector2 movement;
    bool movementEnabled;

    // checks
    [Header("Offensive Checks")]
    public bool defense; // check if on defense
    public bool threepointer; // check if three point range
    public bool beingControlled; // check if current player is being controlled
    public bool passOnClick;
    public bool devMode; // removes the requirement to go from menus to game for attributes
    public bool inLayupRange;
    public bool inASpot;
    public bool rotate;
    bool spotDetermined;

    // scripts
    [Header("Scripts")]
    public ControlManager CM;
    public GameManager GM;
    public ballScript BS;
    public PassingManager passingManager;
    public OffensivePlayerLogic offensivePlayerLogic;

    void Start()
    {
        // set components
        rb = gameObject.GetComponent<Rigidbody2D>();

        // default offensive settings
        movementEnabled = true;
        defense = false;
        threepointer = true;
        inLayupRange = false;
        inASpot = true;
        rotate = false;
        spotDetermined = false;

        // get stats
        walkSpeed = CM.GetSpeedValue(playerNum);
        strength = CM.GetStrengthValue(playerNum);
        accuracy = CM.GetStrengthValue(playerNum);
        playerName = CM.GetName(playerNum);
        if(devMode)
        {
            walkSpeed = 2;
            strength = 10.5f;
            accuracy = 5;
        }
        stamina = 1f;

        // assign movement attributes
        sprintSpeed = walkSpeed + 1f;
        moveSpeed = walkSpeed;
    }


    void Update()
    {
        // if player is controlling
        if(beingControlled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movementEnabled = !CM.shooting && !defense;

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed = sprintSpeed * stamina;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = walkSpeed * stamina;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                CM.reboundBall();
            }
        }

        if(beingControlled && (CM.shooting || CM.shotInAir || CM.ballGrounded))
        {
            if(transform.position.x > 0)
            {
                transform.localScale = new Vector2(-1.5f, 1.5f);
            } else if(transform.position.x < 0) {
                transform.localScale = new Vector2(1.5f, 1.5f);
            }
        } else {
            if(movement.x > 0)
            {
                transform.localScale = new Vector2(1.5f, 1.5f);
            } else if(movement.x < 0) {
                transform.localScale = new Vector2(-1.5f, 1.5f);
            }
        }
    }


    void FixedUpdate()
    {
        // move player
        if(movementEnabled && beingControlled)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        if(rotate)
        {
            if(!spotDetermined)
            {
                courtSpotTarget = DetermineCourtSpot();
            }
            RotateTowardsSpot(courtSpotTarget);
        }
    }

    private Transform DetermineCourtSpot()
    {
        spotDetermined = true;
        Transform courtSpot = LeftCorner;
        if(offensivePlayerLogic.playersInL3 == 0)
        {
            courtSpot = TopOfKey;
        }
        else if(offensivePlayerLogic.playersInL2 == 0)
        {
            courtSpot = LeftWing;
        }
        else if(offensivePlayerLogic.playersInL4 == 0)
        {
            courtSpot = RightWing;
        }
        else if(offensivePlayerLogic.playersInL1 == 0)
        {
            courtSpot = LeftCorner;
        }
        else if(offensivePlayerLogic.playersInL5 == 0)
        {
            courtSpot = RightCorner;
        }
        return courtSpot;
    }

    private void RotateTowardsSpot(Transform target)
    {
        float step = sprintSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        Debug.Log(transform.position + ", " + target.position);
        if((Vector2)transform.position == (Vector2)target.position)
        {
            Debug.Log("Stop Rotating");
            rotate = false;
            spotDetermined = false;
            inASpot = true; // Mark as in a spot to prevent re-triggering
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click");
        Debug.Log(CM.shotInAir + ", " + CM.ballGrounded);
        if(!beingControlled && !BS.isPassing && !CM.isDragging && !CM.shotInAir && !CM.ballGrounded)
        {
            BS.Pass(gameObject);
        }
    }

    void OnMouseOver()
    {
        CM.passOnClick = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if player is controlling
        if(beingControlled)
        {
            // check if layup range
            if(collision.gameObject.name == "LayupTrigger")
            {
                inLayupRange = true;
            }

            // check if within the arc
            if(collision.gameObject.name == "PointAmountTrigger")
            {
                threepointer = false;
            }
        }

        // check if player is in one of the spots
        if(collision.gameObject.name == "LeftCorner")
        {
            offensivePlayerLogic.playersInL1++;
            inASpot = true;
        }
        else if(collision.gameObject.name == "LeftWing")
        {
            offensivePlayerLogic.playersInL2++;
            inASpot = true;
        }
        else if(collision.gameObject.name == "TopOfKey")
        {
            offensivePlayerLogic.playersInL3++;
            inASpot = true;
        }
        else if(collision.gameObject.name == "RightWing")
        {
            offensivePlayerLogic.playersInL4++;
            inASpot = true;
        }
        else if(collision.gameObject.name == "RightCorner")
        {
            offensivePlayerLogic.playersInL5++;
            inASpot = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // exit layup range
        if(collision.gameObject.name == "LayupTrigger")
        {
            inLayupRange = false;
        }

        // outside the three point line
        if(collision.gameObject.name == "PointAmountTrigger")
        {
            threepointer = true;
        }

        // check if player leaves a spot
        if(collision.gameObject.name == "LeftCorner")
        {
            if(offensivePlayerLogic.playersInL1 > 0f)
            offensivePlayerLogic.playersInL1--;
            inASpot = false;
        }
        else if(collision.gameObject.name == "LeftWing")
        {
            if(offensivePlayerLogic.playersInL2 > 0f)
            offensivePlayerLogic.playersInL2--;
            inASpot = false;
        }
        else if(collision.gameObject.name == "TopOfKey")
        {
            Debug.Log("left");
            if(offensivePlayerLogic.playersInL3 > 0f)
            offensivePlayerLogic.playersInL3--;
            inASpot = false;
        }
        else if(collision.gameObject.name == "RightWing")
        {
            if(offensivePlayerLogic.playersInL4 > 0f)
            offensivePlayerLogic.playersInL4--;
            inASpot = false;
        }
        else if(collision.gameObject.name == "RightCorner")
        {
            if(offensivePlayerLogic.playersInL5 > 0f)
            offensivePlayerLogic.playersInL5--;
            inASpot = false;
        }
    }

    // assign player number to each offensive player
    public void AssignPlayerNum(int playerNumParam)
    {
        playerNum = playerNumParam;
    }
}
