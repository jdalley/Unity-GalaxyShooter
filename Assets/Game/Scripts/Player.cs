using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _laserPrefab = null;
    [SerializeField]
    private GameObject _tripleShotPrefab = null;
    [SerializeField]
    private GameObject _playerExplosionPrefab = null;
    [SerializeField]
    private GameObject _shieldGameObject = null;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool canTripleShot;
    [SerializeField]
    private bool shieldsActive;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Movement()
    {
        // Handle movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        var viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        // Limit player from going above half screen, or below bottom of the screen.
        if (transform.position.y > 2.5)
        {
            transform.position = new Vector3(transform.position.x, 2.5f, 0);
        }
        else if (transform.position.y <= -4.25)
        {
            transform.position = new Vector3(transform.position.x, -4.25f, 0);
        }

        // Wrap player left and right as they go off the screen
        if (transform.position.x >= 10.7)
        {
            transform.position = new Vector3(-10.7f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.7)
        {
            transform.position = new Vector3(10.7f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _nextFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _nextFire = Time.time + _fireRate;
        }
    }

    public void Damage(int damage)
    {
        // Negate damage with shield
        if (shieldsActive)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _lives -= damage;

        if (_lives < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void EnableShields()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedBoostPowerupOn()
    {
        _speed *= 2.0f;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= 2.0f;
    }

}
