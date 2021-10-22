///BackgroundController
///101273089 Michael Shular
///Last modified: 10/19/2021
///This set of code controls how the background images move across the 
///screen and when will image reset its position to produce a scrolling 
///background.
///
///Revision History:
///1. Changed _CheckBounds function's if statment, _Move function's transforms and
///_Reset function's transform to preform the same thing just on x-axis instend of 
///y-axis 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //Updated names for variables
    public float horizontalSpeed;
    public float horizontalBoundary;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Reset()
    {
        //switched horizontalBoundary and 0.0f places
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }

    private void _Move()
    {
        //switched horizontalSpeed and 0.0f places
        transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        // if the background is lower than the bottom of the screen then reset
        //changed the .y to .x
        if (transform.position.x <= -horizontalBoundary)
        {
            _Reset();
        }
    }
}
