using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    int menuId;            //Same as Room Id
    Room room;

    public GameObject buy;
    public GameObject remove;
    public GameObject upgrade;
    public GameObject degrade;
    public GameObject cancel;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        TMP_Text roomDesc = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        //SetRoom();
        Debug.Log(room.id.ToString());
        roomDesc.text = room.facility + System.Environment.NewLine;
        roomDesc.text += room.GetLevel().ToString(); 
    }
    /*
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

        //upgradeButton.GetComponent<Menu_Btn>().SetText("Upgrade Room (+" + (gameManager.GetRoomPrice("Classroom") / 2).ToString() + ")");
        upgradeButton.GetComponent<Menu_Btn>().SetText("Upgrade Room");
        upgradeButton.GetComponent<Menu_Btn>().SetDesc("Upgrade");

        if (room.GetLevel() > 1)
        {
            GameObject downGradeButton = Instantiate(upgrade, transform);
            downGradeButton.GetComponent<RectTransform>().SetSiblingIndex(3);
            //downGradeButton.GetComponent<Menu_Btn>().SetText("Cancel Room Upgrade ( +" + (gameManager.GetRoomPrice("Classroom") / 2).ToString() + " )");
            downGradeButton.GetComponent<Menu_Btn>().SetText("Cancel Room Upgrade");
            downGradeButton.GetComponent<Menu_Btn>().SetDesc("Downgrade");
        }
    }
    */

    private void SetRoom()
    {
        room = GameObject.Find("RoomSlot" + menuId.ToString()).GetComponent<Room>();

        if (room.IsLocked())
        {
            GameObject buyButton = Instantiate(buy, transform);
            buyButton.GetComponent<RectTransform>().SetSiblingIndex(1);
        }
        else
        {
            GameObject buyButton = Instantiate(remove, transform);
            buyButton.GetComponent<RectTransform>().SetSiblingIndex(1);

            GameObject upgradeButton = Instantiate(upgrade, transform);
            upgradeButton.GetComponent<RectTransform>().SetSiblingIndex(2);

            if (room.GetLevel() > 1)
            {
                GameObject downGradeButton = Instantiate(degrade, transform);
                downGradeButton.GetComponent<RectTransform>().SetSiblingIndex(3);
            }
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
