using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField]
    GameStateSO gameStateSO;
    [SerializeField]
    Sprite studentMaleSprite, studentFemaleSprite, professorSprite,  bossSprite1, bossSprite2;
    [SerializeField]
    Sprite studentInfoBackground, professorInfoBackground, bossInfoBackground;

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

        card.GetComponent<Image>().sprite = studentMaleSprite;

        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = student.name;

        card.transform.GetChild(0).GetComponent<Image>().sprite = studentInfoBackground;

        TMP_Text pref = infoPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text summary = card.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        string likes = "LIKE: ";
        string dislikes = "DISLIKE: ";
        
        Skill skill;
        int prevSkillType = -1;
        int curSkillType;
        for (int i = 0; i < student.preferences.Length; i++)
        {
            skill = gameStateSO.skillList[i];
            curSkillType = skill.type;
            if (student.preferences[i] == 1)
            {
                if(prevSkillType != curSkillType)
                {
                    likes += System.Environment.NewLine;
                }
                likes += skill.name + ", ";
            }
            else if (student.preferences[i] == -1)
            {
                if (prevSkillType != curSkillType)
                {
                    dislikes += System.Environment.NewLine;
                }
                dislikes += skill.name + ", ";
            }
            prevSkillType = curSkillType;
        }

        likes = likes.Remove(likes.Length - 2);
        dislikes = dislikes.Remove(dislikes.Length - 2);
        pref.text = likes + System.Environment.NewLine + System.Environment.NewLine + dislikes;

        string learning = "Learning: ";
        string mastered = "Mastered: ";

        prevSkillType = -1;
        for (int i = 0; i < student.progressLeft.Length; i++)
        {
            skill = gameStateSO.skillList[i];
            curSkillType = skill.type;
            if(student.progressLeft[i] == 0)
            {
                if (prevSkillType != curSkillType)
                {
                    mastered += System.Environment.NewLine;
                }
                mastered += skill.name + ", ";
            }
            else if(student.progressLeft[i] < skill.turnsToComplete)
            {
                if (prevSkillType != curSkillType)
                {
                    learning += System.Environment.NewLine;
                }
                learning += skill.name + "(" + student.progressLeft[i] + "), ";
            }
            prevSkillType = curSkillType;
        }
        mastered = mastered.Remove(mastered.Length - 2);
        learning = learning.Remove(learning.Length - 2);
        summary.text = mastered + System.Environment.NewLine + learning;
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

        card.GetComponent<Image>().sprite = professorSprite;

        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = professor.name;

        card.transform.GetChild(0).GetComponent<Image>().sprite = professorInfoBackground;

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

        card.transform.GetChild(0).GetComponent<Image>().sprite = bossInfoBackground;

        if ((gameStateSO.cur_sem % 2) == 1)
        {
            card.GetComponent<Image>().sprite = bossSprite1;
        }
        else
        {
            card.GetComponent<Image>().sprite = bossSprite2;
        }
        TMP_Text name = card.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        name.text = "Boss";

    }

    public void ClassroomCloseInfo()
    {
        GameObject existedObject = GameObject.Find("CharacterInfoWindow(Clone)");
        if (existedObject == null)
        {
            existedObject = GameObject.Find("BossInfoWindow(Clone)");
        }

        if (existedObject != null)
        {
            Destroy(existedObject);
        }
    }
}
