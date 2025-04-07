using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRightButton : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button UpButton;

    private playerMovement player;
    private float moveDirection = 0f;

    private void Start()
    {
        player = playerMovement.Instance;
    }
    public void Update()
    {
        float moveInput = moveDirection;
        /*if (!player.isGrappling)
        {
            Debug.Log("moveinput" + moveInput);
            player.rb.velocity = new Vector2(moveInput * player.moveSpeed, player.rb.velocity.y);
        }

        if (player.isGrappling && moveInput != 0)
        {
            player.StopGrapple();
        }*/

        if (!playerMovement.Instance.isGrappling)
        {
            playerMovement.Instance.rb.velocity = new Vector2(moveInput * playerMovement.Instance.moveSpeed, playerMovement.Instance.rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && !playerMovement.Instance.isGrappling)
        {
            playerMovement.Instance.StartGrapple();
        }
    }

    //public void Move(float direction)
    //{
    //    moveDirection = direction;
    //}

    //public void StopMove()
    //{
    //    moveDirection = 0f;
    //}

    public void Up()
    {
        playerMovement.Instance.StartGrapple();
    }
}
