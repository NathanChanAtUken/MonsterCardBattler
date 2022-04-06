using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandView : CardStackView {
  [Header("Layout Parameters")]
  [SerializeField] private float horizontalSpacing;

  public override void Initialize(CardStackLogic cardStackLogic, bool animate) {
    base.Initialize(cardStackLogic, animate);
    cardStackLogic.CardStackObject.GetComponent<CardStack>().DisablePhysics();
  }

  public override Vector3 GetCardLocalPosition(int cardIndex) {
    float localXPosition = ((float)cardIndex / this.cards.Count - 0.5f) * (this.cards.Count - 1) * horizontalSpacing;
    return new Vector3(localXPosition, 0, 0);
  }

  public override bool ShouldCardBeFaceUp(int cardIndex) {
    return true;
  }

  public override int GetCardSortingOrder(int cardIndex) {
    return cardIndex;
  }
}
