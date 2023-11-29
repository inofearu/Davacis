using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    [UsedImplicitly]
    private void Start()
    {
        slider = healthBar.GetComponent<Slider>();
    }
    public void HandlePlayerDeath()
    {
        deathScreen.GetComponent<Image>().enabled = true;
    }
    public void UpdateXPDisplay(int currentLevel, int currentXP, int xpToNextLevel)
    {
        levelText.text = $"Level: {currentLevel}   XP: {currentXP} / {xpToNextLevel}";
    }
    public void UpdateHPDisplay(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }
}
