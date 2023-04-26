using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;

	public int damage;

	public int currentHP;
	public int maxHP;

	public int[] skills;


	public void TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			currentHP = 0;	
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void Buff(int amount){
		this.damage += amount;
	}

	// public int randomAction(){
	// 	float act;
	// 	act = Random.value;
	// 	if(this.skills[1]==1){
	// 		act = Random.value;
	// 		if(act > 0.7)
	// 			return 1;
	// 	}
	// 	if(this.skills[7]==1){
	// 		act = Random.value;
	// 		if(act > 0.8)
	// 			return 7;
	// 	}
	// 	if(this.skills[0]==1){
	// 		act = Random.value;
	// 		if(act > 0.4)
	// 			return 0;
	// 	}
	// 	if(this.skills[0]==1){
	// 		act = Random.value;
	// 		if(act > 0.4)
	// 		return 7;
				
	// 	}
	// 	if(this.skills[6]==1){
	// 		act = Random.value;
	// 		if(act > 0.4)
				
	// 	}
	// 	if(this.skills[0]==1){
	// 		act = Random.value;
	// 		if(act > 0.4)
				
	// 	}
	// 	if(this.skills[0]==1){
	// 		act = Random.value;
	// 		if(act > 0.4)
				
	// 	}
	// }

}
