using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HoverTipsManager : MonoBehaviour
{
    public TMP_Text tipsText;
    public RectTransform tipsWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public void OnEnable()
    {
        OnMouseHover += ShowTips;
        OnMouseLoseFocus += HideTips;
    }

    public void OnDisable()
    {
        OnMouseHover -= ShowTips;
        OnMouseLoseFocus -= HideTips;
    }

    // Start is called before the first frame update
    void Start()
    {
        HideTips();
    }

    public void ShowTips(string text, Vector2 mousePos)
    {
        tipsText.text = text;

        tipsWindow.gameObject.SetActive(true);
        tipsWindow.transform.position = mousePos + new Vector2(0, 5);
    }

    public void HideTips()
    {
        tipsText.text = default;
        tipsWindow.gameObject.SetActive(false);
    }
}
