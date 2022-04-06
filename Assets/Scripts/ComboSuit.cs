using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboSuit : Combo {
  [SerializeField]
  private CardLogic.CardSuit firstSuit;
  public CardLogic.CardSuit FirstSuit {
    get { return this.firstSuit; }
    set { this.firstSuit = value; }
  }

  [SerializeField]
  private CardLogic.CardSuit secondSuit;
  public CardLogic.CardSuit SecondSuit {
    get { return this.secondSuit; }
    set { this.secondSuit = value; }
  }

  public ComboSuit(CombatAction resultingAction, CardLogic.CardSuit firstSuit, CardLogic.CardSuit secondSuit) : base(resultingAction) {
    this.firstSuit = firstSuit;
    this.secondSuit = secondSuit;
  }

  public override bool IsComboSatisfied(GameLogic.CardPlayData cardPlayData) {
    CardLogic.CardSuit firstCardSuit = cardPlayData.playedCard.CardLogic.Suit;
    CardLogic.CardSuit secondCardSuit = cardPlayData.cardPlayedOn.CardLogic.Suit;
    return (firstCardSuit == firstSuit && secondCardSuit == secondSuit) || (firstCardSuit == secondSuit && secondCardSuit == firstSuit);
  }
}
