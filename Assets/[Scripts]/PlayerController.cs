///Player Controller
///101273089 Michael Shular
///Last modified: 10/21/2021
///This set of code controls the actions of the player which are shooting
///bullets by getting a bullet from the bullet manager, moving the player
///with touch input and restricting the movement to inside the screen.
///
///Revision History:
///1. Changed the m_touchesEnded if statement and foreach loop inside of 
///_Move the function so that it affect movement instead of x-axis
///2. Changed the if statements inside of _CheckBounds now check the 
///boundary of the y-axis
///3. Changed a if statement inside of _Move so that newVelocity moves vertically  



using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;
    //Updated names for variables
    [Header("Boundary Check")]
    public float verticalBoundary;

    [Header("Player Speed")]
    public float verticalSpeed;
    public float maxSpeed;
    public float verticalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            //changed the if statment's .x to .y
            if (worldTouch.y > transform.position.y)
            {
                // direction is positive
                direction = 1.0f;
            }

            //changed the if statment's .x to .y
            if (worldTouch.y < transform.position.y)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }
        //changed the if statment's .x to .y
        if (m_touchesEnded.y != 0.0f)
        {
            //switched transform.position and Mathf.Lerp 
            //changed the vector2 .x to .y and .y to .x
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, verticalTValue));
        }
        else
        {
            //switched  0.0f and direction * verticalSpeed
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2( 0.0f, direction * verticalSpeed);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    private void _CheckBounds()
    {
        // check right bounds
        //changed the .x to .y
        if (transform.position.y >= verticalBoundary)
        {
            //switched  transform.position and verticalBoundary
            //changed the .x to .y
            transform.position = new Vector3(transform.position.x, verticalBoundary,  0.0f);
        }

        // check left bounds
        //changed the .x to .y
        if (transform.position.y <= -verticalBoundary)
        {
            //switched  transform.position and verticalBoundary
            //changed the .x to .y
            transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
        }

    }
}
