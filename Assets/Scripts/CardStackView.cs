using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStackView : MonoBehaviour {
  [SerializeField] private CardStackLogic cardStackLogic;
  [SerializeField] private GameObject cardDisplayPrefab;
  [SerializeField] private Transform cardInstantiator;

  [SerializeField] private int extraVisibleCards = 2;
  [SerializeField] private Vector2 cardStackOffset = new Vector2(-50, 50);

  private void Start() {
    CardLogic card1 = new CardLogic();
    card1.Color = CardLogic.CardColor.Red;
    card1.Suit = CardLogic.CardSuit.Heart;
    card1.Rank = 1;
    CardLogic card2 = new CardLogic();
    card2.Color = CardLogic.CardColor.Red;
    card2.Suit = CardLogic.CardSuit.Heart;
    card2.Rank = 2;
    CardLogic card3 = new CardLogic();
    card3.Color = CardLogic.CardColor.Red;
    card3.Suit = CardLogic.CardSuit.Heart;
    card3.Rank = 3;
    CardLogic card4 = new CardLogic();
    card4.Color = CardLogic.CardColor.Red;
    card4.Suit = CardLogic.CardSuit.Heart;
    card4.Rank = 4;
    CardLogic card5 = new CardLogic();
    card5.Color = CardLogic.CardColor.Red;
    card5.Suit = CardLogic.CardSuit.Heart;
    card5.Rank = 5;
    CardLogic card6 = new CardLogic();
    card6.Color = CardLogic.CardColor.Red;
    card6.Suit = CardLogic.CardSuit.Heart;
    card6.Rank = 6;

    this.cardStackLogic.StackLimit = 6;
    this.cardStackLogic.PlayToStack(card6);
    this.cardStackLogic.PlayToStack(card5);
    this.cardStackLogic.PlayToStack(card4);
    this.cardStackLogic.PlayToStack(card3);
    this.cardStackLogic.PlayToStack(card2);
    this.cardStackLogic.PlayToStack(card1);
    this.RefreshCardStack(this.cardStackLogic);
  }

  public void RefreshCardStack(CardStackLogic cardStackLogic) {
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

  private void InstantiateCard(CardLogic card, Vector3 position, bool isCardFaceUp, int sortingOrder) {
    CardDisplay currentCard = Instantiate(cardDisplayPrefab, position, Quaternion.identity, cardInstantiator).GetComponent<CardDisplay>();
    if (currentCard != null) {
      currentCard.Initialize(card, isCardFaceUp, sortingOrder);
    } else {
      Debug.LogError("Error: Tried to instantiate CardDisplay prefab, prefab did not contain CardDisplay script.");
      return;
    }
  }
}
