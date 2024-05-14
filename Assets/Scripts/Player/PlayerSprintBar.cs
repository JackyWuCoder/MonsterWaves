using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprintBar : MonoBehaviour
{
    private float sprintTimer;
    private float lerpTimer;
    [Header("Health Bar")]
    [SerializeField] private float maxSprint = 10.0f; // in seconds
    [SerializeField] private float chipSpeed = 2.0f;
    [SerializeField] private Image frontSprintBar;
    [SerializeField] private Image backSprintBar;
    [SerializeField] TextMeshProUGUI sprintText;

    // Start is called before the first frame update
    void Start()
    {
        sprintTimer = maxSprint;
    }

    // Update is called once per frame
    void Update()
    {
        sprintTimer = Mathf.Clamp(sprintTimer, 0, maxSprint);
        UpdateSprintUI();
    }

    public float GetSprintTimer()
    {
        return sprintTimer;
    }

    public void UpdateSprintUI()
    {
        // Debug.Log(health);
        float fillF = frontSprintBar.fillAmount;
        float fillB = backSprintBar.fillAmount;
        float hFraction = sprintTimer / maxSprint;
        if (fillB > hFraction)
        {
            frontSprintBar.fillAmount = hFraction;
            backSprintBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backSprintBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backSprintBar.color = Color.green;
            backSprintBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontSprintBar.fillAmount = Mathf.Lerp(fillF, backSprintBar.fillAmount, percentComplete);
        }
        sprintText.text = $"{sprintTimer}";
    }

    public void DecreaseSprintTimer()
    {
        sprintTimer -= Time.deltaTime;
        lerpTimer = 0;
    }

    public void IncreaseSprintTimer()
    {
        sprintTimer += Time.deltaTime;
        lerpTimer = 0;
    }

}
