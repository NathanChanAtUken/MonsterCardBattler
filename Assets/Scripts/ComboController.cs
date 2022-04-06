using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboController {
  [SerializeField] private List<Combo> activeCombos;
  [SerializeField] private ComboView comboView;

  public ComboController(List<Combo> activeCombos, ComboView comboView) {
    this.activeCombos = activeCombos;
    this.comboView = comboView;

    this.comboView.Initialize(activeCombos);
  }

  public List<CombatAction> GetActionsFromAllSatisfiedCombos(GameLogic.CardPlayData cardPlayData) {
    List<CombatAction> actions = new List<CombatAction>();
    foreach (Combo combo in this.activeCombos) {
      if (combo.IsComboSatisfied(cardPlayData)) {
        actions.Add(combo.ResultingAction);
      }
    }

    return actions;
  }

  public static Combo GenerateRandomCombo(int actionMaxValue, CombatEntity self, CombatEntity opponent, System.Random seed = null) {
    System.Random random = seed == null ? new System.Random() : seed;
    CombatAction action = CombatAction.GenerateRandomAction(actionMaxValue, self, opponent, random);
    switch (random.Next(0, 2)) {
      case 0:
        return new ComboDifference(action, random.Next(0, 7));
      case 1:
        System.Array values = System.Enum.GetValues(typeof(CardLogic.CardSuit));
        CardLogic.CardSuit firstSuit = (CardLogic.CardSuit)values.GetValue(random.Next(values.Length));
        CardLogic.CardSuit secondSuit = (CardLogic.CardSuit)values.GetValue(random.Next(values.Length));
        return new ComboSuit(action, firstSuit, secondSuit);
      default:
        return null;
    }
  }

  public static List<Combo> GenerateRandomCombos(int comboCount, int actionMaxValue, CombatEntity self, CombatEntity opponent) {
    List<Combo> combos = new List<Combo>();
    System.Random seed = new System.Random();
    for (int i = 0; i < comboCount; i++) {
      combos.Add(GenerateRandomCombo(actionMaxValue, self, opponent, seed));
    }

    return combos;
  }

  public static List<Combo> GetDemoCombos(CombatEntity self, CombatEntity opponent) {
    List<Combo> demoCombos = new List<Combo>();
    demoCombos.Add(new ComboDifference(new CombatAction(CombatAction.CombatActionType.Defend, 3, self), 4));
    demoCombos.Add(new ComboSuit(new CombatAction(CombatAction.CombatActionType.Attack, 4, opponent), CardLogic.CardSuit.Heart, CardLogic.CardSuit.Spade));
    demoCombos.Add(new ComboSuit(new CombatAction(CombatAction.CombatActionType.Defend, 2, self), CardLogic.CardSuit.Diamond, CardLogic.CardSuit.Diamond));
    demoCombos.Add(new ComboDifference(new CombatAction(CombatAction.CombatActionType.Attack, 2, opponent), 2));
    return demoCombos;
  }
}
