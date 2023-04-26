using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using TMPro;


public class StudentCard : MonoBehaviour //, IPointerClickHandler
{

    public Student student;

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

    public static StudentCard CreateComponent(GameObject where, Student student) {
        StudentCard myC = where.AddComponent<StudentCard>();
        myC.student = student;
        Debug.Log(myC.student.id);
        return myC;
    }

    public void Awake() {
        this.isSelectedProperties = false;
    }

    void Start() {
        
    }

    public void ChangeColor(){
        // Debug.Log("change color");
        if (isSelected){
            this.GetComponentInChildren<Image>().color = Color.green;
        } else {
            this.GetComponentInChildren<Image>().color = Color.red;
        }
    }

    public void OnClick() {
        isSelectedProperties = !this.isSelected;
        Debug.Log($"student id {this.student.name} isSelected {this.isSelected}");
        // set is Selected = True
    }
}
