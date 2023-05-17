using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERSELECT, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
     public string[] skillNames = {"Attack I","Attack II","Attack III","Attack IV","Critical Chance",
                        "Life Steal I","Life Steal II","Poison Cloating","Bleeding Effect", "Open Wound",
                         "Guard I","Guard II","Guard III","Guard IV","Evade","Reflect Damage","Counter Attack",
                         "Absorb Damage I","Absorb Damage II"," Absorb Damage III",
                         "Buff I","Buff II","Buff III","Buff IV","Team Buff I","Team Buff II","Team Buff III",
                         "Debuff I","Debuff II","Debuff III","Debuff IV","Heal I","Heal II","Revive I","Revive II","Team Heal I", "Team Heal II"};
     public int[] skillLevels = {1,2,3,4,4,3,4,2,3,4,1,2,3,4,2,3,4,2,3,4,1,2,3,4,2,3,4,1,2,3,4,1,2,3,4,3,4};


	public List<GameObject> playerPrefab;
	public GameObject enemyPrefab;

	public List<Transform> playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit1;
	Unit playerUnit2;
	Unit playerUnit3;
	Unit enemyUnit;

	public Unit[] playerUnits;

	public BattleHUD playerHUD1;
	public BattleHUD playerHUD2;
	public BattleHUD playerHUD3;

	public Sprite charac; 
	public BattleHUD[] playerHUDs;

	public Sprite boss1, boss2;
	public Sprite char1, char2,char3;

	public Text dialogueText;
	public DraftSO classSo;
	public GameStateSO gameStateSO;

	public BattleHUD enemyHUD;

	public BattleState state;
	public List<Student> slctStudents;

	public double atkChance = 0;
	public double defChance = 0;
	public double supChance = 0;

	public int stunt = 0;

	public Button atkBut;
	public Button defBut;
	public Button supBut;

	public double bossChance = 0;

	public bool[] angles;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		slctStudents = new List<Student>();
		List<int> slctStudentsId = new List<int>();
		while(slctStudents.Count<3){
			int rnd = Random.Range(0,gameStateSO.studentList.Count);
			bool isIn = false;
			for(int i=0;i<slctStudentsId.Count;i++){
				if(slctStudentsId[i]==rnd)
					isIn = true;
			}
			if(isIn==false) {
				slctStudentsId.Add(rnd);
				slctStudents.Add(gameStateSO.studentList[rnd]);
			}
		}
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		atkBut.gameObject.SetActive(false);
		defBut.gameObject.SetActive(false);
		supBut.gameObject.SetActive(false);
		playerUnits = new Unit[3];
		playerHUDs = new BattleHUD[3];
		angles = new bool[3];

		playerPrefab[0].GetComponent<SpriteRenderer>().sprite = char1;
		GameObject playerGO1 = Instantiate(playerPrefab[0], playerBattleStation[0]);
		//m_Image.sprite = charac;
		playerUnits[0] = playerGO1.GetComponent<Unit>();
		playerUnits[0].unitName = slctStudents[0].name;
		playerHUD1.SetHUD(playerUnits[0]);
		playerHUDs[0] = playerHUD1;
		
		playerPrefab[1].GetComponent<SpriteRenderer>().sprite = char2;
		GameObject playerGO2 = Instantiate(playerPrefab[1], playerBattleStation[1]);
		playerUnits[1] = playerGO2.GetComponent<Unit>();
		playerUnits[1].unitName = slctStudents[1].name;
		playerHUD2.SetHUD(playerUnits[1]);
		playerHUDs[1] = playerHUD2;
		//playerHUDs[1].player = playerUnits[1];

		playerPrefab[2].GetComponent<SpriteRenderer>().sprite = char3;
		GameObject playerGO3 = Instantiate(playerPrefab[2], playerBattleStation[2]);
		playerUnits[2] = playerGO3.GetComponent<Unit>();
		playerUnits[2].unitName = slctStudents[2].name;
		
		playerHUD3.SetHUD(playerUnits[2]);
		playerHUDs[2] = playerHUD3;
		//playerHUDs[2].player = playerUnits[2];

		Debug.Log("Set skill");
		for(int i =0;i<3;i++){
			playerHUDs[i].nameText.text = slctStudents[i].name;
			Debug.Log("slctStudents skill length: " + slctStudents[i].progressLeft.Length.ToString());
			playerUnits[i].skills = slctStudents[i].progressLeft;
			Debug.Log("playerUnits skill length: " + playerUnits[i].skills.Length.ToString());
		}

		if(gameStateSO.cur_sem==1){

			enemyPrefab.GetComponent<SpriteRenderer>().sprite = boss1;
		}
		if(gameStateSO.cur_sem>=2){

			enemyPrefab.GetComponent<SpriteRenderer>().sprite = boss2;
		}
		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();
		enemyHUD.SetHUD(enemyUnit);

		if(gameStateSO.cur_sem==1){
			enemyUnit.unitName = "Typical Bad Teacher";
			enemyUnit.currentHP = 400;
			enemyUnit.maxHP = 400;

		}
		if(gameStateSO.cur_sem>=2){
			enemyUnit.unitName = "Sadism Bad Teacher";
			enemyUnit.currentHP = 600;
			enemyUnit.maxHP = 600;

		}

		dialogueText.text = enemyUnit.unitName + " approaches...";


		yield return new WaitForSecondsRealtime(3f);

		state = BattleState.PLAYERSELECT;
		StartCoroutine(PlayerSelect());
	}



	// IEnumerator PlayerAttack(int i)
	// {
	// 		yield return new WaitForSecondsRealtime(2f);
	// 		dialogueText.text = playerUnits[i].unitName + "'s Turn";
	// 		yield return new WaitForSecondsRealtime(2f);
	// 		enemyUnit.TakeDamage(playerUnits[i].damage);
	// 		enemyHUD.SetHP(enemyUnit.currentHP);
	// 		dialogueText.text = playerUnits[i].unitName +"'s attack is successful!";
	// 		//UnitMove(enemyUnit, -1);
	// 		yield return new WaitForSecondsRealtime(2f);
	// }

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + "'s Turn";
		if(stunt >0){
			dialogueText.text = enemyUnit.unitName + "  is stun";
			yield return new WaitForSecondsRealtime(2f);
			stunt -= 1;
			StartCoroutine(PlayerSelect());
		}
		else{
		yield return new WaitForSecondsRealtime(2f);
		int damage;
		int attack_mode = 0;
		bool isAlive = true;
		int target = 0;
		while(isAlive){
				target  = Random.Range(0,3);
				if(playerUnits[target].currentHP >0)
					isAlive = false;
		}
		float act  = Random.value;
		if(gameStateSO.cur_sem ==1){
			if(act > 0.85){
				attack_mode = 0;
				enemyUnit.damage += 10;
				enemyUnit.Heal(20);
				enemyHUD.SetHP(enemyUnit.currentHP);
				dialogueText.text = enemyUnit.unitName + " Buff Himself!!!!";
				yield return new WaitForSecondsRealtime(2f);
			}
			else if(act > 0.6){
				dialogueText.text = enemyUnit.unitName + " attacks all heroes!!!!";
				yield return new WaitForSecondsRealtime(2f);
				attack_mode = 2;
			}
			else{
				attack_mode = 1;
			}
		}
		else if(gameStateSO.cur_sem >=2){
			if(act > 0.8){
				attack_mode = 0;
				dialogueText.text = enemyUnit.unitName + " debuff all heross";
				yield return new WaitForSecondsRealtime(2f);
				dialogueText.text = "All heroes' atk is reduced by 10";
				yield return new WaitForSecondsRealtime(2f);
				for (int i = 0; i < 3; i++){
				 	if(playerUnits[i].currentHP >0){
				 		playerUnits[i].damage -= 10;
					}
				}
			}
			else if(act > 0.6){
				dialogueText.text = enemyUnit.unitName + " attacks all heroes!!!!";
				yield return new WaitForSecondsRealtime(2f);
				attack_mode = 2;
			}
			else{
				attack_mode = 1;
				dialogueText.text = enemyUnit.unitName + " take advantages a hero!!!!";
				yield return new WaitForSecondsRealtime(2f);
				dialogueText.text = enemyUnit.unitName + " punishs "+ playerUnits[target].unitName;
				yield return new WaitForSecondsRealtime(2f);
				playerUnits[target].damage -= 10;
				dialogueText.text = playerUnits[target].unitName + "'s atk is reduced by 10";
				yield return new WaitForSecondsRealtime(2f);
			}

		}
		else if(gameStateSO.cur_sem == 3){

		}
		for(int i=0;i<3;i++){
			if((attack_mode ==2 | i==target) & playerUnits[i].currentHP>0 & (attack_mode != 0))
				{
				int real_target = -1;
				int red = 0;
				bool pr = true;
				for(int j =0;j<3;j++){
					act  = Random.value;
					if(playerUnits[j].skills[13]<=0&& pr){
						if(act>0.85 - defChance){
							real_target = j;
							red = 100;
							pr=false;
							dialogueText.text = playerUnits[j].unitName+ " use Protect IV";
							yield return new WaitForSecondsRealtime(2f);
						}
					}
					if(playerUnits[j].skills[12]<=0&& pr){
						if(act>0.7 - defChance){
							real_target = j;
							red = 80;
							pr=false;
							dialogueText.text = playerUnits[j].unitName+ " use Protect III";
							yield return new WaitForSecondsRealtime(2f);
						}
					}
					if(playerUnits[j].skills[11]<=0&& pr){
						if(act>0.55 - defChance){
							real_target = j;
							red = 60;
							pr=false;
							dialogueText.text = playerUnits[j].unitName+ " use Protect II";
							yield return new WaitForSecondsRealtime(2f);
						}
					}
					if(playerUnits[j].skills[10]<=0&& pr){
						if(act>0.4 - defChance){
							real_target = j;
							red = 40;
							pr=false;
							dialogueText.text = playerUnits[j].unitName+ " use Protect I";
							yield return new WaitForSecondsRealtime(2f);
						}
					}
				}
				if(real_target != -1){
					act  = Random.value;
					bool dr = true;
					if(playerUnits[real_target].skills[16]<=0&& dr){
						if(act>0.80 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use Counter";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "damage return to enemy and heroes attacks enemy 1 time";
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(enemyUnit.damage);
							enemyUnit.TakeDamage(playerUnits[real_target].damage);
							enemyHUD.SetHP(enemyUnit.currentHP);
				 			//playerHUDs[real_target].SetHP(playerUnits[i].currentHP);
						}
					}
					if(playerUnits[real_target].skills[19]<=0&& dr){
						if(act>0.70 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use absorb III";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "hero aborbs all damage from enermy attack";
							yield return new WaitForSecondsRealtime(2f);
							playerUnits[real_target].Heal(enemyUnit.damage);
				 			playerHUDs[real_target].SetHP(playerUnits[i].currentHP);
						}
					}
					if(playerUnits[real_target].skills[15]<=0&& dr){
						if(act>0.50 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use Reflect";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "damage return to enemy";
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(enemyUnit.damage);
							enemyHUD.SetHP(enemyUnit.currentHP);
				 			//playerHUDs[real_target].SetHP(playerUnits[i].currentHP);
						}
					}
					if(playerUnits[real_target].skills[18]<=0&& dr){
						if(act>0.60 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use absorb II";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "hero aborbs half damage from enermy attack";
							yield return new WaitForSecondsRealtime(2f);
							playerUnits[real_target].Heal(enemyUnit.damage/2);
				 			playerHUDs[real_target].SetHP(playerUnits[i].currentHP);
						}
					}
					if(playerUnits[real_target].skills[14]<=0&& dr){
						if(act>0.30 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use Evade";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "hero does not get any damage";
							yield return new WaitForSecondsRealtime(2f);
				 			//playerHUDs[real_target].SetHP(playerUnits[i].currentHP);
						}
					}
					if(playerUnits[real_target].skills[17]<=0&& dr){
						if(act>0.40 - defChance){
							dr=false;
							dialogueText.text = playerUnits[real_target].unitName+ " use absorb I";
							yield return new WaitForSecondsRealtime(2f);
							dialogueText.text = "hero aborbs some damage from enermy attack";
							yield return new WaitForSecondsRealtime(2f);
							playerUnits[real_target].Heal(20);
				 			playerHUDs[real_target].SetHP(playerUnits[real_target].currentHP);
						}
					}
					if(dr){
						dialogueText.text = playerUnits[real_target].unitName+ " guards "+slctStudents[i].name;
						yield return new WaitForSecondsRealtime(2f);
						playerUnits[real_target].TakeDamage(enemyUnit.damage*(100-red)/100);
				 		playerHUDs[real_target].SetHP(playerUnits[real_target].currentHP);
				 		StartCoroutine(UnitMove(enemyUnit,1));
				 		StartCoroutine(UnitMove(playerUnits[real_target],1));
					}
				}
				else{
					playerUnits[i].TakeDamage(enemyUnit.damage);
				 	playerHUDs[i].SetHP(playerUnits[i].currentHP);
				 	StartCoroutine(UnitMove(enemyUnit,1));
				 	StartCoroutine(UnitMove(playerUnits[i],1));
				}
			}
		}
		int c =0;
		for (int i = 0; i < 3; i++){
				if(playerUnits[i].currentHP <= 0){
					c += 1;
				}
			}
		if(c==3){
			state = BattleState.LOST;
			EndBattle();
		}
		if(enemyUnit.currentHP <= 0){
			state = BattleState.WON;
			EndBattle();
		}
		state = BattleState.PLAYERSELECT;
		StartCoroutine(PlayerSelect());
		}
	}

	void EndBattle()
	{
		StopAllCoroutines();

		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
			//yield return new WaitForSecondsRealtime(2f);
			dialogueText.text = "You get 700 points";
			gameStateSO.point += 700;
			gameStateSO.money += 700;
			//yield return new WaitForSecondsRealtime(2f);
			// move to draft
			NextScene();

		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
			// move to draft
			NextScene();
		}

		
	}

	IEnumerator PlayerSelect()
	{
		yield return new WaitForSecondsRealtime(2f);
		dialogueText.text = "Choose an tactic : ";
		atkBut.gameObject.SetActive(true);
		defBut.gameObject.SetActive(true);
		supBut.gameObject.SetActive(true);
	}

	IEnumerator PlayerTurn()
	{
		for (int i = 0; i < 3; i++){
			int mode = 0;
			int level = 0;
			bool isAlive = true;
			int target = 0;
			while(isAlive){
				target  = Random.Range(0,3);
				if(playerUnits[target].currentHP >0)
					isAlive = false;
			}
			float act  = Random.value;
			dialogueText.text = slctStudents[i].name + "'s Turn";
			yield return new WaitForSecondsRealtime(2f);
			bool br = true;
			if(playerUnits[i].currentHP > 0){
				//level 4
				if(playerUnits[i].skills[3]<=0 &br){
					act  = Random.value;
					if(act>=0.8 - atkChance){
						//enemyUnit.TakeDamage(playerUnits[i].damage*4);
						//enemyHUD.SetHP(enemyUnit.currentHP);
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[3]+" is successful!";
						mode = 1;
						level = 4;
						//UnitMove(enemyUnit, -1);
						yield return new WaitForSecondsRealtime(2f);
						br = false;
						//StartCoroutine(UnitMove(enemyUnit,-1));
						//StartCoroutine(UnitMove(playerUnits[i],-1));
					}
				}
				if(playerUnits[i].skills[26]<=0 && br){
					act  = Random.value;
					if(act>=0.9 - supChance){
					for(int j=0;j<3;j++){
						playerUnits[j].damage += 20;
						angles[j] = true;
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[26]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					//UnitMove(enemyUnit, -1);
					br = false;
					}
				}
				if(playerUnits[i].skills[23]<=0 && br){
					act  = Random.value;
					if(act>=0.7 - supChance){
						playerUnits[target].damage += 30;
						angles[target] = true;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[23]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						dialogueText.text = slctStudents[target].name +" is buffed";
						yield return new WaitForSecondsRealtime(2f);
						br = false;
					}
				}
				if(playerUnits[i].skills[30]<=0 && br){
					act  = Random.value;
					if(act>=0.8 - supChance){
						enemyUnit.damage -= 20;
						bossChance += 0.1;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[30]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[34]<=0 && br){
					act  = Random.value;
					if(act>=0.8 - supChance){
					//enemyUnit.TakeDamage(playerUnits[i].damage*2);
					for(int j=0;j<3;j++){
							playerUnits[j].Heal(20);
							playerHUDs[j].SetHP(playerUnits[j].currentHP);
						
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[34]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = "All player is revived";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if(playerUnits[i].skills[36]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
					//enemyUnit.TakeDamage(playerUnits[i].damage*2);
					for(int j=0;j<3;j++){
						if(playerUnits[j].currentHP > 0){
							playerUnits[j].Heal(40);
							playerHUDs[j].SetHP(playerUnits[j].currentHP);
						}
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[36]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = "All player is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				//level 3
				if(playerUnits[i].skills[2]<=0&br){
					act  = Random.value;
					if(act>=0.65 - atkChance){
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[2]+" is successful!";
						mode = 1;
						level = 3;
						//UnitMove(enemyUnit, -1);
						yield return new WaitForSecondsRealtime(2f);
						br = false;
					}
				}
				if(playerUnits[i].skills[22]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
						playerUnits[target].damage += 20;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[22]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						dialogueText.text = slctStudents[target].name +" is buffed";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[25]<=0 && br){
					act  = Random.value;
					if(act>=0.7 - supChance){
					for(int j=0;j<3;j++){
						playerUnits[j].damage += 15;
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[25]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					//UnitMove(enemyUnit, -1);
					br = false;
					}
				}
				if(playerUnits[i].skills[29]<=0 && br){
					act  = Random.value;
					if(act>=0.7 - supChance){
						enemyUnit.damage -= 20;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[29]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[33]<=0 && br){
					act  = Random.value;
					if(act>=0.5 - supChance){
					//enemyUnit.TakeDamage(playerUnits[i].damage*2);
					for(int j=0;j<3;j++){
							playerUnits[j].Heal(10);
							playerHUDs[j].SetHP(playerUnits[j].currentHP);
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[33]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = "All player is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if(playerUnits[i].skills[35]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
					//enemyUnit.TakeDamage(playerUnits[i].damage*2);
					for(int j=0;j<3;j++){
						if(playerUnits[j].currentHP > 0){
							playerUnits[j].Heal(30);
							playerHUDs[j].SetHP(playerUnits[j].currentHP);
						}
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[35]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = "All player is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				//level 2
				if(playerUnits[i].skills[1]<=0&br){
					act  = Random.value;
					if(act>=0.5 - atkChance){
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[1]+" is successful!";
						mode = 1;
						level = 2;
						//UnitMove(enemyUnit, -1);
						yield return new WaitForSecondsRealtime(2f);
						br = false;
					}
				}
				if(playerUnits[i].skills[21]<=0 && br){
					act  = Random.value;
					if(act>=0.4 - supChance){
						playerUnits[target].damage += 20;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[21]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						dialogueText.text = slctStudents[target].name +" is buffed";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[24]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
					for(int j=0;j<3;j++){
						playerUnits[j].damage += 5;
					}
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[24]+" is successful!";
					yield return new WaitForSecondsRealtime(2f);
					//UnitMove(enemyUnit, -1);
					br = false;
					}
				}
				if(playerUnits[i].skills[28]<=0 && br){
					act  = Random.value;
					if(act>=0.7 - supChance){
						enemyUnit.damage -= 15;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[28]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[32]<=0 && br){
					act  = Random.value;
					if(act>=0.4 - supChance){
					//enemyUnit.TakeDamage(playerUnits[i].damage*2);
					int k = 0;
					int min = 100;
					for(int j=0;j<3;j++){
						if(playerUnits[j].currentHP<min){
							k = j;
							min = playerUnits[j].currentHP;
						}
					}
					playerUnits[k].Heal(30);
					playerHUDs[k].SetHP(playerUnits[k].currentHP);
					dialogueText.text = slctStudents[i].name +"'s " + skillNames[32] + "is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = playerUnits[k].unitName +" is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
				}
				}
				//level 1
				if(playerUnits[i].skills[0]<=0 && br){
					act  = Random.value;
					if(act>=0.3 - atkChance){
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[0]+" is successful!";
						mode = 1;
						level = 1;
						//UnitMove(enemyUnit, -1);
						yield return new WaitForSecondsRealtime(2f);
						br = false;
					}
				}
				if(playerUnits[i].skills[20]<=0 && br){
					act  = Random.value;
					if(act>=0.4 - supChance){
						playerUnits[target].damage += 10;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[20]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						dialogueText.text = slctStudents[target].name+" is buffed";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[27]<=0 && br){
					act  = Random.value;
					if(act>=0.3 - supChance){
						enemyUnit.damage -= 5;
						dialogueText.text = slctStudents[i].name +"'s " + skillNames[27]+" is successful!";
						yield return new WaitForSecondsRealtime(2f);
						//UnitMove(enemyUnit, -1);
						br = false;
					}
				}
				if(playerUnits[i].skills[31]<=0 && br){
					act  = Random.value;
					if(act>=0.3 - supChance){
					int k = 0;
					int min = 100;
					for(int j=0;j<3;j++){
						if(playerUnits[j].currentHP<min & playerUnits[j].currentHP>0){
							k = j;
							min = playerUnits[j].currentHP;
						}
					}
					playerUnits[k].Heal(20);
					playerHUDs[k].SetHP(playerUnits[k].currentHP);
					dialogueText.text = slctStudents[i].name +"'s revive is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = playerUnits[k].unitName +" is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if (mode == 1)
				{
					bool dr = true;
					int dmg = playerUnits[i].damage + (level - 1) * (20);
					if (playerUnits[i].skills[4] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.6 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[4] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg * 2);
							enemyHUD.SetHP(enemyUnit.currentHP);
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (playerUnits[i].skills[6] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.6 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[6] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg);
							enemyHUD.SetHP(enemyUnit.currentHP);
							playerUnits[i].Heal(dmg);
							playerHUDs[i].SetHP(playerUnits[i].currentHP);
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (playerUnits[i].skills[9] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.6 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[9] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg);
							enemyHUD.SetHP(enemyUnit.currentHP);
							playerUnits[i].damage += 10;
							enemyUnit.damage -= 10;
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (playerUnits[i].skills[5] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.4 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[5] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg);
							enemyHUD.SetHP(enemyUnit.currentHP);
							playerUnits[i].Heal(playerUnits[i].damage);
							playerHUDs[i].SetHP(playerUnits[i].currentHP);
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (playerUnits[i].skills[8] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.4 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[8] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg);
							enemyHUD.SetHP(enemyUnit.currentHP);
							enemyUnit.damage -= 10;
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (playerUnits[i].skills[8] <= 0 && dr)
					{
						act = Random.value;
						if (act >= 0.3 - atkChance)
						{
							dialogueText.text = slctStudents[i].name + "'s " + skillNames[8] + " is successful!";
							//UnitMove(enemyUnit, -1);
							yield return new WaitForSecondsRealtime(2f);
							enemyUnit.TakeDamage(dmg);
							enemyHUD.SetHP(enemyUnit.currentHP);
							enemyUnit.damage -= 5;
							StartCoroutine(UnitMove(enemyUnit, -1));
							StartCoroutine(UnitMove(playerUnits[i], -1));
							dr = false;
						}
					}
					if (dr)
					{
						enemyUnit.TakeDamage(dmg);
						enemyHUD.SetHP(enemyUnit.currentHP);
						StartCoroutine(UnitMove(enemyUnit, -1));
						StartCoroutine(UnitMove(playerUnits[i], -1));
						dr = false;
					}
				}

				if(enemyUnit.currentHP <= 0)
				{
					state = BattleState.WON;
					EndBattle();
				}
				
			}
		}
			state = BattleState.ENEMYTURN;
			yield return new WaitForSecondsRealtime(1f);
			StartCoroutine(EnemyTurn());
		
	}

	IEnumerator PlayerHeal(int i)
	{
		dialogueText.text = playerUnits[i] + "'s Turn";
		yield return new WaitForSecondsRealtime(3f);
		playerUnits[i].Heal(30);
		dialogueText.text = "You feel renewed strength!";

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	IEnumerator UnitMove(Unit player,int mode){
		player.transform.position = player.transform.position - new Vector3(0.3f * mode, 0.0f,0.0f);
		yield return new WaitForSecondsRealtime(0.4f);
		player.transform.position = player.transform.position + new Vector3(0.3f * mode, 0.0f,0.0f);
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERSELECT)
			return;
		
		atkChance = 0.3;
		defChance = -0.2;
		supChance = -0.2;
		state = BattleState.PLAYERTURN;

		atkBut.gameObject.SetActive(false);
		defBut.gameObject.SetActive(false);
		supBut.gameObject.SetActive(false);

		StartCoroutine(PlayerTurn());
	}

	public void OnSupButton()
	{
		if (state != BattleState.PLAYERSELECT)
			return;
		atkChance = -0.2;
		defChance = -0.2;
		supChance = 0.3;
		atkBut.gameObject.SetActive(false);
		defBut.gameObject.SetActive(false);
		supBut.gameObject.SetActive(false);
		state = BattleState.PLAYERTURN;
		StartCoroutine(PlayerTurn());
	}

	public void OnDefButton()
	{
		if (state != BattleState.PLAYERSELECT)
			return;

		atkChance = -0.2;
		defChance =  0.3;
		supChance = -0.2;
		state = BattleState.PLAYERTURN;
		atkBut.gameObject.SetActive(false);
		defBut.gameObject.SetActive(false);
		supBut.gameObject.SetActive(false);
		StartCoroutine(PlayerTurn());
	}

	void NextScene()
    {
		/*List<Student> students = gameStateSO.studentList;
		string studentStr = "";
		foreach (Student student in students)
        {
			string tmp = student.name + student.currentHP.ToString(); ;

			studentStr += tmp + System.Environment.NewLine;
        }
		Debug.Log(studentStr);
		*/
		gameStateSO.newGame = false;
		Debug.Log("Loading Scene...");
		SceneManager.LoadScene(1);
    }

}
