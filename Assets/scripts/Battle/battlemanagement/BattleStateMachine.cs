using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public enum BattleStates
{
    START, PLAYERTURN, ENEMYTURN, WIN, LOSS, FLEE, WAIT
}
public class BattleStateMachine : MonoBehaviour
{
    SceneManager sceneManager;
    PartyManager partyHandler;
    public AudioClip victory;
    public GameObject heroPrefab;
    public GameObject ogrePrefab;
    public GameObject wizardPrefab;
    public GameObject senatorPrefab;
    public GameObject wolfPrefab;
    public EnemyHandler fallbackEnemy;
    public BattleStationManager battleStationManager;
    public Button attackButton;

    public UIHandler uiHandler;

    public float animSpeed = 5f;
    public HUDHandler dataHudHandler;

    public List<Character> turnOrder;
    public Character currentCharacter;
    public List<PlayerCharacter> playerCharacterList;
    public List<Enemy> enemies;
    public List<CharacterHUD> characterHUDList;
    public List<EnemyHUD> enemyHUDList;
    public int turnCounter;
    public BattleStates state;
    // Start is called before the first frame update
    public void Start()
    {
        sceneManager = GameManager.instance.sceneManager;
        StartCoroutine(sceneManager.TransitionTime("Enter_Scene", .5f));
        partyHandler = GameManager.instance.partyManager;
        uiHandler.OnStart();
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        GameObject prefab;
        int index = 0;
        foreach (PlayerCharacterData characterData in partyHandler.partyData)
        {
            if (characterData is HeroData)
                prefab = heroPrefab;
            else if (characterData is WizardData)
                prefab = wizardPrefab;
            else if (characterData is SenatorData)
                prefab = senatorPrefab;
            else
                continue;

            var characterUnit = (PlayerCharacter)battleStationManager.SetStation(prefab, index);
            characterUnit.DeepCopyFrom(characterData);
            playerCharacterList.Add(characterUnit);
            index++;
        }

        index = 3;

        if (sceneManager.enemyInBattle is null) sceneManager.enemyInBattle = fallbackEnemy;

        foreach (GameObject enemyPrefab in sceneManager.enemyInBattle.encounterFormation)
        {
            if (index > 6) break;

            var enemyUnit = (Enemy)battleStationManager.SetStation(enemyPrefab, index);
            enemyUnit.rarity = Enemy.InitializeRarity();
            enemies.Add(enemyUnit);

            index++;
        }

        characterHUDList = dataHudHandler.CreateCharacterHuds(playerCharacterList);
        enemyHUDList = dataHudHandler.CreateEnemyHuds(enemies);

        yield return new WaitForSeconds(1.5f);

        state = BattleStates.WAIT;
        turnCounter = 0;
        StartCoroutine(FindNextTurn());
    }

