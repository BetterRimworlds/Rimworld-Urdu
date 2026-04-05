// ==== ./Source/Main.cs ====
using HarmonyLib;
using Verse;

namespace BetterRimworlds
{
    public class RimworldArabicMod : Mod
    {
        public const string Language = "Arabic";
        public static FontBootstrap Bootstrap { get; private set; }

        public RimworldArabicMod(ModContentPack content) : base(content)
        {
            Bootstrap = new FontBootstrap();

            var harmony = new Harmony(
                $"HopeSeekr.BetterRimworlds.Rimworld{Language}"
            );
            harmony.PatchAll();
            Log.Message($"[BetterRimworlds:{Language}] Harmony patches applied.");
        }
    }
}
