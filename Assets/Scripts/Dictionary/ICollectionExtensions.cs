// ------------------------------------------
//  ...    ::: :::  .   .,:::::::::.    :::.
//  ;;     ;;; ;;; .;;,.;;;;''''`;;;;,  `;;;
// [['     [[[ [[[[[/'   [[cccc   [[[[[. '[[
// $$      $$$_$$$$,     $$""""   $$$ "Y$c$$
// 88    .d888"888"88o,  888oo,__ 888    Y88
//  "YmmMMMM"" MMM "MMP" """"YUMMMMMM     YM
//
// <copyright company="Uken" file="ICollectionExtensions.cs">
//   Uken 2016
// </copyright>
// ------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

public static class ICollectionExtensions {
  public static List<T> AsList<T>(this ICollection<T> collection) {
    return new List<T>(collection);
  }

  public static bool IsEmpty<T>(this ICollection<T> list) {
    return list == null || list.Count == 0;
  }

  public static int AddToListWhere<T>(this ICollection<T> source, ICollection<T> dest, Predicate<T> predicate, bool preClearDestination = false) {
    int added = 0;

    if (preClearDestination) {
      dest.Clear();
    }

    foreach (var t in source) {
      if (predicate(t)) {
        dest.Add(t);
        added++;
      }
    }

    return added;
  }

  public static List<TResult> Map<T, TResult>(this ICollection<T> list, Func<T, TResult> fn) {
    var toReturn = new List<TResult>(list.Count);

    foreach (var element in list) {
      toReturn.Add(fn(element));
    }

    return toReturn;
  }

  public static T ReduceTo<T, S>(this ICollection<S> list, Func<T, S, T> fn, T initialValue = default(T)) {
    T val = initialValue;
    foreach (var t in list) {
      val = fn(val, t);
    }

    return val;
  }

  public static T Reduce<T>(this ICollection<T> list, Func<T, T, T> fn, T initialValue = default(T)) {
    T val = initialValue;
    foreach (var t in list) {
      val = fn(val, t);
    }

    return val;
  }

  public static List<T> Filter<T>(this ICollection<T> list, Func<T, bool> fn) {
    var newList = new List<T>();

    foreach (var element in list) {
      if (fn(element)) {
        newList.Add(element);
      }
    }

    return newList;
  }

  public static List<T> Flatten<T>(this ICollection<List<T>> list) {
    return list.Reduce(
      (newLst, subLst) => {
        subLst.ForEach(item => newLst.Add(item));
        return newLst;
      },
      new List<T>());
  }

  public static List<T> FlattenRemovingDupes<T>(this ICollection<List<T>> list) {
    return list.Reduce(
       (newLst, subLst) => {
         foreach (var item in subLst) {
           if (!newLst.Contains(item)) {
             newLst.Add(item);
           }
         }

         return newLst;
       },
       new List<T>());
  }

  public static List<T> DistinctPreservingOrder<T>(this ICollection<T> list) {
    var known = new HashSet<T>();
    var newList = new List<T>();

    foreach (var item in list) {
      if (known.Contains(item)) {
        continue;
      }

      newList.Add(item);
      known.Add(item);
    }

    return newList;
  }

  public static List<T> DistinctBy<T, T2>(this ICollection<T> list, Func<T, T2> getProperty) {
    var known = new HashSet<T2>();
    var newList = new List<T>();

    foreach (var item in list) {
      var property = getProperty(item);

      if (known.Contains(property)) {
        continue;
      }

      newList.Add(item);
      known.Add(property);
    }

    return newList;
  }

  public static bool IsSubsetOf<T>(this ICollection<T> ourList, ICollection<T> otherList) {
    var ourSet = new HashSet<T>(ourList);
    var otherSet = new HashSet<T>(otherList);

    return ourSet.IsSubsetOf(otherSet);
  }

  public static bool HasSameElementsAs<T>(this ICollection<T> ourList, ICollection<T> otherList) {
    if (ourList == null ^ otherList == null) {
      return false;
    }

    if (ourList == null && otherList == null) {
      return true;
    }

    var ourListElementsByCount = ElementsInListByCount<T>(ourList);
    var otherListElementsByCount = ElementsInListByCount<T>(otherList);

    if (ourListElementsByCount.Keys.Count != otherListElementsByCount.Keys.Count) {
      return false;
    }

    foreach (var kvp in ourListElementsByCount) {
      if (!otherListElementsByCount.ContainsKey(kvp.Key)) {
        return false;
      }

      if (otherListElementsByCount[kvp.Key] != kvp.Value) {
        return false;
      }
    }

    return true;
  }

  public static Dictionary<T, int> CountOccurrences<T>(this ICollection<T> list) {
    Dictionary<T, int> occurrences = new Dictionary<T, int>();

    foreach (var elem in list) {
      if (!occurrences.ContainsKey(elem)) {
        occurrences.Add(elem, 0);
      }

      occurrences[elem]++;
    }

    return occurrences;
  }

