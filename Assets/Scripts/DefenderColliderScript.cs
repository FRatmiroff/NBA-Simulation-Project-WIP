using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderColliderScript : MonoBehaviour
{
    public bool touchingOffensivePlayer = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touchingOffensivePlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touchingOffensivePlayer = false;
        }
    }
}
