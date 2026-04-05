using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Verse;

namespace BetterRimworlds
{
    public class LanguageWorker_Urdu : LanguageWorker
    {
        // Urdu grammar notes:
        // - No definite or indefinite articles like English "the"/"a"
        // - Indefiniteness can be marked with "ایک" (one/a) but is often omitted
        // - Definiteness is contextual (no prefix like Arabic "ال")
        // - Urdu uses postpositions, not prepositions
        // - Script: Nastaliq (Urdu variant of Perso-Arabic script), RTL
        // - Number agreement is simpler than Arabic: singular vs plural (no dual)

        public override string WithIndefiniteArticle(string str, Gender gender, bool plural = false, bool name = false)
        {
            if (str.NullOrEmpty())
                return "";

            if (name)
                return str;

            // Urdu has no mandatory indefinite article.
            // "ایک" (one) can serve as an indefinite article but is usually
            // omitted in game UI text. Return as-is.
            return str;
        }

        public override string WithDefiniteArticle(string str, Gender gender, bool plural = false, bool name = false)
        {
            if (str.NullOrEmpty())
                return "";

            if (name)
                return str;

            // Urdu has no definite article. Definiteness is understood from context.
            return str;
        }

        public override string PostProcessed(string str)
        {
            return PostProcessedInt(base.PostProcessed(str));
        }

        private static string FixUrduForRimWorld(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var tokens = new Dictionary<string, string>();
            int tokenIndex = 0;

            // 1. Protect tokens (same pattern as Arabic - {0}, [tag], <tag>)
            string protectedText = Regex.Replace(input, @"(\{.*?\}|\[.*?\]|<.*?>)", match =>
            {
                string key = $"@{tokenIndex++}@";
                tokens[key] = match.Value;
                return key;
            });

            // 2. Shape the characters (connect the Urdu/Nastaliq letters)
            // Urdu uses the same Unicode Arabic shaping rules, so ArabicShaper works.
            string shaped = ArabicShaper.Shape(protectedText);

            // 3. REVERSE the string (crucial for RimWorld's LTR engine)
            char[] charArray = shaped.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            // 4. Restore tokens and fix their internal order
            // Since the whole string was reversed, the token keys are also
            // reversed (e.g. "@0@" stays "@0@" but "@12@" becomes "@21@").
            // We must match the reversed key and replace with the original token.
            foreach (var kvp in tokens)
            {
                string reversedKey = new string(kvp.Key.ToCharArray().Reverse().ToArray());
                reversed = reversed.Replace(reversedKey, kvp.Value);
            }

            return reversed;
        }

        private string PostProcessedInt(string str)
        {
            if (str.NullOrEmpty())
                return str;

            // Normalize double spaces that may come from templates
            str = str.Replace("  ", " ");

            return FixUrduForRimWorld(str);
        }

        // ---------- Urdu number cases ----------
        //
        // Urdu plural rules (CLDR):
        // - one:   n = 1 (and integer)
        // - other: everything else
        //
        // However, for richer translation support we accept up to 4 forms:
        // 0: zero   (0)
        // 1: one    (1)
        // 2: few    (2-10, optional refinement)
        // 3: other  (11+ / fallback)
        //
        // If only 3 args are provided (RimWorld default), degrade gracefully.
        public override string ResolveNumCase(float number, List<string> args)
        {
            if (args == null || args.Count == 0)
                return null;

            List<string> forms = new List<string>(args.Count);
            for (int i = 0; i < args.Count; i++)
                forms.Add(args[i]?.Trim('\'') ?? "");

            // Fractional numbers
            if (number - (float)Math.Floor(number) > float.Epsilon)
            {
                string fracForm = PickFractionForm(forms);
                return $"{number} {fracForm}";
            }

            int n = (int)number;
            string form = GetUrduFormForNumber(n, forms);
            return $"{n} {form}";
        }

        protected override string GetFormForNumber(int num, string formOne, string formSeveral, string formMany)
        {
            // Urdu: singular (1) vs plural (everything else)
            // Map RimWorld's 3-form system:
            // 1 -> formOne
            // 2-10 -> formSeveral
            // 11+ -> formMany
            if (num == 1)
                return formOne;

            if (num >= 2 && num <= 10)
                return formSeveral;

            return formMany;
        }

        private static string GetUrduFormForNumber(int n, List<string> forms)
        {
            if (forms.Count == 1)
                return forms[0];

            if (forms.Count == 2)
                return (n == 1) ? forms[0] : forms[1];

            if (forms.Count == 3)
            {
                // forms[0]=one, forms[1]=several, forms[2]=many
                if (n == 1) return forms[0];
                if (n >= 2 && n <= 10) return forms[1];
                return forms[2];
            }

            // 4+ forms: zero / one / few / other
            if (n == 0)
                return forms[0];

            if (n == 1)
                return forms.Count > 1 ? forms[1] : forms[0];

            if (n >= 2 && n <= 10)
                return forms.Count > 2 ? forms[2] : forms[Math.Min(1, forms.Count - 1)];

            // other: 11+
            return forms.Count > 3 ? forms[3] : forms[forms.Count - 1];
        }

        private static string PickFractionForm(List<string> forms)
        {
            // Fractional quantities in Urdu use the "other"/plural form
            return forms[forms.Count - 1];
        }

        public override string OrdinalNumber(int number, Gender gender = Gender.None)
        {
            // Urdu ordinal numbers
            // Common ordinals have specific words; beyond that, use the
            // suffix "واں" (masculine) / "ویں" (feminine/general)
            if (gender == Gender.Female)
            {
                return number switch
                {
                    1 => "پہلی",
                    2 => "دوسری",
                    3 => "تیسری",
                    4 => "چوتھی",
                    5 => "پانچویں",
                    6 => "چھٹی",
                    7 => "ساتویں",
                    8 => "آٹھویں",
                    9 => "نویں",
                    10 => "دسویں",
                    _ => number + "ویں"
                };
            }

            return number switch
            {
                1 => "پہلا",
                2 => "دوسرا",
                3 => "تیسرا",
                4 => "چوتھا",
                5 => "پانچواں",
                6 => "چھٹا",
                7 => "ساتواں",
                8 => "آٹھواں",
                9 => "نواں",
                10 => "دسواں",
                _ => number + "واں"
            };
        }
    }
}

