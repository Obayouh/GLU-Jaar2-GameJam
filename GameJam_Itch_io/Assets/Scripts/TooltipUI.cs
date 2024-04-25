using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{

    public static TooltipUI Instance { get; private set; }
    [SerializeField] private NewTooltip tooltip;
    private void Awake()
    {
        Instance = this;
    }

    public static void ShowTooltip_Static(string content)
    {
        Instance.tooltip.SetText(content);
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void HideTooltip_Static()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
