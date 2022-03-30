using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardStack : CardStack {
    #region Fields
        [SerializeField]
        private PlayCardStackView playCardStackView;
        public PlayCardStackView PlayCardStackView {
          get { return this.playCardStackView; }
          set { this.playCardStackView = value; }
        }
    #endregion
    
    #region Initialization Methods
    public override void InitializeValues(CardStackLogic cardStackLogic) {
        this.cardStackLogic = cardStackLogic;
        this.playCardStackView.Initialize(cardStackLogic);
    }
    #endregion

    public override void AddCardToStack(Card card) {
        this.playCardStackView.AddCardToStack(card);
    }
}
