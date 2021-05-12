using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public AudioSource audio;
    public AudioClip[] footsteps;

    public float speed;
    public float gravity;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public float crouchHeight;
    public float crouchWidth;

    //Handles movement and gravity
    void Update()
    {
        //Ground checker
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //If the player is grounded and the Y pos is less than 0, reset it
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Get input axis from keyboard
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;


        controller.Move(move * speed * Time.deltaTime);
        //Plays sound when in any direction and while grounded
        if ((moveX > 0 || moveX < 0) || (moveZ > 0 || moveZ < 0) && isGrounded)
        {
            if (!audio.isPlaying)
            {
                audio.clip = footsteps[Random.Range(1, 4)];
                audio.Play();
            }
        }
        //If the input 'Jump' is pressed and is grounded, jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Crouching
        if(Input.GetKeyDown(KeyCode.C))
        {
            //Sets the player's height down to crouch height and width
            GetComponent<CapsuleCollider>().height = crouchHeight;
            GetComponent<CapsuleCollider>().radius = crouchWidth;
            transform.localScale = new Vector3(0.8f, crouchHeight / 2, 0.8f);
            controller.height = 0.3f;
        //When the player stands up
        } else if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            controller.height = 1.65f;
        }

        //Handles gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
