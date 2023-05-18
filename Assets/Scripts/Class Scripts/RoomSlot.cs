using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomSlot : MonoBehaviour, IDropHandler
{
    public Room room;
    public int roomSlotId;
    public GameObject profButtonObject;
    public GameObject studentButtonObject;
    public GameObject dropdownObject;

    void Start()
    {
        room = GetComponent<Room>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject dropped = eventData.pointerDrag;

            if (dropped.TryGetComponent<Student>(out Student student))
            {
                if (room.IsLocked())
                {
                    Debug.Log("This room is not yet Unlocked. Please use another room in the meanwhile.");
                    return;
                }
                if (room.IsAssigned())
                {
                    if (!room.IsFull())
                    {
                        GameObject studentButtonObjectClone = Instantiate(studentButtonObject, transform.GetChild(1).transform);
                        GameObject studentButtonPanel = studentButtonObjectClone.transform.GetChild(0).gameObject;
                        Image studentPic = studentButtonObjectClone.transform.GetChild(1).GetComponent<Image>();

                        Stud_Btn studentButton = studentButtonPanel.GetComponent<Stud_Btn>();

                        studentButton.SetText(student.name);
                        studentButton.SetId(student.id);
                        studentButton.setStudent(student);
                        studentButton.setStudentRoom(room);

                        profButtonObject.GetComponent<RectTransform>().SetSiblingIndex(0);

                        room.AddStudent(student);
                        student.Assign();                    
                    }
                    else
                    {
                        Debug.Log("This room is already full!. Please unassign at least one student first.");
                        student.UnAssign();
                        return;
                    }
                }
                else
                {
                    Debug.Log("This room is yet Assigned by any professor. Please assign professor first.");
                    student.UnAssign();
                    return;
                }
            }
            else if (dropped.TryGetComponent<Professor>(out Professor prof))
            {
                if (room.IsLocked())
                {
                    Debug.Log("This room is not yet Unlocked. Please use another room in the meanwhile.");
                    return;
                }
                if (room.IsAssigned())
                {
                    Debug.Log("This room is Assigned. Please unassign first.");
                    prof.UnAssign();
                    return;
                }
                else
                {
                    room.Assign(prof);
                    prof.Assign();

                    GameObject profButtonObjectClone = Instantiate(profButtonObject, transform.GetChild(0).transform);
                    GameObject profButtonObjectPanel = profButtonObjectClone.transform.GetChild(0).gameObject;
                    Image profPic = profButtonObjectClone.transform.GetChild(1).GetComponent<Image>();

                    profButtonObjectClone.transform.SetSiblingIndex(0);
                    Prof_Btn profButton = profButtonObjectPanel.GetComponent<Prof_Btn>();

                    profButton.SetText(prof.name);
                    profButton.SetId(prof.id);
                    profButton.setProfessor(prof);
                    profButton.setProfessorRoom(room);

                    //GameObject dropdownClone = Instantiate(dropdownObject, transform).gameObject;
                    //dropdownClone.transform.SetSiblingIndex(1);
                    GameObject dropdownClone = Instantiate(dropdownObject, profButtonObjectPanel.transform).gameObject;


                    /*Debug.Log("prof: " + prof.name);
                    Debug.Log("prof skills: " + prof.subjects.Length.ToString());
                    for (int i = 0; i < prof.subjects.Length; i++)
                    {
                        Debug.Log("prof skill: " + prof.subjects[i].ToString());
                        if (prof.subjects[i] == 1)
                        {
                            Debug.Log("prof skill: " + prof.subjects[i]);
                        }
                    }*/

                    dropdownClone.GetComponent<DropDownHandler>().SkillOptionEdit(dropdownClone.GetComponent<TMP_Dropdown>(), prof.subjects);

                    int selectedSkill = dropdownClone.GetComponent<DropDownHandler>().GetSelectedSkill();
                    room.SetLectureSkill(selectedSkill);
                }
            }
        }
    }
}
