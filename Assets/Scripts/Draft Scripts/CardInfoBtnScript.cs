using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using TMPro;

public class CardInfoBtnScript : MonoBehaviour
{

    Button button;
    bool isStudent;

    List<string> info;

    Student student;
    Professor professor;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });

        student = this.GetComponentInParent<Student>();
        professor = this.GetComponentInParent<Professor>();

        if (student) {
            isStudent = true;
        } // assume either student or prof
    }

    public void ButtonClicked() {

        if (isStudent) {
            
            Debug.Log("clicked student info");
            info = new List<string>();
            info.Add($"Name : {student.name}");
            info.Add($"Year : {student.year}");
            info.Add($"Max HP : {student.maxHP}");
            
            string preferedSkill = "";
            string unpreferredSkill = "";
            for (int i = 0; i<student.preferences.Length; i++) {
                if (student.preferences[i] == 1){
                    preferedSkill += i.ToString()+", ";
                }
                else if (student.preferences[i] == -1){
                    unpreferredSkill += i.ToString()+", ";
                }
            }
            info.Add($"Prefered skills : {preferedSkill}");
            info.Add($"Unpreferred skills : {unpreferredSkill}");

            foreach (string e in info) {
                Debug.Log(e);
            }
            
            GameObject.Find("DraftArea").GetComponent<DraftPanel>().CardInfoPopup.GetComponentInChildren<CardInfoTxtAreaScript>().infoTxtProperties = info;
            GameObject.Find("DraftArea").GetComponent<DraftPanel>().CardInfoPopup.SetActive(true);

        } else {
            Debug.Log("clicked prof info");
        }
    }
}
