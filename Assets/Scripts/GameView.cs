using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameView : MonoBehaviour {
  [Header("Prefab Instantiators")]
  [SerializeField] private Transform cardStackInstantiator;
  [SerializeField] private Transform playerHandInstantiator;
  [SerializeField] private Transform drawStackInstantiator;

  [Header("Layout Parameters")]
  [SerializeField]
  private float horizontalSpacing;

  private List<CardStack> cardStacks;
  private CardStack playerHand;
  private CardStack drawStack;

  public void Initialize(List<CardStackLogic> cardStackLogics, CardStackLogic playerHandLogic, CardStackLogic drawStackLogic) {
    this.RefreshPlayStacks(cardStackLogics.Select(logic => logic.CardStackObject.GetComponent<CardStack>()).ToList());
    this.RefreshPlayerHand(playerHandLogic.CardStackObject.GetComponent<CardStack>());
    this.RefreshDrawStack(drawStackLogic.CardStackObject.GetComponent<CardStack>());
  }

  public void RefreshPlayStacks(List<CardStack> cardStacks) {
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

  public void RefreshPlayerHand(CardStack playerHand) {
    playerHand.transform.SetParent(this.playerHandInstantiator);
    playerHand.transform.localPosition = Vector3.zero;
    playerHand.transform.localScale = Vector3.one;
    this.playerHand = playerHand;
  }

  public void RefreshDrawStack(CardStack drawStack) {
    drawStack.transform.SetParent(this.drawStackInstantiator);
    drawStack.transform.localPosition = Vector3.zero;
    drawStack.transform.localScale = Vector3.one;
    this.drawStack = drawStack;
  }
}
