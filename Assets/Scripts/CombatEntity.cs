using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CombatEntity {
  [SerializeField]
  private int health;
  public int Health {
    get { return this.health; }
    set { this.health = value; }
  }

  [SerializeField]
  private List<CombatAction> turnActions;
  public List<CombatAction> TurnActions {
    get { return this.turnActions; }
    set { this.turnActions = value; }
  }

  [SerializeField]
  private int turnDefense;
  public int TurnDefense {
    get { return this.turnDefense; }
    set { this.turnDefense = value; }
  }

  public bool IsDead {
    get { return this.health <= 0; }
  }

  public CombatEntity(int health) {
    this.health = health;
    this.turnActions = new List<CombatAction>();
  }

  public abstract void ApplyTurnActions();

  public abstract void NewTurnPreProcessing(List<CombatAction> turnActions);
}
