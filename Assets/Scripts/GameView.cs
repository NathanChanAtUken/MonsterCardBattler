using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameView : MonoBehaviour {
  [Header("Prefab Instantiators")]
  [SerializeField]
  private Transform cardStackInstantiator, playerHandInstantiator;

  [Header("Layout Parameters")]
  [SerializeField]
  private float horizontalSpacing;

  private List<CardStack> cardStacks;
  private PlayerHand playerHand;

  public void ArrangePlayStacks(List<CardStackLogic> cardStackLogics) {
    this.ArrangePlayStacks(cardStackLogics.Select(logic => logic.CardStackObject.GetComponent<CardStack>()).ToList());
  }

  public void ArrangePlayStacks(List<CardStack> cardStacks) {
    this.cardStacks = new List<CardStack>();

    float totalWidth = (cardStacks.Count - 1) * horizontalSpacing;
    for (int i = 0; i < cardStacks.Count; i++) {
      float localXPosition = ((float)i / cardStacks.Count - 0.5f) * totalWidth;
      cardStacks[i].transform.SetParent(this.cardStackInstantiator);
      cardStacks[i].transform.localPosition = new Vector3(localXPosition, 0, 0);
      cardStacks[i].transform.localScale = Vector3.one;
      this.cardStacks.Add(cardStacks[i]);
    }
  }
}
