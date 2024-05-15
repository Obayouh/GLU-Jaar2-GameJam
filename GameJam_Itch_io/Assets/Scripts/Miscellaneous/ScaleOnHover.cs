using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnHover : MonoBehaviour
{
    private bool isHovering = false;
    private Vector3 originalScale;

    void Start()
    {
        // Store the original scale of the object
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Scale the object if the mouse is hovering over it
        if (isHovering)
        {
            transform.localScale = originalScale * 1.2f; // Scale up by 20%
        }
        else
        {
            // Reset the scale to its original size if not hovering over
            transform.localScale = originalScale;
        }
    }

    void OnMouseEnter()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }
}
