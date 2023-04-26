using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    [SerializeField]
    PlayerData player;

    public void PrintStudentInfos()
    {
        List<Student> studentList = player.LoadStudentData();

        foreach (Student student in studentList)
        {
            Debug.Log(student.GetStudentID());
        }
            
    }
}
