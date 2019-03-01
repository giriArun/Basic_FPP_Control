using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPMove : MonoBehaviour {

    private Rigidbody playerRB;
    private bool grounded = false;

    public float forceSpeed = 0f;
    public float jumpSpeed = 0f;

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float HorizentalMove = Input.GetAxisRaw("Horizontal");
        float VerticalMove = Input.GetAxisRaw("Vertical");
        float jumpInput = Input.GetAxisRaw("Jump");

        playerRB.AddForce(transform.forward * VerticalMove * forceSpeed);
        playerRB.AddForce(transform.right * HorizentalMove * forceSpeed);
        if (grounded)
        {
            playerRB.AddForce(new Vector2(playerRB.velocity.x, jumpSpeed * jumpInput));
        }
        if (grounded && HorizentalMove == 0 && VerticalMove == 0)
        {
            playerRB.AddForce(playerRB.velocity * -forceSpeed / 2);

            if(playerRB.velocity.sqrMagnitude < 0.1)
            {
                playerRB.Sleep();
            }
        }
	}

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
        }
    }
}
