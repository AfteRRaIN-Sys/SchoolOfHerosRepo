using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    
    //Game State Attributes

    public GameStateSO gameStateSO;

    // public static int point = 700;
    // public static int money = 0;
    // public List<Student> studentList = new List<Student>();
    // public  List<Professor> profList = new List<Professor>();
    // public int cur_sem = 1;
    // // public Sprite studentArt, professorArt, roomArt;
    
    // public GameManager roomManager;
    // public List<Skill> skillList = new List<Skill>();

    // //Student Part
    public string[] StudentTemplateName = {"Somchai","Somying","Ramuel","Karl","Grimgor",
                                         "Emhyr","Paul",
                                          "Zero","Yamada","Satoshi",
                                        "Don","Justine","Anzu","Arthur",
                                        "Arnia"};
     public bool[] studentAvail = {true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
     public int[][] prefSkill = {new int[7]{0,1,2,3,4,5,6},
                                 new int[7]{0,1,2,3,7,8,9},
                                 new int[7]{0,1,2,3,10,11,12},
                                 new int[7]{0,1,2,3,20,21,22},
                                 new int[7]{0,1,2,3,31,32,33},
                                 new int[7]{0,1,2,3,4,5,6},
                                 new int[7]{0,1,2,3,7,8,9},
                                 new int[7]{0,1,2,3,10,11,12},
                                 new int[7]{0,1,2,3,20,21,22},
                                 new int[7]{0,1,2,3,31,32,33},
                                 new int[7]{0,1,2,3,4,5,6},
                                 new int[7]{0,1,2,3,7,8,9},
                                 new int[7]{0,1,2,3,10,11,12},
                                 new int[7]{0,1,2,3,20,21,22},
                                 new int[7]{0,1,2,3,31,32,33}};
    public int[][] hateSkill = {new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16},
                                 new int[7]{10,11,12,13,14,15,16}};

    // Constructor needed
    public Student StudentFactory(int id)
    {
        int[] pref = new int[38];
        for(int i =0;i<7;i++){
            pref[prefSkill[id][i]] = 1;
        }
        for(int i =0;i<7;i++){
            pref[hateSkill[id][i]] = -1;
        }
        Student student = new Student(id, StudentTemplateName[id], pref);
        return student;
    }


    public List<Student> DraftStudentGenerator ()
    {
        Debug.Log("start gen");
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
        foreach (Student s in draftStudent) {
            Debug.Log(s.id);
        }
        return draftStudent;
    }


    //Professor Part
    public string[] ProfessorTemplateName = {"Sword_Master","Assassin_Master","Guard_Master","Couter_Master","Heal_Master","Priest_Master","Witch_Master"};
    public bool[] profAvail = {true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
    public int[] profCost = {100,200,100,100,200,100,100,200,100};
    public int[][] teachSkill = {new int[6]{0,1,2,3,4,5},
                                 new int[6]{6,7,8,9,10,11},
                                 new int[6]{12,13,14,15,16,17},
                                 new int[6]{18,19,20,21,22,23},
                                 new int[6]{24,25,26,27,28,29},
                                 new int[6]{24,25,26,27,28,29},
                                 new int[6]{24,25,26,27,28,29}
                                };

    //Skill Part
    public string[] SkillTemplateName = new string[38];
    public int[] prereqID = new int[38];
    public int[] turnsToComplete = new int[38];

    //Constructor needed
    public Professor ProfessorFactory(int id)
    {
        int[] subjects = new int[38];
        for(int i =0;i<6;i++){
            subjects[teachSkill[id][i]] = 1;
        }
        Professor prof = new Professor(id, ProfessorTemplateName[id], profCost[id], subjects);
        return prof;
    }

    public List<Professor> DraftProfGenerator ()
    {
        List<Professor> draftProf = new List<Professor>();
        int count = 5;
        while(count > 0){
            int rnd = Random.Range(0, 7);
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

// public class Skill {
//     public string name;
//     public int id;
//     public int type;
//     public string description;
//     public int prereqID;
//     public int level;
//     public int turnsToComplete;
//     public string[] skillNames = {"Attack I","Attack II","Attack III","Attakc IV","Critical Chance",
//                         "Life Steal I","Life Steal II","Poison Cloating","Bleeding Effect", "Open Wound",
//                         "Guard I","Guard II","Guard III","Guard IV","Evade","Reflect Damage","Counter Attack",
//                         "Absorb Damage I","Absorb Damage II"," Absorb Damage III",
//                         "Buff I","Buff II","Buff III","Buff IV","Team Buff I","Team Buff II","Team Buff III",
//                         "Debuff I","Debuff II","Debuff III","Debuff IV","Heal I","Heal II","Revive I","Revive II","Team Heal I", "Team Heal II"};
//     public int[] preReqs = {0,1,2,3,3,2,6,1,8,9,0,11,12,13,11,15,16,11,18,19,0,21,22,23,21,25,26,0,28,29,30,0,32,33,34,35,33,37};
//     public int[] skillLevels = {1,2,3,4,4,3,4,2,3,4,1,2,3,4,2,3,4,2,3,4,1,2,3,4,2,3,4,1,2,3,4,1,2,3,4,3,4};

//     public Skill(int id, int level)
//     {
//         this.name = this.skillNames[id-1];
//         this.id = id;
//         if(id <= 10)
//             this.type = 1;
//         else if(id <=20)
//             this.type = 2;
//         else if(id <=27)
//             this.type = 3;
//         else if(id<= 31)
//             this.id = 4;
//         else    
//             this.id = 5;

//         this.level = this.skillLevels[id-1];
//         this.prereqID = this.preReqs[id-1];
//         this.description = "test";
//         this.turnsToComplete = this.level * 2;
//     }
// }
