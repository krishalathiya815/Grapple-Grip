using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    public static LockButton Instance;
    public bool button = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            button = true;
            collision.gameObject.SetActive(false);
            Debug.Log("Button Clicked..!");
        }

        if (collision.gameObject.CompareTag("Lock") && button)
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Lock Opened..!");
            gameObject.SetActive(true);
        }     
    }
}