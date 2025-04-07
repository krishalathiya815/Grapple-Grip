using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider slider;
    public float speed = 0.5f;

    void Update()
    {
        if (slider.value < slider.maxValue)
        {
            slider.value += speed * Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}