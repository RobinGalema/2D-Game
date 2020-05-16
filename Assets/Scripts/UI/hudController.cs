using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudController : MonoBehaviour
{
    public playerStats stats;
    [Header("Lumina")]
    public GameObject luminaBar;
    [Header("Health")]
    public Slider healthBarSlider;

    private Image[] luminaIcons;

    // Start is called before the first frame update
    void Start()
    {
        luminaIcons = luminaBar.GetComponentsInChildren<Image>();
        setupLuminaBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.checkHealthUpdate())
        {
            updateHealthBar();
        }
    }

    private void setupLuminaBar()
    {
        for (int i = 0; i < luminaIcons.Length; i++)
        {
            if (i > stats.MaxLumina - 1)
            {
                luminaIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void updateLuminaBar()
    {
        for (int i = 0; i < luminaIcons.Length; i++)
        {
            if ( i > stats.currentLumina - 1)
            {
               if (luminaIcons[i].gameObject.activeInHierarchy)
                {
                    luminaIcons[i].color = Color.red;
                }
            }
            else
            {
                if (luminaIcons[i].gameObject.activeInHierarchy)
                {
                    luminaIcons[i].color = Color.white;
                }
            }
        }
    }

    private void updateHealthBar()
    {
        float healthPercentage = (stats.currentHealth / stats.maxHealth) * 100f;
        Debug.Log("|HUDCONTROLLER| ===> The current health percentage is " + healthPercentage);
        healthBarSlider.value = healthPercentage;
    }
}
