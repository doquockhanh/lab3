using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lv2winCondition : MonoBehaviour
{
    public int book = 10;
    private int bookCounter = 0;
    public Text bookText;
    void Start()
    {
        bookText.text = bookCounter + "/" + book;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "winPlace" && bookCounter >= book)
        {
            Debug.Log("win");
        }

        if (other.gameObject.tag == "book")
        {
            bookCounter++;
            bookText.text =  bookCounter + "/" + book;
        }
    }
}
