using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab = null;
    [SerializeField]
    private GameObject[] _powerups = null;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (true)
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
        while (true)
        {
            Instantiate(_powerups[Random.Range(0, numPowerups)],
                new Vector3(Random.Range(-8.0f, 8.0f), 6, 0),
                Quaternion.identity);

            yield return new WaitForSeconds(6.0f);
        }
    }

}
