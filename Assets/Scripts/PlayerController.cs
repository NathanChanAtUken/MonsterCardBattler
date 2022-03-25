using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private CardStackLogic playerHand;
        public CardStackLogic PlayerHand {
            get { return playerHand; }
            set { playerHand = value; }
        }

        [SerializeField]
        private CardStackLogic drawStack;
        public CardStackLogic DrawStack {
            get { return drawStack; }
            set { drawStack = value; }
        }

        [SerializeField]
        private List<CardStackLogic> playStacks;
        public List<CardStackLogic> PlayStacks {
            get { return playStacks; }
            set { playStacks = value; }
        }

        [Header("Debugging Viewables")]
        [SerializeField]
        private List<CardLogic> selectedCards = new List<CardLogic>();
    #endregion

    #region Constructors
    public PlayerController(CardStackLogic playerHand, CardStackLogic drawStack, List<CardStackLogic> playStacks) {
        this.playerHand = playerHand;
        this.drawStack = drawStack;
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

    public void PlayFromStackToStack(CardStackLogic fromStack, CardStackLogic toStack) {
        toStack.PlayToStack(fromStack.UseTop());
    }

    public void PlayFromHandToStack(CardStackLogic playStack) {
        PlayFromStackToStack(playerHand, playStack);
    }

    public void DrawCard() {
        PlayFromStackToStack(drawStack, playerHand);
    }
    #endregion
}
