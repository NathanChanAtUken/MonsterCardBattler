using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStackView : MonoBehaviour {
  [Header("Prefab Instantiators")]
  [SerializeField] private Transform drawStackInstantiator;

  [Header("Layout Parameters")]
  [SerializeField] private float horizontalSpacing;

  public void Initialize(CardStackLogic drawStackLogic) {
    this.Refresh(drawStackLogic);
  }

  public void Refresh(CardStackLogic drawStackLogic) {
    int cardCount = drawStackLogic.StackLimit - drawStackLogic.EmptySlots();
    if (cardCount <= 0) {
      return;
    }

    float totalWidth = (cardCount - 1) * horizontalSpacing;
    for (int i = 0; i < cardCount; i++) {
      float localXPosition = ((float)i / cardCount - 0.5f) * totalWidth;
      this.ArrangeCard(drawStackLogic.CardAt(i).CardObject.GetComponent<Card>(), new Vector3(localXPosition, 0, 0), i);
    }
  }

  private void ArrangeCard(Card card, Vector3 localPosition, int sortingOrder) {
    if (card != null && card.CardView != null) {
      card.transform.SetParent(this.drawStackInstantiator);
      card.transform.localPosition = localPosition;
      card.CardView.SortingOrder = sortingOrder;
      card.CardView.ShowBack();
    } else {
      Debug.LogError("Error: Attempted to arrange null card or null card view.");
    }
  }
}
