using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab = null;
    [SerializeField]
    private GameObject[] _powerups = null;
    [SerializeField]
    private GameManager _gameManager = null;

    // Start is called before the first frame update
    private void Start()
    {
        if (_gameManager.gameStarted)
        {
            StartSpawns();
        }
    }

    public void StartSpawns()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameStarted)
        {
            Instantiate(_enemyShipPrefab,
                new Vector3(Random.Range(-8.0f, 8.0f), 6, 0),
                Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        int numPowerups = _powerups.Length;
        while (_gameManager.gameStarted)
        {
            Instantiate(_powerups[Random.Range(0, numPowerups)],
                new Vector3(Random.Range(-8.0f, 8.0f), 6, 0),
                Quaternion.identity);

            yield return new WaitForSeconds(6.0f);
        }
    }
}
