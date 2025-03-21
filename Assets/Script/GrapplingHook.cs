using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer hand;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        hand.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
            origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
            direction: Vector2.zero,
            distance: Mathf.Infinity,
            layerMask: grappleLayer);

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                hand.SetPosition(0, grapplePoint);
                hand.SetPosition(1, transform.position);
                hand.enabled = true;
            }
        }
        if (Input.GetKeyUp(0))
        { 
            joint.enabled = false;
            hand.enabled = false;
        }

        if (hand.enabled == true)
        {
            hand.SetPosition(1,transform.position);
        }
    }
}
