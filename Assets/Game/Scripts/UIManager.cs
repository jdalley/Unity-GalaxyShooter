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
    public float screenYTopEdge = 0.0f;
    public float screenYBottomEdge = 0.0f;
    public float screenXLeftEdge = 0.0f;
    public float screenXRightEdge = 0.0f;

    private void Start()
    {
        SetScreenEdgesInUnits();
    }

    private void Update()
    {
        if (screenXRightEdge != (screenYTopEdge * Screen.width / Screen.height))
        {
            // Screen changed, re-set edges:
            SetScreenEdgesInUnits();
        }
    }

    public void SetScreenEdgesInUnits()
    {
        screenYTopEdge = Camera.main.orthographicSize;
        screenXRightEdge = screenYTopEdge * Screen.width / Screen.height;
        screenYBottomEdge = -screenYTopEdge;
        screenXLeftEdge = -screenXRightEdge;
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
