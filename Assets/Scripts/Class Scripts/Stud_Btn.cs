using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stud_Btn : Game_Button
{
    GameManager gameManager;
    Student student;
    Room room;
    int studId;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cg = GetComponent<CanvasGroup>();
    }

    public void Onclick()
    {
        student.UnAssign();
        room.RemoveStudent(student);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void SetText(string text)
    {
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
    }

    public void setStudent(Student student)
    {
        this.student = student;
    }

    public void setStudentRoom(Room room)
    {
        this.room = room;
    }

    public void SetId(int id)
    {
        studId = id;
    }

    public int GetProfessorId()
    {
        return studId;
    }
}
