using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyHit : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to attack
        {
            animator.SetBool("isSlashing", true);
            Invoke("ResetSlash", 0.1f); // Adjust time to match animation length
        }
    }

    void ResetSlash()
    {
        animator.SetBool("isSlashing", false);
    }

}
