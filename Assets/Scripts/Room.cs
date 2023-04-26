using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int id;
    public string facility;
    private int level = 0;
    public int maxLevel = 4;
    private int value = 0;
    public int maxCapacity;
    private int currentCapacity;

    bool locked;
    bool assigned;
    bool full;
    bool maxedLevel;
    bool ready;

    Professors host;
    int skillLecture;
    List<Student> students = new List<Student>();
    GameManager gameManager;

    public Sprite ruinedSprite;
    public Sprite classroomSprite;

    public void Start()
    {
        locked = true;
        full = false;
        assigned = false;
        maxedLevel = false;
        ready = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        skillLecture = -1;

        ChangeRoomImage("Ruined");
    }

    public void ChangeRoomImage(string fac)
    {
        switch (fac)
        {
            case "Ruined":
                gameObject.GetComponent<Image>().sprite = ruinedSprite;
                break;
            case "Classroom":
                gameObject.GetComponent<Image>().sprite = classroomSprite;
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
    }

    public void UnlockAs(string fac)
    {
        locked = false;
        facility = fac;
        ChangeRoomImage(fac);
        level += 1;
        maxCapacity = 1;
    }

    public void Upgrade()
    {
        level += 1;
        maxCapacity += 1;

        CheckCapacity();
        Debug.Log("Room " + id.ToString() + ": " + facility.ToString() + " Is Now level: " + level.ToString());
    }

    public void Remove()
    {
        facility = null;
        level = 0;
        locked = true;
        assigned = false;
        full = false;
        value = 0;
        ChangeRoomImage("Ruined");
    }

    public void Downgrade()
    {
        level -= 1;
        maxCapacity -= 1;

        CheckCapacity();

        Debug.Log("Room " + id.ToString() + ": " + facility.ToString() + " Is Now level: " + level.ToString());
    }

    public void Assign(Professors professor)
    {
        assigned = true;
        host = professor;

        Debug.Log("Professor " + professor.GetProfessorId().ToString() + " Added.");
    }

    public void UnAssign()
    {
        Debug.Log("Professor " + host.GetProfessorId().ToString() + " Unassigned.");
        assigned = false;
        host = null;
        skillLecture = -1;
    }

    public bool IsAssigned()
    {
        return assigned;
    }

    private void CheckReady()
    {
        if(host != null && students.Count > 0)
        {
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

    public void AddStudent(Student student)
    {
        currentCapacity += 1;
        CheckCapacity();
        students.Add(student);
        CheckReady();
        gameManager.CheckTurnButton(gameObject.GetComponent<Room>());

        Debug.Log("Student " + student.id.ToString() + " Added.");
        Debug.Log("Capacity:" + currentCapacity.ToString() + " / " + maxCapacity.ToString());
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        currentCapacity -= 1;
        CheckCapacity();
        CheckReady();
        gameManager.CheckTurnButton(gameObject.GetComponent<Room>());

        Debug.Log("Student " + student.id.ToString() + " Removed.");
        Debug.Log("Capacity:" + currentCapacity.ToString() + " / " + maxCapacity.ToString());
    }

    public void SetLectureSkill(int skill)
    {
        skillLecture = skill;
    }

    string notification;
    public void Learn()
    {
        notification = "";
        foreach (Student stu in students)
        {
            if (stu.progressLeft[skillLecture] == 0)
            {
                notification += "Student" + stu.GetStudentID().ToString() + " has already learned: Skill" + skillLecture.ToString() + System.Environment.NewLine;
                Debug.Log(notification);
            }
            else
            {
                notification += "Student" + stu.GetStudentID().ToString() + " is learning : Skill" + skillLecture.ToString() + System.Environment.NewLine;
                Debug.Log(notification);
                int progress = 1;
                if (stu.GetPref()[skillLecture] == 1)
                {
                    int chance = randChance(100);
                    if (chance > 70)
                    {
                        notification += "And he loves it! (progress + 1 as a bonus)" + System.Environment.NewLine;
                        Debug.Log("Student " + stu.id.ToString() + " Loves the lecture! :)" + System.Environment.NewLine);
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
                else if (stu.GetPref()[skillLecture] == -1)
                {
                    int chance = randChance(100);
                    if (chance > 70)
                    {
                        notification += "But he hates it... (progress - 1 as a penalty)" + System.Environment.NewLine;
                        Debug.Log("Student " + stu.id.ToString() + " Hates the lecture! :(" + System.Environment.NewLine);
                        progress -= 1;
                    }
                }
                if (stu.progressLeft[skillLecture] > 0)
                {
                    stu.progressLeft[skillLecture] = stu.progressLeft[skillLecture] - progress;
                    if (stu.progressLeft[skillLecture] <= 0)
                    {
                        stu.progressLeft[skillLecture] = 0;
                        notification += "And He has Complete the course! Skill: " + skillLecture.ToString() + System.Environment.NewLine;
                        Debug.Log("Student " + stu.id.ToString() + " has Complete the course! Skill: " + skillLecture.ToString());
                    }
                }
            } 
        }
        //gameManager.SetLearnNotification();
        //gameManager.GetComponent<Notification>().Notify(notification);
    }

    public string GetLearnNotification()
    {
        return notification;
    }

    public CanvasGroup minigameBoard;
    int player = -1;

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
     public Professors teacher;
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