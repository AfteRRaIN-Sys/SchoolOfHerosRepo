using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Button : MonoBehaviour
{
    protected CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
    }

    public void SetEnable(bool onoff)
    {
        cg.interactable = cg.blocksRaycasts = onoff;
    }

    public void HideButton()
    {
        cg.alpha = 0;
        SetEnable(false);
    }

    public void ShowButton()
    {
        cg.alpha = 1;
    }
}
