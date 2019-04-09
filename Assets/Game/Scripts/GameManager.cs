using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;
    public Player player = null;
    private UIManager _uIManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
    
    public void StartGame()
    {
        gameStarted = true;
        _uIManager.HideTitleScreen();

        // Spawn player
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        // Player died, clean up remaining game objects.
        ClearGameObjects();
        gameStarted = false;

        _uIManager.ShowTitleScreen();
    }

    public void ClearGameObjects()
    {
        var powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (var powerup in powerups)
        {
            Destroy(powerup);
        }

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
