using System;
using FMODUnity;
using HarmonyLib;
using KSL.API;
using UnityEngine;

namespace AlwaysAudioKSL
{
	[KSLMeta("AlwaysAudioKSL", "1.0.0", "Pyro")]
	public class AlwaysAudioKSL : BaseMod
	{
		private const string HarmonyId = "AlwaysAudioKSL.patch";
		private Harmony _harmony;

		void Awake()
		{
			try
			{
				_harmony = new Harmony(HarmonyId);
				var target = AccessTools.Method(typeof(RuntimeManager), "OnApplicationPause", new[] { typeof(bool) });
				_harmony.Patch(target, new HarmonyMethod(typeof(AlwaysAudioKSL), nameof(SkipPause)));
			}
			catch (Exception ex)
			{
				Debug.LogError("[AlwaysAudioKSL] failed to patch RuntimeManager.OnApplicationPause: " + ex);
			}
		}

		void OnDestroy()
		{
			_harmony?.UnpatchSelf();
		}

		private static bool SkipPause()
		{
			return false;
		}
	}
}
