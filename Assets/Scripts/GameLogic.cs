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
  #endregion

  #region Initialization Methods
  public GameLogic(PlayerController playerController, MonsterController monsterController, ComboController comboController) {
    this.playerController = playerController;
    this.monsterController = monsterController;
    this.comboController = comboController;

    this.playerController.playFromStackToStackEvent += this.ResolveCardPlay;

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

  private void ResolveCardPlay(CardLogic cardPlayed, CardStackLogic fromStack, CardStackLogic toStack) {
    List<CombatAction> playerActions = this.comboController.GetActionsFromAllSatisfiedCombos(new CardPlayData(cardPlayed.CardObject.GetComponent<Card>(), toStack.CardStackObject.GetComponent<CardStack>()));
    List<CombatAction> monsterActions = new List<CombatAction> { this.monsterController.PopNextAction() };
    this.ResolveCombatPhase(playerActions, monsterActions);
  }

  private void ResolveCombatPhase(List<CombatAction> playerActions, List<CombatAction> monsterActions) {
    this.playerController.PlayerCombatEntity.NewTurnPreProcessing(playerActions);
    this.monsterController.MonsterCombatEntity.NewTurnPreProcessing(monsterActions);

    // Assume player has priority, this can be a setting
    this.playerController.PlayerCombatEntity.ApplyTurnActions();
    if (this.playerController.PlayerCombatEntity.IsDead) {
      Debug.Log("You Lose! (Handle lose condition here)");
      return;
    }

    this.monsterController.MonsterCombatEntity.ApplyTurnActions();
    if (this.monsterController.MonsterCombatEntity.IsDead) {
      Debug.Log("You Win! (Handle win condition here)");
      return;
    }
  }
  #endregion

  public struct CardPlayData {
    public Card playedCard;
    public CardStack playedCardStack;

    public CardPlayData(Card playedCard, CardStack playedCardStack) {
      this.playedCard = playedCard;
      this.playedCardStack = playedCardStack;
    }
  }
}
