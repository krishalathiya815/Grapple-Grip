using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Button backButton;

    public void Start()
    {
        backButton.onClick.AddListener(Back);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Back Button Pressed");
            Back();
        }
    }
    public void Back()
    {
        gameObject.SetActive(true);
        foreach (GameObject level in HomePanel.Instance.Levels)
        {
            level.SetActive(false);
        }
    }
}