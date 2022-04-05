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
    // TO BE IMPLEMENTED
    return true;
  }
}
