using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Build.Content;

public class ClickAttack : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject clickableObject;

    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    private Renderer currentlyActiveMaterial;

    private bool itemUsed = false;

    private void Start()
    {
        currentlyActiveMaterial = GetComponent<Renderer>();
        itemUsed = false;
    }
    void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            IClickedOn();
        }

        if (Input.GetMouseButtonUp(0) && itemUsed == false)
        {
            IClickedOff();
        }
    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }

    private bool isPointerOverUIObject()
    {

        PointerEventData ped = new PointerEventData(EventSystem.current);

        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(ped, results);

        return results.Count > 0;
    }

    private void SetMaterial()
    {
        var materialSwapper = currentlyActiveMaterial.materials;
        materialSwapper[0] = activeMaterial;
        currentlyActiveMaterial.materials = materialSwapper;
    }

    public void IClickedOn()
    {
        if (clickableObject == GetClickedObject(out RaycastHit hit) && itemUsed == false)
        {
            //Debug.Log("clicked/touched!");
            SetMaterial();
            itemUsed = true;
        }
    }

    public void IClickedOff()
    {
        //Debug.Log(gameObject.name + " has not been activated yet");
    }
}
