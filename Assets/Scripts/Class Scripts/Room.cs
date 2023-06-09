using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    public GameStateSO gameStateSO;
    public int id;
    public string facility;
    private int level = 0;
    public int maxLevel = 4;
    private int value = 0;
    public int maxCapacity;
    private int currentCapacity;

    //                          |lvl1|  lvl2|   lvl3|   lvl4|   store index of skill that is prerequisite for each skill (-1 means no prerequisite needed)
    private int[] preReqSkill;

    bool locked;
    bool assigned;
    bool full;
    bool maxedLevel;
    bool ready;
    bool empty;

    Professor host;
    int skillLecture;
    List<Student> students = new List<Student>();
    GameManager gameManager;
    [SerializeField]
    Notification notificationSystem;
    [SerializeField]
    float notificationDelayInterval = 1f;

    public Sprite ruinedSprite;
    public Sprite classroom1, classroom2, classroom3, classroom4;
    [SerializeField]
    Sprite preferLecture, hateLecture, masteredLecture, needPrerequisite;

    public void Start()
    {
        locked = true;
        full = false;
        assigned = false;
        maxedLevel = false;
        ready = false;
        empty = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        skillLecture = -1;

        ChangeRoomImage("Ruined");
    }

    public void loadRoomData(Room saved)
    {
        UnlockAs(saved.facility);
        for (int i = 1; i < saved.level; i++)
        {
            Upgrade();
        }
    }

    public void ChangeRoomImage(string fac, int level = 0)
    {
        switch (fac)
        {
            case "Ruined":
                gameObject.GetComponent<Image>().sprite = ruinedSprite;
                break;
            case "Classroom":
                if(level == 1)
                    gameObject.GetComponent<Image>().sprite = classroom1;
                else if (level == 2)
                    gameObject.GetComponent<Image>().sprite = classroom2;
                else if (level == 3)
                    gameObject.GetComponent<Image>().sprite = classroom3;
                else if (level == 4)
                    gameObject.GetComponent<Image>().sprite = classroom4;
                break;
        }  
    }

    private void CheckCapacity()
    {
        if (currentCapacity == maxCapacity)
        {
            full = true;
        }
        else
        {
            full = false;
        }

        if(currentCapacity == 0 && host == null)
        {
            empty = true;
        }
    }

    public void UnlockAs(string fac)
    {
        locked = false;
        facility = fac;
        ChangeRoomImage(fac, 1);
        level += 1;
        maxCapacity = 1;
    }

    public void Upgrade()
    {
        level += 1;
        maxCapacity += 1;

        CheckCapacity();
        Debug.Log("Room " + id.ToString() + ": " + facility.ToString() + " Is Now level: " + level.ToString());

        ChangeRoomImage(facility, level);
    }

    public void Remove()
    {
        facility = null;
        level = 0;
        locked = true;
        assigned = false;
        full = false;
        value = 0;
        empty = true;
        ChangeRoomImage("Ruined");
    }

    public void Downgrade()
    {
        level -= 1;
        maxCapacity -= 1;

        CheckCapacity();

        Debug.Log("Room " + id.ToString() + ": " + facility.ToString() + " Is Now level: " + level.ToString());
        ChangeRoomImage(facility, level);
    }

    public void Assign(Professor professor)
    {
        empty = false;
        assigned = true;
        host = professor;

        Debug.Log("Professor " + professor.name + " Added.");

        CheckCapacity();
        CheckReady();
        gameManager.CheckTurnButton();
    }

    public void UnAssign()
    {
        Debug.Log("Professor " + host.name + " Unassigned.");
        assigned = false;
        host = null;
        skillLecture = -1;

        int numStudents = students.Count;
        for (int i = 0; i < numStudents; i++)
        {
            students[0].UnAssign();
            RemoveStudent(students[0]);
        }

        GameObject studentButtonSlot = transform.GetChild(1).gameObject;
        numStudents = studentButtonSlot.transform.childCount;
        Debug.Log("Number of button(s) in room: " + numStudents.ToString());
        for (int i = 0; i < numStudents; i++)
        {
            Destroy(studentButtonSlot.transform.GetChild(i).gameObject);
        }

        CheckCapacity();
        CheckReady();
        gameManager.CheckTurnButton();
    }

    public bool IsAssigned()
    {
        return assigned;
    }

    private void CheckReady()
    {
        if(host != null && students.Count > 0)
        {
            foreach (Student stu in students)
            {
                if(!CheckPrerequisite(stu, skillLecture))
                {
                    ready = false;
                    return;
                }
            }
            ready = true;
        }
        else
        {
            ready = false;
        }
    }

    public bool IsReady()
    {
        return ready;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    public void AddStudent(Student student)
    {
        empty = false;
        currentCapacity += 1;
        CheckCapacity();
        students.Add(student);
        CheckReady();
        gameManager.CheckTurnButton();
        StudentIconChange(skillLecture);

        Debug.Log("Student " + student.name + " Added.");
        Debug.Log("Learn from:  " + host.name);
        Debug.Log("Capacity:" + currentCapacity.ToString() + " / " + maxCapacity.ToString());
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        currentCapacity -= 1;
        CheckCapacity();
        CheckReady();
        gameManager.CheckTurnButton();

        Debug.Log("Student " + student.name + " Removed.");
        Debug.Log("Capacity:" + currentCapacity.ToString() + " / " + maxCapacity.ToString());
    }

    public void SetLectureSkill(int skill)
    {
        foreach (Student student in students)
        {
            if(student != null)
            {
                CheckPrerequisite(student, skill);
            }     
        }
        skillLecture = skill;
        StudentIconChange(skillLecture);
        CheckReady();
        gameManager.CheckTurnButton();
    }
    /**/
    IEnumerator DelayedNotification(string notification, float delayTimer)
    {
        yield return new WaitForSeconds(delayTimer);
        notificationSystem.Notify(notification);
    }

    string notification;
    string turnSummary;
    public void Learn()
    {
        turnSummary = "Room ( " + id.ToString() + " ): " + facility + System.Environment.NewLine;
        //Debug.Log(turnSummary);//Checked Clear
        //Debug.Log(host.name);//host missing??!!!
        turnSummary += "Professor: " + host.name + System.Environment.NewLine;
        turnSummary += "Lecter Skill: " + gameStateSO.skillList[skillLecture].name + System.Environment.NewLine;

        string mastered = "Mastered Student(s): " + System.Environment.NewLine; 
        string learning = "Learning Student(s): " + System.Environment.NewLine;

        //notification = "";
        float delayTimer = .5f;
        foreach (Student stu in students)
        {
            if (CheckPrerequisite(stu, skillLecture))
            {
                if (stu.progressLeft[skillLecture] == 0)
                {
                    //notification += "Student" + stu.GetStudentID().ToString() + " has already learned: Skill" + skillLecture.ToString() + System.Environment.NewLine;
                    notification = stu.name + " has already learned: " + gameStateSO.skillList[skillLecture].name + System.Environment.NewLine;
                    StartCoroutine(DelayedNotification(notification, delayTimer));
                    delayTimer += notificationDelayInterval;
                    //notificationSystem.Notify(notification);
                    Debug.Log(notification);
                }
                else
                {
                    //notification += "Student" + stu.GetStudentID().ToString() + " is learning : Skill" + skillLecture.ToString() + System.Environment.NewLine;
                    notification = stu.name + " is learning : " + gameStateSO.skillList[skillLecture].name + System.Environment.NewLine;
                    StartCoroutine(DelayedNotification(notification, delayTimer));
                    delayTimer += notificationDelayInterval;
                    //notificationSystem.Notify(notification);
                    Debug.Log(notification);
                    int progress = 1;
                    if (stu.preferences[skillLecture] == 1)
                    {
                        int chance = randChance(100);
                        if (chance > 70)
                        {
                            //notification += "And he loves it! (progress + 1 as a bonus)" + System.Environment.NewLine;
                            notification = stu.name + " loves it! (progress + 1 as a bonus)" + System.Environment.NewLine;
                            StartCoroutine(DelayedNotification(notification, delayTimer));
                            delayTimer += notificationDelayInterval;
                            //notificationSystem.Notify(notification);
                            Debug.Log("Student " + stu.name + " Loves the lecture! :)" + System.Environment.NewLine);
                            progress += 1;
                        }
                        /*
                        player = -1;
                        if (minigame())
                        {
                            Debug.Log("Student " + stu.studentId.ToString() + " Loves the lecture! :)");
                            progress += 1;
                        }
                        */
                    }
                    else if (stu.preferences[skillLecture] == -1)
                    {
                        int chance = randChance(100);
                        if (chance > 70)
                        {
                            //notification += "But he hates it... (progress - 1 as a penalty)" + System.Environment.NewLine;
                            notification = stu.name + " hates it... (progress - 1 as a penalty)" + System.Environment.NewLine;
                            StartCoroutine(DelayedNotification(notification, delayTimer));
                            delayTimer += notificationDelayInterval;
                            //notificationSystem.Notify(notification);
                            Debug.Log("Student " + stu.name + " Hates the lecture! :(" + System.Environment.NewLine);
                            progress -= 1;
                        }
                    }
                    if (stu.progressLeft[skillLecture] > 0)
                    {
                        stu.progressLeft[skillLecture] = stu.progressLeft[skillLecture] - progress;
                        if (stu.progressLeft[skillLecture] <= 0)
                        {
                            stu.progressLeft[skillLecture] = 0;
                            //notification += "And He has Complete the course! Skill: " + skillLecture.ToString() + System.Environment.NewLine;
                            notification = stu.name + " has Complete the course!: " + gameStateSO.skillList[skillLecture].name + System.Environment.NewLine;
                            mastered += stu.name + System.Environment.NewLine;
                            StartCoroutine(DelayedNotification(notification, delayTimer));
                            delayTimer += notificationDelayInterval;
                            //notificationSystem.Notify(notification);
                            Debug.Log("Student " + stu.name + " has Complete the course! Skill: " + gameStateSO.skillList[skillLecture].name);
                        }
                        else
                        {
                            learning += stu.name + " (turns remaining: " + stu.progressLeft[skillLecture] + ") " + System.Environment.NewLine;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Student " + stu.id.ToString() + " has yet complete the prerequisite Skill: " + gameStateSO.skillList[skillLecture].prereqID.ToString());
            }
        }
        StudentIconChange(skillLecture);
        turnSummary += mastered + System.Environment.NewLine + learning + System.Environment.NewLine + "------------------------------------------------" + System.Environment.NewLine;
        //gameManager.GetComponent<Notification>().Notify(notification);
    }
    public string GetTurnSummary()
    {
        return turnSummary;
    }
    public string GetLearnNotification()
    {
        return notification;
    }

    public CanvasGroup minigameBoard;
    int player = -1;

    private bool CheckPrerequisite(Student stu, int skillIndex)
    {
        Skill skill = gameStateSO.skillList[skillIndex];
        int prerequisiteIndex = skill.prereqID-1;
        //Debug.Log(prerequisiteIndex);
        if(skill.prereqID == 0)
        {
            return true;
        }
        else if(stu.progressLeft[prerequisiteIndex] == 0)
        {
            return true;
        }
        else
        {
            ready = false;
            return false;
        }

    }

    private void StudentIconChange(int skillIndex)
    {
        GameObject studentButtonSlot = transform.GetChild(1).gameObject;
        Debug.Log(studentButtonSlot.name);
        Debug.Log(students.Count);
        for (int i = 0; i < students.Count; i++)
        {
            Student stu = students[i];
            Debug.Log(stu.name);
            GameObject stuButton = studentButtonSlot.transform.GetChild(i).gameObject;
            Image stuIcon = stuButton.transform.GetChild(0).GetChild(1).GetComponent<Image>();

            stuIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            if (CheckPrerequisite(stu, skillIndex))
            {
                if(stu.progressLeft[skillIndex] != 0)
                {
                    if(stu.preferences[skillIndex] == 1)
                    {
                        stuIcon.sprite = preferLecture;
                    }
                    else if (stu.preferences[skillIndex] == -1)
                    {
                        stuIcon.sprite = hateLecture;
                    }
                    else
                    {
                        stuIcon.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
                        stuIcon.sprite = null;
                    }
                }
                else
                {
                    stuIcon.sprite = masteredLecture;
                }
            }
            else
            {
                stuIcon.sprite = needPrerequisite;
            }
        }
    }
    private bool minigame()
    {
        minigameBoard.alpha = 1;
        minigameBoard.blocksRaycasts = true;

        if (player == -1)
        {
            
        }
        int AI = randChance(3);

        TMP_Text desc = minigameBoard.transform.GetChild(1).GetComponent<TMP_Text>();
        GameObject AIbutton = minigameBoard.transform.GetChild(2).gameObject;

        if(AI == 0)
        {
            AIbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Hammer";
            AIbutton.GetComponent<CanvasGroup>().alpha = 1;
        }
        else if(AI == 1)
        {
            AIbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Scissors";
            AIbutton.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            AIbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Paper";
            AIbutton.GetComponent<CanvasGroup>().alpha = 1;
        }


        if (player == 0 && AI == 1)
        {
            desc.text = "Congratulations! You Won!";
            return true;
        }
        else if (player == 1 && AI == 2)
        {
            desc.text = "Congratulations! You Won!";
            return true;
        }
        else if (player == 2 && AI == 0)
        {
            desc.text = "Congratulations! You Won!";
            return true;
        }
        else
        {
            desc.text = "Good Luck Next Time!";
            return false;
        }
    }

    public void playerMiniGameInput(int player)
    {
        this.player = player;
    }

    public int randChance(int range)
    {
        return Random.Range(0,range);
    }

    public bool IsFull()
    {
        return full;
    }

    public void AddValue(int price)
    {
        value += price;
    }

    public int GetValue()
    {
        return value;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool IsLocked()
    {
        return locked;
    }

    public bool IsMaxed()
    {
        return maxedLevel;
    }

    /* 
     public int no;
     public string type;
     public int level;
     public int maxCapa;
     public Professor teacher;
     public Skill skill;
     public List<(Student,int)> students;


     public Room(int no)
     {   
         this.no = no;
         this.type = "lecture";
         this.level = 0;
         this.maxCapa = 0;
         this.teacher = null;
         this.skill = null;
         this.students = new List<(Student,int)>();
     }

     public void buy(Player player){
         if(this.level == 0){
             player.point -= 500;
             this.level += 1;
             maxCapa += 1;
         } 
     }

     public void upgrade(Player player){
         if(this.level != 0){
             player.point -= 200+100*this.level;
             this.level += 1;
             maxCapa += 1;
         }
     }

     public int checkStudent(Student student){
         bool isAvail = false;
         if(this.skill.level==1)
             isAvail = true;
         foreach (Skill i in student.learnedSkills) {
             if(i.type == this.skill.type && i.level <= this.skill.level)
                 isAvail = true;
             }
         if(isAvail){
             if(student.preferedSkills[this.skill.no]==1)
                 return 1;
             else if(student.preferedSkills[this.skill.no]==0)
                 return 0;
             else if(student.preferedSkills[this.skill.no]==-1)
                 return -1;
         }
         return -2;
     }

     public void addStudent(Student student){
         int status = checkStudent(student);
         if(checkStudent(student) != -2 && students.length() < this.maxCapa){
             this.students.Add((student,status));
         }
     }

     public void skipTurn(){
         foreach ((Student,int) i in this.students) {
             i[0].skills[this.skill.no] -= 1-0.5*i[1];
         }
     }
     */
}