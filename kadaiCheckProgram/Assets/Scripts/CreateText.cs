using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    [SerializeField]
    private static Text statusText;

    private void Start()
    {
        statusText = GetComponent<Text>();
    }
    public static void SendText(string text)
    {
        statusText.text = text;
    }
}
