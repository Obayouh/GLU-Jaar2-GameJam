using UnityEngine;

public class Spacebar_Text : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ReferenceInstance.refInstance.turnManager.ReturnState() == TurnState.PickCard)
        {
            this.gameObject.SetActive(false);
        }
    }
}
