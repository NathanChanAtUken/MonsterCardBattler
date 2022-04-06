using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLogic {
  #region Fields
  [Header("Injected References")]
  [SerializeField]
  private PlayerController playerController;
  [SerializeField]
  private MonsterController monsterController;
  [SerializeField]
  private ComboController comboController;
  [SerializeField]
  private EndGameScreen endGameScreen;
  #endregion

  #region Initialization Methods
  public GameLogic(PlayerController playerController, MonsterController monsterController, ComboController comboController, EndGameScreen endGameScreen) {
    this.playerController = playerController;
    this.monsterController = monsterController;
    this.comboController = comboController;
    this.endGameScreen = endGameScreen;

    this.playerController.playFromHandToStackEvent += this.ResolveCardPlay;

    GameStart();
  }

  public void GameStart() {
    DrawCards(true);
  }
  #endregion

  #region Event Methods
  private void DrawCards(bool fillHand = false, int numCards = 1) {
    numCards = fillHand ? playerController.PlayerHand.EmptySlots() : numCards;

    for (int i = 0; i < numCards; i++) {
      playerController.DrawCard();
    }
  }

  private void ResolveCardPlay(CardLogic cardPlayed, CardStackLogic toStack) {
    List<CombatAction> playerActions = this.comboController.GetActionsFromAllSatisfiedCombos(new CardPlayData(cardPlayed.CardObject.GetComponent<Card>(), toStack.CardStackObject.GetComponent<CardStack>()));
    List<CombatAction> monsterActions = new List<CombatAction> { this.monsterController.PopNextAction() };
    this.monsterController.GenerateNewAction();
    this.ResolveCombatPhase(playerActions, monsterActions);
  }

  private void ResolveCombatPhase(List<CombatAction> playerActions, List<CombatAction> monsterActions) {
    bool win = false;
    bool lose = false;

    this.playerController.PlayerCombatEntity.NewTurnPreProcessing(playerActions);
    this.monsterController.MonsterCombatEntity.NewTurnPreProcessing(monsterActions);

    // Assume player has priority, this can be a setting
    this.playerController.PlayerCombatEntity.ApplyTurnActions();
    this.monsterController.MonsterCombatEntity.ApplyTurnActions();

    if (this.monsterController.MonsterCombatEntity.IsDead) {
      win = true;
    }

    if (this.playerController.PlayerCombatEntity.IsDead) {
      lose = true;
    }

    this.monsterController.RefreshMonsterView();
    this.playerController.RefreshPlayerView();

    if (win) {
      this.endGameScreen.ShowScreen(true);
    } else if (lose) {
      this.endGameScreen.ShowScreen(false);
    }
  }
  #endregion

  public struct CardPlayData {
    public Card playedCard;
    public Card cardPlayedOn;

    public CardPlayData(Card playedCard, CardStack playedCardStack) {
      this.playedCard = playedCard;
      this.cardPlayedOn = playedCardStack.CardStackLogic.CardAt(playedCardStack.CardStackLogic.StackLength() - 2).CardObject.GetComponent<Card>();
    }
  }
}
