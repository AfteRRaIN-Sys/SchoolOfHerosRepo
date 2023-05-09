using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using TMPro;


public class ProfessorCard : MonoBehaviour
{

    public Professor professor;

    // draft phrase
    public bool isSelected;

    public bool isSelectedProperties {
        get {
            return isSelected;
        }
        set {
            isSelected = value;
            this.ChangeColor();       
        }
    }

    public static ProfessorCard CreateComponent(GameObject where, Professor professor) {
        ProfessorCard myC = where.AddComponent<ProfessorCard>();
        myC.professor = professor;
        Debug.Log(myC.professor.id);
        return myC;
    }

    public void Awake() {
        this.isSelectedProperties = false;
    }

    void Start() {
        
    }

     public void ChangeColor(){
        // Debug.Log("change color");
        Image img = this.GetComponentInChildren<Image>();
        if (isSelected){
            // img.color = Color.green;
            img.color = new Color(1f,1f,1f,1f);;
            // .a = 0.2;
        } else {
            // img.color = Color.red;
            img.color = new Color(1f,1f,1f,0.4f);
        }
    }

    public void OnClick() {
        int left_points = GameObject.Find("DraftArea").GetComponent<DraftPanel>().point;
        // if (this.isSelected == false || left_points >= this.professor.cost) {
            isSelectedProperties = !this.isSelected;
            Debug.Log($"Professor id {this.professor.name} isSelected {this.isSelected}");
            Debug.Log($"Professor cost {this.professor.id}, {this.professor.cost}");
            // set is Selected = True
        // }
    }
}
