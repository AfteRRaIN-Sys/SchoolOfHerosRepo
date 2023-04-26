using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    int playerMoney;

    List<int> studentsID = new List<int>();
    List<Student> studentsData = new List<Student>();

    public void SaveStudentData(Student std)
    {
        studentsData.Add(std);
        studentsID.Add(std.GetStudentID());
    }

    public void SaveMoneyData(int money)
    {
        playerMoney = money;
    }

    public List<Student> LoadStudentData()
    {
        return studentsData;
    }

    public List<int> LoadStudentID()
    {
        return studentsID;
    }
    public int LoadMoneyData()
    {
        return playerMoney;
    }
}
