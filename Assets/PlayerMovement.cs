using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isHorizonMove;
    private float moveX;
    private float moveY;
    private Vector2 moveDirection;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Processing Inputs
        ProcessInputs();

        // Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if(hDown){
            isHorizonMove = true;
        }
        else if(vDown){
            isHorizonMove = false;
        }
        else if(hUp || vUp){
            isHorizonMove = moveX != 0;
        }

        // Animation
        if(anim.GetInteger("hAxisRaw") != moveX){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)moveX);
        }
        else if(anim.GetInteger("vAxisRaw") != moveY){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)moveY);
        }
        else{
            anim.SetBool("isChange", false);
        }
    }

    void FixedUpdate()
    {
        // Physics Calculations
        Move();
    }
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        // Move
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
