using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

    private Transform playerBattleStation;
	private Transform enemyBattleStation;

    private Button attackButton;
    private Button talkButton;
    private Button fleeButton;
    private Button giveItemButton;
    
	PlayerStatus playerUnit;
	Unit enemyUnit;
    

	private Text dialogueText;
    
    private BattleHUD playerHUD;
	private BattleHUD enemyHUD;
    
	public BattleState state;
    public string startQuip;
    public string[] damageQuips = {"Ouch Ouch Ouch!"};
    public string[] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        talkButton = GameObject.Find("TalkButton").GetComponent<Button>();
        fleeButton = GameObject.Find("FleeButton").GetComponent<Button>();
        giveItemButton = GameObject.Find("GiveItemButton").GetComponent<Button>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        playerHUD = GameObject.Find("PlayerBattleHud").GetComponent<BattleHUD>();
        enemyHUD = GameObject.Find("EnemyBattleHud").GetComponent<BattleHUD>();        
        playerBattleStation = GameObject.Find("PlayerBattleStation").GetComponent<Transform>();
        enemyBattleStation = GameObject.Find("EnemyBattleStation").GetComponent<Transform>();
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<PlayerStatus>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
        Debug.Log("Player health is = " + playerUnit.health);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(1.5f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
    {
        DisableButtons();
		// Scene changer HERE
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
		dialogueText.text = "You attack him";
		yield return new WaitForSeconds(1.5f);
        
		// MINIGAME SUCCESS
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = enemyUnit.unitName +": " + damageQuips[Random.Range(0, damageQuips.Length)];
        // MINIGAME SUCCESS
        
        // MINIGAME FAILURE
        // dialogueText.text = enemyUnit.unitName + ": Well that tickled";
        // MINIGAME FAILURE
        
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

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.health);

		yield return new WaitForSeconds(1f);

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
            // SCENE CHANGER TO 2D TOP DOWN HERE
            StartCoroutine(Wait());
            //SceneManager.LoadScene("DemoStart");
        }
        else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

    //Change to WaitToScene(int time, string Scene)
    IEnumerator Wait ()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("DemoStart");
    }

	IEnumerator PlayerTalk()
    {
        DisableButtons();
		// Scene changer HERE
		dialogueText.text = startQuip +  " " + enemyUnit.unitName;

        yield return new WaitForSeconds(1.5f);

        // MINIGAME SUCCESS
        foreach(string i in dialogue) { 
            dialogueText.text = enemyUnit.unitName + ": " + i;
            yield return new WaitForSeconds(1.5f);
        }

		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            EnableButtons();
        }

        // MINIGAME SUCCESS

        // MINIGAME FAILURE
        // dialogueText.text = "Dwayne: ...Oh interesting oh well still gonna punch you";
        // yield return new WaitForSeconds(1.5f);
        // state = BattleState.ENEMYTURN;
        // StartCoroutine(EnemyTurn());
        // MINIGAME FAILURE

    }

    IEnumerator PlayerFlee()
    {
        DisableButtons();
        dialogueText.text = "You try to run from " + enemyUnit.unitName;
        yield return new WaitForSeconds(1.5f);

        // SCENE CHANGE GOES HERE
        SceneManager.LoadScene("Runner-MG");

        // MINIGAME FAILURE
        dialogueText.text = enemyUnit.unitName + ": running away?";
        yield return new WaitForSeconds(1.5f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        // MINIGAME FAILURE

    }

    IEnumerator PlayerGiveItem()
    {
        DisableButtons();
        dialogueText.text = enemyUnit.unitName + ": Oh you gave me the item I asked for";
        yield return new WaitForSeconds(2.5f);

        state = BattleState.WON;
        EndBattle();
        //yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene("Demostart");
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
