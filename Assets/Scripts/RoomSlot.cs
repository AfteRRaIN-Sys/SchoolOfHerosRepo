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

    GameManager gameManager;

    void Start()
    {
        room = GameObject.Find("Room"+ roomSlotId.ToString()).GetComponent<Room>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject dropped = eventData.pointerDrag;

            if (dropped.TryGetComponent<Student>(out Student student))
            {
                if (room.IsAssigned())
                {
                    if (!room.IsFull())
                    {
                        room.AddStudent(student);
                        student.Assign();

                        GameObject studentButtonObjectClone = Instantiate(studentButtonObject, transform);
                        Stud_Btn studentButton = studentButtonObjectClone.GetComponent<Stud_Btn>();

                        studentButton.SetText("Student" + student.id);
                        studentButton.SetId(student.id);
                        studentButton.setStudent(student);
                        studentButton.setStudentRoom(room);

                        profButtonObject.GetComponent<RectTransform>().SetSiblingIndex(0);                      
                    }
                    else
                    {
                        Debug.Log("This room is already full!. Please unassign at least one student first.");
                        student.UnAssign();
                    }
                }
                else
                {
                    Debug.Log("This room is yet Assigned by any professor. Please assign professor first.");
                    student.UnAssign();
                }
            }
            else if (dropped.TryGetComponent<Professor>(out Professor prof))
            {
                if (room.IsAssigned())
                {
                    Debug.Log("This room is Assigned. Please unassign first.");
                    prof.UnAssign();
                }
                else
                {
                    room.Assign(prof);
                    prof.Assign();

                    GameObject profButtonObjectClone = Instantiate(profButtonObject, transform);
                    profButtonObjectClone.transform.SetSiblingIndex(0);
                    Prof_Btn profButton = profButtonObjectClone.GetComponent<Prof_Btn>();

                    profButton.SetText("Professor " + prof.id.ToString());
                    profButton.SetId(prof.id);
                    profButton.setProfessor(prof);
                    profButton.setProfessorRoom(room);

                    GameObject dropdownClone = Instantiate(dropdownObject, transform).gameObject;
                    dropdownClone.transform.SetSiblingIndex(1);
                    dropdownClone.GetComponent<DropDownHandler>().SkillOptionEdit(dropdownClone.GetComponent<TMP_Dropdown>(), prof.getSubjects(prof.id));

                    int selectedSkill = dropdownClone.GetComponent<DropDownHandler>().GetSelectedSkill();
                    room.SetLectureSkill(selectedSkill);
                }
            }
        }
    }
}
