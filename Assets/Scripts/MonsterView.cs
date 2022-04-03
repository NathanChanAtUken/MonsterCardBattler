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

  void Start() {
    this.Initialize(Monster.GenerateDefaultMonster());
  }

  public void Initialize(Monster monster) {
    this.monsterName.text = monster.MonsterName;
    this.monsterImage.sprite = monsterSprites.Get(monster.MonsterKey);
    this.RefreshHealth(monster);

    foreach (CombatAction combatAction in monster.ActionQueue) {
      MonsterQueueEntry monsterQueueEntry = Instantiate(monsterQueueEntryPrefab, this.queueEntryInstantiator).GetComponent<MonsterQueueEntry>();
      monsterQueueEntry.Initialize(combatAction);
    }
  }

  private void RefreshHealth(Monster monster) {
    this.monsterHealth.text = System.String.Format("{0} / {1}", monster.CurrentHealth, monster.MaxHealth);
  }
}
