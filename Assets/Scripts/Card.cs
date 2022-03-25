using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    #region Fields
        [Header("Debugging Viewables")]
        [SerializeField]
        private CardLogic cardLogic;
        public CardLogic CardLogic {
            get { return cardLogic; }
            set { cardLogic = value; }
        }
    #endregion
    
    #region Initialization Methods
    public void InitializeValues(CardLogic cardLogic) {
        this.cardLogic = cardLogic;
    }
    #endregion
}
