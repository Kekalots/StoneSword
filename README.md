# 石中剑 Stone Sword

A 2D action-platformer demo built with **Tuanjie Engine** (Unity 2022.3 LTS, China edition). A small adventure loop with combat, inventory, shops, portals, save & death systems across four scenes.

> 一款基于团结引擎（Unity 中国版）开发的 2D 横版动作游戏 Demo。包含完整的「主菜单 → 关卡1 → 关卡2 → 通关」流程，可玩时长约数分钟。

---

## 🎮 Gameplay Flow

```
MainMenu → Level_1 → Level_2 → End
```

- Fight slimes and ghosts with melee sword attacks
- Open chests, collect coins, buy weapons & potions from NPC shops
- Cross portals between levels (auto-save), manually save at save points
- Die → death panel appears → back to menu → load game resumes from last save

## 🕹️ Controls

| Key | Action |
|-----|--------|
| A / D | Move |
| J | Attack |
| F | Interact (NPC / chest / portal) |
| I | Open inventory |
| Q | Use selected item |
| X | Destroy selected item |
| E | Save game |
| ESC | Pause menu |

## ✨ Features

- **Save system** — two-layer storage (`PlayerPrefs` persistence + in-memory `pendingData` across scene loads); saves player HP/coins/inventory/weapon, chest & coin states per scene
- **Death system** — full-screen death UI with pause & safe reset
- **Shop & dialogue** — NPC shop with 2 weapons + healing potions, conversation panels
- **Inventory** — grid UI with tooltips, use / destroy items
- **Pause manager** — reference-counted pause (prevents nested UI conflicts)
- **Procedural audio** — all SFX synthesized at runtime (attack, coin, hurt, jump) via `AudioClip` generation, no audio assets needed
- **Prefab-based level reuse** — Player & UI canvases shared across levels via auto-find fallbacks

## 🛠️ Tech Notes

- Engine: Tuanjie 1.9.3 / Unity 2022.3.62t11
- Language: C# (comments stripped for submission; UI & code messages in English)
- Scenes use the `.scene` extension (Tuanjie format)

## 🚀 Run

1. Open the project in Tuanjie Engine (2022.3.62t11+)
2. Open scene `Assets/Scenes/MainMenu.scene`
3. Press Play, or build an executable via **File → Build Settings**

## 📁 Structure

```
Assets/
├── Player/      Player & UI prefabs (PlayerBody, PlayerCanvas, DeathCanvas)
├── Enemy/       Slime & Ghost: detect / move / attack / health
├── Chest/       Openable chests (normal / golden / cursed)
├── Coin/        Ground coins & managers
├── Shop/        NPC shop, shop UI & slots
├── Sword/       Weapon ScriptableObjects & ground weapons
├── Potion/      Potion items
├── Buff/        Temporary buffs (poison, slow-down)
├── Trap/        Spike & swamp hazards
├── Scenes/      MainMenu, Level_1, Level_2, End
├── Pause/       Reference-counted pause manager
├── Sound/       Procedural sound manager
└── Others/      Save/load, data classes, death & end controllers
```

---
*Student demo project — made while self-learning Unity/C#.*
