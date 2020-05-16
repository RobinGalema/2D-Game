using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    public float MaxLumina;
    [HideInInspector]
    public float currentLumina;

    private float lastFrameHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLumina = MaxLumina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkHealthUpdate()
    {
        if (lastFrameHealth != currentHealth)
        {
            lastFrameHealth = currentHealth;
            return true;
        }
        else
        {
            return false;
        }
    }
}
