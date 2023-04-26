using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string tipsToShow;

    public GameObject objectTips;
    public string objectName;
    public int[] skill_progresses;
    public int[] preferred_skills;
    public int health;

    public bool isStudent;

    public float timeToShow = 0.5f;

    public void SetObject(GameObject gameObject) {
        objectTips = gameObject;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        tipsToShow = "";
        if (isStudent)
        {
            Student studentInfo = objectTips.GetComponent<Student>();
            skill_progresses = studentInfo.GetStudentProgress();
            preferred_skills = studentInfo.GetPref();

            tipsToShow += objectName + System.Environment.NewLine;
            string skills = "Learned: ";
            string remainings = "Turns left to complete: ";
            string likes = "Likes: ";
            string dislikes = "Dislikes: ";
            for (int i = 0; i < preferred_skills.Length; i++)
            {
                if (skill_progresses[i] == 0)
                {
                    skills += i.ToString() + ", ";
                }
                remainings += skill_progresses[i].ToString() + ", ";
                if (preferred_skills[i] == 1)
                {
                    likes += i.ToString() + ", ";
                }
                else if (preferred_skills[i] == -1)
                {
                    dislikes += i.ToString() + ", ";
                }
            }
            tipsToShow += skills + System.Environment.NewLine;
            tipsToShow += remainings + System.Environment.NewLine;
            tipsToShow += likes + System.Environment.NewLine;
            tipsToShow += dislikes + System.Environment.NewLine;
            tipsToShow += health;
        }
        else if (!isStudent)
        {
            Professor profInfo = objectTips.GetComponent<Professor>();
            preferred_skills = profInfo.subjects;
            tipsToShow += objectName + System.Environment.NewLine;
            string skills = "Can teach: ";
            for (int i = 0; i < preferred_skills.Length; i++)
            {
                if (preferred_skills[i] == 1)
                {
                    skills += i.ToString() + ", ";
                }
            }
            tipsToShow += skills + System.Environment.NewLine;
        }
        StartCoroutine(StartTimer());
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverTipsManager.OnMouseLoseFocus();
    }

    private void ShowMessages()
    {
        Vector2 screenPoint = Input.mousePosition;
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(screenPoint);

        HoverTipsManager.OnMouseHover(tipsToShow, cursorPosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToShow);

        ShowMessages();
    }
}
