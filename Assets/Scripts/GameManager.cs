using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public DraftSO classSO;

    public bool semesterEnd = true;
    public int semester = 0;
    public int money = 1000;

    public int maxTurn = 20;
    public int turn = 0;

    public TMP_Text money_text, turn_text;

    public Canvas canvas;

    private int shoppingCart = 0;

    CanvasGroup studentGrid, professorGrid, roomSlotGrid;

    [SerializeField]
    PlayerData player;

    // cardprefab
    public GameObject cardPrefab;

    public void Start()
    {
        money_text.text = "Money: " + money.ToString();
        turn_text.text = "";
        shoppingCart = 0;
        studentGrid = GameObject.Find("StudentGrid").GetComponent<CanvasGroup>();
        professorGrid = GameObject.Find("ProfessorGrid").GetComponent<CanvasGroup>();
        roomSlotGrid = GameObject.Find("RoomSlotGrid").GetComponent<CanvasGroup>();

        
        List<Student> slctStudents = classSO.studentList;
        
        foreach (Student s in slctStudents){
            // init student card
            GameObject studentCardObj = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            studentCardObj.GetComponent<Button>().enabled = false;
            studentCardObj.AddComponent<CanvasGroup>();
            studentCardObj.AddComponent<DragDrop>();
            studentCardObj.AddComponent<HoverTips>();
            studentCardObj.GetComponent<HoverTips>().SetObject(studentCardObj);
            studentCardObj.GetComponent<HoverTips>().isStudent = true;

            studentCardObj.transform.SetParent(studentGrid.transform, false);

            Student tmp = studentCardObj.AddComponent<Student>();
            tmp.CopyStudent(s);
            // tmp = s;
            Debug.Log($"student {s.name} {studentCardObj.GetComponent<Student>().name}");
        }

        List<Professor> slctProfessors = classSO.professorList;
        foreach (Professor s in slctProfessors){
            // init Professor card
            GameObject professorCardObj = Instantiate(cardPrefab, new Vector3(0,0,0), Quaternion.identity);
            professorCardObj.transform.SetParent(professorGrid.transform, false);
            professorCardObj.GetComponent<Button>().enabled = false;
            professorCardObj.AddComponent<CanvasGroup>();
            professorCardObj.AddComponent<DragDrop>();
            professorCardObj.AddComponent<HoverTips>();
            professorCardObj.GetComponent<HoverTips>().SetObject(professorCardObj);
            professorCardObj.GetComponent<HoverTips>().isStudent = false;
            
            // ***
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

    void SaveData()
    {
        player.SaveMoneyData(money);

        foreach (Transform child in studentGrid.transform)
        {
            Student student = child.GetComponent<Student>();
            player.SaveStudentData(student);
        }     
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public GameObject InstantiateObjectAtCursor(GameObject instObject)
    {
        GameObject newObject = Instantiate(instObject, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);

        Vector2 screenPoint = Input.mousePosition;
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(screenPoint);

        newObject.transform.position = cursorPosition;

        return newObject;
    }

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

            Room room = GameObject.Find("Room" + id.ToString()).GetComponent<Room>();
            room.UnlockAs(fac);
            room.AddValue(price);
            Room_Btn room_Btn = GameObject.Find("RoomButton" + id.ToString()).GetComponent<Room_Btn>();
            room_Btn.ToggleLock(false);     

            ShowMoney();
        }
        else
        {
            gameObject.GetComponent<Notification>().Notify("Can't upgrade Room" + id.ToString() + ". Insufficient Budget! Required: " + price.ToString());
            Debug.Log("Insufficient Budget!");
        }

        SemesterCheck();
    }

    public void UpgradeRoom(int id)
    {
        Room room = GameObject.Find("Room" + id.ToString()).GetComponent<Room>();
        int price = GetRoomPrice(room.facility) / 2;
        if (shoppingCart + price <= money)
        {
            shoppingCart += price;

            room.Upgrade();
            room.AddValue(price);

            gameObject.GetComponent<Notification>().Notify("Room" + id.ToString() + "is now level " + room.GetLevel().ToString());
            ShowMoney();
        }
        else
        {
            gameObject.GetComponent<Notification>().Notify("Can't upgrade Room" + id.ToString() + ". Insufficient Budget! Required: " + price.ToString());
            Debug.Log("Insufficient Budget!");
        }
    }

    public void DegradeRoom(int id)
    {
        Room room = GameObject.Find("Room" + id.ToString()).GetComponent<Room>();
        int price = GetRoomPrice(room.facility)/2;
        if (room.GetLevel() > 1)
        {
            shoppingCart -= price;

            room.Downgrade();
            room.AddValue(-price);

            ShowMoney();
            gameObject.GetComponent<Notification>().Notify("Room" + id.ToString() + "is now level " + room.GetLevel().ToString());
        }
        else
        {
            gameObject.GetComponent<Notification>().Notify("Room" + id.ToString() + "is at lowest level!");
            Debug.Log("This room is at lowest level!");
        }
    }

    public void RemoveRoom(int id)
    {
        Room room = GameObject.Find("Room" + id.ToString()).GetComponent<Room>();

        shoppingCart -= room.GetValue();

        GameObject gameObject = GameObject.Find("RoomButton" +id.ToString());
        Room_Btn room_button = gameObject.GetComponent<Room_Btn>();
        room.Remove();
        room_button.ToggleLock(true);

        ShowMoney();

        SemesterCheck();
    }

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

    public void StartSemester()
    {
        semesterSwitch();
        addSemester(1);

        GameObject gameObject = GameObject.Find("Txt_Semester").gameObject;
        TMP_Text semester_txt = gameObject.GetComponent<TMP_Text>();
        semester_txt.text = "Semester " + semester.ToString();
       
        turn_text.text = "Turn 1 / " + maxTurn.ToString();
        setTurn(1);

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

    public void CheckTurnButton(Room room)
    {
        Turn_Btn turnButton = GameObject.Find("Btn_EndTurn").GetComponent<Turn_Btn>();
        if (room.IsReady())
        {
            turnButton.SetEnable(true);
            turnButton.ShowButton();
        }
        else
        {
            turnButton.SetEnable(false);
        }
    }
    string learnNotification = "";

    public void EndTurn()
    {
        if (turn < maxTurn)
        {
            GameObject rooms = GameObject.Find("RoomImageGrid").gameObject;
            foreach (Transform room in rooms.transform)
            {            
                if (!room.GetComponent<Room>().IsLocked())
                {
                    room.GetComponent<Room>().Learn();
                    learnNotification += room.GetComponent<Room>().GetLearnNotification();
                }
            }
            gameObject.GetComponent<Notification>().Notify(learnNotification);
            addTurn(1);
            ShowTurn();
        }
        else
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

            SaveData();
            NextScene();
        }
        learnNotification = "";
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
