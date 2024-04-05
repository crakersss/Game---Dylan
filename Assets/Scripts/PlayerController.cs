using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   

    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate(){

        bool success = false;
        // if movement input is not 0, try to move  
        if(movementInput != Vector2.zero){
            success = TryMove(movementInput);

            if(!success) { 
                //if normal movement fails, try X only
                success = TryMove(new Vector2(movementInput.x, 0));
                
                if(!success) {
                // if x only fails, try Y only
                success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);
            
        } else {
            animator.SetBool("isMoving", false);
            }


        //set direction of movement on x axis
        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0){
            spriteRenderer.flipX = false;
        }

        //set downwards direction
        if(movementInput.y < 0){
            animator.SetBool("isMovingDown", true);
        } else if(movementInput.y >= 0){
            animator.SetBool("isMovingDown", false);
        }
        
        //set upwards direction
        if(movementInput.y > 0){
            animator.SetBool("isMovingUp", true);
        } else if (movementInput.y <= 0){
            animator.SetBool("isMovingUp", false);
        }
        
    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero){
            // checks for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // determines if a collision can occur
                castCollisions, // list of collisions to store the found collisons after cast is finished
                moveSpeed * Time.fixedDeltaTime +collisionOffset); // the amount to cast equal to the movement plus an offset
            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else{
                return false;
            }
        } else{
            // cant move if there is no place to move
            return false;
        }
    }


    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack(){
        if(spriteRenderer.flipX == true){
            swordAttack.AttackLeft();
        } else{
            swordAttack.AttackRight();
        }

    }
}
