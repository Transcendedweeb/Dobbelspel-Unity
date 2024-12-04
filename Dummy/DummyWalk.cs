using UnityEngine;

public class DummyWalk : MonoBehaviour
{
    // Public variables for speed and distance
    public float speed = 2f;
    public float distance = 5f;

    // Internal variables to manage movement
    private Vector3 startingPosition;
    private bool movingRight = true;

    void Start()
    {
        // Store the starting position
        startingPosition = transform.position;
    }

    void Update()
    {
        // Calculate the bounds of movement
        float rightLimit = startingPosition.x + distance;
        float leftLimit = startingPosition.x - distance;

        // Move the dummy
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
                movingRight = false; // Change direction at the right limit
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
                movingRight = true; // Change direction at the left limit
        }
    }
}
