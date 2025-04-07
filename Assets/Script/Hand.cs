using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StopGrapple"))
        {
            playerMovement.Instance.StopGrapple();
        }

        if (collision.gameObject.CompareTag("Button"))
        {
            LockButton.Instance.button = true;
            collision.gameObject.SetActive(false);
            Debug.Log("Button Collected by hand..!");
        }

        if (collision.gameObject.CompareTag("Lock") && LockButton.Instance.button)
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Lock Opened by hand..!");
        }
    }
}