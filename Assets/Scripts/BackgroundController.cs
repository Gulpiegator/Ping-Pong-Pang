using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private Transform[] layers; // Array of background layer transforms
    [SerializeField] private float[] parallaxFactors; // How much each layer moves relative to the player's movement

    private Vector3 previousPlayerPosition;

    void Start()
    {
        if (layers.Length != parallaxFactors.Length)
        {
            Debug.LogError("Layers and ParallaxFactors arrays must have the same length.");
            return;
        }

        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 deltaMovement = player.position - previousPlayerPosition;

        // Adjust each layer's position based on the parallax factor
        for (int i = 0; i < layers.Length; i++)
        {
            Vector3 layerPosition = layers[i].position;
            layerPosition.x += deltaMovement.x * parallaxFactors[i];
            layers[i].position = layerPosition;
        }

        previousPlayerPosition = player.position;
    }
}

