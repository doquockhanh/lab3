using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lv3wincondition : MonoBehaviour
{
    public int book = 10;
    private int bookCounter = 0;
    public Text bookText;
    private new AudioManager audio;
    void Start()
    {
        bookText.text = bookCounter + "/" + book;
        audio = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "winPlace" && bookCounter >= book)
        {
            audio.PlaySFX(audio.win);
            Debug.Log("win");
        }

        if (other.gameObject.tag == "book")
        {
            bookCounter++;
            bookText.text = bookCounter + "/" + book;
        }
    }
}
