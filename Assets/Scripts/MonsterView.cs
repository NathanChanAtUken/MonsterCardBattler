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
  [SerializeField] private float queueSpacing = 25f;

  public void Initialize(Monster monster, int currentHealth, List<CombatAction> actionQueue) {
    this.monsterName.text = monster.MonsterName;
    this.monsterImage.sprite = monsterSprites.Get(monster.MonsterKey);
    this.RefreshHealth(currentHealth, monster.MaxHealth);

    foreach (Transform child in queueEntryInstantiator.transform) {
      Destroy(child.gameObject);
    }

    foreach (CombatAction action in actionQueue) {
      CombatActionView monsterQueueEntry = Instantiate(monsterQueueEntryPrefab, this.queueEntryInstantiator).GetComponent<CombatActionView>();
      monsterQueueEntry.Initialize(action);
    }
  }

  private void RefreshHealth(int currentHealth, int maxHealth) {
    this.monsterHealth.text = System.String.Format("{0} / {1}", currentHealth, maxHealth);
  }
}
