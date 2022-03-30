using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardStackView : MonoBehaviour {
  [SerializeField] private Transform cardInstantiator;

  [SerializeField] private int extraVisibleCards = 1;
  [SerializeField] private Vector2 cardStackOffset = new Vector2(-50, 50);

  private enum PlayCardStackViewState {
    IsInBottomStack,
    IsTopOfBottomStack,
    IsInExtraVisibleCards,
    IsTopCard
  }

  private List<Card> cards;

  private int ActualExtraVisibleCards {
    get { return Mathf.Max(Mathf.Min(this.extraVisibleCards, cards.Count - 1), 0); }
  }

  private int BottomStackCount {
    get { return Mathf.Max(cards.Count - 1 - ActualExtraVisibleCards, 0); }
  }

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

  private void RefreshAllCards(bool animate) {
    for (int i = 0; i < this.cards.Count; i++) {
      Card card = this.cards[i];
      PlayCardStackViewState cardState = this.GetCardState(i);
      card.transform.SetParent(this.cardInstantiator);
      card.CardView.SortingOrder = i;

      Vector3 newLocalPosition = this.GetCardLocalPosition(i);
      if (card.transform.localPosition != newLocalPosition) {
        if (animate) {
          card.CardView.MoveToLocal(newLocalPosition);
        } else {
          card.transform.localPosition = newLocalPosition;
        }
      }

      if (cardState == PlayCardStackViewState.IsInBottomStack || cardState == PlayCardStackViewState.IsTopOfBottomStack) {
        if (card.CardView.IsFaceUp) {
          if (animate) {
            card.CardView.FlipCardDown();
          } else {
            card.CardView.ShowBack();
          }
        }
      } else if (card.CardView.IsFaceUp == false) {
        if (animate) {
          card.CardView.FlipCardUp();
        } else {
          card.CardView.ShowFront();
        }
      }
    }
  }

  private PlayCardStackViewState GetCardState(int cardIndex) {
    if (cardIndex < BottomStackCount - 1) {
      return PlayCardStackViewState.IsInBottomStack;
    } else if (cardIndex == BottomStackCount - 1) {
      return PlayCardStackViewState.IsTopOfBottomStack;
    } else if (cardIndex < ActualExtraVisibleCards + BottomStackCount) {
      return PlayCardStackViewState.IsInExtraVisibleCards;
    } else {
      return PlayCardStackViewState.IsTopCard;
    }
  }

  private Vector3 GetCardLocalPosition(int cardIndex) {
    switch (this.GetCardState(cardIndex)) {
      case PlayCardStackViewState.IsInBottomStack:
        return new Vector3(cardStackOffset.x * (extraVisibleCards + 1), cardStackOffset.y * (extraVisibleCards + 1), 0);
      case PlayCardStackViewState.IsTopOfBottomStack:
        return new Vector3(cardStackOffset.x * (extraVisibleCards + 1), cardStackOffset.y * (extraVisibleCards + 1), 0);
      case PlayCardStackViewState.IsInExtraVisibleCards:
        return new Vector3(cardStackOffset.x * (ActualExtraVisibleCards - (cardIndex - BottomStackCount)), cardStackOffset.y * (ActualExtraVisibleCards - (cardIndex - BottomStackCount)), 0);
      case PlayCardStackViewState.IsTopCard:
        return Vector3.zero;
      default:
        return Vector3.zero;
    }
  }
}
