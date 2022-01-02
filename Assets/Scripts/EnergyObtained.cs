using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObtained : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.CompareTag("Player"))
        {
            // If the GameObject's name matches the one you suggest, destroy it
            player.jumpForce = player.jumpForce * 2.25f;
            player.playerAnim.SetFloat("DoubleJump", 0.5f);
            Destroy(gameObject);
        }
    }
}
