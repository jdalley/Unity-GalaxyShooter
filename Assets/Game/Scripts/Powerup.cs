using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId = 0; // 0 = triple shot, 1 = speed boost, 2 = shields

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;

        var player = other.GetComponent<Player>();
        if (player != null)
        {
            if (_powerupId == 0)
            {
                player.TripleShotPowerupOn();
            }
            else if (_powerupId == 1)
            {
                player.SpeedBoostPowerupOn();
            }
            else if (_powerupId == 2)
            {
                player.EnableShields();
            }
        }

        Destroy(gameObject);
    }
}
