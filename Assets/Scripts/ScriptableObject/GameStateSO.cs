using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateSO : ScriptableObject{
    //Game State Attributes

    [SerializeField]
    public int point{ get; set; }
    public int money{ get; set; }
    public List<Student> slctTable{get; set;}
    public List<Student> studentList{ get; set; }
    public  List<Professor> professorList{ get; set; }
    public List<Room> roomList { get; set; }
    public int cur_sem{ get; set; }
    // public Sprite studentArt, professorArt, roomArt;
    public bool newGame { get; set; }

    
    public List<Skill> skillList{ get; set; }
    public int[] progressLeftTemplate{ get; set; }

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