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
    private UIManager _uIManager = null;
    private GameManager _gameManager = null;

    // Start is called before the first frame update
    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_uIManager is object)
        {
            _uIManager.UpdateLives(_lives);
        }
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
        float playerYLimit = _uIManager.screenYTopEdge - 2;
        float playerYBottom = _uIManager.screenYBottomEdge + 0.69f;

        // Limit player from going above half screen, or below bottom of the screen.
        if (transform.position.y > playerYLimit)
        {
            transform.position = new Vector3(transform.position.x, playerYLimit, 0);
        }
        else if (transform.position.y <= playerYBottom)
        {
            transform.position = new Vector3(transform.position.x, playerYBottom, 0);
        }

        // Wrap player left and right as they go off the screen
        if (transform.position.x >= _uIManager.screenXRightEdge)
        {
            transform.position = new Vector3(_uIManager.screenXLeftEdge, transform.position.y, 0);
        }
        else if (transform.position.x <= _uIManager.screenXLeftEdge)
        {
            transform.position = new Vector3(_uIManager.screenXRightEdge, transform.position.y, 0);
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
        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.GameOver();
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
