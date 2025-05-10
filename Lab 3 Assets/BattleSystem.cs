using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For using Button UI

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public TMP_Text dialogueText;
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    // Skill Menu UI elements
    public GameObject skillMenu;  // The skill menu UI (panel)
    public Button[] skillButtons; // Buttons for each skill (must be set in the Unity inspector)

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject PlayerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = PlayerGO.GetComponent<Unit>();
        GameObject EnemyGO = Instantiate(enemyPrefab, enemyPosition);
        enemyUnit = EnemyGO.GetComponent<Unit>();

        dialogueText.text = "A " + enemyUnit.unitName + " wants to fight!";

        playerUnit.SetUpSkills();  // Initialize skills
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // Basic Attack
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.ATK);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " attacks for " + playerUnit.ATK + " damage!";
        state = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    // End Battle
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Player wins!";
        }
        if (state == BattleState.LOST)
        {
            dialogueText.text = "Player lost...";
        }
    }

    // Enemy Turn
    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.ATK);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    // Player's Turn
    void PlayerTurn()
    {
        dialogueText.text = "What will " + playerUnit.unitName + " do?";

        // Show skill menu

        // Set up skill buttons
        SetUpSkillButtons();
    }

    // Set up skill buttons
    void SetUpSkillButtons()
    {
        for (int i = 0; i < playerUnit.skills.Count; i++)
        {
            int skillIndex = i;
            skillButtons[i].gameObject.SetActive(true);
            skillButtons[i].GetComponentInChildren<TMP_Text>().text = playerUnit.skills[i].skillName;
            skillButtons[i].onClick.RemoveAllListeners();
            skillButtons[i].onClick.AddListener(() => OnSkillButtonPressed(skillIndex));
        }

        // Hide unused buttons
        for (int i = playerUnit.skills.Count; i < skillButtons.Length; i++)
        {
            skillButtons[i].gameObject.SetActive(false);
        }
    }

    // When a skill is pressed
    void OnSkillButtonPressed(int skillIndex)
    {
        skillMenu.SetActive(false);  // Hide skill menu
        StartCoroutine(UseSkill(skillIndex));
    }

    // Using a skill
    IEnumerator UseSkill(int skillIndex)
    {
        Skill skill = playerUnit.skills[skillIndex];

        dialogueText.text = playerUnit.unitName + " uses " + skill.skillName + "!";
        bool isDead = skill.Execute(playerUnit, enemyUnit);  // Executes skill logic

        yield return new WaitForSeconds(2f);

        enemyHUD.SetHP(enemyUnit.currentHP);
        playerHUD.SetMP(playerUnit.currentMP);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        skillMenu.SetActive(true);
    }
}

