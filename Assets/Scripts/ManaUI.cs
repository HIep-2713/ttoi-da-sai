using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [Header("UI References")]
    public Slider manaSlider;          // Slider cho thanh mana
    public Image backgroundImage;      // Hình n?n c?a thanh mana
    public Sprite normalBg;            // Background lúc ch?a ??y
    public Sprite fullBg;              // Background khi ??y

    [Header("Mana Settings")]
    public int maxMana = 100;          // Mana t?i ?a
    private int currentMana = 0;       // Mana hi?n t?i

    void Start()
    {
        if (manaSlider != null)
        {
            manaSlider.maxValue = maxMana;
            manaSlider.value = 0;
        }
        UpdateUI();
    }

    public void GainMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        UpdateUI();
    }

    public bool IsFull()
    {
        return currentMana >= maxMana;
    }

    public void ResetMana()
    {
        currentMana = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (manaSlider != null)
            manaSlider.value = currentMana;

        if (backgroundImage != null)
        {
            if (IsFull())
                backgroundImage.sprite = fullBg;
            else
                backgroundImage.sprite = normalBg;
        }
    }

    // ? Hàm này ?? Player.cs g?i ???c
    public int GetCurrentMana()
    {
        return currentMana;
    }

    // (tu? ch?n) Hàm set mana th? công
    public void SetMana(int value)
    {
        currentMana = Mathf.Clamp(value, 0, maxMana);
        UpdateUI();
    }
}
