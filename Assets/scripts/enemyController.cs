using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float dx = 0f;
        if (Random.value < 0.5f)
            dx = Random.Range(-1.0f, -0.5f);
        else
            dx = Random.Range(0.5f, 1.0f);
        float dy = 0f;
        if (Random.value < 0.5f)
            dy = Random.Range(-1.0f, -0.5f);
        else
            dy = Random.Range(0.5f, 1.0f);
        Vector2 initialDirection = new Vector2(dx, dy).normalized;
        rb.velocity = initialDirection * speed;
    }


    void FixedUpdate()
    {
        // Move the object continuously
        Vector2 movement = rb.velocity.normalized * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("map") || collision.gameObject.CompareTag("enemy"))
        {
            Vector2 incomingVelocity = rb.velocity;
            Vector2 collisionNormal = collision.contacts[0].normal;
            Vector2 reflectedVelocity = Vector2.Reflect(incomingVelocity, collisionNormal);

            rb.velocity = reflectedVelocity;
        }
    }
}
