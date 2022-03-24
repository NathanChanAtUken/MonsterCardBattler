using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private CardStackLogic playerHand;
        [SerializeField]
        private List<CardStackLogic> playStacks;

        [Header("Debugging Viewables")]
        [SerializeField]
        private List<CardLogic> selectedCards = new List<CardLogic>();
    #endregion

    #region Constructors
    public PlayerController(CardStackLogic playerHand, List<CardStackLogic> playStacks) {
        this.playerHand = playerHand;
        this.playStacks = playStacks;
    }
    #endregion

    #region Data Methods
    public bool IsInPlayerHand(CardLogic card) {
        return playerHand.ContainsCard(card);
    }
    #endregion

    #region Event Methods
    public void SelectCard(CardLogic selectedCard) {
        if (!selectedCards.Contains(selectedCard)) {
            selectedCards.Add(selectedCard);
        }
        else {
            selectedCards.Remove(selectedCard);
        }
    }
    #endregion
}
