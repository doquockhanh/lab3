using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int lucky = 25;
    public GameObject[] rewardPrefabs;
    private Animator animator;
    private CapsuleCollider2D enemyCollider;
    private new AudioManager audio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("map") || collision.gameObject.CompareTag("enemy") || collision.gameObject.layer == LayerMask.NameToLayer("box"))
        {
            Vector2 incomingVelocity = rb.velocity;
            Vector2 collisionNormal = collision.contacts[0].normal;
            Vector2 reflectedVelocity = Vector2.Reflect(incomingVelocity, collisionNormal);

            rb.velocity = reflectedVelocity;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            int unlucky = Random.Range(0, 100);
            if (lucky > unlucky)
            {
                int randomIndex = Random.Range(0, rewardPrefabs.Length);
                GameObject randomReward = rewardPrefabs[randomIndex];
                if (randomReward != null)
                {
                    Instantiate(randomReward, transform.position, Quaternion.identity);
                }
            }
            audio.PlaySFX(audio.hit);
            animator.SetTrigger("Hit");
            animator.SetBool("Dead", true);
            rb.simulated = false;
            enemyCollider.enabled = false;
            goRandom goRandomComponent = gameObject.GetComponent<goRandom>();
            if (goRandomComponent != null)
            {
                goRandomComponent.speed = 0f;
            }
            goFollow goFlowComponent = gameObject.GetComponent<goFollow>();
            if (goFlowComponent != null)
            {
                goFlowComponent.moveSpeed = 0f;
            }
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait until the current death animation clip length
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}