    public IEnumerator FindNextTurn()
    {
        yield return new WaitForSeconds(.25f);

        uiHandler.ResetUI();
        UpdateHUDs();
        turnOrder.RemoveAll(c => !c.isActive);

        if (playerCharacterList.Any(c => !c.isActive && !c.isBackRow) &&
            playerCharacterList.Any(c => c.isActive && c.isBackRow))
        {
            battleStationManager.SwapStations(playerCharacterList.Where(c => !c.isActive && !c.isBackRow).First());
        }

        if (!playerCharacterList.Any(c => c.isActive))
        {
            state = BattleStates.LOSS;
            EndBattle();
            yield break;
        }
        else if (!enemies.Any(e => e.isActive))
        {
            state = BattleStates.WIN;
            EndBattle();
            yield break;
        }

        if (turnOrder.Count == 0)
        {
            turnCounter++;

            turnOrder.AddRange(playerCharacterList.Where(c => c.isActive && c.isInParty).ToList());
            turnOrder.AddRange(enemies.Where(c => c.isActive).ToList());

            turnOrder = turnOrder.OrderByDescending(c => FindTurnValue(c)).ToList();
        }
        currentCharacter = turnOrder.First();
        turnOrder.Remove(turnOrder[0]);

        if (currentCharacter.currStatuses.Count > 0)
            Statuses.HandleStatuses(currentCharacter, turnCounter);

        if (currentCharacter is PlayerCharacter)
        {
            state = BattleStates.PLAYERTURN;
            PlayerTurn();
            yield break;
        }

        else if (currentCharacter is Enemy)
        {
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn((Enemy)currentCharacter));
            yield break;
        }
        yield break;
    }

    public void PlayerTurn()
    {
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Poisoned)) currentCharacter.currHP -= (int)(currentCharacter.maxHP / 16);
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Bleeding)) currentCharacter.currHP -= (int)(currentCharacter.maxHP / 10);
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Paralyzed))
        {
            StartCoroutine(FindNextTurn());
            return;
        }
        if (currentCharacter.currHP <= 0)
        {
            currentCharacter.currHP = 0;
            currentCharacter.isActive = false;
            StartCoroutine(FindNextTurn());
            return;
        }
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Berserk))
        {
            if (enemies.Count > 1)
            {
                List<Enemy> targets = enemies.Where(e => e.isActive).ToList()
                    ;
                if (targets.Where(e => !e.isBackRow).Any())
                {
                    targets = targets.Where(e => !e.isBackRow).ToList();
                }

                StartCoroutine(PlayerAttack(targets[Random.Range(0, targets.Count - 1)]));
            }
            else
                StartCoroutine(PlayerAttack(enemies.First()));
            return;
        }
        uiHandler.battleHUD.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(!currentCharacter.isBackRow);
    }
    public IEnumerator PlayerAttack(Character target)
    {
        state = BattleStates.WAIT;

        uiHandler.ResetUI();

        int attackPower = currentCharacter.Attack(target, turnCounter);

        battleStationManager.SetText(attackPower.ToString(), target);

        yield return new WaitForSeconds(.55f);

        battleStationManager.SetText(string.Empty, target);

        if (target.currHP <= 0) { target.isActive = false; }

        yield return new WaitForSeconds(.75f);

        state = BattleStates.WAIT;
        StartCoroutine(FindNextTurn());
    }
    public void EndBattle()
    {
        uiHandler.ResetUI();
        dataHudHandler.DeactivateEnemyHud();

        if (state == BattleStates.LOSS)
        {
            uiHandler.StartCoroutine(uiHandler.OnLoss());
        }
        else if (state == BattleStates.WIN)
        {
            uiHandler.OnWin(enemies, playerCharacterList);
            GameManager.instance.audioManager.ChangeAudio(victory);
            StartCoroutine(sceneManager.TransitionFromBattle(playerCharacterList, true));
        }
        else if (state == BattleStates.FLEE)
        {
            uiHandler.OnFlee(true);
            StartCoroutine(sceneManager.TransitionFromBattle(playerCharacterList, false));
        }
    }
    public IEnumerator EnemyTurn(Enemy currentEnemy)
    {
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Poisoned)) currentCharacter.currHP -= (int)(currentCharacter.maxHP / 16);
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Bleeding)) currentCharacter.currHP -= (int)(currentCharacter.maxHP / 10);
        if (currentCharacter.currStatuses.Any(s => s.status == Status.Paralyzed))
        {
            StartCoroutine(FindNextTurn());
            yield break;
        }
        if (currentCharacter.currHP <= 0)
        {
            currentCharacter.currHP = 0;
            currentCharacter.isActive = false;
            StartCoroutine(FindNextTurn());
            yield break;
        }

        yield return StartCoroutine(currentEnemy.EnemyTurn(playerCharacterList, enemies, turnCounter, battleStationManager, uiHandler.enemySkillUI));

        state = BattleStates.WAIT;
        StartCoroutine(FindNextTurn());
    }
    public void OnAttackButton()
    {
        uiHandler.UIOnAttack();
    }

    public void OnFleeButton()
    {
        uiHandler.ResetUI();

        bool fleeCheck = currentCharacter.FleeCheck(enemies.Where(e => e.isActive).OrderByDescending(c => c.agility).First(), turnCounter);
        uiHandler.fleeBox.SetFleeBox(fleeCheck);

        if (fleeCheck)
        {
            state = BattleStates.FLEE;
            EndBattle();
        }
        else
        {
            StartCoroutine(FailedFlee());
        }
    }

    IEnumerator FailedFlee()
    {
        uiHandler.OnFlee(true);
        yield return new WaitForSeconds(1f);
        uiHandler.OnFlee(false);
        yield return new WaitForSeconds(.5f);

        StartCoroutine(FindNextTurn());
    }

    public void UpdateHUDs()
    {
        foreach (CharacterHUD hud in characterHUDList) hud.SetHUD();
        foreach (EnemyHUD hud in enemyHUDList) hud.SetHUD();
    }

    public int FindTurnValue(Character character)
    {
        int returnValue = character.agility + (character.luck / 10);

        returnValue += Random.Range(0, 3);

        return returnValue;
    }
}
