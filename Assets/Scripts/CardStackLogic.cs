using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CardStackLogic {
    #region Fields
        [Header("Injected Parameters")]
        [SerializeField]
        private int stackLimit;
        public int StackLimit {
            get { return stackLimit; }
            set { stackLimit = value; }
        }

        [Header("Debugging Viewables")]
        [SerializeField]
        private List<CardLogic> cardStack;
    #endregion

    #region Initialization Methods
    public CardStackLogic(List<CardLogic> cardStack = null, int stackLimit = int.MaxValue) {
        StackLimit = stackLimit;

        if (cardStack == null) {
            this.cardStack = new List<CardLogic>();
        }
        else {
            this.cardStack = cardStack;
        }
    }
    #endregion

    #region Data Methods
    public void PlayCardAt(CardLogic playedCard, int index) {
        cardStack.Insert(index, playedCard);
    }

    public void PlayToStack(CardLogic playedCard) {
        PlayCardAt(playedCard, cardStack.Count);
    }

    public CardLogic CardAt(int index) {
        return cardStack[index];
    }

    public CardLogic TopCard() {
        return CardAt(cardStack.Count - 1);
    }

    public CardLogic RemoveAt(int index) {
        CardLogic removedCard = CardAt(index);
        cardStack.RemoveAt(index);
        return removedCard;
    }

    public CardLogic UseTop() {
        return RemoveAt(cardStack.Count - 1);
    }

    public int EmptySlots() {
        return stackLimit - cardStack.Count;
    }

    public bool IsFull() {
        return cardStack.Count == stackLimit;
    }

    public bool ContainsCard(CardLogic card) {
        return cardStack.Contains(card);
    }
    #endregion
}