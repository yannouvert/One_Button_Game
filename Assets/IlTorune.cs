using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlTorune : MonoBehaviour
{
    public float speed = 30.0f; // Speed of the orbit in degrees per second

    private Vector3 initialOffset;

    void Start()
    {
        // Store the initial offset from the parent
        initialOffset = transform.position - transform.parent.position;
    }

    void Update()
    {
        // Ensure there is a parent object
        Transform parentTransform = transform.parent;
        if (parentTransform)
        {
            // Calculate new position around the parent object
            transform.position = parentTransform.position + Quaternion.Euler(0, 0, -speed * Time.deltaTime) * initialOffset;

            // Update the initial offset for the next frame
            initialOffset = transform.position - parentTransform.position;
        }
    }
}


