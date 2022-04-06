using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterController {
  [SerializeField]
  private CombatEntity monsterCombatEntity;
  public CombatEntity MonsterCombatEntity {
    get { return this.monsterCombatEntity; }
    set { this.monsterCombatEntity = value; }
  }

  [SerializeField] private Monster monster;
  [SerializeField] private CombatEntity opponent;
  [SerializeField] private List<CombatAction> actionQueue;
  [SerializeField] private MonsterView monsterView;

  public MonsterController(Monster monster, CombatEntity opponent, MonsterView monsterView) {
    this.monsterCombatEntity = new BasicCombatEntity(monster.MaxHealth);
    this.monster = monster;
    this.opponent = opponent;
    this.actionQueue = CombatAction.GenerateRandomActions(5, 3, this.monsterCombatEntity, this.opponent);
    this.monsterView = monsterView;

    this.RefreshMonsterView();
  }

  public CombatAction PopNextAction() {
    if (this.actionQueue.Count == 0) {
      return null;
    }

    CombatAction action = this.actionQueue[this.actionQueue.Count - 1];
    this.actionQueue.RemoveAt(this.actionQueue.Count - 1);
    return action;
  }

  public void GenerateNewAction() {
    this.actionQueue.Insert(0, CombatAction.GenerateRandomAction(3, this.monsterCombatEntity, this.opponent));
  }

  public void RefreshMonsterView() {
    this.monsterView.Initialize(this.monster, this.monsterCombatEntity.Health, this.actionQueue);
  }
}
