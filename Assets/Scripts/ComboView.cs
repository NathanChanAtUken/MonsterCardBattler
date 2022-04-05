using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboView : MonoBehaviour {
  [SerializeField] private Transform entryInstantiator;
  [SerializeField] private GameObject differenceComboPrefab;
  [SerializeField] private GameObject suitComboPrefab;

  public void Initialize(List<Combo> activeCombos) {
    foreach (Combo combo in activeCombos) {
      if (combo is ComboDifference comboDifference) {
        ComboViewEntryDifference entry = Instantiate(this.differenceComboPrefab, this.entryInstantiator).GetComponent<ComboViewEntryDifference>();
        entry.Initialize(comboDifference);
      } else if (combo is ComboSuit comboSuit) {
        ComboViewEntrySuit entry = Instantiate(this.suitComboPrefab, this.entryInstantiator).GetComponent<ComboViewEntrySuit>();
        entry.Initialize(comboSuit);
      }
    }
  }
}
