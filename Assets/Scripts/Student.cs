using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// using Random = System.Random;

public class Skill {
    public int id;
    public int type;
    public int level;
    public Skill(int id, int type, int level){
        this.id = id;
        this.type = type;
        this.level = level;
    }
}

public class Student : MonoBehaviour
{
    // student info
    public string name;
    public int id;
    public int year;

    // hp
    public int maxHP;
    public int currentHP;

    bool isAssigned;
    CanvasGroup canvasGroup;
    
    // skills -> list
    public int[] preferences;
    public int[] progressLeft;

    public List<Skill> learnedSkills;

    // random
    // var rnd = new System.Random();

    // info from GameManager
    int n_skills;

    public static Student CreateComponent(GameObject where, string name, int id) {
        Student myC = where.AddComponent<Student>();
        myC.name = name;
        myC.id = id;
        return myC;
    }

    public void CopyStudent(Student s) {
        this.name = s.name;
        this.id = s.id;
        this.year = s.year;
        this.maxHP = s.maxHP;
        this.currentHP = s.currentHP;
        this.isAssigned = s.isAssigned;
        this.canvasGroup = s.canvasGroup;
        this.preferences = s.preferences;
        this.progressLeft = s.progressLeft;
        this.learnedSkills = s.learnedSkills;
        this.n_skills = s.n_skills;
    }

    public void Awake() {
        this.year = 1;
        this.maxHP = 100;
        this.currentHP = this.maxHP;
        
        this.learnedSkills = new List<Skill>() {
            new Skill(1,1,1),
        };

        // hard code : must be edited later
        this.n_skills = 10;

        this.preferences = this.GetPref();

        this.progressLeft = new int[] {2,4,8,2,4,8,3,6,3,3};
        // progressLeft = GameManager.get...
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isAssigned = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int[] GetPref() {

        // default like 3, doesnt like 3

        int sid = this.id;

        // v1 : hard code
        int[][] fixed_prefs = {
            new int[] {1,1,1,0,0,0,-1,-1,-1,0},
            new int[] {0,-1,-1,1,1,0,1,0,0,-1},
            new int[] {1,1,0,-1,-1,-1,0,0,1,0},
            new int[] {1,0,-1,1,-1,0,1,-1,0,0},
            new int[] {-1,-1,-1,0,0,0,0,1,1,1},
        };

        if (sid < 5) {
            // pick from fixed
            return fixed_prefs[sid];
        } else {
            // random
            int[] init = {1,0,-1,1,0,-1,1,-1,-1,0};
            int[] pref = new int[n_skills];

            int[] counter = {0,0,0};
            int[] maxCounter = {3,4,3};

            int tmp;

            for (int i=0; i<n_skills; i++) {
                do {
                    tmp = Random.Range(-1, 2);
                } while (counter[tmp + 1] >= maxCounter[tmp + 1]);
                pref[i] = tmp;
                counter[tmp + 1] += 1;
            }

            return pref;
        }

    }

    public int GetStudentID() {
        return this.id;
    }

    public void Assign()
    {
        isAssigned = true;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void UnAssign()
    {
        isAssigned = false;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public bool IsAssigned()
    {
        return isAssigned;
    }

     public int[] GetStudentProgress()
    {
        return progressLeft;
    }

}