using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : CardStack {
    #region Fields
        [SerializeField]
        private PlayerHandView playerHandView;
        public PlayerHandView PlayerHandView {
          get { return this.playerHandView; }
          set { this.playerHandView = value; }
        }
    #endregion
    
    #region Initialization Methods
    public override void InitializeValues(CardStackLogic cardStackLogic) {
        this.cardStackLogic = cardStackLogic;
        this.playerHandView.Initialize(cardStackLogic);
    }
    #endregion

    public override void AddCardToStack(Card card) {
        // TO BE IMPLEMENTED
    }
}
