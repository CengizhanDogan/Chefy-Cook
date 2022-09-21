using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
	public static GemCollectEvent OnGemCollected = new GemCollectEvent();
	public static GemSpendEvent OnGemSpend = new GemSpendEvent();
	public static GemChangeEvent OnGemChange = new GemChangeEvent();
 	public static CombinationEvent OnCombination = new CombinationEvent();
	public static CollectionEvent OnCollection = new CollectionEvent();
	public static DeliveryEvent OnDelivery = new DeliveryEvent();
	#region Editor
	public static UnityEvent OnLevelDataChange = new UnityEvent();
	#endregion
}
public class GemCollectEvent : UnityEvent<Vector3, Action> { }
public class GemSpendEvent : UnityEvent<int> { }
public class GemChangeEvent : UnityEvent { }
public class CombinationEvent : UnityEvent<int, Transform> { }
public class CollectionEvent : UnityEvent<int> { }
public class DeliveryEvent : UnityEvent<int> { }
