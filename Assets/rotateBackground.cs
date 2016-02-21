using UnityEngine;
using System.Collections;

public class rotateBackground : MonoBehaviour 
{
    float yRotation = 10.0F;
    public float rotationSpeed = 10.0F;

    void Update()
    {
        //spin
        yRotation += rotationSpeed;
        transform.eulerAngles = new Vector3(10, 0, yRotation);
    }
}
