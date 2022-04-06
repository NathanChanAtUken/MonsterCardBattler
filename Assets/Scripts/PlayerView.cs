using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerView : MonoBehaviour {
  [SerializeField] private SpriteDictionary playerSprites;
  [SerializeField] private TextMeshProUGUI playerName;
  [SerializeField] private Image playerImage;
  [SerializeField] private TextMeshProUGUI playerHealth;

  public void Initialize(Player player, int currentHealth) {
    this.playerName.text = player.PlayerName;
    this.playerImage.sprite = playerSprites.Get(player.PlayerKey);
    this.RefreshHealth(currentHealth, player.MaxHealth);
  }

  private void RefreshHealth(int currentHealth, int maxHealth) {
    this.playerHealth.text = System.String.Format("{0} / {1}", currentHealth, maxHealth);
  }
}
