using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboViewEntrySuit : MonoBehaviour {
  [SerializeField] private Image leftSuit;
  [SerializeField] private Image rightSuit;
  [SerializeField] private CombatActionView combatActionView;
  [SerializeField] private SpriteDictionary cardSprites;

  public void Initialize(ComboSuit comboSuit) {
    this.leftSuit.sprite = this.GetSuitSprite(comboSuit.FirstSuit);
    this.rightSuit.sprite = this.GetSuitSprite(comboSuit.SecondSuit);
    this.combatActionView.Initialize(comboSuit.ResultingAction);
  }

  private Sprite GetSuitSprite(CardLogic.CardSuit cardSuit) {
    switch (cardSuit) {
      case CardLogic.CardSuit.Club:
        return cardSprites.Get("Suit_Club");
      case CardLogic.CardSuit.Diamond:
        return cardSprites.Get("Suit_Diamonds");
      case CardLogic.CardSuit.Heart:
        return cardSprites.Get("Suit_Hearts");
      case CardLogic.CardSuit.Spade:
        return cardSprites.Get("Suit_Spades");
      default:
        Debug.LogError("Error: Invalid card suit.");
        return null;
    }
  }
}
