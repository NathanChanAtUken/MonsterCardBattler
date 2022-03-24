using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjector : MonoBehaviour {
    #region Fields
        [Header("Inspector Injected References")]
        [SerializeField]
        private InputManager inputManager;

        [Header("Inspector Injected Parameters")]
        [SerializeField]
        private int numCardStacks, numCardsPerStack, initialHandSize;

        [Header("Prefab References")]
        [SerializeField]
        private GameObject CardPrefab, CardStackPrefab;

        [Header("Debugging Viewables")]
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private InputLogic inputLogic;
    #endregion

    #region Initialization Methods
    private void Awake() {
        CardStackLogic playerHand = InitializePlayerHand();
        List<CardStackLogic> playStacks = InitializePlayStacks();
        playerController = InitializePlayerController(playerHand, playStacks);
        inputLogic = InitializeInputLogic(inputManager, playerController);
    }

    private CardStackLogic InitializePlayerHand() {
        CardStackLogic playerHand = new CardStackLogic(null, initialHandSize);
        GameObject playerHandObject = Instantiate(CardStackPrefab);
        
        playerHandObject.name = "Player Hand";
        playerHandObject.GetComponent<CardStack>().InitializeValues(playerHand);

        return playerHand;
    }

    private List<CardStackLogic> InitializePlayStacks() {
        List<CardStackLogic> playStacks = new List<CardStackLogic>();

        for (int i = 0; i < numCardStacks; i++) {
            CardStackLogic newPlayStack = InitializePlayStack(numCardsPerStack);
            playStacks.Add(newPlayStack);
        }

        return playStacks;
    }

    private CardStackLogic InitializePlayStack(int numCards) {
        List<CardLogic> cards = new List<CardLogic>();

        CardStackLogic newPlayStack = new CardStackLogic(cards);
        GameObject playStackObject = Instantiate(CardStackPrefab);

        for (int i = 0; i < numCards; i++) {
            CardLogic newCard = new CardLogic();
            cards.Add(newCard);

            GameObject newCardObject = Instantiate(CardPrefab, playStackObject.transform);
            newCardObject.GetComponent<Card>().InitializeValues(newCard);
        }

        playStackObject.GetComponent<CardStack>().InitializeValues(newPlayStack);

        return newPlayStack;
    }

    private PlayerController InitializePlayerController(CardStackLogic playerHand, List<CardStackLogic> playStacks) {
        return new PlayerController(playerHand, playStacks);
    }

    private InputLogic InitializeInputLogic(InputManager inputManager, PlayerController playerController) {
        return new InputLogic(inputManager, playerController);
    }
    #endregion
}
