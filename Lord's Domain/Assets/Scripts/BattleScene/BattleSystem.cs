using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

    public Transform playerBattleStation;
	public Transform enemyBattleStation;

    public Button attackButton;
    public Button talkButton;
    public Button fleeButton;
    public Button giveItemButton;
    
	Unit playerUnit;
	Unit enemyUnit;
    

	public Text dialogueText;
    
    public BattleHUD playerHUD;
	public BattleHUD enemyHUD;
    
	public BattleState state;

    // Start is called before the first frame update
    void Start()
    {

		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(1.5f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
    {
        DisableButtons();
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "You attack him";

		yield return new WaitForSeconds(1.5f);

        dialogueText.text = "Dwayne: Ouch Ouch Ouch";
        
		yield return new WaitForSeconds(1.5f);
        
        if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
            
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1.5f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1.5f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
            PlayerTurn();
            EnableButtons();
        }

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerTalk() 
	{
		// playerUnit.Heal(5);

		// playerHUD.SetHP(playerUnit.currentHP);
		DisableButtons();
        dialogueText.text = "You begin conversation with " + enemyUnit.unitName;

		yield return new WaitForSeconds(1.5f);

		dialogueText.text = "Dwayne: ...Oh interesting oh well still gonna punch you";

		yield return new WaitForSeconds(1.5f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

    IEnumerator PlayerFlee()
    {
        DisableButtons();
        dialogueText.text = "You try to run from " + enemyUnit.unitName;

        yield return new WaitForSeconds(1.5f);
        
		dialogueText.text = enemyUnit.unitName + ": running away?";

        yield return new WaitForSeconds(1.5f);
        EnableButtons();
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
	}

    IEnumerator PlayerGiveItem()
    {
        DisableButtons();
		dialogueText.text = "Dwayne: Oh you gave me the item I asked for";
		yield return new WaitForSeconds(1.5f);

        state = BattleState.WON;
        EndBattle();
    }
	public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
	}

	public void OnTalkButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerTalk());
	}

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerFlee());
    }

    public void OnGiveItemButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerGiveItem());
    }

    public void EnableButtons()
    {
        attackButton.interactable = true;
		talkButton.interactable = true;
		giveItemButton.interactable = true;
		fleeButton.interactable = true;
	}

    public void DisableButtons()
    {
        attackButton.interactable = false;
        talkButton.interactable = false;
        giveItemButton.interactable = false;
        fleeButton.interactable = false;
	}
}
