using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour
{
    float yRotation = 5.0F;
    public float rotationSpeed = 1.0F;
    void Update()
    {
        //yRotation += yRotation;
        yRotation += rotationSpeed;
        transform.eulerAngles = new Vector3(10, 0, yRotation);
    }
}
