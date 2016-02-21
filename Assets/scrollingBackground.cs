using UnityEngine;
using System.Collections;

public class scrollingBackground : MonoBehaviour
{

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // print("here");
        //float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        startPosition.x += 0.6f;
        transform.position = startPosition;
        if (startPosition.x > 47.3f)
        {
            startPosition.x = -47.3f;
        }
    }
}
