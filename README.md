# RimWorld Urdu (Urdu Translation Mod)
**by [Better Rimworlds](https://github.com/BetterRimworlds)** • powered by Autonomo AI

Bring a **full Urdu localization** to RimWorld — built to be **playable, UI-safe, and consistent** across the game’s terminology.

> ✅ Designed for real gameplay: stable placeholders, consistent RimWorld vernacular, and UI-friendly strings.

---

## What you get

- **Urdu translation** for RimWorld UI + game text
- **Consistent RimWorld terminology** (custom glossary / vernacular)
- **Placeholder-safe strings** (no broken `{0}`, `[PAWN_nameDef]`, etc.)
- **UI-safe length constraints** (labels/titles kept readable where possible)

---

## Translation Costs

```
```


## Installation

### Option A: Steam Workshop (recommended)
1. Subscribe to the mod on Steam Workshop
2. Launch RimWorld
3. Go to **Mods** → enable **RimWorld Urdu**
4. Restart RimWorld when prompted

> If you don’t see it in your list, restart Steam and RimWorld.

*(Workshop link: add once published.)*

---

### Option B: Manual install (GitHub download)
1. Download this repository as a ZIP:
   - Click **Code** → **Download ZIP**
2. Extract it
3. Copy the folder into your RimWorld Mods directory:

**Windows**
```

C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\

```

**Linux**
```

~/.steam/steam/steamapps/common/RimWorld/Mods/

```

**macOS**
```

~/Library/Application Support/Steam/steamapps/common/RimWorld/RimWorldMac.app/Mods/

```

4. Make sure the folder structure looks like:
```

RimWorld/Mods/RimWorld-Urdu/
About/
Languages/
...

```

5. Launch RimWorld → **Mods** → enable **RimWorld Urdu** → restart.

---

## Enable Urdu in RimWorld

After the mod is enabled:

1. Go to **Options**
2. Find **Language**
3. Select **Urdu**
4. Restart RimWorld if asked

---

## Load order

Typically:
- **Core**
- DLCs (if any)
- Other mods
- **RimWorld Urdu**

If another mod includes its own translation files, it may override parts of the Urdu text depending on load order.

---

## Known behavior

- Some UI strings are deliberately kept short to avoid overflow.
- Some mod-added content may remain in English unless those mods ship Urdu translations or you add patches.
- If you use many mods, translation completeness depends on whether those mods provide keyed strings / translation keys.

---

## Troubleshooting

### “Urdu isn’t showing up in the language menu”
- Confirm the mod is **enabled**
- Confirm the folder path is correct:
  - `Mods/RimWorld-Urdu/Languages/Urdu/`
- Restart RimWorld after enabling the mod

### “Some text is still in English”
- That text likely comes from:
  - another mod (no Urdu translation available)
  - newly added RimWorld content that hasn’t been updated yet
- Please open an issue with:
  - a screenshot
  - the exact English text
  - your mod list + load order (if possible)

### “Text looks weird / missing characters”
- RimWorld font rendering is sensitive to:
  - font mods
  - UI scaling
- Try disabling font/UI mods to confirm compatibility.

---

## Bug reports & requests

Open a GitHub issue here:
- Include **screenshots**
- Include the **exact string** (English if possible)
- Include your **RimWorld version** and **mod list**

---

## Credits

Published by **[Better Rimworlds](https://github.com/BetterRimworlds)**
Built with the **Autonomo AI** localization pipeline (Automated QA Inspection & Copyediting).

---

## Disclaimer

RimWorld is the property of its respective owner(s).
This translation mod is an independent community project and is not affiliated with or endorsed by Ludeon Studios.
