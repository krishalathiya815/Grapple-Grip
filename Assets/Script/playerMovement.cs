using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement Instance;
    public float moveSpeed = 10f;
    public float grappleSpeed = 2f;
    public Transform hand;
    public LineRenderer lineRenderer;
    public LayerMask grappleLayer;
    public bool isGrappling = false;


    public Rigidbody2D rb;
    private Vector2 grapplePoint;

    private void Awake()
    {
        Instance = this;
    }

    Vector3 initialPosition;

    private void OnEnable()
    {
        if (initialPosition == Vector3.zero)
        {
            initialPosition = transform.position;

        }
        transform.position = initialPosition;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer.enabled = false;
        hand.gameObject.SetActive(false);
    } 

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isGrappling)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && !isGrappling)
        {
            StartGrapple();
        }

        if (isGrappling && moveInput != 0)
        {
            StopGrapple();
        }

        if (isGrappling)
        {
            rb.velocity = new Vector2(0, grappleSpeed);
            hand.position = grapplePoint; 
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    
    }

    public void StartGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 10f, grappleLayer);

        if (hit.collider != null)
        {
            isGrappling = true;
            grapplePoint = hit.point;

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            hand.gameObject.SetActive(true);
            hand.position = grapplePoint;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
        else
        {
            Debug.Log("No Grapple Surface Found..!");
        }
    }

    public void StopGrapple()
    {
        isGrappling = false;
        rb.gravityScale = 2;
        rb.velocity = new Vector2(rb.velocity.x, 0);

        hand?.gameObject.SetActive(false);

        lineRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("Player reached the Finishpoint..! Opening next level.");
            HomePanel.Instance.OnCollision();
            StopGrapple();
        }
    }
}