using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmmoPickUp : MonoBehaviour
{
    float despawnTimer = 30;

    void Update()
    {
        despawnTimer -= 1f * Time.deltaTime;
        Debug.Log("Despawning in... " + despawnTimer);
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
