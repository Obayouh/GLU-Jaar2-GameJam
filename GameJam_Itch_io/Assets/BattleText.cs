using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void OutOfManaText()
    {
        battleText.text = "Out of Mana!";
    }
}
