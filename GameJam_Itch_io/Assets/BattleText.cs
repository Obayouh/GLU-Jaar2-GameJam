using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battleText;

    private void Start()
    {
        battleText.text = "";
    }

    public void ChangeText(TurnState state)
    {
        if (state == TurnState.EnemyTurn)
            battleText.text = "Enemy turn";
        else
            battleText.text = "";
    }

    public void OutOfManaText(int number)
    {
        if (number == 1)
            battleText.text = "Not Enough Mana!";
        else if (number == 2)
            battleText.text = "Out of Mana!";
    }
}
