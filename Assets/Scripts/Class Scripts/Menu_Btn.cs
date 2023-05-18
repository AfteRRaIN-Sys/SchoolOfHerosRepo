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

    public void ExpandBuyList(GameObject buyListMenu)
    {
        GameObject existedMenu = GameObject.Find("BuyList(Clone)");
        if (existedMenu != null)
        {
            Destroy(existedMenu);
        }

        GameObject buyList = gameManager.InstantiateObjectAtCursor(buyListMenu);
        buyList.GetComponent<BuyList>().SetID(menu.GetId());
    }

    public void Remove()
    {
        gameManager.RemoveRoom(menu.GetId());
        Destroy(transform.parent.gameObject);
    }
    public void Upgrade()
    {
        gameManager.UpgradeRoom(menu.GetId());
        Destroy(transform.parent.gameObject);
    }
    public void Degrade()
    {
        gameManager.DegradeRoom(menu.GetId());
        Destroy(transform.parent.gameObject);
    }

    public void Cancel()
    {
        GameObject existedMenu = GameObject.Find("BuyList(Clone)");
        if(existedMenu != null)
        {
            Destroy(existedMenu);
        }
        Destroy(transform.parent.gameObject);
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
