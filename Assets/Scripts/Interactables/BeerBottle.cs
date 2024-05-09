using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : Interactable
{
    [SerializeField] private List<Rigidbody> allParts = new List<Rigidbody>();

    public float fadeDuration = 5f; // Duration of fade effect in seconds.

    private int interactionCount = 0; // Number of interactions with the object.
    private bool fading = false; // Flag to track if the fading effect is currently active.
    private MeshRenderer[] meshRenderers; // Array to store references to mesh renderers.
    private Color[] initialColors; // Array to store initial colors of mesh renderers.

    private void Start()
    {
        // Get all mesh renderers attached to the GameObject and its children.
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Store the initial colors of all mesh renderers.
        initialColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            initialColors[i] = meshRenderers[i].material.color;
        }
    }

    protected override void Interact()
    {
        interactionCount++;
        // Check if this is the second interaction and start fading if true
        if (interactionCount == 2 && !fading)
        {
            StartCoroutine(FadeOut());
        }

    }

    IEnumerator FadeOut()
    {
        // Set the fading flag to true to indicate that the fade effect is active.
        fading = true;

        // Get the current time.
        float startTime = Time.time;

        // Fade out gradually over time.
        while (Time.time - startTime < fadeDuration)
        {
            // Calculate the fade amount based on time.
            float fadeAmount = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeDuration);

            // Modify the colors of all mesh renderers to fade them out.
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                Color fadedColor = initialColors[i];
                fadedColor.a = fadeAmount;
                meshRenderers[i].material.color = fadedColor;
            }

            // Wait for the next frame.
            yield return null;
        }
        // Ensure all mesh renderers are fully transparent.
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Color transparentColor = initialColors[i];
            transparentColor.a = 0f;
            meshRenderers[i].material.color = transparentColor;
        }

        // Disable the GameObject. (optional)
        gameObject.SetActive(false);

        // Reset the interaction count and fading flag.
        interactionCount = 0;
        fading = false;
 
    }

    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
            part.AddExplosionForce(1000.0f, transform.position, 5.0f);
        }
        SetPromptMessage("Press [E] : Destroy");
    }
}
