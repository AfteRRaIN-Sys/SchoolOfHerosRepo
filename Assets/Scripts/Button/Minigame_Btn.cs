using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Btn : MonoBehaviour
{
    Room room;
    CanvasGroup cg;

    private void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
    }

    public void Hammer()
    {
        room.playerMiniGameInput(0);
    }

    public void Scissors()
    {
        room.playerMiniGameInput(1);
    }

    public void Paper()
    {
        room.playerMiniGameInput(2);
    }

    public void SetMinigameForRoom (Room room)
    {
        this.room = room;
    }

    public void Cancel()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }
}
