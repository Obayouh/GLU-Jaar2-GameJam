using UnityEngine;

public class Spacebar_Text : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.SetActive(false);
        }
    }
}
