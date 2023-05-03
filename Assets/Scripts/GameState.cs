using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //Game State Attributes
    /*
    - point
    - studentList
    - profList
    - availRoomList
    - current Semester
    */

    public int points;
    public List<Student> studentsList;
    public List<Professor> professorList;
    public List<Skill> skillTemplateList;
    public Sprite studentArt, professorArt, roomArt;
    public int currentSemester;
    public GameManager roomManager;
    

    //Student Part
    public string[] StudentTemplateName;

    /* Constructor needed
    public Student StudentFactory(int id)
    {

    }
    */

    public void DraftStudentGenerator ()
    {
        //random set of students
        //construct through factory
        //feed it to DraftArea
    }


    //Professor Part
    public string[] ProfessorTemplateName;
    
    public void DraftProfessorGenerator()
    {
        //random set of professors
        //construct through factory
        //feed it to DraftArea
    }

    //Skill Part
    public string[] SkillTemplateName;

    /* Constructor needed
    public Professor ProfessorFactory(int id)
    {

    }
    */
}

public class Skill
{
    public int id;
    public int type;
    public string description;
    public int prereqID;
    public int level;
    public int turnsToComplete;

    public Skill(int id, int type, int level)
    {
        this.id = id;
        this.type = type;
        this.level = level;
    }
}
