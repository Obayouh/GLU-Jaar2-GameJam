using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasCollector : MonoBehaviour
{
    //All public vars are hidden so they cant be changed in the inspector
    [HideInInspector] public GameObject EndTurnButton;
    [HideInInspector] public TextMeshProUGUI CurrentFloor;
    [HideInInspector] public TextMeshProUGUI CurrentTurn;
    [HideInInspector] public Animator CrossfadeAnimator;

    void Start()
    {
        //Searches in buttons to find the endturn gameobject
        EndTurnButton = transform.Find("Buttons/EndTurn").gameObject;
        
        //Searches for the gameobjects where the text or animator is located
        GameObject findFloor = transform.Find("CurrentInfoText/Current_Floor").gameObject;
        GameObject findTurn = transform.Find("CurrentInfoText/Current_Turn").gameObject;
        GameObject findCrossfade = transform.Find("Crossfade").gameObject;

        //Gets the text or animator component from the the found gameobject and puts it in the public var
        CurrentFloor = findFloor.GetComponent<TextMeshProUGUI>();
        CurrentTurn = findTurn.GetComponent<TextMeshProUGUI>();
        CrossfadeAnimator = findCrossfade.GetComponent<Animator>();
    }
}
