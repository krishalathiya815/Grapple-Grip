using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public static HomePanel Instance;
    public List<GameObject> Levels = new List<GameObject>();
    public int ActiveLevelIndex;
    public int currentLevelIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void TurnOnLevel(int Level)
    {
        foreach (GameObject level in Levels)
        {
            level.SetActive(false);
        }
        Levels[Level].SetActive(true);
        ActiveLevelIndex = Level;
        gameObject.SetActive(false);
        currentLevelIndex = Level;
    }

    public void OnCollision()
    {
        Debug.Log("Collision detected..! Current Level : " + currentLevelIndex);

        if (currentLevelIndex < Levels.Count - 1)
        {
            currentLevelIndex++;
        }
        else
        {
            Debug.Log("All levels completed. Restarting from Level 0.");
            currentLevelIndex = 0;
        }

        Debug.Log("Loading Next Level : " + currentLevelIndex);
        TurnOnLevel(currentLevelIndex);
    }
}