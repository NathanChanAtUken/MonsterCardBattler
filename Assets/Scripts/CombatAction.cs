using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAction {
  public enum CombatActionType {
    Attack,
    Defend
  }

  public CombatActionType Type { get; set; }

  public int Value { get; set; }

  public CombatAction(CombatActionType type, int value) {
    this.Type = type;
    this.Value = value;
  }

  public static CombatAction GenerateRandomAction(int maxValue, System.Random seed = null) {
    System.Random random = seed == null ? new System.Random() : seed;
    System.Array values = System.Enum.GetValues(typeof(CombatActionType));
    return new CombatAction((CombatActionType)values.GetValue(random.Next(values.Length)), random.Next(1, maxValue + 1));
  }

  public static List<CombatAction> GenerateRandomActions(int actionCount, int maxValue) {
    List<CombatAction> combatActions = new List<CombatAction>();
    System.Random seed = new System.Random();
    for (int i = 0; i < actionCount; i++) {
      combatActions.Add(GenerateRandomAction(maxValue, seed));
    }

    return combatActions;
  }
}
