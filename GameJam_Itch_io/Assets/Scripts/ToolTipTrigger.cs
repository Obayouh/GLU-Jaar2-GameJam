using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipTrigger : MonoBehaviour
{
    [Multiline()]
    public string content;
    private void OnMouseEnter()
    {
        TooltipUI.ShowTooltip_Static(content);
    }

    private void OnMouseExit()
    {
        TooltipUI.HideTooltip_Static();
    }
}
