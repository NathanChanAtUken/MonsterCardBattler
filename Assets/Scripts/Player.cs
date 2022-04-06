using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
  public string PlayerKey { get; set; }

  public string PlayerName { get; set; }

  public int MaxHealth { get; set; }

  public Player(string playerKey, string playerName, int maxHealth) {
    this.PlayerKey = playerKey;
    this.PlayerName = playerName;
    this.MaxHealth = maxHealth;
  }

  public static Player GenerateDefaultPlayer() {
    return new Player("rogan", "Rogan, Intrepid Intern", 10);
  }
}
