using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room_Btn : Game_Button
{
    public int roomId;

    public Sprite locked_icon;
    public Sprite edit_icon;
    public bool locked;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cg = GetComponent<CanvasGroup>();

        if (locked)
        {
            ChangeButtonImage(locked_icon);
        }
    }

    public void OnClick(GameObject menu)
    {
        GameObject existedMenu = GameObject.Find("Menu(Clone)");
        if(existedMenu != null)
        {
            Destroy(existedMenu);
        }

        existedMenu = GameObject.Find("BuyList(Clone)");
        if (existedMenu != null)
        {
            Destroy(existedMenu);
        }

        GameObject roomMenu = gameManager.InstantiateObjectAtCursor(menu);

        SetUpRoomMenu(roomMenu);
    }

    public void SetUpRoomMenu(GameObject roomMenu)
    {
        roomMenu.GetComponent<Menu>().SetId(roomId);

        /*
        GameObject button = roomMenu.transform.GetChild(1).gameObject;

        GameObject gameObject = GameObject.Find("RoomSlot" + roomId.ToString());
        Room room = gameObject.GetComponent<Room>();

        if (!room.IsLocked())
        {
            button.GetComponent<Menu_Btn>().SetDesc("Remove");
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Remove Classroom" ;
        }
        else
        {
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buy Classroom";
            button.GetComponent<Menu_Btn>().SetDesc("Buy");
        }
        */
    }

    public void ToggleLock(bool onoff)
    {
        if (onoff)
        {
            ChangeButtonImage(locked_icon);
        }
        else
        {
            ChangeButtonImage(edit_icon);
        }
        locked = onoff;
    }

    public bool IsLocked()
    {
        return locked;
    }

    public void ChangeButtonImage(Sprite newButtonImage)
    {
        Button button = GetComponent<Button>();
        button.image.sprite = newButtonImage;
    }

    public int GetBtnId()
    {
        return roomId;
    }
}
