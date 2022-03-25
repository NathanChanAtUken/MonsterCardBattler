using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStackLayout : MonoBehaviour {
  [Header("Prefab References")]
  [SerializeField]
  private GameObject CardStackPrefab, PlayerHandPrefab;

  [Header("Prefab Instantiators")]
  [SerializeField]
  private Transform cardStackInstantiator, playerHandInstantiator;

  [Header("Layout Parameters")]
  [SerializeField]
  private float horizontalSpacing;

  private List<CardStack> cardStacks;
  private PlayerHand playerHand;

  public void InitializePlayStacks(List<CardStackLogic> cardStackLogics) {
    cardStacks = new List<CardStack>();

    float totalWidth = (cardStackLogics.Count - 1) * horizontalSpacing;
    for (int i = 0; i < cardStackLogics.Count; i++) {
      float localXPosition = ((float)i / cardStackLogics.Count - 0.5f) * totalWidth;
      cardStacks.Add(this.InstantiateCardStack(cardStackLogics[i], new Vector3(localXPosition, 0, 0)));
    }
  }

  public void InitializePlayerHand(CardStackLogic playerHandLogic) {
    this.playerHand = Instantiate(this.PlayerHandPrefab, this.playerHandInstantiator).GetComponent<PlayerHand>();
  }

  private CardStack InstantiateCardStack(CardStackLogic cardStackLogic, Vector3 localPosition) {
    CardStack cardStack = Instantiate(this.CardStackPrefab, this.cardStackInstantiator).GetComponent<CardStack>();
    cardStack.InitializeValues(cardStackLogic);
    cardStack.transform.localPosition = localPosition;
    return cardStack;
  }
}
