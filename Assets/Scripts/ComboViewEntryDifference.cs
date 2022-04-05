using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboViewEntryDifference : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI differenceText;
  [SerializeField] private CombatActionView combatActionView;

  public void Initialize(ComboDifference comboDifference) {
    this.differenceText.text = comboDifference.Difference.ToString();
    this.combatActionView.Initialize(comboDifference.ResultingAction);
  }
}
