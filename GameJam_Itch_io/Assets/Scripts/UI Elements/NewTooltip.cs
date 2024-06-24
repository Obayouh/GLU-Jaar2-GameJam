using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class NewTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private LayoutElement layoutElement;

    [SerializeField, Range(0, 200)] private int characterWrapLimit;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponentInParent<RectTransform>();
    }

    public void SetText(string content)
    {
        contentField.text = content;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            int contentLength = contentField.text.Length;

            layoutElement.enabled = contentLength > characterWrapLimit ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
