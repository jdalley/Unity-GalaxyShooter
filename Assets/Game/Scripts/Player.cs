using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Handle movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        // Limit player from going above half screen, or below bottom of the screen.
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
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

}
