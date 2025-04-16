using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public static RestartButton Instance;
    public Transform[] objects;
    public Vector3[] initialPositions;
    public Button retryButton;


    private void Awake()
    {
        Instance = this;
        initialPositions = new Vector3[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            initialPositions[i] = objects[i].position;
        }
    }
    void Start()
    { 
        retryButton.onClick.AddListener(Retry);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void OnEnable()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].gameObject.SetActive(true);
                objects[i].position = initialPositions[i];
            }
        }
    }

    public void Retry()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].gameObject.SetActive(true);
                objects[i].position = initialPositions[i];
            }
        }
    }
}