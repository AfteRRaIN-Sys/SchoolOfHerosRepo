using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateSO : ScriptableObject{
    //Game State Attributes

    [SerializeField]
    public int point{ get; set; }
    public int money{ get; set; }
    public List<Student> studentList{ get; set; }
    public  List<Professor> professorList{ get; set; }
    public int cur_sem{ get; set; }
    // public Sprite studentArt, professorArt, roomArt;

    public GameManager roomManager;
    // public List<Skill> skillList{ get; set; }

    // //Student Part
    public string[] StudentTemplateName{ get; set; }
     public bool[] studentAvail{ get; set; }
     public int[][] prefSkill{ get; set; }

    //Professor Part
    public string[] ProfessorTemplateName{ get; set; }
    //public boolean[] profAvail{ get; set; }
    public bool[] profAvail{ get; set; }

    //Skill Part
    public string[] SkillTemplateName{ get; set; }
    public int[] prereqID{ get; set; }
    public int[] turnsToComplete{ get; set; }    
}

// public class Skill
// {
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
    
// }
