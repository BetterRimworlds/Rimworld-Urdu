// ==== ./Source/DebugPatches.cs ====
using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterRimworlds
{
    [HarmonyPatch(typeof(MainMenuDrawer), "DoMainMenuControls")]
    public static class Patch_MainMenuDrawer_Debug
    {
        private static bool _logged;

        public static void Prefix()
        {
            if (_logged)
                return;

            if (LanguageDatabase.activeLanguage?.folderName != RimworldArabicMod.Language)
                return;

            _logged = true;

            if (RimworldArabicMod.Bootstrap.LoadedFont == null)
            {
                Log.Warning(
                    $"[BetterRimworlds:{RimworldArabicMod.Language}] " +
                    "DebugPatch: LoadedFont is null."
                );
                return;
            }

            Log.Message(
                $"[BetterRimworlds:{RimworldArabicMod.Language}] DebugPatch: " +
                $"ActiveLanguage={LanguageDatabase.activeLanguage?.folderName}, " +
                $"Font={RimworldArabicMod.Bootstrap.LoadedFont.name}, " +
                $"dynamic={RimworldArabicMod.Bootstrap.LoadedFont.dynamic}"
            );
        }
    }
}
