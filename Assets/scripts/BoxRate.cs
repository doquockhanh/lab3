using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRate : MonoBehaviour
{
    public GameObject[] rewardPrefabs;
    public int lucky = 50;
    private bool isTouched = false;
    private float timer = 0f;
    public float disappearDelay = 2f; // Thời gian trễ trước khi hộp biến mất
    void Start()
    {

    }

    private void Update()
    {
        if (isTouched)
        {
            timer += Time.deltaTime;
            if (timer >= disappearDelay)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouched = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouched = false;
        }
    }
}
