using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjector : MonoBehaviour {
    #region Fields
        [Header("Inspector Injected References")]
        [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private GameView gameView;

        [Header("Inspector Injected Parameters")]
        [SerializeField]
        private int numCardStacks;
        [SerializeField]
        private int numCardsPerStack;
        [SerializeField]
        private int initialHandSize;
        [SerializeField]
        private int initialDrawStackSize;

        [Header("Prefab References")]
        [SerializeField]
        private GameObject CardPrefab;
        [SerializeField]
        private GameObject PlayCardStackPrefab;
        [SerializeField]
        private GameObject PlayerHandPrefab;
        [SerializeField]
        private GameObject DrawStackPrefab;

        [Header("Debugging Viewables")]
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private InputLogic inputLogic;
        [SerializeField]
        private GameLogic gameLogic;
    #endregion

    #region Initialization Methods
    private void Awake() {
        CardStackLogic playerHand = InitializePlayerHand(initialHandSize);
        CardStackLogic drawStack = InitializeDrawStack(initialDrawStackSize);
        List<CardStackLogic> playStacks = InitializePlayStacks();
        playerController = InitializePlayerController(playerHand, drawStack, playStacks);
        inputLogic = InitializeInputLogic(inputManager, playerController);
        gameLogic = InitializeGameLogic(playerController);

        gameView.Initialize(playStacks, playerHand, drawStack);
    }

    private CardStackLogic InitializePlayerHand(int initialHandSize) {
        return InitializeStack(Instantiate(PlayerHandPrefab).GetComponent<CardStack>(), initialHandSize, "PlayerHand", initialHandSize);
    }

    private CardStackLogic InitializeDrawStack(int initialDrawStackSize) {
        return InitializeStack(Instantiate(DrawStackPrefab).GetComponent<CardStack>(), initialDrawStackSize, "DrawStack");
    }

    private List<CardStackLogic> InitializePlayStacks() {
        List<CardStackLogic> playStacks = new List<CardStackLogic>();

        for (int i = 0; i < numCardStacks; i++) {
            CardStackLogic newPlayStack = InitializeStack(Instantiate(PlayCardStackPrefab).GetComponent<CardStack>(), numCardsPerStack, "Stack " + i);
            playStacks.Add(newPlayStack);
        }

        return playStacks;
    }

    private CardStackLogic InitializeStack(CardStack cardStack, int numCards = 0, string stackName = "Stack", int stackLimit = int.MaxValue) {
        List<CardLogic> cards = new List<CardLogic>();

        for (int i = 0; i < numCards; i++) {
            GameObject newCardObject = Instantiate(CardPrefab, cardStack.transform);
            CardLogic newCard = new CardLogic(newCardObject);
            cards.Add(newCard);
            newCardObject.GetComponent<Card>().InitializeValues(newCard);
        }

        CardStackLogic newPlayStack = new CardStackLogic(cardStack.gameObject, cards, stackLimit);
        cardStack.name = stackName;
        cardStack.InitializeValues(newPlayStack);

        return newPlayStack;
    }

    private PlayerController InitializePlayerController(CardStackLogic playerHand, CardStackLogic drawStack, List<CardStackLogic> playStacks) {
        return new PlayerController(playerHand, drawStack, playStacks);
    }

    private InputLogic InitializeInputLogic(InputManager inputManager, PlayerController playerController) {
        return new InputLogic(inputManager, playerController);
    }

    private GameLogic InitializeGameLogic(PlayerController playerController) {
        return new GameLogic(playerController);
    }
    #endregion
}
