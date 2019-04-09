using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private int _scorePoints = 10;
    [SerializeField]
    private GameObject _enemyExplosionPrefab = null;
    private UIManager _uIManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // When off the screen at the bottom, respawn at a random X point between bounds
        if (transform.position.y <= -6.0)
        {
            var xRnd = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(xRnd, 6.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            if (player is object)
            {
                // Damage player
                player.Damage(1);
                Destroyed();
            }
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_uIManager is object)
            {
                _uIManager.UpdateScore(_scorePoints);
            }

            Destroyed();
        }
    }

    private void Destroyed()
    {
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
