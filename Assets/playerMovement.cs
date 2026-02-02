using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    /*
     * MY IDEA FOR THIS GAME IS THAT THE GAME STARTS OUT WITH REALLY BUGGY AND BAD CODE FOR THE PLAYER MOVEMENT, AND AS THE PLAYER PROGRESSES THEY PICK UP MORE AND MORE FIXES TO THE CODE TO MAKE IT WORK
     * LIKE AN ACTUAL GAME, HOWEVER AT THE END THE PLAYER REALIZES HOW BORING AND UNORIGINAL THE MOVEMENT IS AND THEY HAVE THE OPTION TO REVERT BACK TO THE FUN BUGGY MOVEMENT, OR CONTINUE WITH THE
     * STANDARD BORING MOVEMENT, IF THEY PICK THE FUN WAY THEY HAVE A REALLY AWESOME LEVEL TO END OFF, AND IF THEY PICK THE BORING WAY THEY HAVE A BORING WALK TO THE FINISH LINE OR SOMETHING IDK.
     * I WANT TO DO THAT OR SOMETHING LIKE MIXING A TON OF DIFFERENT MOVEMENT STYLES THROUGHOUT THE GAME.
     */
    [Header("Player Attributes")]
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public DialogueBox dialogueBox;
    private Rigidbody2D rb;
    //private Animator animator;
    //private SpriteRenderer spriteRenderer;
    [Header("Movement Settings")]
    public bool isFacingRight = true;
    public float MoveSpeed = 8f;
    public float JumpForce = 9.81f;
    public Vector2 MovementDirection;
    private bool isGrounded = false;
    private int spamJumpCount = 0;
    public int currentMovementSystem = 0;
    private float spinStrength = 5f;
    private float spinDuration = 2f;
    private float spinTimer = 0f;
    private bool isSpinning = false;
    public Transform currentSpawnPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpawnPoint = this.transform;
    }

    private void Update()
    {
        Debug.Log(spinTimer);
        isGrounded = GroundCheck();
        //ProcessFling();
        rb.linearVelocity = new Vector2(MovementDirection.x * MoveSpeed, rb.linearVelocity.y);
    }

    public void ChangeMovementSystem(int systemNumber)
    {
        switch(systemNumber)
        {
            case 1:
                rb.freezeRotation = true;
                currentMovementSystem = 1;
                break;
            default:
                Debug.Log("Nothing has been changed");
                break;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        MovementDirection = ctx.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if(currentMovementSystem == 0 || currentMovementSystem == 1) // this movement system has flutter jump.
        {
            if (ctx.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                return;
            }
            if (ctx.performed)
            {
                Debug.Log("The right one is being called");
                if (!isGrounded) return;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
                return;
            }
        } 
        else if(currentMovementSystem == 2) // this movement system gets rid of the flutter jump
        {
            if (ctx.canceled)
            {
                if (spamJumpCount > 0) return; // these 2 lines fix the flutter jump bug
                spamJumpCount++; // these 2 lines fix the flutter jump bug
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                return;
            }
            if (ctx.performed)
            {
                Debug.Log("The wrong one is being called");
                if (!isGrounded) return;
                spamJumpCount = 0; // comment this out for the flutter jump thing
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
                return;
            }
        }
        
    }

    public void Fling(InputAction.CallbackContext ctx)
    {

        if(ctx.performed && !isSpinning)
        {
            spinTimer = spinDuration;
            Vector2 upwardForce = new Vector2(0.0f, 8.0f);
            rb.AddForce(upwardForce, ForceMode2D.Impulse);
        }
    }

    private void ProcessFling()
    {
        if(spinTimer > 0f)
        {
            spinTimer -= Time.deltaTime;
            rb.angularVelocity = spinStrength * 360f;
            isSpinning = true;
        } 
        else
        {
            isSpinning = false;
            spinTimer = 0f;
            rb.angularVelocity = 0f;
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(groundCheckPosition.position, 0.5f);
    }
}
