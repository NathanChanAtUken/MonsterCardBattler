using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardLogic {
    #region Fields
        [Header("Injected References")]
        [SerializeField]
        private GameObject cardObject;
        public GameObject CardObject {
            get { return cardObject; }
            set { cardObject = value; }
        }

        public enum CardColor {
            Black = 0,
            Red = 1
        }
        [Header("Card Properties")]
        [SerializeField]
        private CardColor color;
        public CardColor Color {
            get { return color; }
            set { color = value; }
        }
        
        public enum CardSuit {
            Diamond = 0,
            Club = 1,
            Heart = 2,
            Spade = 3
        }
        [SerializeField]
        private CardSuit suit;
        public CardSuit Suit {
            get { return suit; }
            set { suit = value; }
        }

        [SerializeField]
        private int rank;
        public int Rank {
            get { return rank; }
            set { rank = value; }
        }
    #endregion

    #region Constructors
    public CardLogic(GameObject cardObject = null, bool instantiateRandomized = true, CardColor color = CardColor.Red, CardSuit suit = CardSuit.Diamond, int rank = 1) {
        this.cardObject = cardObject;

        this.color = color;
        this.suit = suit;
        this.rank = rank;

        if (instantiateRandomized) {
            this.rank = UnityEngine.Random.Range(1, 14); // Inclusive 1 = A, Exclusive 14 (13 = K)
            this.suit = (CardSuit)UnityEngine.Random.Range(0, Enum.GetNames(typeof(CardSuit)).Length);
            if (this.suit == CardSuit.Club || this.suit == CardSuit.Spade) {
                this.color = CardColor.Black;
            } else {
                this.color = CardColor.Red;
            }
        }
    }
    #endregion

    #region Event Methods
    #endregion
}
