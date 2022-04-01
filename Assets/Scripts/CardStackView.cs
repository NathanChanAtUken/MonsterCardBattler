using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardStackView : MonoBehaviour {
  [Header("Prefab Instantiators")]
  [SerializeField] protected Transform cardInstantiator;

  protected List<Card> cards;

  public void Initialize(CardStackLogic cardStackLogic) {
    int cardCount = cardStackLogic.StackLimit - cardStackLogic.EmptySlots();
    cards = new List<Card>();
    for (int cardIndex = 0; cardIndex < cardCount; cardIndex++) {
      cards.Add(cardStackLogic.CardAt(cardIndex).CardObject.GetComponent<Card>());
    }

    this.RefreshAllCards(false);
  }

  public void AddCardToStack(Card card) {
    this.cards.Add(card);
    this.RefreshAllCards(true);
  }

  public void RemoveCardFromStack(Card card) {
    if (this.cards.Remove(card)) {
      Debug.LogError("Error: Tried removing a card from card stack that does not exist.");
    }

    this.RefreshAllCards(true);
  }

  public void RefreshAllCards(bool animate) {
    for (int i = 0; i < this.cards.Count; i++) {
      Card card = this.cards[i];

      card.transform.SetParent(this.cardInstantiator);
      card.CardView.SortingOrder = this.GetCardSortingOrder(i);

      Vector3 newLocalPosition = this.GetCardLocalPosition(i);
      if (card.transform.localPosition != newLocalPosition) {
        if (animate) {
          card.CardView.MoveToLocal(newLocalPosition);
        } else {
          card.transform.localPosition = newLocalPosition;
        }
      }

      if (this.ShouldCardBeFaceUp(i)) {
        if (card.CardView.IsFaceUp == false) {
          if (animate) {
            card.CardView.FlipCardUp();
          } else {
            card.CardView.ShowFront();
          }
        }
      } else if (card.CardView.IsFaceUp) {
        if (animate) {
          card.CardView.FlipCardDown();
        } else {
          card.CardView.ShowBack();
        }
      }
    }
  }

  public abstract Vector3 GetCardLocalPosition(int cardIndex);

  public abstract bool ShouldCardBeFaceUp(int cardIndex);

  public abstract int GetCardSortingOrder(int cardIndex);
}
