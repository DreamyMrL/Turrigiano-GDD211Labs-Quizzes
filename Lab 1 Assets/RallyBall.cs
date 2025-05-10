using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RallyBall : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // Initial movement direction
    public float speed = 10f; // Initial speed
    public float speedIncrease = .5f; // Speed boost when hitting the speed-up object
    public int score = 0; // Player's score
    public TMP_Text scoreText; // Reference to UI Text (optional)

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter2D for 2D
    {
        // Reverse direction
        moveDirection = -moveDirection;
        if (other.CompareTag("SpeedUp"))
        {

            // Increase speed
            speed += speedIncrease;

            // Increase score
            score++;
            Debug.Log("Score: " + score);

            // Update UI text if assigned
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }
}
