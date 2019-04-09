using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen = null;
    public Player player = null;
    public Sprite[] lives = null;
    public Image livesImage = null;
    public Text scoreText = null;
    public int score = 0;

    private void Update()
    {
        
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: ";
    }
 
    public void UpdateLives(int livesLeft)
    {
        livesImage.sprite = lives[livesLeft];
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }
}
