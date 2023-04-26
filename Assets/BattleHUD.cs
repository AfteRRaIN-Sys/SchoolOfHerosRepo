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
        for(int i=0;i<10;i++){
            if(player.skills[i]<=0)
                list += i.ToString() + ", ";
        }
        descText.text = player.name + "\nATK =  "+player.damage + "\nSkills : " + list;
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
        for(int i=0;i<10;i++){
            if(player.skills[i]<=0)
                list += i.ToString() + ", ";
        }
        descText.text = player.name + "\nATK =  "+player.damage + "\nSkills : " + list;
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
