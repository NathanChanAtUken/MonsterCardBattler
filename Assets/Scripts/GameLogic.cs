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

  public delegate void OnCardPlayDelegate(CardPlayData data);
  public static event OnCardPlayDelegate onCardPlayDelegate;
  #endregion

  #region Initialization Methods
  public GameLogic(PlayerController playerController, ComboController comboController) {
    this.playerController = playerController;
    this.comboController = comboController;

    onCardPlayDelegate += this.ResolveCardPlay;

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

  private void ResolveCardPlay(CardPlayData data) {
    List<CombatAction> playerActions = this.comboController.GetActionsFromAllSatisfiedCombos(data);
    List<CombatAction> monsterActions = new List<CombatAction> { this.monsterController.PopNextAction() };
    this.ResolveCombatPhase(playerActions, monsterActions);
  }

  private void ResolveCombatPhase(List<CombatAction> playerActions, List<CombatAction> monsterActions) {
    playerController.PlayerCombatEntity.NewTurnPreProcessing(playerActions);
    monsterController.MonsterCombatEntity.NewTurnPreProcessing(monsterActions);

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
