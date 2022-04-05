using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster {

  public string MonsterKey { get; set; }

  public string MonsterName { get; set; }

  public int MaxHealth { get; set; }

  public Monster(string monsterKey, string monsterName, int maxHealth) {
    this.MonsterKey = monsterKey;
    this.MonsterName = monsterName;
    this.MaxHealth = maxHealth;
  }

  public static Monster GenerateDefaultMonster() {
    return new Monster("vanilla", "Vanilla, Defeater Of Chocolate", 10);
  }
}
