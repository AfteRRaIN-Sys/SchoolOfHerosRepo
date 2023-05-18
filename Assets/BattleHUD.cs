using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	public Unit player;
	public Text nameText;
	public Slider hpSlider;
	public Text currentHPText;
    public Color MaxHealthColor = Color.green;
    public Color MinHealthColor = Color.red;
	public Image Fill; 
	public Text descText;
	public GameObject descBack;
	public string[] skillNames = {"Attack I","Attack II","Attack III","Attakc IV","Critical Chance",
                        "Life Steal I","Life Steal II","Poison Cloating","Bleeding Effect", "Open Wound",
                         "Guard I","Guard II","Guard III","Guard IV","Evade","Reflect Damage","Counter Attack",
                         "Absorb Damage I","Absorb Damage II"," Absorb Damage III",
                         "Buff I","Buff II","Buff III","Buff IV","Team Buff I","Team Buff II","Team Buff III",
                         "Debuff I","Debuff II","Debuff III","Debuff IV","Heal I","Heal II","Revive I","Revive II","Team Heal I", "Team Heal II"};


	public void SetHUD(Unit unit)
	{
		player = unit;
		descText.gameObject.SetActive(false);
		descBack.gameObject.SetActive(false);
		nameText.text = unit.unitName;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		currentHPText.text = unit.currentHP.ToString();
		Fill.color = MaxHealthColor; 
		string list = "";
		
		Debug.Log(player.skills.Length.ToString());
		if(unit.maxHP > 200 ){
			list = "?????????????????????????????????";
		}
        for(int i=0;i<37;i++){
             if(player.skills[i]<=0)
                 list += skillNames[i] + ", ";
        }
        descText.text = "ATK =  "+player.damage + "\nSkills : " + list;
	}



	public void SetHP(int hp)
	{
		hpSlider.value = hp;
		Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, (float)hp / 100);
		currentHPText.text = hp.ToString();
	}
	
    public void OnMouseOver()
    {
        string list = "";
		if(player.maxHP > 200 ){
			list = "?????????????????????????????????";
		}
        for(int i=0;i<37;i++){
             if(player.skills[i]<=0)
                 list += skillNames[i] + ", ";
        }
        descText.text = "ATK =  "+player.damage + "\nSkills : " + list;
		Debug.Log('1');
        descText.gameObject.SetActive(true);
		descBack.gameObject.SetActive(true);
    }

        public void OnMouseExit()
    {
		Debug.Log('2');
        descText.gameObject.SetActive(false);
		descBack.gameObject.SetActive(false);
    }

}
