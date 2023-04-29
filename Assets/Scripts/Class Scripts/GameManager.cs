using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Real private variables
    bool semesterEnd; //
    int turn;
    int shoppingCart;
    Notification notificationSystem;

    //Private variables but need to be shown for debugging
    [SerializeField]
    int semester = 0;
    [SerializeField]
    int money = 1000;
    [SerializeField]
    int maxTurn = 20;

    //Need to be shown in Inspector
    [SerializeField] //Roster data (Students and Professors)
    DraftSO classSO;
    [SerializeField] //This Scene Canvas
    Canvas canvas;
    [SerializeField] //Text on this canvas (Scene)
    TMP_Text money_text, turn_text;
    [SerializeField] //Grid storing items
    CanvasGroup studentGrid, professorGrid, roomSlotGrid;
    [SerializeField] //Card Prefab for Students and Professors
    GameObject cardPrefab;


    //This will be called in the start of the scene
    public void Start()
    {
        money_text.text = "Money: " + money.ToString();
        turn_text.text = "";
        semesterEnd = true;
        turn = 0;
        shoppingCart = 0;

        notificationSystem = gameObject.GetComponent<Notification>();
        List<Student> slctStudents = classSO.studentList;
        
        foreach (Student s in slctStudents){
            // init student card
            GameObject studentCardObj = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            studentCardObj.GetComponent<Button>().enabled = true;
            Destroy(studentCardObj.GetComponent<Button>());
            studentCardObj.AddComponent<CanvasGroup>();
            studentCardObj.AddComponent<DragDrop>();
            studentCardObj.AddComponent<HoverTips>();
            studentCardObj.GetComponent<HoverTips>().SetObject(studentCardObj);
            studentCardObj.GetComponent<HoverTips>().isStudent = true;

            studentCardObj.transform.SetParent(studentGrid.transform, false);

            Student tmp = studentCardObj.AddComponent<Student>();
            tmp.CopyStudent(s);
            Debug.Log($"student {s.name} {studentCardObj.GetComponent<Student>().name}");
        }

        List<Professor> slctProfessors = classSO.professorList;
        foreach (Professor s in slctProfessors){
            // init Professor card
            GameObject professorCardObj = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            professorCardObj.transform.SetParent(professorGrid.transform, false);
            professorCardObj.GetComponent<Button>().enabled = true;
            Destroy(professorCardObj.GetComponent<Button>());
            professorCardObj.AddComponent<CanvasGroup>();
            professorCardObj.AddComponent<DragDrop>();
            professorCardObj.AddComponent<HoverTips>();
            professorCardObj.GetComponent<HoverTips>().SetObject(professorCardObj);
            professorCardObj.GetComponent<HoverTips>().isStudent = false;
            
            professorCardObj.AddComponent<Professor>();
            Professor tmp = professorCardObj.GetComponent<Professor>();
            tmp.CopyProfessor(s);
            Debug.Log($"professor {s.name} {professorCardObj.GetComponent<Professor>().name}");
        }

        // foreach(Student s in slctStudents){
        //     Debug.Log($"select s : {s.name}");
        // }

        // foreach(Professor p in slctProfessors){
        //     Debug.Log($"select p : {p.name}");
        // }
    }

    // Move to next scene in order from Built scenes
    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Will create anything onto the mouse cursor
    public GameObject InstantiateObjectAtCursor(GameObject instObject)
    {
        GameObject newObject = Instantiate(instObject, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);

        Vector2 screenPoint = Input.mousePosition;
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(screenPoint);

        newObject.transform.position = cursorPosition;

        return newObject;
    }

    //--------------------------------------------------  Renovation Phase -------------------------------------

    // Player click to Buy a room
    public void BuyRoom(int id, string fac)
    {
        int price = GetRoomPrice(fac);
        if(price == -1)
        {
            Debug.Log("Room Facility Error (Type Not Found.)");
            return;
        }

        if (shoppingCart + price <= money)
        {
            shoppingCart += price;

            Room room = GameObject.Find("RoomSlot" + id.ToString()).GetComponent<Room>();
            room.UnlockAs(fac);
            room.AddValue(price);
            Room_Btn room_Btn = GameObject.Find("RoomButton" + id.ToString()).GetComponent<Room_Btn>();
            room_Btn.ToggleLock(false);     

            ShowMoney();
        }
        else
        {
            notificationSystem.Notify("Can't upgrade Room" + id.ToString() + ". Insufficient Budget! Required: " + price.ToString());
            Debug.Log("Insufficient Budget!");
        }

        SemesterCheck();
    }

    // Player click to Upgrade a room
    public void UpgradeRoom(int id)
    {
        Room room = GameObject.Find("RoomSlot" + id.ToString()).GetComponent<Room>();
        int price = GetRoomPrice(room.facility) / 2;
        if (shoppingCart + price <= money)
        {
            shoppingCart += price;

            room.Upgrade();
            room.AddValue(price);

            notificationSystem.Notify("Room" + id.ToString() + "is now level " + room.GetLevel().ToString());
            ShowMoney();
        }
        else
        {
            notificationSystem.Notify("Can't upgrade Room" + id.ToString() + ". Insufficient Budget! Required: " + price.ToString());
            Debug.Log("Insufficient Budget!");
        }
    }

    //If player click to de-upgrade the room
    public void DegradeRoom(int id)
    {
        Room room = GameObject.Find("RoomSlot" + id.ToString()).GetComponent<Room>();
        int price = GetRoomPrice(room.facility)/2;
        if (room.GetLevel() > 1)
        {
            shoppingCart -= price;

            room.Downgrade();
            room.AddValue(-price);

            ShowMoney();
            notificationSystem.Notify("Room" + id.ToString() + "is now level " + room.GetLevel().ToString());
        }
        else
        {
            notificationSystem.Notify("Room" + id.ToString() + "is at lowest level!");
            Debug.Log("This room is at lowest level!");
        }
    }

    // Player click to Remove a room
    public void RemoveRoom(int id)
    {
        Room room = GameObject.Find("RoomSlot" + id.ToString()).GetComponent<Room>();

        shoppingCart -= room.GetValue();

        GameObject gameObject = GameObject.Find("RoomButton" +id.ToString());
        Room_Btn room_button = gameObject.GetComponent<Room_Btn>();
        room.Remove();
        room_button.ToggleLock(true);

        ShowMoney();

        SemesterCheck();
    }

    // This is the price for each type of facility
    public int GetRoomPrice(string fac)
    {
        switch (fac)
        {
            case "Classroom":
                return 400;
            case "Medic":
                return 300;
            case "Guide":
                return 800;
            case "Delete":
                return 200;
        }

        return -1;
    }

    // Player click to Start a Semester
    public void StartSemester()
    {
        semesterSwitch();
        addSemester(1);

        GameObject gameObject = GameObject.Find("Txt_Semester").gameObject;
        TMP_Text semester_txt = gameObject.GetComponent<TMP_Text>();
        semester_txt.text = "Semester " + semester.ToString();
       
        turn_text.text = "Turn 1 / " + maxTurn.ToString();
        setTurn(1);

        // Update each room Icon
        GameObject roomBtnGrid = GameObject.Find("RoomButtonGrid").gameObject;
        foreach (Transform child in roomBtnGrid.transform)
        {
            Room_Btn button = child.gameObject.GetComponent<Room_Btn>();
            button.SetEnable(false);
            if (!button.IsLocked())
            {
                button.HideButton();
            }
        }

        //Show the lecture phase elements
        Turn_Btn turnButton = GameObject.Find("Btn_EndTurn").GetComponent<Turn_Btn>(); ;
        turnButton.ShowButton();

        studentGrid.alpha = 1;
        studentGrid.blocksRaycasts = true;

        professorGrid.alpha = 1;
        professorGrid.blocksRaycasts = true;

        roomSlotGrid.blocksRaycasts = true;

        money_text.GetComponent<CanvasGroup>().alpha = 0;
        foreach (Transform child in roomSlotGrid.gameObject.transform)
        {
            CanvasGroup slot = child.gameObject.GetComponent<CanvasGroup>();
            slot.ignoreParentGroups = true;
        }
    }

    // -------------------------------------------- Lecturing Phase -------------------------------------------

    //If player click to end turn
    string learnNotification = "";
    public void EndTurn()
    {
        if (turn < maxTurn)
        {
            //proceed the lecture or the usage for each and every room available
            GameObject rooms = GameObject.Find("RoomSlotGrid").gameObject;
            foreach (Transform child in rooms.transform)
            {
                Room room = child.GetComponent<Room>();
                string facility = room.facility;
                if (!room.IsLocked())
                {
                    switch (facility)
                    {
                        case "Classroom":
                            room.Learn();
                            learnNotification += room.GetLearnNotification();
                            continue;
                        case "Medic":
                            continue;
                        case "Guide":
                            continue;
                        case "Delete":
                            continue;
                    }    
                }
            }
            notificationSystem.Notify(learnNotification);
            addTurn(1);
            ShowTurn();
        }
        else //Transition to next scene
        {
            GameObject roomBtnGrid = GameObject.Find("RoomButtonGrid").gameObject;
            foreach (Transform child in roomBtnGrid.transform)
            {
                Room_Btn button = child.gameObject.GetComponent<Room_Btn>();
                button.SetEnable(true);
                if (!button.IsLocked())
                {
                    button.ShowButton();
                }
            }

            Turn_Btn turnButton = GameObject.Find("Btn_EndTurn").GetComponent<Turn_Btn>(); ;
            turnButton.HideButton();
            semesterSwitch();

            NextScene();
        }
        learnNotification = "";
    }

    //
    public void CheckTurnButton()
    {
        Turn_Btn turnButton = GameObject.Find("Btn_EndTurn").GetComponent<Turn_Btn>();

        bool ready = false;
        GameObject roomGrid = roomSlotGrid.gameObject;
        foreach (Transform child in roomGrid.transform)
        {
            Room room = child.GetComponent<Room>();
            if(room.IsLocked() || room.IsEmpty())
            {
                continue;
            }
            if (!room.IsReady())
            {
                ready = false;
                break;
            }
            else
            {
                ready = true;
            }
        }

        if (ready)
        {
            turnButton.SetEnable(true);
            turnButton.ShowButton();
        }
        else
        {
            turnButton.SetEnable(false);
        }
    }

    private bool SemesterCheck()
    {
        GameObject roomBtnGrid = GameObject.Find("RoomButtonGrid").gameObject;
        Sem_Btn semesterButton = GameObject.Find("Btn_Start Semester").GetComponent<Sem_Btn>(); ;
        foreach (Transform child in roomBtnGrid.transform)
        {
            Room_Btn button = child.gameObject.GetComponent<Room_Btn>();
            if (!button.IsLocked())
            {
                semesterButton.ShowButton();
                semesterButton.SetEnable(true);
                return true;
            }
        }
        semesterButton.SetEnable(false);
        return false;
    }

    public void semesterSwitch()
    {
        semesterEnd = !semesterEnd;

        GameObject gameObject = GameObject.Find("Btn_Start Semester");
        Sem_Btn sem_btn = gameObject.GetComponent<Sem_Btn>();

        if (semesterEnd)
        {
            turn_text.text = "";
            sem_btn.ShowButton();
            money_text.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void DismissClass()
    {

    }

    public int getSemester()
    {
        return semester;
    }

    public void setSemester(int amount)
    {
        semester = amount;
    }

    public void addSemester(int amount)
    {
        semester += amount;
    }

    public int getTurn()
    {
        return turn;
    }

    public void setTurn(int amount)
    {
        turn = amount;
    }

    public void addTurn(int amount)
    {
        turn += amount;
    }

    public void ShowTurn()
    {
        turn_text.text = "Turn " + turn.ToString() + " /  " + maxTurn.ToString();
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int amount)
    {
        money = amount;
    }

    public void addMoney(int amount)
    {
        money += amount;
    }

    public void ShowMoney()
    {
        int remaining = money - shoppingCart;
        money_text.text = "Money: " + remaining.ToString();
    }
}
