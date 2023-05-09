using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : MonoBehaviour
{
    public string name;

    public int n_subjects = 10;
    public int id;

    public int cost;

    public int[] subjects;

    public bool assigned;

    CanvasGroup canvasGroup;

    public void Awake() {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public Professor(int id, string name) {
        this.name = name;
        this.id = id;
    }

    public static Professor CreateComponent(GameObject where, string name, int id) {
        Professor myC = where.AddComponent<Professor>();
        myC.id = id;
        myC.cost = myC.getCost(myC.id);
        myC.name = name+$"\n{myC.cost}";
        myC.subjects = myC.getSubjects(id);
        myC.assigned = false;
        
        return myC;
    }

    public int getCost(int id) {
        if (id < 4) {
            return 100;
        } 
        else {
            return 200;
        }
    }

    public void Assign()
    {
        assigned = true;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void UnAssign()
    {
        assigned = false;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void CopyProfessor(Professor p)
    {
        this.id = p.id;
        this.cost = p.cost;
        this.n_subjects = p.n_subjects;
        this.name = p.name;
        this.subjects = p.subjects;
    }

    public int[] getSubjects(int sid) {

        // default like 3, doesnt like 3

        // v1 : hard code
        int[][] fixed_subjects = {
            new int[] {1,1,1,0,0,0,0,0,0,0},
            new int[] {0,0,0,1,1,1,0,0,0,0},
            new int[] {0,0,0,0,0,0,1,1,0,0},
            new int[] {0,0,0,0,0,0,0,0,1,1},
            new int[] {1,0,0,1,0,0,1,0,0,0},
            new int[] {0,0,1,0,0,1,0,1,0,0},
            new int[] {0,1,0,0,1,0,0,0,1,1},
        };

        if (sid < 7) {
            // pick from fixed
            return fixed_subjects[sid];
        } else {
            // random
            int[] init = {1,0,-1,1,0,-1,1,-1,-1,0};
            int[] subject = new int[n_subjects];

            int[] counter = {0,0,0};
            int[] maxCounter = {3,4,3};

            int tmp;

            for (int i=0; i<n_subjects; i++) {
                do {
                    tmp = Random.Range(-1, 2);
                } while (counter[tmp + 1] >= maxCounter[tmp + 1]);
                subject[i] = tmp;
                counter[tmp + 1] += 1;
            }

            return subject;
        }
    }


}
