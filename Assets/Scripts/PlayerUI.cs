using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject healthBar;
    [SerializeField] float xpScale = 1.2f;
    private TMP_Text levelField;
    private Slider slider;
    private int Damage;
    private int maxHealth;
    private float health;
    private float HealthBarSeek = 1.0f;
    private int CurrentXP = 0;
    private int CurrentLevel = 0;
    private int XPToNextLevel = 5;

    // Start is called before the first frame update
    [UsedImplicitly]
    private void Start()
    {
        slider = healthBar.GetComponent<Slider>();
        levelField = healthBar.GetComponent<TMP_Text>();
        UpdateXPDisplay();
    }
    public void HandlePlayerDeath()
    {
        deathScreen.GetComponent<Image>().enabled = true;
    }
    // Update is called once per frame
    [UsedImplicitly]
    private void OnHit(Collision other)
    {
        HealthBarSeek = health / maxHealth;
        slider.value = HealthBarSeek;
    }
    public void AddXP(int AddedXP)
    {
        CurrentXP += AddedXP;
        UpdateXPDisplay();
        while(CurrentXP > XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            XPToNextLevel = Mathf.RoundToInt(XPToNextLevel * xpScale);
            CurrentLevel ++;
            UpdateXPDisplay();
        }
    }
    private void UpdateXPDisplay()
    {
        levelText.text = $"Level: {CurrentLevel}   XP: {CurrentXP} / {XPToNextLevel}";
    }
}
