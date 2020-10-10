using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 10;
    [SerializeField]
    private float jumpForce = 350;
    private bool jump = false;


    private float Horizontal = 0;
    private float Vertical = 0;

    private Rigidbody2D playerRB;
    private Animator playerAnimator;

    public void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        InputHandler();
        PlayerMove(Horizontal);
        Jump();
        Double();
    }



    private void InputHandler()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Vertical > 0.5f)
        {
            jump = true;
        }

    }

    private void PlayerMove(float Horizontal)
    {
        playerRB.velocity = new Vector2(Mathf.Abs(Horizontal * movementSpeed), playerRB.velocity.y);
        playerAnimator.SetFloat("Speed", playerRB.velocity.x);
    }

    private void Jump()
    {
        if (jump && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Jump") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Double"))
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void Double()
    {
        if (jump && this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetTrigger("Double");
        }
    }

}
