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

  private Color attackColor = new Color(191, 48, 48);
  private Color defendColor = new Color(43, 112, 192);
}
