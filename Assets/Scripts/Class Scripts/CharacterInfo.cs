using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
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
