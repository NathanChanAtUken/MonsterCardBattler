using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI winLoseText;
  [SerializeField] private Button playAgainButton;

  public void ShowScreen(bool won) {
    this.winLoseText.text = won ? "You win!" : "You lose!";
    this.playAgainButton.onClick.AddListener(this.ReloadScene);
    this.gameObject.SetActive(true);
  }

  public void ReloadScene() {
    Scene scene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(scene.name);
  }
}
