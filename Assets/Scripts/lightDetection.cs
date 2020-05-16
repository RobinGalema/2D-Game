using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDetection : MonoBehaviour
{
    public float checkRadius;
    public LayerMask whatIsLight;

    private bool isActive;

    // --- Debugging Variables ---
    private SpriteRenderer sprite;
    private bool wasActiveLastFrame;
    // ---------------------------

    private void Start()
    {
        // --- Debug ---
        sprite = GetComponent<SpriteRenderer>();
        wasActiveLastFrame = isActive;
        // -------------
    }

    private void Update()
    {
        isActive = CheckForLight(checkRadius);
        ChangeSpriteColor();
    }

    private bool CheckForLight(float radius)
    {
        return Physics2D.OverlapBox(gameObject.transform.position, new Vector2(radius, radius), 0f, whatIsLight);
    }

    public bool IsThisActive()
    {
        return isActive;
    }

    // ---- Debugging Functions ----
    // These functions are for debugging purposes only and should not be used for game related functionality
    private void ChangeSpriteColor()
    {
        if (wasActiveLastFrame != isActive)
        {
            if (isActive)
            {
                sprite.color = Color.green;
            }
            else
            {
                sprite.color = Color.red;
            }
        }

        wasActiveLastFrame = isActive;
    }

}
