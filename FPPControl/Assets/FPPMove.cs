using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPMove : MonoBehaviour {

    private Rigidbody playerRB;
    private bool grounded = false;
    private float pitch = 0f;
    private float minPitch = -30f;
    private float maxPitch = 60f;

    public float forceSpeed = 0f;
    public float jumpSpeed = 0f;
    public float mouseSpeed = 0f;
    public Transform cameraTF;

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float HorizentalMove = Input.GetAxisRaw("Horizontal");
        float VerticalMove = Input.GetAxisRaw("Vertical");
        float jumpInput = Input.GetAxisRaw("Jump");
        float mouseInputHorizental = Input.GetAxis("Mouse X");
        float mouseInputVertical = Input.GetAxis("Mouse Y");

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

        transform.Rotate(Vector3.up, mouseInputHorizental * mouseSpeed);

        Vector3 cameraRotat = cameraTF.eulerAngles;
        pitch -= mouseInputVertical * mouseSpeed;

        if (pitch < maxPitch +1 && pitch > minPitch - 1)
        {
            pitch = pitch;
        }
        else if(pitch > maxPitch)
        {
            pitch = maxPitch;
        }
        else if (pitch < minPitch)
        {
            pitch = minPitch;
        }
        else
        {
            pitch = 0;
        }

        cameraRotat.x = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTF.eulerAngles = cameraRotat;
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
