// ------------------------------------------
//  ...    ::: :::  .   .,:::::::::.    :::.
//  ;;     ;;; ;;; .;;,.;;;;''''`;;;;,  `;;;
// [['     [[[ [[[[[/'   [[cccc   [[[[[. '[[
// $$      $$$_$$$$,     $$""""   $$$ "Y$c$$
// 88    .d888"888"88o,  888oo,__ 888    Y88
//  "YmmMMMM"" MMM "MMP" """"YUMMMMMM     YM
//
// <copyright company="Uken" file="AssetDictionary.cs">
//   Uken 2016
// </copyright>
// ------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public abstract class AssetDictionary : ScriptableObject {
  public abstract bool ContainsKey(string key);
}

[SuppressMessage(
  "Microsoft.StyleCop.CSharp.MaintainabilityRules",
  "SA1402:FileMayOnlyContainASingleClass",
  Justification = "Can't have two files in this folder named AssetDictionary.")]
public abstract class AssetDictionary<T> : AssetDictionary {
  private Dictionary<string, T> dictionary;

  public int Count {
    get { return this.Dictionary.Count; }
  }

  protected abstract T DefaultValue { get; }

  private Dictionary<string, T> Dictionary {
    get {
      if (this.dictionary == null) {
        this.UpdateDictionary();
      }

      return this.dictionary;
    }
  }

  public T this[string key] {
    get {
      T t;
      return this.Dictionary.TryGetValue(key, out t) ? t : this.DefaultValue;
    }
  }

  public override bool ContainsKey(string key) {
    return this.Dictionary.ContainsKey(key);
  }

  public void UpdateDictionary() {
    this.dictionary = this.PopulateDictionary();
  }

  public IEnumerable<string> AllKeys() {
    return this.Dictionary.Keys;
  }

  protected abstract Dictionary<string, T> PopulateDictionary();

  protected abstract bool ShouldRepopulate();

  protected void OnValidate() {
    if (this.ShouldRepopulate()) {
      this.UpdateDictionary();
    }
  }
}