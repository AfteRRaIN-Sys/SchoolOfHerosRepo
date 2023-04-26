using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu_Btn : Game_Button
{
    GameManager gameManager;
    Menu menu;
    string description;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cg = GetComponent<CanvasGroup>();

        menu = transform.parent.GetComponent<Menu>();
    }

    public void Onclick()
    {
        menu.Onclick(description);
    }

    public void SetText(string text)
    {
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        
    }

    public void SetDesc(string desc)
    {
        description = desc;
    }

    public string GetDesc()
    {
        return description;
    }
}
