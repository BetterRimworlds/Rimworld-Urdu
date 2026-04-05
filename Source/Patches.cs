// ==== ./Source/Patches.cs ====
using System;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace BetterRimworlds
{
    [HarmonyPatch(typeof(Text), nameof(Text.StartOfOnGUI))]
    public static class Patch_Text_StartOfOnGUI
    {
        private static string _initializedForLanguage;

        public static void Postfix()
        {
            string activeLang = LanguageDatabase.activeLanguage?.folderName;

            if (activeLang != RimworldArabicMod.Language)
                return;

            if (_initializedForLanguage == activeLang)
                return;

            _initializedForLanguage = activeLang;

            RimworldArabicMod.Bootstrap.Init(activeLang);

            if (!RimworldArabicMod.Bootstrap.ShouldUseCustomFont())
                return;

            var font = RimworldArabicMod.Bootstrap.LoadedFont;

            foreach (GameFont value in Enum.GetValues(typeof(GameFont)))
            {
                int i = (int)value;
                Text.fontStyles[i].font = font;
                Text.textFieldStyles[i].font = font;
                Text.textAreaStyles[i].font = font;
                Text.textAreaReadOnlyStyles[i].font = font;
                Log.Message(
                    $"[BetterRimworlds:{activeLang}] " +
                    $"Applied font to GameFont.{value} (size {Text.fontStyles[i].fontSize})"
                );
            }
        }
    }
}
