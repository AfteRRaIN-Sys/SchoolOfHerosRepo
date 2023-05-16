using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownHandler : MonoBehaviour
{
    int selectedSkill;
    string[] skillNames = {"Attack I","Attack II","Attack III","Attack IV","Critical Chance",
                        "Life Steal I","Life Steal II","Poison Cloating","Bleeding Effect", "Open Wound",
                        "Guard I","Guard II","Guard III","Guard IV","Evade","Reflect Damage","Counter Attack",
                        "Absorb Damage I","Absorb Damage II"," Absorb Damage III",
                        "Buff I","Buff II","Buff III","Buff IV","Team Buff I","Team Buff II","Team Buff III",
                        "Debuff I","Debuff II","Debuff III","Debuff IV","Heal I","Heal II","Revive I","Revive II","Team Heal I", "Team Heal II"};

    public void SkillOptionEdit(TMP_Dropdown dropdown, int[] skills)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == 1)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = skillNames[i]});
            }
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
  
        string option = dropdown.options[index].text;

        //selectedSkill = option[option.Length-1] - '0';
        selectedSkill = System.Array.IndexOf(skillNames, option);
        Debug.Log(option);
        Debug.Log(selectedSkill.ToString());

        RoomSlot parent = gameObject.transform.parent.parent.parent.parent.GetComponent<RoomSlot>();

        parent.room.SetLectureSkill(selectedSkill);
    }

    public int GetSelectedSkill()
    {
        return selectedSkill;
    }
}
