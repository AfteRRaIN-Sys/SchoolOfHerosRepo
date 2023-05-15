using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField]
    GameStateSO gameStateSO;
    public void ClassroomShowStudentInfo(GameObject infoPrefab)
    {
        GameObject existedObject = GameObject.Find("CharacterInfoWindow(Clone)");
        if(existedObject != null)
        {
            Destroy(existedObject);
        }

        Student student = transform.parent.gameObject.GetComponent<Student>();
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameObject infoPanel = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, canvas.transform);
        GameObject card = infoPanel.transform.GetChild(1).gameObject;
        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = student.name;
        TMP_Text pref = card.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        string likes = "LIKE: ";
        string dislikes = "DISLIKE: ";
        for (int i = 0; i < student.preferences.Length; i++)
        {
            if (student.preferences[i] == 1)
            {
                likes += gameStateSO.skillList[i].name + ", ";
            }
            else if (student.preferences[i] == -1)
            {
                dislikes += gameStateSO.skillList[i].name + ", ";
            }
        }
        likes = likes.Remove(likes.Length - 2);
        dislikes = dislikes.Remove(dislikes.Length - 2);
        pref.text = likes + System.Environment.NewLine + dislikes;

    }

    public void ClassroomShowProfessorInfo(GameObject infoPrefab)
    {
        GameObject existedObject = GameObject.Find("CharacterInfoWindow(Clone)");
        if (existedObject != null)
        {
            Destroy(existedObject);
        }

        Professor professor = transform.parent.gameObject.GetComponent<Professor>();
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameObject infoPanel = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, canvas.transform);
        GameObject card = infoPanel.transform.GetChild(1).gameObject;
        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = professor.name;

        TMP_Text pref = card.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        string subjects = "SUBJECT: " + System.Environment.NewLine;
        for (int i = 0; i < professor.subjects.Length; i++)
        {
            if (professor.subjects[i] == 1)
            {
                subjects += gameStateSO.skillList[i].name + ", ";
            }
        }
        subjects = subjects.Remove(subjects.Length - 2);
        pref.text = subjects;
    }

    public void ClassroomShowBossInfo(GameObject infoPrefab)
    {
        GameObject existedObject = GameObject.Find("CharacterInfoWindow(Clone)");
        if (existedObject != null)
        {
            Destroy(existedObject);
        }

        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameObject infoPanel = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, canvas.transform);
        GameObject card = infoPanel.transform.GetChild(1).gameObject;
        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = "Boss";

    }

    public void ClassroomCloseInfo()
    {
        GameObject existedObject = GameObject.Find("CharacterInfoWindow(Clone)");
        if (existedObject != null)
        {
            Destroy(existedObject);
        }
    }
}
