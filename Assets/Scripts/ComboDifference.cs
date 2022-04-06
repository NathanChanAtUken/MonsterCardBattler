using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboDifference : Combo {
  [SerializeField]
  private int difference;
  public int Difference {
    get { return this.difference; }
    set { this.difference = value; }
  }

  public ComboDifference(CombatAction resultingAction, int difference) : base(resultingAction) {
    this.difference = difference;
  }

  public override bool IsComboSatisfied(GameLogic.CardPlayData cardPlayData) {
    int firstCardRank = cardPlayData.playedCard.CardLogic.Rank;
    int secondCardRank = cardPlayData.cardPlayedOn.CardLogic.Rank;

    int largerRank = Mathf.Max(firstCardRank, secondCardRank);
    int smallerRank = Mathf.Min(firstCardRank, secondCardRank);

    int diff = Mathf.Min(largerRank - smallerRank, 13 - (largerRank - smallerRank));

    return this.difference == diff;
  }
}
