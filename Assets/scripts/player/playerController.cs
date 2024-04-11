using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Text countdownText;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Transform respawnPlace;
    public GameObject gun;
    private Animator animator;
    private new AudioManager audio;
    private int hp;
    public GameObject gameOverPanel;
    public HpManager healthManager;
    void Start()
    {
        hp = PlayerPrefs.GetInt("Hp", 0);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0f || moveVertical != 0f)
        {
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f).normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movement);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }


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
            hp--;
            healthManager.UpdateHealth(-1);
            if(hp <= 0) {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "update")
        {
            audio.PlaySFX(audio.take);
            Gun g = gun.GetComponent<Gun>();
            g.updateFireRate += 0.1f;
        }
    }
}
