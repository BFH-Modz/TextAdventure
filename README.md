# 🗺️ Text Adventure (C#) — Modular OOP Starter

A lightweight **C# console text adventure** built with clean **Object-Oriented Design** so you can easily add **new rooms**, **new items**, and swap the **map layout** (or randomise it later) without rewriting the game loop.

---

## ✨ Current Features

### 🧭 Movement + Exploration
- Move using **N / S / E / W** (also accepts **North/South/East/West**)
- **LOOK** reprints the current room description
- Room output shows:
  - Room name + description  
  - Items in the room  
  - Available exits

### 🎒 Items + Inventory
- **TAKE `<item>`** to pick up items from the room
- **DROP `<item>`** to drop items into the current room
- **INV** to list inventory contents

### 🧱 Clean, Extendable Structure (OOP)
- `Room` handles exits + items
- `Item` models reusable item objects
- `Map` builds the world layout (easy to swap or randomise later)
- `Program` only runs the game loop and interprets commands

---

## 🎮 Controls

| Command | Action |
|--------:|--------|
| `N` `S` `E` `W` | Move between rooms |
| `LOOK` | Reprint the current room |
| `INV` | Show inventory |
| `TAKE key` | Pick up an item |
| `DROP apple` | Drop an item |
| `HELP` | Show commands |
| `Q` | Quit |

---

## 🗺️ Current Map Layout

### Rooms
- **Bedroom**
- **Hall**
- **Kitchen**
- **Garden**

### ASCII Map
       [ Kitchen ] --E--> [ Garden ]
           |
           S
           |
       [ Bedroom ] --E--> [ Hall ]

### Items
- Bedroom: **Key**
- Kitchen: **Apple**

---

## 🧠 Why This Project Is Designed This Way

This project was refactored from a single-file approach into **separate classes** using:
- **Object-Oriented Design (OOD)**
- **Separation of Concerns**
- **Single Responsibility Principle (SRP)**

That means you can:
- add 20+ rooms without touching the game loop
- add new items anywhere
- replace the map builder with:
  - `BuildRandom(seed)`
  - `BuildFromJson("map.json")`
  - `BuildGrid(width, height)`

---
