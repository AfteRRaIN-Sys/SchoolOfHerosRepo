using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyList : MonoBehaviour
{
    GameManager gameManager;
    int id;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    public void Buy(string fac)
    {
        gameManager.BuyRoom(id, fac);
        Destroy(gameObject);
        GameObject existedMenu = GameObject.Find("Menu(Clone)");
        if (existedMenu != null)
        {
            Destroy(existedMenu);
        }

    }
}
