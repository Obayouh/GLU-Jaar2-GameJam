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

    private GameObject selectedCard;
    private GameObject selectedEnemy;

    private CardStats cardStats;

    private int StoreCardDamage;

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

            //select playercard to use if not already defined
            if (Input.GetMouseButtonDown(0) && selectedCard == null && hit.transform.CompareTag("PlayerCard"))
            {
                selectedCard = hit.transform.gameObject;
                cardStats = selectedCard.GetComponent<CardStats>();
                StoreCardDamage = cardStats.ReturnDamage();
                //Debug.Log(selectedCard);
            }

            //Select enemy to attack if not already defined and then empty card and enemy selections
            if (Input.GetMouseButtonDown(0) && selectedEnemy == null && selectedCard != null && hit.transform.CompareTag("Enemy"))
            {

                selectedEnemy = hit.transform.gameObject;
                selectedEnemy.GetComponent<HealthSystem>().TakeDamage(cardStats.ReturnDamage());
                //Logic needs to be added here that either turns the card off or deletes it after use
                selectedCard = null;
                selectedEnemy = null;
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
