using System;
using FMODUnity;
using HarmonyLib;
using KSL.API;

namespace AlwaysAudioKSL
{
	[KSLMeta("AlwaysAudioKSL", "1.0.1", "Pyro")]
	public class AlwaysAudioKSL : BaseMod
	{
		private Harmony _harmony;

		void Awake()
		{
			try
			{
				_harmony = new Harmony("AlwaysAudioKSL.patch");
				var target = AccessTools.Method(typeof(RuntimeManager), "MuteAllEvents", new[] { typeof(bool) });
				_harmony.Patch(target, new HarmonyMethod(typeof(AlwaysAudioKSL), nameof(SkipMute)));
			}
			catch (Exception)
			{
			}
		}

		void OnDestroy()
		{
			_harmony?.UnpatchSelf();
		}

		private static bool SkipMute()
		{
			return false;
		}
	}
}
