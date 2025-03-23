using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public GameObject gameOverTextObject; 
    public SubmarineManager submarineManager; 
    public GameObject restartButton; 

    void Start()
    {
        gameOverTextObject.SetActive(false);
        restartButton.SetActive(false); 
    }

    public void GameOver()
    {
        gameOverTextObject.SetActive(true); 
        restartButton.SetActive(true); 
        submarineManager.StopAllObstaclePlacers();
        Time.timeScale = 0; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}