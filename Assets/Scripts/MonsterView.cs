using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour {
  [SerializeField] private SpriteDictionary monsterSprites;
  [SerializeField] private TextMeshProUGUI monsterName;
  [SerializeField] private Image monsterImage;
  [SerializeField] private TextMeshProUGUI monsterHealth;
  [SerializeField] private Transform queueEntryInstantiator;

  [SerializeField] private GameObject monsterQueueEntryPrefab;

  public void Initialize(Monster monster, int currentHealth, List<CombatAction> actionQueue) {
    this.monsterName.text = monster.MonsterName;
    this.monsterImage.sprite = monsterSprites.Get(monster.MonsterKey);
    this.RefreshHealth(currentHealth, monster.MaxHealth);

    foreach (CombatAction combatAction in actionQueue) {
      CombatActionView monsterQueueEntry = Instantiate(monsterQueueEntryPrefab, this.queueEntryInstantiator).GetComponent<CombatActionView>();
      monsterQueueEntry.Initialize(combatAction);
    }
  }

  private void RefreshHealth(int currentHealth, int maxHealth) {
    this.monsterHealth.text = System.String.Format("{0} / {1}", currentHealth, maxHealth);
  }
}
