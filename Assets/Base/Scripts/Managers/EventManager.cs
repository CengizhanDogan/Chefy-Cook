﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
	public static GemCollectEvent OnGemCollected = new GemCollectEvent();
	public static CombinationEvent OnCombination = new CombinationEvent();
	#region Editor
	public static UnityEvent OnLevelDataChange = new UnityEvent();
	#endregion
}
public class GemCollectEvent : UnityEvent<Vector3, Action> { }
public class CombinationEvent : UnityEvent<Combination, Transform> { }