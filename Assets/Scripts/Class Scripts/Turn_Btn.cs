using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Btn : Game_Button
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cg = GetComponent<CanvasGroup>();
    }

    public void Onclick()
    {
        gameManager.EndTurn();
    }
}
