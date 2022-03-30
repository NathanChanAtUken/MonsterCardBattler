using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardStackView : CardStackView {
  [SerializeField] private int extraVisibleCards = 1;
  [SerializeField] private Vector2 cardStackOffset = new Vector2(-50, 50);

  private enum PlayCardStackViewState {
    IsInBottomStack,
    IsTopOfBottomStack,
    IsInExtraVisibleCards,
    IsTopCard
  }

  private int ActualExtraVisibleCards {
    get { return Mathf.Max(Mathf.Min(this.extraVisibleCards, cards.Count - 1), 0); }
  }

  private int BottomStackCount {
    get { return Mathf.Max(cards.Count - 1 - ActualExtraVisibleCards, 0); }
  }

  public override Vector3 GetCardLocalPosition(int cardIndex) {
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

  public override bool ShouldCardBeFaceUp(int cardIndex) {
    switch (this.GetCardState(cardIndex)) {
      case PlayCardStackViewState.IsInBottomStack:
        return false;
      case PlayCardStackViewState.IsTopOfBottomStack:
        return false;
      case PlayCardStackViewState.IsInExtraVisibleCards:
        return true;
      case PlayCardStackViewState.IsTopCard:
        return true;
      default:
        return true;
    }
  }

  public override int GetCardSortingOrder(int cardIndex) {
    return cardIndex;
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
}
