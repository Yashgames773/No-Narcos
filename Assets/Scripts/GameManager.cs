using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ScoreText;

    private SlicingBlade Blades;
    private Spawner Spawners;

    private int Score;

    public GameObject GameOverPanel;
    


    private void Awake()
    {
        Blades = FindObjectOfType<SlicingBlade>();
        Spawners = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
    }
    public void IncreasingScore(int amount)
    {
        Score += amount;
        ScoreText.text = Score.ToString();
    }

    public void Explode()
    {
        Blades.enabled = false;
        Spawners.enabled = false;
        GameOverPanel.SetActive(true);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
        GameOverPanel.SetActive(false);
        

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
