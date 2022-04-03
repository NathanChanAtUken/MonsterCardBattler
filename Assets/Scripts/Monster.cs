using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster {

  public string MonsterKey { get; set; }

  public string MonsterName { get; set; }

  public int MaxHealth { get; set; }

  public int CurrentHealth { get; set; }

  public List<CombatAction> ActionQueue { get; set; }

  public Monster(string monsterKey, string monsterName, int maxHealth, int currentHealth, List<CombatAction> actionQueue) {
    this.MonsterKey = monsterKey;
    this.MonsterName = monsterName;
    this.MaxHealth = maxHealth;
    this.CurrentHealth = currentHealth;
    this.ActionQueue = actionQueue;
  }

  public static Monster GenerateDefaultMonster() {
    return new Monster("vanilla", "Vanilla, Defeater Of Chocolate", 10, 10, CombatAction.GenerateRandomActions(5, 5));
  }
}
