using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour{
    //Game State Attributes

    public int point = 0;
    public List<Student> studentList = {};
    public  List<Professor> profList = {};
    public int cur_sem = 1;
    public Sprite studentArt, professorArt, roomArt;
    public GameManager roomManager;
    public List<Skill> = {};
    

    // //Student Part
    // public string[] StudentTemplateName = new string[15]{'Somchai','Somying','Ramuel','Karl','Grimgor',
    //                                     'Emhyr','Paul','Zero','Yamada','Satoshi',
    //                                     'Don','Justine','Anzu','Arthur','Arnia'};
    // public boolean[] studentAvail = new boolean[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    // public int[][] prefSkill = {};


    // Constructor needed
    public Student StudentFactory(int id)
    {
        Student student = Student(StudentTemplateName[id]);
        return Student;
    }


    public List<Student> DraftStudentGenerator ()
    {
        List<Student>  draftStudent  = new List<Student>();
        int count = 5;
        while(count > 0){
            int rnd = Random.Range(0, 15);
            if(studentAvail[rnd]){
                student = StudentFactory(rnd);
                draftStudent.add(student);
                studentAvail[rnd] = false;
                count -= 1 ;
            }
        }
        return draftStudent;
    }


    //Professor Part
    public string[] ProfessorTemplateName;
    public boolean[] profAvail = new boolean[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    

    //Skill Part
    public string[] SkillTemplateName = new string[];
    public int[] prereqID = new int[];
    public int[] turnsToComplete = new int[];

    //Constructor needed
    public Professor ProfessorFactory(int id)
    {
        Professor prof = Professor(ProfessorTemplateName[id]);
        return prof;
    }

    public List<Professor> DraftProfGenerator ()
    {
        List<Professor>  draftProf  = new List<ProfessorFactory>();
        int count = 5;
        while(count > 0){
            int rnd = Random.Range(0, 15);
            if(profAvail[rnd]){
                prof = ProfessorFactory(rnd);
                draftProf.add(prof);
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
