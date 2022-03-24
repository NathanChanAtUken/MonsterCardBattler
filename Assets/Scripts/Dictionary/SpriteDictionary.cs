// ------------------------------------------
//  ...    ::: :::  .   .,:::::::::.    :::.
//  ;;     ;;; ;;; .;;,.;;;;''''`;;;;,  `;;;
// [['     [[[ [[[[[/'   [[cccc   [[[[[. '[[
// $$      $$$_$$$$,     $$""""   $$$ "Y$c$$
// 88    .d888"888"88o,  888oo,__ 888    Y88
//  "YmmMMMM"" MMM "MMP" """"YUMMMMMM     YM
//
// <copyright company="Uken" file="SpriteDictionary.cs">
//   Uken 2016
// </copyright>
// ------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpriteDictionaryElement {
  public string spriteKey;
  public Sprite sprite;
}

[CreateAssetMenu(fileName = "Sprites", menuName = "UI/SpriteDictionary")]
public class SpriteDictionary : AssetDictionary<Sprite> {
  [SerializeField] private SpriteDictionaryElement[] spriteElements;

  public int Length {
    get {
      return this.spriteElements.Length;
    }
  }

  public Sprite First {
    get {
      return this.spriteElements.Length == 0 ? null : this.spriteElements[0].sprite;
    }
  }

  protected override Sprite DefaultValue {
    get { return null; }
  }

  public Sprite this[int i] {
    get {
      return this.spriteElements[i].sprite;
    }
  }

  public Sprite Get(string key) {
    return this[key];
  }

  protected override Dictionary<string, Sprite> PopulateDictionary() {
    return this.spriteElements
      .ToDictionary(
        element => string.IsNullOrEmpty(element.spriteKey) ? element.sprite.name : element.spriteKey,
        element => element.sprite);
  }

  protected override bool ShouldRepopulate() {
    return this.spriteElements.Length != this.Count;
  }
}