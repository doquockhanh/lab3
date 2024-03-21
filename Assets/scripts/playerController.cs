using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed = 5f;
    public float timeRemaining = 60;
    public Text countdownText;
    private Rigidbody2D rb;
    public Transform[] gunPivots
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CountDown());
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    IEnumerator CountDown()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            countdownText.text = "Time: " + timeRemaining.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hInput, vInput, 0f).normalized;
        transform.Translate(movement * speed * Time.deltaTime);

        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput < 0)
        {
            Flip(true);
        }
        else if (moveInput > 0)
        {
            Flip(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            transform.position = new Vector2(-9, 0);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "winPlace")
        {
            Debug.Log("win");
        }
    }

    void Flip(bool flip)
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.flipX = flip;
        }
    }
}
