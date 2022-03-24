using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : MonoBehaviour {
    #region Fields
        public enum CardColor {
            Black = 0,
            Red = 1
        }
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
        private CardSuit suit;
        public CardSuit Suit {
            get { return suit; }
            set { suit = value; }
        }

        private int rank;
        public int Rank {
            get { return rank; }
            set { rank = value; }
        }
    #endregion

    #region Initialization Methods
    private void Start() {
        
    }
    #endregion

    #region Cycle Methods
    private void Update() {
        
    }
    #endregion

    #region Event Methods
    #endregion
}
