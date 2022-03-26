using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardStackView : MonoBehaviour {
  [SerializeField] private Transform cardInstantiator;

  [SerializeField] private int extraVisibleCards = 1;
  [SerializeField] private Vector2 cardStackOffset = new Vector2(-50, 50);

  public void Initialize(CardStackLogic cardStackLogic) {
    int cardCount = cardStackLogic.StackLimit - cardStackLogic.EmptySlots();
    if (cardCount <= 0) {
      return;
    }

    Vector3 cardPosition;
    int actualExtraVisibleCards = Mathf.Min(this.extraVisibleCards, cardCount - 1);
    int bottomStackCount = cardCount - 1 - actualExtraVisibleCards;
    int i = 0;

    // Bottom Stack
    for (; i < bottomStackCount; i++) {
      cardPosition = new Vector3(cardStackOffset.x * (extraVisibleCards + 1), cardStackOffset.y * (extraVisibleCards + 1), 0);
      this.ArrangeCard(cardStackLogic.CardAt(i).CardObject.GetComponent<Card>(), cardPosition, false, i);
    }

    // Visible Cards
    for (; i < actualExtraVisibleCards + bottomStackCount; i++) {
      cardPosition = new Vector3(cardStackOffset.x * (actualExtraVisibleCards - (i - bottomStackCount)), cardStackOffset.y * (actualExtraVisibleCards - (i - bottomStackCount)), 0);
      this.ArrangeCard(cardStackLogic.CardAt(i).CardObject.GetComponent<Card>(), cardPosition, true, i);
    }

    // Top Card
    if (cardCount > 0) {
      this.ArrangeCard(cardStackLogic.TopCard().CardObject.GetComponent<Card>(), Vector3.zero, true, i);
    }
  }

  private void ArrangeCard(Card card, Vector3 localPosition, bool isCardFaceUp, int sortingOrder) {
    if (card != null && card.CardView != null) {
      card.transform.SetParent(this.cardInstantiator);
      card.transform.localPosition = localPosition;
      card.CardView.SortingOrder = sortingOrder;
      if (isCardFaceUp) {
        card.CardView.ShowFront();
      } else {
        card.CardView.ShowBack();
      }
    } else {
      Debug.LogError("Error: Attempted to arrange null card or null card view.");
    }
  }
}
