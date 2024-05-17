using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform cardPos;
    [SerializeField] private Transform gamePos;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ReferenceInstance.refInstance.turnManager.ReturnState() != TurnState.PickCard)
            return;

        if (ReferenceInstance.refInstance.cam.switchView == false)
        {
            ReferenceInstance.refInstance.cam.switchView = true;

            if (ReferenceInstance.refInstance.cam.state == ViewState.game)
            {
                transform.position = cardPos.position;
            }
            else
            {
                transform.position = gamePos.position;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
