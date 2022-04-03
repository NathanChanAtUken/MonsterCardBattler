using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        private CardStackLogic cardStackLogic;
        public CardStackLogic CardStackLogic {
            get { return cardStackLogic; }
            set { cardStackLogic = value; }
        }

        [SerializeField]
        private CardStackView cardStackView;
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardStackLogic cardStackLogic, CardStackView cardStackView) {
        this.cardStackLogic = cardStackLogic;
        this.cardStackView = cardStackView;
        this.cardStackView.Initialize(cardStackLogic);
    }
    #endregion

    public void AddCardToStack(Card card) {
        this.cardStackView.AddCardToStack(card);
    }

    public void RemoveCardFromStack(Card card) {
        this.cardStackView.RemoveCardFromStack(card);
    }
}
