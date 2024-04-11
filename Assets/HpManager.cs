using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("Hp", 0);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth;
    }

    // Cập nhật máu hiện tại và gọi hàm UpdateHealthUI()
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        PlayerPrefs.SetInt("Hp", currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Đảm bảo máu không vượt quá giới hạn
        UpdateHealthUI();
    }

}
