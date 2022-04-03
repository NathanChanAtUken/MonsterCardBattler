using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterQueueEntry : MonoBehaviour {
  [SerializeField] private Image actionIcon;
  [SerializeField] private TextMeshProUGUI actionText;
  [SerializeField] private Image background;

  [SerializeField] private Sprite attackIcon;
  [SerializeField] private Sprite defendIcon;

  private Color attackColor = new Color(0.75f, 0.19f, 0.19f);
  private Color defendColor = new Color(0.17f, 0.44f, 0.75f);

  public void Initialize(CombatAction combatAction) {
    string actionDesc;
    switch (combatAction.Type) {
      case CombatAction.CombatActionType.Attack:
        this.actionIcon.sprite = attackIcon;
        actionDesc = "Attack";
        this.background.color = this.attackColor;
        break;
      case CombatAction.CombatActionType.Defend:
        this.actionIcon.sprite = defendIcon;
        actionDesc = "Defend";
        this.background.color = this.defendColor;
        break;
      default:
        actionDesc = "Null Action";
        break;
    }

    this.actionText.text = System.String.Format("{0} {1}", actionDesc, combatAction.Value);
  }
}
