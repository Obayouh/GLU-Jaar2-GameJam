using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class Tooltip : MonoBehaviour
{
    public string tooltipTextToShow;
    private void Update()
    {
        if (tooltipTextToShow != null)
        {

            System.Func<string> getTooltipTextFunc = () =>
            {
                return tooltipTextToShow;
            };
            TooltipUI.ShowTooltip_Static(getTooltipTextFunc);
        }
        else return;
    }
}
