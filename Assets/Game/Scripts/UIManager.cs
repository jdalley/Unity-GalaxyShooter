using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives = null;
    public Image livesImage = null;

    public void UpdateLives(int livesLeft)
    {
        Debug.Log($"Player lives: {livesLeft}");

        livesImage.sprite = lives[livesLeft];
    }

    public void UpdateScore()
    {

    }
}
