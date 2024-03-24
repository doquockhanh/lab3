using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float timeRemaining = 60;
    public Text countdownText;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Transform respawnPlace;
    public GameObject gun;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CountDown());
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
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f).normalized * moveSpeed * Time.fixedDeltaTime;

        // Move the player
        rb.MovePosition(transform.position + movement);
        // Flip the player if necessary
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        Transform viewpoint = transform.Find("viewpoint");
        if (viewpoint != null)
        {
            viewpoint.Rotate(0f, 180f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            transform.position = respawnPlace.position;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "update")
        {
            Gun g = gun.GetComponent<Gun>();
            g.updateFireRate += 0.1f;
        }
    }
}
