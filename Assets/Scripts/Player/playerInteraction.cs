using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    public Vector2 respawnPos;
    public hudController hud;

    private playerStats stats;
    private playerMovement movement;
    private luminaController lumina;
    private float lastFrameHealth;

    // Start is called before the first frame update
    void Start()
    {
        respawnPos = transform.position;
        stats = GetComponent<playerStats>();
        movement = GetComponent<playerMovement>();
        lumina = GetComponent<luminaController>();
        lastFrameHealth = stats.currentHealth;
    }

    private void Update()
    {
        if (CheckDeath()){ Respawn(); };
        CheckInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("FallCheck"):
            {
                stats.currentHealth = 0;
                Debug.Log("|PLAYERINTERACTION| ===> The player fell out of the world");
                break;
            }

            case ("PlayerLight"):
                {
                    lumina.luminaInRange = true;
                    break;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerLight"):
                lumina.luminaInRange = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case ("DamagePlayer"):
            {
                stats.currentHealth -= 10;
                break;
            }
        }
    }

    private bool CheckDeath()
    {
        if (stats.currentHealth <= 0)
        {
            Debug.Log("|PLAYERINTERACTION| ===> The player died");
            return true;
        }
        else
            return false;
    }

    private void Respawn()
    {
        Debug.Log("|PLAYERINTERACTION| ===> Respawning player...");
        stats.currentHealth = stats.maxHealth;
        transform.position = respawnPos;

        // Reset all lights
        lumina.returnLights();
        hud.updateLuminaBar();
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("PlaceLight"))
        {
            Debug.Log("|PLAYERINTERACTION| ===> Player pressed the PlaceLight key");
            lumina.spawnLight();
            hud.updateLuminaBar();
        }

        if (Input.GetButtonDown("RecallLight"))
        {
            Debug.Log("|PLAYERINTERACTION| ===> Player pressed the RecallLight key");
            lumina.returnLights();
            hud.updateLuminaBar();
        }
    }
}
