using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int lucky = 25;
    public GameObject[] rewardPrefabs;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Destroy(gameObject);
        }
    }
}
