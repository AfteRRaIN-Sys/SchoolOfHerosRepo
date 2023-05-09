using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERSELECT, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

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

	public BattleHUD[] playerHUDs;

	public Text dialogueText;
	public DraftSO classSo;
	public GameStateSO gameStateSO;

	public BattleHUD enemyHUD;

	public BattleState state;
	public List<Student> slctStudents;

	public double atkChance = 0;
	public double defChance = 0;
	public double supChance = 0;

	public Button atkBut;
	public Button defBut;
	public Button supBut;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		slctStudents = gameStateSO.studentList;

		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		atkBut.gameObject.SetActive(false);
		defBut.gameObject.SetActive(false);
		supBut.gameObject.SetActive(false);
		playerUnits = new Unit[3];
		playerHUDs = new BattleHUD[3];

		
		GameObject playerGO1 = Instantiate(playerPrefab[0], playerBattleStation[0]);
		//playerUnit1 = playerGO1.GetComponent<Unit>();
		//playerUnit1.name = playerPrefab[0].name;
		//playerHUD1.SetHUD(playerUnit1);
		playerUnits[0] = playerGO1.GetComponent<Unit>();
		//playerUnits[0].name = playerPrefab[0].name;
		playerHUD1.SetHUD(playerUnits[0]);
		playerHUDs[0] = playerHUD1;
		//playerHUDs[0].player = playerUnits[0];
		
		
		GameObject playerGO2 = Instantiate(playerPrefab[1], playerBattleStation[1]);
		//playerUnit2 = playerGO2.GetComponent<Unit>();
		//playerUnit2.name = playerPrefab[1].name;
		//playerHUD2.SetHUD(playerUnit2);
		playerUnits[1] = playerGO2.GetComponent<Unit>();
		playerHUD2.SetHUD(playerUnits[1]);
		playerHUDs[1] = playerHUD2;
		//playerHUDs[1].player = playerUnits[1];


		GameObject playerGO3 = Instantiate(playerPrefab[2], playerBattleStation[2]);
		//playerUnit3 = playerGO3.GetComponent<Unit>();
		//playerUnit3.name = playerPrefab[2].name;
		//playerHUD3.SetHUD(playerUnit3);
		playerUnits[2] = playerGO3.GetComponent<Unit>();
		playerHUD3.SetHUD(playerUnits[2]);
		playerHUDs[2] = playerHUD3;
		//playerHUDs[2].player = playerUnits[2];

		for(int i =0;i<3;i++){
			playerHUDs[i].nameText.text = slctStudents[i].name;
			playerUnits[i].skills = slctStudents[i].progressLeft;
			for(int j=0;j<10;j++){
				Debug.Log(slctStudents[0].progressLeft[j]);
				Debug.Log(playerUnits[0].skills[j]);
			}
		}

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();
		enemyHUD.SetHUD(enemyUnit);

		dialogueText.text = enemyUnit.unitName + " approaches...";


		yield return new WaitForSecondsRealtime(3f);

		state = BattleState.PLAYERSELECT;
		StartCoroutine(PlayerSelect());
	}



	IEnumerator PlayerAttack(int i)
	{
			yield return new WaitForSecondsRealtime(2f);
			dialogueText.text = playerUnits[i].unitName + "'s Turn";
			yield return new WaitForSecondsRealtime(2f);
			enemyUnit.TakeDamage(playerUnits[i].damage);
			enemyHUD.SetHP(enemyUnit.currentHP);
			dialogueText.text = playerUnits[i].unitName +"'s attack is successful!";
			//UnitMove(enemyUnit, -1);
			yield return new WaitForSecondsRealtime(2f);
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + "'s Turn";
		yield return new WaitForSecondsRealtime(2f);
		float act  = Random.value;
		if(act > 0.85){
			enemyUnit.damage += 10;
			dialogueText.text = enemyUnit.unitName + " Buff Himself!!!!";
		}
		else if(act > 0.6){
			dialogueText.text = enemyUnit.unitName + " attacks all heroes!!!!";
			for (int i = 0; i < 3; i++){
				playerUnits[i].TakeDamage(enemyUnit.damage);
				playerHUDs[i].SetHP(playerUnits[i].currentHP);
					StartCoroutine(UnitMove(enemyUnit,1));
					StartCoroutine(UnitMove(playerUnits[i],5));
				//StartCoroutine(UnitMove(playerUnits[i],1));
			}
		}
		else{
			int target  = Random.Range(0,3);
			int red = 0;
			bool br = true;
			for(int i =0;i<3;i++){
				act  = Random.value;
				if(playerUnits[i].skills[4]<=0&& br){
					if(act>0.8 - defChance){
						target = i;
						red = 15;
						br=false;
						dialogueText.text = playerUnits[i].unitName+ "use High Protect";
					}
				}
				else if(playerUnits[i].skills[3]<=0&&  br){
					act  = Random.value;
					if(act>0.6 - defChance){
						target = i;
						red = 25;
						br=false;
						dialogueText.text = playerUnits[i].unitName+ "use Protect";
					}
				}
			}
			yield return new WaitForSecondsRealtime(2f);
			playerUnits[target].TakeDamage(enemyUnit.damage - red);
			playerHUDs[target].SetHP(playerUnits[target].currentHP);
			dialogueText.text = enemyUnit.unitName + " attacks " + playerUnits[target].unitName ;
			yield return new WaitForSecondsRealtime(2f);
					StartCoroutine(UnitMove(enemyUnit,1));
					StartCoroutine(UnitMove(playerUnits[target],1));
			//StartCoroutine(UnitMove(playerUnits[target],1));
		}
		yield return new WaitForSecondsRealtime(2f);

		int c =0;
		for (int i = 0; i < 3; i++){
				if(playerUnits[i].currentHP <= 0){
					c += 1;
				}
			}
		if(c==3)
				state = BattleState.LOST;
		if(state == BattleState.LOST)
			EndBattle();
		else{
			state = BattleState.PLAYERSELECT;
			StartCoroutine(PlayerSelect());
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
			
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
			float act  = Random.value;
			dialogueText.text = playerUnits[i].unitName + "'s Turn";
			yield return new WaitForSecondsRealtime(2f);
			bool br = true;
			if(playerUnits[i].currentHP > 0){
				if(playerUnits[i].skills[2]<=0){
					act  = Random.value;
					if(act>=0.8 - atkChance){
					enemyUnit.TakeDamage(playerUnits[i].damage*3);
					enemyHUD.SetHP(enemyUnit.currentHP);
					dialogueText.text = playerUnits[i].unitName +"'s critical strike is successful!";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					StartCoroutine(UnitMove(enemyUnit,-1));
					StartCoroutine(UnitMove(playerUnits[i],-1));
					}
				}
				if(playerUnits[i].skills[1]<=0&br){
					act  = Random.value;
					if(act>=0.6 - atkChance){
					enemyUnit.TakeDamage(playerUnits[i].damage*2);
					enemyHUD.SetHP(enemyUnit.currentHP);
					dialogueText.text = playerUnits[i].unitName +"'s critical strike is successful!";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					StartCoroutine(UnitMove(enemyUnit,-1));
					StartCoroutine(UnitMove(playerUnits[i],-1));
					}
				}
				if(playerUnits[i].skills[7]<=0 && br){
					act  = Random.value;
					if(act>=0.8 - supChance){
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
					dialogueText.text = playerUnits[i].unitName +"'s revive is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = playerUnits[k].unitName +" is revived";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if(playerUnits[i].skills[0]<=0 && br){
					act  = Random.value;
					if(act>=0.4 - atkChance){
					enemyUnit.TakeDamage(playerUnits[i].damage);
					enemyHUD.SetHP(enemyUnit.currentHP);
					dialogueText.text = playerUnits[i].unitName +"'s strike is successful!";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					StartCoroutine(UnitMove(enemyUnit,-1));
					StartCoroutine(UnitMove(playerUnits[i],-1));
					}
				}
				if(playerUnits[i].skills[6]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
					int k = 0;
					int min = 100;
					for(int j=0;j<3;j++){
						if(playerUnits[j].currentHP<min & playerUnits[j].currentHP>0){
							k = j;
							min = playerUnits[j].currentHP;
						}
					}
					playerUnits[k].Heal(30);
					playerHUDs[k].SetHP(playerUnits[k].currentHP);
					dialogueText.text = playerUnits[i].unitName +"'s revive is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = playerUnits[k].unitName +" is healed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if(playerUnits[i].skills[8]<=0 && br){
					act  = Random.value;
					if(act>=0.6 - supChance){
					int k = 0;
					int min = 0;
					for(int j=0;j<3;j++){
						if(playerUnits[j].damage<min){
							k = j;
							min = playerUnits[j].damage;
						}
					}
					playerUnits[k].damage += 15;
					dialogueText.text = playerUnits[i].unitName +"'s buff is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = playerUnits[k].unitName +" is buffed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}
				if(playerUnits[i].skills[9]<=0 && br){
					act  = Random.value;
					if(act>=0.8 - supChance){
					enemyUnit.damage -= 10;
					dialogueText.text = playerUnits[i].unitName +"'s debuff is successful!";
					yield return new WaitForSecondsRealtime(2f);
					dialogueText.text = enemyUnit.unitName +" is debuffed";
					//UnitMove(enemyUnit, -1);
					yield return new WaitForSecondsRealtime(2f);
					br = false;
					}
				}

			}
		}
		if(enemyUnit.currentHP <= 0)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else{
			state = BattleState.ENEMYTURN;
			yield return new WaitForSecondsRealtime(1f);
			StartCoroutine(EnemyTurn());
		}
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
        SceneManager.LoadScene(0);
    }

}
