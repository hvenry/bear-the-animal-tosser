using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Move speed
    public float moveSpeed;
    
    // Reference to rigidbody
    public Rigidbody2D rb;
    public GameObject prefab;

    public PickUp pickUp;
    private Vector2 moveDirection;

    // animator component
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        pickUp = gameObject.GetComponent<PickUp>();
        pickUp.Direction = new Vector2(0, -1);


    }

    // fixed update is called a set amount of times per update loop (unlike frames)
    // this is where we want to do all of our physics calculations
    void FixedUpdate() {
        Move();
    }


    void ProcessInputs(){
        // we want to check at inputs from unity editor
        // GetAxisRaw gives us a 0 or 1
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // animator component
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", new Vector2(moveX, moveY).sqrMagnitude);


        // create vector with moveX, moveY
        // we use .normalized so our vectors do not stack in non-cardinal directions
        // this caps our vector at 1
        moveDirection = new Vector2(moveX, moveY).normalized;
        
        //spawns new animal prefab at player
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     Instantiate(prefab, rb.position, Quaternion.identity);
        // }

        

    }

    void Move() {
        // turn float values into vector
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
