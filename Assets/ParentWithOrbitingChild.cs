using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentWithOrbitingChild : MonoBehaviour
{
    public GameObject childObject; // Reference to the child GameObject
    public float orbitSpeed = 30.0f; // Speed of the orbit in degrees per second
    public float moveSpeed = 1.0f; // Speed of the parent object
    public float moveDuration = 0.5f; // Time to complete the movement
    public float cooldownTime = 1.0f; // Cooldown time before next move

    private Vector3 initialOffset;
    private float lastMoveTime;

    void Start()
    {
        // Store the initial offset from the child
        initialOffset = childObject.transform.position - transform.position;
        lastMoveTime = -cooldownTime; // Initialize so that you can move immediately
    }

    void Update()
    {
        if (childObject)
        {
            // Orbit child object around parent (Clockwise)
            childObject.transform.position = transform.position + Quaternion.Euler(0, 0, -orbitSpeed * Time.deltaTime) * initialOffset;

            // Update the initial offset for the next frame
            initialOffset = childObject.transform.position - transform.position;

            // Move parent object in the direction of the child object when space is pressed
            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastMoveTime >= cooldownTime)
            {
                lastMoveTime = Time.time; // Update the last move time
                StartCoroutine(SmoothMove());
            }
        }
    }

    IEnumerator SmoothMove()
    {
        Vector3 startPosition = transform.position;
        Vector3 directionToChild = (childObject.transform.position - transform.position).normalized;
        Vector3 endPosition = startPosition + directionToChild * moveSpeed;
        float startTime = Time.time;

        while (Time.time < startTime + moveDuration)
        {
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        transform.position = endPosition; // Ensure it reaches the destination
    }
}