  public static bool All<T>(this ICollection<T> list, Predicate<T> pred) {
    foreach (var item in list) {
      if (!pred(item)) {
        return false;
      }
    }

    return true;
  }

  public static S Max<T, S>(this ICollection<T> list, Func<T, S> maxFunc, S min = default(S)) where S : IComparable<S> {
    S max = min;
    foreach (T t in list) {
      S val = maxFunc(t);
      max = val.CompareTo(max) > 0 ? val : max;
    }

    return max;
  }

  public static T Max<T>(this ICollection<T> list) where T : IComparable<T> {
    if (list.IsEmpty()) {
      return default(T);
    }

    bool firstValue = false;
    T max = default(T);
    foreach (var val in list) {
      if (!firstValue) {
        firstValue = true;
        max = val;
      } else if (max.CompareTo(val) < 0) {
        max = val;
      }
    }

    return max;
  }

  public static T Min<T>(this ICollection<T> list) where T : IComparable<T> {
    if (list.IsEmpty()) {
      return default(T);
    }

    bool firstValue = false;
    T min = default(T);
    foreach (var val in list) {
      if (!firstValue) {
        firstValue = true;
        min = val;
      } else if (min.CompareTo(val) > 0) {
        min = val;
      }
    }

    return min;
  }

  public static S Min<T, S>(this ICollection<T> list, Func<T, S> getValue, S initialValue = default(S)) where S : IComparable<S> {
    S current = initialValue;
    foreach (T t in list) {
      S tvalue = getValue(t);
      current = tvalue.CompareTo(current) < 0 ? tvalue : current;
    }

    return current;
  }

  public static int CountWhere<T>(this ICollection<T> list, Predicate<T> match) {
    int count = 0;

    foreach (T t in list) {
      if (match(t)) {
        count++;
      }
    }

    return count;
  }

  public static List<U> Cast<T, U>(this ICollection<T> tList) where T : U {
    var uList = new List<U>();
    foreach (var t in tList) {
      uList.Add(t);
    }

    return uList;
  }

  public static List<T> Concat<T>(this List<T> list, params List<T>[] listsToAppend) {
    if (list == null) {
      list = new List<T>();
    }

    foreach (var l in listsToAppend) {
      if (l != null && l.Count > 0) {
        list.AddRange(l);
      }
    }

    return list;
  }

  public static void PopulateWitAscendingIntegers(this ICollection<int> list, int length) {
    for (int i = 0; i < length; i++) {
      list.Add(i);
    }
  }

  public static int Sum<T>(this ICollection<T> list, Func<T, int> sumFunc) {
    int sum = 0;
    foreach (var t in list) {
      sum += sumFunc(t);
    }

    return sum;
  }

  public static T First<T>(this ICollection<T> list) {
    if (list.IsEmpty()) {
      return default(T);
    }

    IEnumerator en = list.GetEnumerator();
    en.MoveNext();
    return (T)en.Current;
  }

  public static void ForEach<T>(this ICollection<T> list, Action<T> action) {
    if (list.IsEmpty()) {
      return;
    }

    foreach (var element in list) {
      action(element);
    }
  }

  public static void AddIfNotNull<T>(this ICollection<T> list, T t) {
    if (list != null && t != null) {
      list.Add(t);
    }
  }

  public static T Find<T>(this ICollection<T> list, Func<T, bool> pred) {
    foreach (var t in list) {
      if (pred(t)) {
        return t;
      }
    }

    return default(T);
  }

  public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue, TList>(
    this ICollection<TList> list,
    Func<TList, TKey> keyFunc,
    Func<TList, TValue> valueFunc) {
    var dict = new Dictionary<TKey, TValue>();

    foreach (var element in list) {
      dict[keyFunc(element)] = valueFunc(element);
    }

    return dict;
  }

  public static T PickWeighted<T>(this ICollection<T> list, Func<T, float> getWeight) {
    if (list.IsEmpty()) {
      return default(T);
    }

    float sum = 0;

    foreach (var item in list) {
      sum += getWeight(item);
    }

    float target = UnityEngine.Random.Range(0, sum);
    sum = 0;

    foreach (var item in list) {
      sum += getWeight(item);
      if (sum > target) {
        return item;
      }
    }

    return list.First();
  }

  private static Dictionary<T, int> ElementsInListByCount<T>(ICollection<T> list) {
    var elementsByCount = new Dictionary<T, int>();

    foreach (var element in list) {
      if (elementsByCount.ContainsKey(element)) {
        elementsByCount[element]++;
        continue;
      }

      elementsByCount[element] = 1;
    }

    return elementsByCount;
  }
}
