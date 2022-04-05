using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAction {
  public enum CombatActionType {
    Attack,
    Defend
  }

  [SerializeField]
  private CombatActionType type;
  public CombatActionType Type {
    get { return this.type; }
    set { this.type = value; }
  }

  [SerializeField]
  private int value;
  public int Value {
    get { return this.value; }
    set { this.value = value; }
  }

  [SerializeField]
  private CombatEntity target;
  public CombatEntity Target {
    get { return this.target; }
    set { this.target = value; }
  }

  public CombatAction(CombatActionType type, int value, CombatEntity target) {
    this.type = type;
    this.value = value;
    this.target = target;
  }

  public static CombatAction GenerateRandomAction(int maxValue, CombatEntity self, CombatEntity opponent, System.Random seed = null) {
    System.Random random = seed == null ? new System.Random() : seed;

    System.Array values = System.Enum.GetValues(typeof(CombatActionType));
    CombatActionType actionType = (CombatActionType)values.GetValue(random.Next(values.Length));
    CombatEntity actionTarget = null;
    switch (actionType) {
      case CombatActionType.Attack:
        actionTarget = opponent;
        break;
      case CombatActionType.Defend:
        actionTarget = self;
        break;
      default:
        actionTarget = null;
        break;
    }

    return new CombatAction(actionType, random.Next(1, maxValue + 1), actionTarget);
  }

  public static List<CombatAction> GenerateRandomActions(int actionCount, int maxValue, CombatEntity self, CombatEntity opponent) {
    List<CombatAction> combatActions = new List<CombatAction>();
    System.Random seed = new System.Random();
    for (int i = 0; i < actionCount; i++) {
      combatActions.Add(GenerateRandomAction(maxValue, self, opponent, seed));
    }

    return combatActions;
  }
}
