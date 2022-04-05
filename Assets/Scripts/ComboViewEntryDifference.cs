using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboViewEntryDifference : ComboViewEntry {
  [SerializeField] private TextMeshProUGUI differenceText;

  public void Initialize(ComboDifference comboDifference) {
    this.differenceText.text = comboDifference.Difference.ToString();
  }
}
