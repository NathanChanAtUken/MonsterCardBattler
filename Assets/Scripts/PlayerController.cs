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

        public delegate void OnPlayFromHandToStack(CardLogic cardPlayed, CardStackLogic toStack);
        public event OnPlayFromHandToStack playFromHandToStackEvent;

        [SerializeField] private Player player;
        [SerializeField] private PlayerView playerView;
    #endregion

    #region Constructors
    public PlayerController(CardStackLogic playerHand, CardStackLogic drawStack, List<CardStackLogic> playStacks, Player player, PlayerView playerView) {
        this.playerCombatEntity = new BasicCombatEntity(10);
        this.playerHand = playerHand;
        this.drawStack = drawStack;
        this.playStacks = playStacks;
        this.player = player;
        this.playerView = playerView;

        this.RefreshPlayerView();
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
        selectedCard.CardObject.GetComponent<Card>().CardView.ShowHighlight();
    }

    public void DeselectCard(CardLogic deselectedCard) {
        if (!selectedCards.Contains(deselectedCard)) {
            return;
        }

        selectedCards.Remove(deselectedCard);
        deselectedCard.CardObject.GetComponent<Card>().CardView.HideHighlight();
    }

    public void PlayFromStackToStack(CardLogic cardPlayed, CardStackLogic fromStack, CardStackLogic toStack) {
        fromStack.Remove(cardPlayed);
        toStack.PlayToStack(cardPlayed);

        playFromStackToStackEvent?.Invoke(cardPlayed, fromStack, toStack);
    }

    public void PlayFromHandToStack(CardLogic cardPlayed, CardStackLogic playStack) {
        PlayFromStackToStack(cardPlayed, playerHand, playStack);
        DrawCard();
        DeselectCard(cardPlayed);

        playFromHandToStackEvent?.Invoke(cardPlayed, playStack);
  }

    public void DrawCard() {
        PlayFromStackToStack(drawStack.TopCard(), drawStack, playerHand);
    }
    #endregion

    public void RefreshPlayerView() {
        this.playerView.Initialize(this.player, this.playerCombatEntity.Health);
    }
}
