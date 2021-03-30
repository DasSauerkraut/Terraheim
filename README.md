
# Terraheim - Armor Overhaul
[Nexus](https://www.nexusmods.com/valheim/mods/803)
[Thunderstore](https://valheim.thunderstore.io/package/DasSauerkraut/Terraheim/)
[GitHub](https://github.com/DasSauerkraut/Terraheim)
**Terraheim** is an overhaul for Valheim's armor system with the goal of making each armor set viable all the way to the end of the game. 
It does this by giving each piece of armor its own unique effect and each armor set its own set bonus while also allowing armor to be 'uptiered,' which increases all of the piece's stats to be usable in the next tier of gameplay.
## Classes
Each armor set has been divided into three classes:
#### Berserkr
Berserkr's have a focus on dodging, axe damage, two handed weapon damage and stamina management. 

#### Bowman
Bowmen focus on dealing damage via, well bows, as well as daggers and spears. They focus on ammo consumption, backstab bonus, and sneaking. They have the worst armor rating in game.

#### Shieldbrother
Shieldbrothers are adept at blocking and have increased health. They can effectively deal damage with all melee weapons, but they are slower than the other classes. They have the highest armor rating in game.

You can view the full values for each set across every tier [here!](https://docs.google.com/spreadsheets/d/1DlnnJOvorgCQ1k1e4lO5nF8M-QQpf7FZmO5bRj3mwLI/edit?usp=sharing)

### Utility Items
Currently two utility items have been added, the Aescfell Belt and the Stánbrysan Belt. These items increase your damage dealt to trees and ores, respectively.
## Uptiering
As you progress through the game, you will be able to uptier armor. Essentially leveling the armor up to utilize the new materials you come across. When you do so, the armor set's armor rating increases, and every one of its bonuses becomes more effective.
For example, you can uptier the Bronze Helmet to become an Iron-Reinforced Bronze Helmet by combining a Bronze Helmet, some Iron, and some Deer Hide at the forge. By doing this, it's armor has increased from 15 to 19, and it's bonus(+5% Melee Damage) increases to +7% Melee Damage.
Uptiering means that you won't have chests full of out-dated equipment as you can continually have the armor set keep pace with your progression!
Note that to uptier padded armor, you have to use the Artisan Table asides from the forge or workbench.
## Balance
**Terraheim** is highly customizable as the effectiveness, material costs, and almost every armor stat can be tweaked in the `balance.json` file.
**Bow Balance** - To prevent bowmen from being the be all end all class, the bow weapon type has been slightly nerfed by halving movement speed while the bow is drawn. This can be mitigated by wearing silver armor, or if you don't like it at all, you can remove it by setting `"baseBowDrawMoveSpeeed"` to 1 in the `balance.json` file.
## Compatibility
Currently no known issues.
EpicLoot **IS** compatible, but there may be balance issues if both are installed.
## Future Plans
Flametal armor sets for each class
New weapons to fill gaps in the roster
EpicLoot version with tweaked balance
### Known Issues
- You will appear naked on the character selection screen  
- Currently, armor set bonuses do not scale with uptiering.

### Recommended Mods
These mods compliment Terraheim by adding new weapons and rebalancing mechanics.
[Unique Weapons](https://www.nexusmods.com/valheim/mods/799) by v801
[Combat Overhaul](https://www.nexusmods.com/valheim/mods/591) by leseryk (*This is the inspiration for the weapon draw speed effect.*)
[Better Archery](https://www.nexusmods.com/valheim/mods/348?tab=description) by Elfking23

### Patchnotes
**v1.1.0**
- Added two new utility belts! The Aescfell Belt and the Stánbrysan Belt which increase your damage dealt to trees and ores, respectively.
- Fixed bug where uptiering armor removed the set bonus.
**v1.0.2:**
- Fixed bug with weird duplicating arrows when an arrow was refunded by an armor effect
- Moved Padded equipment to the forge so they can be upgraded
