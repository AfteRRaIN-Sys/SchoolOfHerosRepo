using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prof_Btn : Game_Button
{
    GameManager gameManager;
    Professors professor;
    Room room;
    int profId;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cg = GetComponent<CanvasGroup>();
    }

    public void Onclick()
    {
        professor.UnAssign();
        room.UnAssign();
        Destroy(gameObject.transform.parent.GetChild(1).gameObject);
        Destroy(gameObject);
    }

    public void SetText(string text)
    {
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        
    }

    public void setProfessor(Professors professor)
    {
        this.professor = professor;
    }

    public void setProfessorRoom(Room room)
    {
        this.room = room;
    }

    public void SetId(int id)
    {
        profId = id;
    }

    public int GetProfessorId()
    {
        return profId;
    }
}
