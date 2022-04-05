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
        public List<CardLogic> SelectedCards {
            get { return selectedCards; }
            set { selectedCards = value; }
        }

        [SerializeField]
        private CombatEntity playerCombatEntity;
        public CombatEntity PlayerCombatEntity {
            get { return this.playerCombatEntity; }
            set { this.playerCombatEntity = value; }
        }

        public delegate void OnPlayFromStackToStack(CardLogic cardPlayed, CardStackLogic fromStack, CardStackLogic toStack);
        public event OnPlayFromStackToStack playFromStackToStackEvent;
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
        if (selectedCards.Contains(selectedCard)) {
            return;
        }

        selectedCards.Add(selectedCard);
    }

    public void DeselectCard(CardLogic deselectedCard) {
        if (!selectedCards.Contains(deselectedCard)) {
            return;
        }

        selectedCards.Remove(deselectedCard);
    }

    public void PlayFromStackToStack(CardLogic cardPlayed, CardStackLogic fromStack, CardStackLogic toStack) {
        fromStack.Remove(cardPlayed);
        toStack.PlayToStack(cardPlayed);
        playFromStackToStackEvent?.Invoke(cardPlayed, fromStack, toStack);
    }

    public void PlayFromHandToStack(CardLogic cardPlayed, CardStackLogic playStack) {
        PlayFromStackToStack(cardPlayed, playerHand, playStack);
    }

    public void DrawCard() {
        PlayFromStackToStack(drawStack.TopCard(), drawStack, playerHand);
    }
    #endregion
}
