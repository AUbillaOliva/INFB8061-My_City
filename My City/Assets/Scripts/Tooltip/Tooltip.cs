using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField, messageField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public void SetText(string content, string header = "")
    {
        if (!string.IsNullOrEmpty(header))
        {
            headerField.text = header;
        }
        messageField.text = content;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int messageLength = headerField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || messageLength > characterWrapLimit);
        }
        
    }
}
