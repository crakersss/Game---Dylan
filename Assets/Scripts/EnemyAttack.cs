using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider2D axeCollider;
    public float damage = 5f;
    public Animator animatorController;
    public float specificFrameTime = 3f; // Adjust this value to the desired time in the animation

    private Coroutine attackCoroutine;

    private void Start()
    {
        // Ensure that the attack collider is initially disabled
        axeCollider.enabled = false;
        animatorController = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        // Start the attack coroutine
        attackCoroutine = StartCoroutine(ActivateAttack());
    }

    public void StopAttack()
    {
        // Stop the attack coroutine if it's running
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        // Ensure that the attack collider is disabled
        axeCollider.enabled = false;
    }

    private IEnumerator ActivateAttack()
    {
        // Enable the attack collider
        axeCollider.enabled = true;

        // Wait for one frame to ensure the collider is enabled before continuing
        yield return null;

        // Wait until the specific animation frame where the attack begins
        while (!IsAtSpecificFrame())
        {
            yield return null;
        }

        // Disable the attack collider after the specific animation frame where the attack ends
        while (!IsAtSpecificEndFrame())
        {
            yield return null;
        }

        // Disable the attack collider
        axeCollider.enabled = false;
    }

    public bool IsAtSpecificFrame()
    {
        // Check if the current normalized time of the animation is greater than or equal to the specific frame time
        return animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime >= specificFrameTime;
    }

    public bool IsAtSpecificEndFrame()
    {
        // Check if the current normalized time of the animation is greater than or equal to 1 (end of animation)
        return animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
{
    if (collider.CompareTag("Player"))
    {
        // Check if the enemy is at a specific frame of the attack animation
        if (IsAtSpecificFrame())
        {
            // Deal damage to player
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                // Optionally trigger other effects or animations
            }
        }
    }
    else{
        return;
    }
}

}
