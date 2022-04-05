using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicCombatEntity : CombatEntity {
  public BasicCombatEntity(int health) : base(health) { }

  public override void ApplyTurnActions() {
    foreach (CombatAction action in this.TurnActions) {
      switch (action.Type) {
        case CombatAction.CombatActionType.Attack:
          this.ApplyAttackAction(action);
          break;
        case CombatAction.CombatActionType.Defend:
          // Do nothing; defense should be applied in preprocessing step
          break;
        default:
          break;
      }
    }
  }

  public override void NewTurnPreProcessing(List<CombatAction> turnActions) {
    this.TurnActions = turnActions;
    this.TurnDefense = 0;

    foreach (CombatAction turnAction in this.TurnActions) {
      if (turnAction.Type == CombatAction.CombatActionType.Defend) {
        this.TurnDefense += turnAction.Value;
      }
    }
  }

  private void ApplyAttackAction(CombatAction attackAction) {
    int damage = attackAction.Value;
    if (attackAction.Target.TurnDefense > damage) {
      attackAction.Target.TurnDefense -= damage;
      return;
    }

    damage -= attackAction.Target.TurnDefense;
    attackAction.Target.TurnDefense = 0;
    attackAction.Target.Health -= damage;
  }
}
