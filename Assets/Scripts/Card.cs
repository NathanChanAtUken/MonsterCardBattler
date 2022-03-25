using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        private CardLogic cardLogic;
        [SerializeField]
        private CardView cardView;
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardLogic cardLogic, bool isFaceUp, int sortingOrder) {
        this.cardLogic = cardLogic;
        this.cardView.Initialize(cardLogic, isFaceUp, sortingOrder);
    }
    #endregion
}
