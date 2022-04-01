using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CardStackLogic {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private GameObject cardStackObject;
        public GameObject CardStackObject {
            get { return cardStackObject; }
            set { cardStackObject = value; }
        }

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
    public CardStackLogic(GameObject cardStackObject = null, List<CardLogic> cardStack = null, int stackLimit = int.MaxValue) {
        this.cardStackObject = cardStackObject;
        this.stackLimit = stackLimit;

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
        playedCard.CardObject.transform.SetParent(cardStackObject.transform, true);
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

    public int Remove(CardLogic cardToRemove) {
        int indexOfCardToRemove = cardStack.IndexOf(cardToRemove);
        cardStack.Remove(cardToRemove);
        return indexOfCardToRemove;
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
