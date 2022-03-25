using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStackView : MonoBehaviour {
  [SerializeField] private GameObject cardPrefab;
  [SerializeField] private Transform cardInstantiator;

  [SerializeField] private int extraVisibleCards = 2;
  [SerializeField] private Vector2 cardStackOffset = new Vector2(-50, 50);

  public void Initialize(CardStackLogic cardStackLogic) {
    int cardCount = cardStackLogic.StackLimit - cardStackLogic.EmptySlots();
    if (cardCount <= 0) {
      return;
    }

    Vector3 cardPosition;
    int sortOrder = 0;
    int actualExtraVisibleCards = Mathf.Min(this.extraVisibleCards, cardCount - 1);
    bool showBottomStack = cardCount > actualExtraVisibleCards + 1;

    // Bottom Stack
    if (showBottomStack) {
      cardPosition = new Vector3(cardStackOffset.x * (actualExtraVisibleCards + 1), cardStackOffset.y * (actualExtraVisibleCards + 1), 0);
      this.InstantiateCard(null, cardPosition, false, sortOrder);
      sortOrder++;
    }

    // Visible Cards
    for (int i = actualExtraVisibleCards; i > 0; i--) {
      cardPosition = new Vector3(cardStackOffset.x * i, cardStackOffset.y * i, 0);
      this.InstantiateCard(cardStackLogic.CardAt(cardCount - 1 - i), cardPosition, true, sortOrder);
      sortOrder++;
    }

    // Top Card
    if (cardCount > 0) {
      this.InstantiateCard(cardStackLogic.TopCard(), Vector3.zero, true, sortOrder);
    }
  }

  private void InstantiateCard(CardLogic cardLogic, Vector3 position, bool isCardFaceUp, int sortingOrder) {
    Card currentCard = Instantiate(cardPrefab, cardInstantiator).GetComponent<Card>();
    if (currentCard != null) {
      currentCard.InitializeValues(cardLogic, isCardFaceUp, sortingOrder);
      currentCard.transform.localPosition = position;
    } else {
      Debug.LogError("Error: Tried to instantiate CardDisplay prefab, prefab did not contain CardDisplay script.");
      return;
    }
  }
}
