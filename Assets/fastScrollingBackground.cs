using UnityEngine;
using System.Collections;

public class fastScrollingBackground : MonoBehaviour
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
        startPosition.y += 0.5f;
        transform.position = startPosition;
		if (startPosition.y > 46.4f)
        {
			startPosition.y = -46.4f;
        }
    }
}
