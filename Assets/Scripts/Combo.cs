using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Combo {
  [SerializeField]
  private CombatAction resultingAction;
  public CombatAction ResultingAction {
    get { return this.resultingAction; }
    set { this.resultingAction = value; }
  }

  public Combo(CombatAction resultingAction) {
    this.resultingAction = resultingAction;
  }

  public abstract bool IsComboSatisfied(GameLogic.CardPlayData cardPlayData);
}
