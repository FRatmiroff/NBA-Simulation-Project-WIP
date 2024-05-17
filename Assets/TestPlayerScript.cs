using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    public TestBallPassScript tbps;
    public Rigidbody2D rb;
    Vector2 movement;

    public bool beingControlled;
    public int playerNum;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(beingControlled)
        rb.MovePosition(rb.position + movement.normalized * 3f * Time.fixedDeltaTime);
    }
    private void OnMouseDown()
    {
        if(!beingControlled)
        tbps.Pass(gameObject);
    }

    public void AssignPlayerNum(int playerNumParam)
    {
        playerNum = playerNumParam;
    }
}
