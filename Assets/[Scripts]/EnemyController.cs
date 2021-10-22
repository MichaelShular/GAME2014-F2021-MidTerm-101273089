///EnemyController
///101273089 Michael Shular
///Last modified: 10/19/2021
///This set of code controls how enemies will move and respone when hitting the  
///edge of the screen.
///
///Revision History:
///1. Changed _CheckBounds function's if statment and _Move function's transforms
///to preform the same thing just on y-axis instend of x-axis 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Updated names for variables
    public float verticalSpeed;
    public float verticalBoundary;
    public float direction;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        //switched verticalSpeed* direction * Time.deltaTime and the 0.0f in vector3.x
        transform.position += new Vector3( 0.0f, verticalSpeed * direction * Time.deltaTime, 0.0f);
    }

    private void _CheckBounds()
    {
        // check right boundary
        //changed the if statements .x to .y
        if (transform.position.y >= verticalBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        //changed the if statements .x to .y
        if (transform.position.y <= -verticalBoundary)
        {
            direction = 1.0f;
        }
    }
}
