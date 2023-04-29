using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameManager gameManager;
    int menuId;            //Same as Room Id
    Room room;

    public GameObject classroom;
    public GameObject upgrade;
    public GameObject cancel;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetRoom();
        TMP_Text roomDesc = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        roomDesc.text = "Room: " + room.facility + System.Environment.NewLine;
        roomDesc.text += "Level: " + room.GetLevel().ToString();

        Menu_Btn cancelButton = gameObject.transform.GetChild(transform.childCount - 1).GetComponent<Menu_Btn>();
        cancelButton.SetDesc("Cancel");
    }

    public void Onclick(string desc)
    {
        switch (desc)
        {
            case "Buy":
                gameManager.BuyRoom(menuId, "Classroom");
                break;

            case "Upgrade":
                gameManager.UpgradeRoom(menuId);
                break;

            case "Remove":
                gameManager.RemoveRoom(menuId);
                break;

            case "Downgrade":
                gameManager.DegradeRoom(menuId);
                break;
        }
        Cancel();
    }

    public void CreateUpgradeButton()
    {
        GameObject upgradeButton = Instantiate(upgrade, transform);
        upgradeButton.GetComponent<RectTransform>().SetSiblingIndex(2);
        upgradeButton.GetComponent<Menu_Btn>().SetText("Upgrade Room");
        upgradeButton.GetComponent<Menu_Btn>().SetDesc("Upgrade");

        if (room.GetLevel() > 1)
        {
            GameObject downGradeButton = Instantiate(upgrade, transform);
            downGradeButton.GetComponent<RectTransform>().SetSiblingIndex(3);
            downGradeButton.GetComponent<Menu_Btn>().SetText("Cancel Room Upgrade");
            downGradeButton.GetComponent<Menu_Btn>().SetDesc("Downgrade");
        }
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }

    private void SetRoom()
    {
        room = GameObject.Find("RoomSlot" + menuId.ToString()).GetComponent<Room>();
        Menu_Btn classroomButton = gameObject.transform.GetChild(1).GetComponent<Menu_Btn>();

        if (room.IsLocked())
        {
            classroomButton.SetText("Buy Classroom");
        }
        else
        {
            classroomButton.SetText("Remove Classroom");
        }
    }

    public void SetId(int id)
    {
        menuId = id;
        SetRoom();
    }

    public int GetId()
    {
        return menuId;
    }
}
