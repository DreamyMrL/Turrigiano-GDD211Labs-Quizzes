using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RallyDeath : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter2D for 2D
    {
        if (other.CompareTag("Enemy") && !isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true); // Play death animation
            Destroy(other.gameObject); // Destroy enemy
            StartCoroutine(HandleDeath()); // Wait for animation to complete
        }
    }

    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2.5f); // Adjust to match animation length
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        Debug.Log("Player Respawned!");
        animator.SetBool("isDead", false); // Reset animation state (if needed)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }
}

