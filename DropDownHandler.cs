using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownHandler : MonoBehaviour
{
    int selectedSkill;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void SkillOptionEdit(TMP_Dropdown dropdown, int[] skills)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == 1)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Skill " + i.ToString() });
            }
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
  
        string option = dropdown.options[index].text;

        selectedSkill = option[option.Length-1] - '0';

        RoomSlot parent = gameObject.transform.parent.GetComponent<RoomSlot>();

        parent.room.SetLectureSkill(selectedSkill);
    }

    public int GetSelectedSkill()
    {
        return selectedSkill;
    }
}
