using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManagerPrime : MonoBehaviour
{
    private RaycastHit hit;
    private Transform previousHit;
    private Transform currentHitTransform;

    [SerializeField] private Material outlineMaterial;
    private string outlineMaterialInstanceName;

    public GameObject selectedCard;
    private GameObject selectedEnemy;

    void Start()
    {
        currentHitTransform = null;
        selectedCard = null;
        outlineMaterialInstanceName = outlineMaterial.name + " (Instance)";
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 400.0f))
        {

            // Inform the previously hit object that the mouse is no longer hovering over it
            if (previousHit != null && previousHit != hit.transform)
            {
                RemoveOutline(previousHit);
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }

            // Inform the newly hit object that the mouse is now hovering over it
            currentHitTransform = hit.transform;
            SetOutline(currentHitTransform);
            currentHitTransform.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);

            // Store the current hit target as the previous hit target
            previousHit = currentHitTransform;

            if (Input.GetMouseButtonDown(0) && selectedCard == null && hit.transform.CompareTag("PlayerCard"))
            {
                //If card is too costly, do nothing
                //cardStats = GetComponent<CardStats>();
                //playerStats.CheckIfUsable();
                selectedCard = hit.transform.gameObject;
                //Debug.Log(selectedCard);
            }
        }
        else
        {
            if (previousHit != null)
            {
                RemoveOutline(previousHit);
                previousHit.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
                previousHit = null;
            }
        }
    }

    // Set outline for an object
    void SetOutline(Transform target)
    {
        if (target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Check if the outline material is already added
                Material[] materials = renderer.materials;
                bool hasOutlineMaterial = false;
                foreach (Material material in materials)
                {
                    if (material.name == outlineMaterial.name || material.name == outlineMaterialInstanceName)
                    {
                        hasOutlineMaterial = true;
                        break;
                    }
                }

                // Add the outline material if not already added
                if (!hasOutlineMaterial)
                {
                    Material[] newMaterials = new Material[materials.Length + 1];
                    for (int i = 0; i < materials.Length; i++)
                    {
                        newMaterials[i] = materials[i];
                    }
                    newMaterials[newMaterials.Length - 1] = outlineMaterial;
                    renderer.materials = newMaterials;
                }
            }
        }
    }

    void RemoveOutline(Transform target)
    {
        if (target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Remove the outline material from the materials list
                List<Material> materials = new List<Material>();
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    if (renderer.materials[i].name != outlineMaterial.name && renderer.materials[i].name != outlineMaterialInstanceName)
                    {
                        materials.Add(renderer.materials[i]);
                    }
                }
                renderer.materials = materials.ToArray();
            }
        }
    }
}
