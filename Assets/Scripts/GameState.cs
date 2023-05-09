using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour{
    //Game State Attributes

    public int point = 0;
    public List<Student> studentList = new List<Student>();
    public  List<Professor> profList = new List<Professor>();
    public int cur_sem = 1;
    public Sprite studentArt, professorArt, roomArt;
    public GameManager roomManager;
    public List<Skill> skillList = new List<Skill>();
    

    // //Student Part
    // public string[] StudentTemplateName = new string[15]{'Somchai','Somying','Ramuel','Karl','Grimgor',
    //                                     'Emhyr','Paul','Zero','Yamada','Satoshi',
    //                                     'Don','Justine','Anzu','Arthur','Arnia'};
    // public boolean[] studentAvail = new boolean[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    // public int[][] prefSkill = {};


    // Constructor needed
    public Student StudentFactory(int id)
    {
        Student student = new Student(StudentTemplateName[id]);
        return student;
    }


    public List<Student> DraftStudentGenerator ()
    {
        List<Student>  draftStudent  = new List<Student>();
        int count = 5;
        while(count > 0){
            int rnd = Random.Range(0, 15);
            if(studentAvail[rnd]){
                Student student = StudentFactory(rnd);
                draftStudent.Add(student);
                studentAvail[rnd] = false;
                count -= 1 ;
            }
        }
        return draftStudent;
    }


    //Professor Part
    public string[] ProfessorTemplateName;
    public bool[] profAvail = new bool[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    

    //Skill Part
    public string[] SkillTemplateName = new string[];
    public int[] prereqID = new int[];
    public int[] turnsToComplete = new int[];

    //Constructor needed
    public Professor ProfessorFactory(int id)
    {
        Professor prof = new Professor(ProfessorTemplateName[id]);
        return prof;
    }

    public List<Professor> DraftProfGenerator ()
    {
        List<Professor>  draftProf  = new List<ProfessorFactory>();
        int count = 5;
        while(count > 0){
            int rnd = Random.Range(0, 15);
            if(profAvail[rnd]){
                Professor prof = ProfessorFactory(rnd);
                draftProf.Add(prof);
                profAvail[rnd] = false;
                count -= 1 ;
            }
        }
        return draftProf;
    }
    
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
