using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPathController : MonoBehaviour
{
    public int currentPathIndex = 0;
    public Vector2[] setPaths;
    public float speed = 4;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, setPaths[currentPathIndex], speed * Time.deltaTime);
        if (transform.position.x == setPaths[currentPathIndex].x && transform.position.y == setPaths[currentPathIndex].y)
        {
            currentPathIndex++;
            //go back to the first location
            if (currentPathIndex >= setPaths.Length)
            {
                currentPathIndex = 0;
            }
        }
    }
}