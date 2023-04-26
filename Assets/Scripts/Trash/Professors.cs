using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professors : MonoBehaviour
{ 
    public int professorId;
    public string professorName;
    public int[] subject = new int[10];
    bool isAssigned;
    CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isAssigned = false;
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

    public int GetProfessorId()
    {
        return professorId;
    }

    public string GetProfessorName()
    {
        return professorName;
    }

    public int[] GetProfessorSubjects()
    {
        return subject;
    }

    
}
