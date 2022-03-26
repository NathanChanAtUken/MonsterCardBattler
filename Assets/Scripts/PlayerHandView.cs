using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandView : MonoBehaviour {
  [Header("Prefab Instantiators")]
  [SerializeField] private Transform playerHandInstantiator;

  [Header("Layout Parameters")]
  [SerializeField] private float horizontalSpacing;

  public void Initialize(CardStackLogic playerHandLogic) {
    this.Refresh(playerHandLogic);
  }

  public void Refresh(CardStackLogic playerHandLogic) {
    int cardCount = playerHandLogic.StackLimit - playerHandLogic.EmptySlots();
    if (cardCount <= 0) {
      return;
    }

    float totalWidth = (cardCount - 1) * horizontalSpacing;
    for (int i = 0; i < cardCount; i++) {
      float localXPosition = ((float)i / cardCount - 0.5f) * totalWidth;
      this.ArrangeCard(playerHandLogic.CardAt(i).CardObject.GetComponent<Card>(), new Vector3(localXPosition, 0, 0), i);
    }
  }

  private void ArrangeCard(Card card, Vector3 localPosition, int sortingOrder) {
    if (card != null && card.CardView != null) {
      card.transform.SetParent(this.playerHandInstantiator);
      card.transform.localPosition = localPosition;
      card.CardView.SortingOrder = sortingOrder;
      card.CardView.ShowFront();
    } else {
      Debug.LogError("Error: Attempted to arrange null card or null card view.");
    }
  }
}
