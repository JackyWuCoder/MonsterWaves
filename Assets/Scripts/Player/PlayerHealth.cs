using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float chipSpeed = 2.0f;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] TextMeshProUGUI healthText;

    public GameObject gameOverUI;
    public bool isDead;

    [Header("Damage Overlay")]
    [SerializeField] private Image overlay; // Our damage overlay gameobject.
    [SerializeField] private float duration; // How long the image stays fully opague.
    [SerializeField] private float fadeSpeed; // How quickly the image will fade.
    private float durationTimer; // Timer to check against the duration.

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health <= 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= fadeSpeed * Time.deltaTime;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        if (health <= 0)
        {
            UpdateHealthUI();
            PlayerDead();
            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.GetComponent<ZombieHand>().GetDamage());
            }
        }
    }

    public void RestoreHealth(float healAmount) 
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    private void PlayerDead()
    {
        GetComponent<PlayerHealth>().enabled = false;
        GetComponent<PlayerInputManager>().enabled = false;
        GetComponent<PlayerInteract>().enabled = false;
        GetComponent<PlayerSprintBar>().enabled = false;
        GetComponent<PlayerUI>().enabled = false;
        // Dying Animation
        Camera.main.GetComponent<Animator>().enabled = true;
        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        int waveSurvived = GlobalReferences.Instance.waveNumber;
        if (waveSurvived - 1 > SaveLoadManager.Instance.LoadHighScore())
        {
            SaveLoadManager.Instance.SaveHighScore(waveSurvived - 1);
        }
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }
}
