using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luminaController : MonoBehaviour
{
    public GameObject lightPrefab;

    [HideInInspector]
    public bool luminaInRange = false;

    private playerStats stats;
    private playerMovement movement;
    private List<GameObject> spawnedLights = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<playerStats>();
        movement = GetComponent<playerMovement>();
    }

    public void spawnLight()
    {
        if (movement.isGrounded && !((stats.currentLumina - 1) < 0) && !luminaInRange)
        {
            Debug.Log("|LUMINACONTROLLER| ===> Spawning a light is possible");
            stats.currentLumina -= 1;
            Debug.Log("|LUMINACONTROLLER| ===> Lumina after spawning = " + stats.currentLumina);

            GameObject spawnedLight = Instantiate(lightPrefab, transform.position + new Vector3(0,0.75f,0), Quaternion.identity);
            spawnedLights.Add(spawnedLight);
            Debug.Log("|LUMINACONTROLLER| ===> Light spawned, current active lights:");
        }
        else if ((stats.currentLumina - 1) < 0)
        {
            Debug.Log("|LUMINACONTROLLER| ===> <color=red>Player does not have enough Lumina to spawn a light</color>");
        }
        else if (luminaInRange)
        {
            Debug.Log("|LUMINACONTROLLER| ===> <color=red>There is already a Light source in range</color>");
        }
    }

    public void returnLights()
    {
        if (spawnedLights.Count > 0)
        {
            int luminaToRefund = spawnedLights.Count;
            Debug.Log("|LUMINACONTROLLER| ===> Starting to recall lights");
            foreach (var light in spawnedLights)
            {
                Destroy(light.gameObject);
            }

            Debug.Log("|LUMINACONTROLLER| ===> Lights removed from map, clearing list");
            spawnedLights.Clear();

            Debug.Log("|LUMINACONTROLLER| ===> Lights succesfully removed, refunding <color=blue>" + luminaToRefund + "</color> Lumina to the player");
            stats.currentLumina += luminaToRefund;
        }
        else
        {
            Debug.Log("|LUMINACONTROLLER| ===> <color=red>There are no lights to recall!</color>");
        }
    }
}
