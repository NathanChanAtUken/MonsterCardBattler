using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour {
  [SerializeField] private SpriteDictionary cardSprites;
  [SerializeField] private GameObject cardBack;
  [SerializeField] private GameObject cardFront;
  [SerializeField] private SpriteRenderer suitRenderer;
  [SerializeField] private SpriteRenderer numberCornerRenderer;
  [SerializeField] private SpriteRenderer numberCenterRenderer;
  [SerializeField] private SortingGroup sortingGroup;

  public void Initialize(CardLogic cardLogic, bool isFaceUp = true, int sortingOrder = 0) {
    this.sortingGroup.sortingOrder = sortingOrder;

    if (cardLogic == null) {
      this.ShowBack();
      return;
    }

    this.suitRenderer.sprite = this.GetSuitSprite(cardLogic.Suit);
    this.numberCornerRenderer.sprite = this.GetNumberSprite(cardLogic.Color, cardLogic.Rank);
    this.numberCenterRenderer.sprite = this.GetNumberSprite(cardLogic.Color, cardLogic.Rank);

    if (isFaceUp) {
      this.ShowFront();
    } else {
      this.ShowBack();
    }
  }

  public void ShowFront() {
    this.cardFront.SetActive(true);
    this.cardBack.SetActive(false);
  }

  public void ShowBack() {
    this.cardFront.SetActive(false);
    this.cardBack.SetActive(true);
  }

  private Sprite GetNumberSprite(CardLogic.CardColor cardColor, int rank) {
    switch (cardColor) {
      case CardLogic.CardColor.Black:
        switch (rank) {
          case (1):
            return cardSprites.Get("Black_A");
          case (2):
            return cardSprites.Get("Black_2");
          case (3):
            return cardSprites.Get("Black_3");
          case (4):
            return cardSprites.Get("Black_4");
          case (5):
            return cardSprites.Get("Black_5");
          case (6):
            return cardSprites.Get("Black_6");
          case (7):
            return cardSprites.Get("Black_7");
          case (8):
            return cardSprites.Get("Black_8");
          case (9):
            return cardSprites.Get("Black_9");
          case (10):
            return cardSprites.Get("Black_10");
          case (11):
            return cardSprites.Get("Black_J");
          case (12):
            return cardSprites.Get("Black_Q");
          case (13):
            return cardSprites.Get("Black_K");
          default:
            Debug.Log("Error: Invalid card rank for black card.");
            return null;
        }
      case CardLogic.CardColor.Red:
        switch (rank) {
          case (1):
            return cardSprites.Get("Red_A");
          case (2):
            return cardSprites.Get("Red_2");
          case (3):
            return cardSprites.Get("Red_3");
          case (4):
            return cardSprites.Get("Red_4");
          case (5):
            return cardSprites.Get("Red_5");
          case (6):
            return cardSprites.Get("Red_6");
          case (7):
            return cardSprites.Get("Red_7");
          case (8):
            return cardSprites.Get("Red_8");
          case (9):
            return cardSprites.Get("Red_9");
          case (10):
            return cardSprites.Get("Red_10");
          case (11):
            return cardSprites.Get("Red_J");
          case (12):
            return cardSprites.Get("Red_Q");
          case (13):
            return cardSprites.Get("Red_K");
          default:
            Debug.Log("Error: Invalid card rank for red card.");
            return null;
        }
      default:
        Debug.Log("Error: Invalid card colour.");
        return null;
    }
  }

  private Sprite GetSuitSprite(CardLogic.CardSuit cardSuit) {
    switch (cardSuit) {
      case CardLogic.CardSuit.Club:
        return cardSprites.Get("Suit_Club");
      case CardLogic.CardSuit.Diamond:
        return cardSprites.Get("Suit_Diamonds");
      case CardLogic.CardSuit.Heart:
        return cardSprites.Get("Suit_Hearts");
      case CardLogic.CardSuit.Spade:
        return cardSprites.Get("Suit_Spades");
      default:
        Debug.LogError("Error: Invalid card suit.");
        return null;
    }
  }
}
