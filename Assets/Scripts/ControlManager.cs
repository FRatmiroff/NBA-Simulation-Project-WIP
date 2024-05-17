using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    // object components
    [Header("Object Components")]
    Camera cam;

    // scripts
    [Header("Scripts")]
    public ballScript ball;
    public PlayerMovement playerOneMovement;
    public PlayerMovement playerTwoMovement;
    public PlayerMovement playerThreeMovement;
    public PlayerMovement playerFourMovement;
    public PlayerMovement playerFiveMovement;
    public ballScript ballScript;
    public PassingManager passingManager;
    public GameManager gameManager;
    public Trajectory trajectory;

    // gameobjects
    [Header("GameObjects")]
    public GameObject basketball;
    public GameObject playerNumberOne;
    public GameObject playerNumberTwo;
    public GameObject playerNumberThree;
    public GameObject playerNumberFour;
    public GameObject playerNumberFive;
    public GameObject playerInControl;
    public GameObject defender;

    // gameobject components
    [Header("GameObject Components")]
    Rigidbody2D basketballRB;
    SpriteRenderer basketballSR;
    
    // shot variables
    [Header("Variables")]
    [SerializeField] float pushForce = 4f;
    public bool isDragging;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    public static float playerOneMaxForce, playerTwoMaxForce, playerThreeMaxForce, playerFourMaxForce, playerFiveMaxForce;
    public static float playerOneSpeed, playerTwoSpeed, playerThreeSpeed, playerFourSpeed, playerFiveSpeed;
    public static float playerOneAccuracy, playerTwoAccuracy, playerThreeAccuracy, playerFourAccuracy, playerFiveAccuracy;
    public static string playerOneName, playerTwoName, playerThreeName, playerFourName, playerFiveName;
    float distance;
    float shotY;

    // checks
    [Header("Checks")]
    public bool layup;
    public bool shooting;
    public bool possession;
    public bool ballGrounded;
    public bool shotInAir;
    public bool onHoop;
    int rotationSide; // which side the player is on to indicate which way the ball should rotate
    public bool defense;
    public int shotValue;
    public bool shotMade;
    public bool passOnClick;
    public bool devMode;

    [Header("Player Values")]
    public int playerCourtLayer;
    

    [Header("Defender Difficulty Settings")]
    [SerializeField] float defenderShotBlockDistance;
    [SerializeField] float defenderLayupBlockDistance;

    void Start()
    {
        // set object components
        cam = Camera.main;
        basketballRB = basketball.GetComponent<Rigidbody2D>();
        basketballSR = basketball.GetComponent<SpriteRenderer>();

        // assign default check variables
        layup = false;
        shooting = false;
        ballGrounded = false;
        possession = true;
        onHoop = false;
        basketballRB.freezeRotation = true;
        defense = false;
        shotMade = false;
        shotInAir = false;

        if(devMode)
        {
            // maxForce = 13;
            // accuracy = 10;
        }
    }
    void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition); // position of mouse on screen
        if(Input.GetMouseButtonDown(0) && !playerWithBallLayupCheck() && possession && !defense && !passOnClick && !ballScript.isPassing) // start shot
        {
            isDragging = true;
            OnDragStart();
        }
        if(Input.GetMouseButtonUp(0) && !playerWithBallLayupCheck() && possession && !defense && isDragging) // end shot
        {
            isDragging = false;
            ChangeStamina(0.07f);
            OnDragEnd();

        }
        if(isDragging) // during shot
        {
            OnDrag();
        }

        if(Input.GetMouseButtonDown(0) && playerWithBallLayupCheck() & possession && !defense && !passOnClick && !ballScript.isPassing) // layup
        {
            // calculate force required for layup
            Vector2 layupForce = new Vector2(passingManager.GetPlayerInControlGO().transform.position.x * -0.8f, 8f - passingManager.GetPlayerInControlGO().transform.position.y * 0.5f);
            EnableBall(); // enable ball for layup

            // set shot variables
            shotInAir = true;
            ballScript.highestPoint = ball.transform.position.y;
            shotValue = 2;
            ballGrounded = false;
            basketballRB.freezeRotation = false;
            basketballRB.AddTorque(200 * rotationSide * Time.deltaTime);
            possession = false;
            basketballRB.gravityScale = 1f;
            basketball.transform.position = passingManager.GetPlayerInControlGO().transform.position;
            shotY = passingManager.GetPlayerInControlGO().transform.position.y;
            basketball.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

            // check if defender can block
            if(Vector3.Distance(passingManager.GetPlayerInControlGO().transform.position, gameManager.GetDefenderOnBallGO().transform.position) <= defenderLayupBlockDistance)
            {
                layupForce = layupForce / 1.5f; // shot gets blocked
            }
            ball.Push(layupForce); // lay up ball
        }

        if(basketballRB.velocity.y >= 0f) // set sorting order of ball depending on direction of velocity
        {
            basketballSR.sortingOrder = 2;
        } else if(ballScript.highestPoint > 3.5f) {
            basketballSR.sortingOrder = 0;
        }

        if(passingManager.GetPlayerInControlGO().transform.position.x < 0f) // check which side player is on to determine ball rotation
        {
            rotationSide = -1;
        } else {
            rotationSide = 1;
        }
    }

    void FixedUpdate()
    {
        // calculate when ball should hit ground
        if(basketballRB.velocity.y < force.y / -2f && basketball.transform.position.y < 2.0f && !defense)
        {
            // make ball land
            GroundBall();

            // defense
            if(!shotMade)
            {
                gameManager.DefenseScreen();
            }
        }

        // make ball scale up and down during shot
        if(basketballRB.velocity.y > 0 && !onHoop && basketball.transform.localScale.x < 0.7f)
        {
            basketball.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
        }
        else if(basketballRB.velocity.y < 0 && !onHoop && basketball.transform.localScale.x > 0.5f)
        {
            basketball.transform.localScale += new Vector3(-0.01f, -0.01f, 0f);
        }
    }

    // functions for shooting
    void OnDragStart()
    {
        EnableBall();
        basketballRB.gravityScale = 0f;
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
        basketball.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
        shooting = true; // used to check for close out
        ballGrounded = false;
    }
    void OnDrag()
    {
        //basketball.transform.position = ballScript.GetPlayerInControlPos(passingManager.playerBeingControlled);
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * Mathf.Min(distance * pushForce, passingManager.GetPlayerInControlPM().strength);

        trajectory.UpdateDots(ball.pos, force);
    }
    void OnDragEnd()
    {
        // assign accuracy effect
        float accuracyEffect = Random.Range(0, 10 - passingManager.GetPlayerInControlPM().accuracy) / 5f;
        float accuracyEffectSign = 1f;
        if(Random.Range(1, 3) == 1)
        {
            accuracyEffectSign = -1f;
        }

        // check if shot is blocked
        shotInAir = true;
        if(Vector3.Distance(passingManager.GetPlayerInControlGO().transform.position, gameManager.GetDefenderOnBallGO().transform.position) <= defenderShotBlockDistance)
        {
            force = force / 1.5f;
            Debug.Log("Blocked");
        }
        
        // apply accuracy effect
        force.x += accuracyEffect * accuracyEffectSign;

        // apply stamina effect
        force.x *= passingManager.GetPlayerInControlPM().stamina;
        
        // shoot ball
        Debug.Log(force);
        ball.Push(force);
        trajectory.Hide();
        basketballRB.gravityScale = 1f;
        shotY = passingManager.GetPlayerInControlGO().transform.position.y;
        ballScript.highestPoint = ball.transform.position.y;
        shooting = false;
        possession = false;

        // if player is high enough on court, scale ball up
        if(shotY > 1)
        {
            basketball.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        }

        // rotate ball
        basketballRB.freezeRotation = false;
        basketballRB.AddTorque(200 * rotationSide * Time.deltaTime);

        // assign value of shot
        if(passingManager.GetPlayerInControlPM().threepointer)
        {
            shotValue = 3;
        } 
        else if(!passingManager.GetPlayerInControlPM().threepointer)
        {
            shotValue = 2;
        }
    }

    // give player possession of ball
    public void reboundBall()
    {
        possession = true;
        DisableBall();
        Debug.Log("rebound");
    }

    // enable/disable ball functions
    public void EnableBall()
    {
        basketball.SetActive(true);
    }

    public void DisableBall()
    {
        basketball.SetActive(false);
    }

    // function to make ball land
    void GroundBall()
    {
        basketballRB.velocity = new Vector2(0f, 0f);
        basketballRB.gravityScale = 0f;
        basketball.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        ballGrounded = true;
        shotInAir = false;
        basketballRB.freezeRotation = true;
    }

    bool playerWithBallLayupCheck()
    {
        if(passingManager.playerBeingControlled == playerOneMovement.playerNum)
        {
            return playerOneMovement.inLayupRange;
        } 
        else if(passingManager.playerBeingControlled == playerTwoMovement.playerNum) 
        {
            return playerTwoMovement.inLayupRange;
        }
        else if(passingManager.playerBeingControlled == playerThreeMovement.playerNum)
        {
            return playerThreeMovement.inLayupRange;
        }
        else if(passingManager.playerBeingControlled == playerFourMovement.playerNum)
        {
            return playerFourMovement.inLayupRange;
        }
        else
        {
            return playerFiveMovement.inLayupRange;
        } 
    }

    public float GetSpeedValue(int playerNum)
    {
        if(playerNum == 1)
        {
            return playerOneSpeed;
        }
        else if(playerNum == 2)
        {
            return playerTwoSpeed;
        }
        else if(playerNum == 3)
        {
            return playerThreeSpeed;
        }
        else if(playerNum == 4)
        {
            return playerFourSpeed;
        }
        else{
            return playerFiveSpeed;
        }
    }

    public float GetStrengthValue(int playerNum)
    {
        if(playerNum == 1)
        {
            return playerOneMaxForce;
        }
        else if(playerNum == 2)
        {
            return playerTwoMaxForce;
        }
        else if(playerNum == 3)
        {
            return playerThreeMaxForce;
        }
        else if(playerNum == 4)
        {
            return playerFourMaxForce;
        }
        else{
            return playerFiveMaxForce;
        }
    }

    public float GetAccuracyValue(int playerNum)
    {
        if(playerNum == 1)
        {
            return playerOneAccuracy;
        }
        else if(playerNum == 2)
        {
            return playerTwoAccuracy;
        }
        else if(playerNum == 3)
        {
            return playerThreeAccuracy;
        }
        else if(playerNum == 4)
        {
            return playerFourAccuracy;
        }
        else{
            return playerFiveAccuracy;
        }
    }

    public string GetName(int playerNum)
    {
        if(playerNum == 1)
        {
            return playerOneName;
        }
        else if(playerNum == 2)
        {
            return playerTwoName;
        }
        else if(playerNum == 3)
        {
            return playerThreeName;
        }
        else if(playerNum == 4)
        {
            return playerFourName;
        }
        else{
            return playerFiveName;
        }
    }

    void ChangeStamina(float effect)
    {
        if(passingManager.GetPlayerInControlPM().stamina > 0.5f)
        {
            passingManager.GetPlayerInControlPM().stamina -= effect;
            if(passingManager.GetPlayerInControlPM().stamina < 0.5f)
            {
                passingManager.GetPlayerInControlPM().stamina = 0.5f;
            }
        }
    }
}
