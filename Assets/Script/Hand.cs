using System.Collections;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Grappling Hook")]
    public GameObject ropeSprite;
    public DistanceJoint2D grappleJoint;
    public Transform grapplePoint;
    public LayerMask grappleLayer;

    private bool isGrappling = false;
    private bool ropeExtended = false;
    private Vector2 grappleTarget;
    public float grappleSpeed = 3f;
    public float grappleMaxSpeed = 6f;
    public float grappleAcceleration = 8f;
    public float grappleHoldTime = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grappleJoint.enabled = false;
        grappleJoint.autoConfigureDistance = false;
        ropeSprite.SetActive(false);
    }

    void Update()
    {
        Move();
        Jump();
        HandleGrapple();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (isGrappling && Mathf.Abs(moveInput) > 0.1f)
        {
            // If player moves horizontally, cancel grapple
            ReleaseGrapple();
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else if (!isGrappling)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void HandleGrapple()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!ropeExtended)
            {
                StartCoroutine(ExtendRopeThenGrapple());
            }
        }
    }

    private IEnumerator ExtendRopeThenGrapple()
    {
        ropeExtended = true;
        ropeSprite.SetActive(true);
        ropeSprite.transform.localScale = new Vector3(0.1f, 1, 1);

        float extendTime = 0.2f;
        float elapsed = 0f;

        while (elapsed < extendTime)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(0.1f, 1f, elapsed / extendTime);
            ropeSprite.transform.localScale = new Vector3(scale, 1, 1);
            yield return null;
        }

        Vector2 direction = Vector2.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, grappleLayer);

        if (hit.collider != null)
        {
            grappleTarget = hit.point;
            grappleJoint.connectedAnchor = hit.point;
            grappleJoint.distance = 0.5f;
            grappleJoint.enabled = true;
            isGrappling = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
            StartCoroutine(SmoothGrappleMove());
        }
        else
        {
            ropeExtended = false;
            ropeSprite.SetActive(false);
        }
    }

    private IEnumerator SmoothGrappleMove()
    {
        float currentSpeed = grappleSpeed;

        while (Vector2.Distance(transform.position, grappleTarget) > 0.1f && isGrappling)
        {
            currentSpeed = Mathf.Min(currentSpeed + grappleAcceleration * Time.deltaTime, grappleMaxSpeed);
            transform.position = Vector2.MoveTowards(transform.position, grappleTarget, currentSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = grappleTarget;
        yield return new WaitForSeconds(grappleHoldTime);
    }

    private void ReleaseGrapple()
    {
        isGrappling = false;
        ropeExtended = false;
        grappleJoint.enabled = false;
        rb.gravityScale = 1f;
        rb.isKinematic = false;
        ropeSprite.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isGrappling = false;
        }
    }
}
