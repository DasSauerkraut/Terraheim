

# Terraheim - Weapons/Tools/Armor
**Terraheim** is both an overhaul for Valheim's armor system with the goal of making each armor set viable all the way to the end of the game and a weapon expansion that fills the gaps in the games base roster. 
![Beeg boe](https://media.discordapp.net/attachments/610164117277245482/841774554695204894/unknown.png?width=960&height=540)
## Weapons and Tools
23 new weapons and tools have been added with the goal of filling in the gaps in each tier.
12 of which are new Flametal weapons which have unique and dangerous effects, from hurtling waves of fire across the battlefield with Mistilteinn to teleporting to the struck location with Rhongomiant! 
![weapons](https://cdn.discordapp.com/attachments/610164117277245482/828743095369334864/Capture.PNG)![Flametal Weapons](https://media.discordapp.net/attachments/610164117277245482/841773595658289182/unknown.png?width=960&height=632)
### Throwing Weapons
Five new throwing axes have been added as well as Fire, Frost and Lightning bombs!
![Throwing Axes](https://cdn.discordapp.com/attachments/610164117277245482/834155655367491684/unknown.png)
You can view damage values, moveset information, and recipes [here](https://docs.google.com/spreadsheets/d/1DlnnJOvorgCQ1k1e4lO5nF8M-QQpf7FZmO5bRj3mwLI/edit?usp=sharing) in the Weapons tab.
If you only want the new weapons, you can set the `armorChangesEnabled` field to false in the `balance.json` file to disable the armor changes.
## Armor
Each piece of armor has been given its own unique effect and each armor set its own set bonus while also allowing armor to be 'uptiered,' which increases all of the piece's stats to be usable in the next tier of gameplay. These effects can range from simple damage bonuses all the way to interesting effects like Wyrdarrow﻿!
If you only want the armor changes, you can delete the TerraheimItems.dll file to disable the armor changes.
### Classes
Each armor set has been divided into four classes:
#### Berserkr
Berserkr's have a focus on dodging, axe damage, two handed weapon damage and stamina management. 

#### Bowman
Bowmen focus on dealing damage via, well bows, as well as daggers and spears. They focus on ammo consumption, backstab bonus, and sneaking. They have the worst armor rating in game.

#### Shieldbrother
Shieldbrothers are adept at blocking and have increased health. They can effectively deal damage with all melee weapons, but they are slower than the other classes. They have the highest armor rating in game.

#### Braggart
Braggart armor is for those who want a challenge, they provide some minor stat benefits with some large detriments too. Think of these sets as the Calamity ring from Dark Souls.

### Barbarian Armor
If you have the [Barbarian Armor](https://www.nexusmods.com/valheim/mods/640) mod installed, it has been fully incorporated into Terraheim. The barbarian armor set provides several bonuses to throwing weapons as well as some interesting tertiary effects like a stacking move speed buff on kill and increased damage against low health targets.

![barbarian armor](https://cdn.discordapp.com/attachments/610164117277245482/834156356680286249/unknown.png)

You can view the full values for each set across every tier [here!](https://docs.google.com/spreadsheets/d/1DlnnJOvorgCQ1k1e4lO5nF8M-QQpf7FZmO5bRj3mwLI/edit?usp=sharing)

### Reforging
As you progress through the game, you will be able to reforge armor. Essentially leveling the armor up to utilize the new materials you come across. When you do so, the armor set's armor rating increases, and every one of its bonuses becomes more effective.
For example, you can reforge the Bronze Helmet to become an Iron-Reinforced Bronze Helmet by combining a Bronze Helmet, some Iron, and some Deer Hide at the forge. By doing this, it's armor has increased from 15 to 19, and it's bonus(+5% Melee Damage) increases to +7% Melee Damage.
Reforging means that you won't have chests full of out-dated equipment as you can continually have the armor set keep pace with your progression!
You can reforge an item by going to the requisite crafting station with new materials and craft it as a new piece of armor.
## Utility Items
Currently five utility items have been added:
- Aescfell Belt: Increased damage vs trees
- Stánbrysan Belt: Increased damage vs ores
- Eorðtilia Belt: Harvest additional flora from bushes and crops
- Stulor Belt: Increased sneak speed and less sneak stamina use
- Casul Belt: Immunity to the wet ailment
## Balance
**Terraheim** is highly customizable as the effectiveness, material costs, and almost every armor stat can be tweaked in the `balance.json` file. Weapons can be adjusted and disabled in the `weaponBalance.json` file. Flametal weapons' special effects can be tweaked and disabled there as well.

You can even change out what armor sets have what set effect! If you want the bronze armor set to have ranged damage or Wyrdarrow, you can!
Just note that set effects are seperate from standard armor effects, so you can't assign Thorns to a helmet, it has to be the set bonus. You can view the effect ids on the balance sheet, there are several unimplemented effects there if you want to experiment a bit.

**Bow Balance** - To prevent bowmen from being the be all end all class, the bow weapon type has been slightly nerfed by halving movement speed while the bow is drawn. This can be mitigated by wearing silver armor, or if you don't like it at all, you can remove it by setting `"baseBowDrawMoveSpeeed"` to 1 in the `balance.json` file.
## Compatibility
There seems to be an incompatibility with ValheimLibOpenDatabasePatch.dll. Unfortunately I can't do anything about this issue, sorry.
TripleBronze also appears to be incompatible.
EpicLoot is compatible but when you reforge an enchanted piece of armor, it will lose that enchantment until you relog/reload your world. A more graceful fix is coming.
## Future Plans
Flametal armor sets for each class
EpicLoot version with tweaked balance
### Known Issues
none atm

### Recommended Mods
These mods compliment Terraheim by adding new weapons and rebalancing mechanics.
[Unique Weapons](https://www.nexusmods.com/valheim/mods/799) by v801
[Combat Overhaul](https://www.nexusmods.com/valheim/mods/591) by leseryk (*Terraheim is balanced with CO in mind.*)
[Better Archery](https://www.nexusmods.com/valheim/mods/348?tab=description) by Elfking23
[Monsternomicon](https://www.nexusmods.com/valheim/mods/1166?tab=description) by Belasias (*Adds new enemies to make the Ashlands a bit more fun to explore*)
[Forgotten Biomes](https://www.nexusmods.com/valheim/mods/1128?tab=description) by Alree (*Adds a bunch of new clutter to the Ashlands, Ocean, and Deep North biomes.*)

### Mirrors
[Github Armor Repo](https://github.com/DasSauerkraut/Terraheim)
[Github Items Repo](https://github.com/DasSauerkraut/TerraheimItems)
[Nexus](https://www.nexusmods.com/valheim/mods/803?tab=description)

### Patchnotes
**v2.0.4**
- Fixed NPCs being unable to fire bows
- Fixed some bows being unable to fire arrows
- Changed how to disable the Terraheim armor changes. Simply set the armorChangesEnabled field to false in the balance.json file. The Terraheim.dll is now REQUIRED to be installed!
**v2.0.3**
- Fixed bug where Gywnttorrwr would not shoot arrows
- O L Y M P I A can now be disabled
**v2.0.2**
- Fixed bug where new characters could not be created
- Fixed characters being naked on the main menu
- Fixed bug where characters were missing beards/hair etc.
**v2.0.1**
- Forgot to change dependancies for the TerraheimItems.dll oops
**v2.0.0 - Embers of Surtr**
This is a hefty update, adding 14 weapons, tons of effects, a new armor set, and a rework of the Troll Leather and Bronze armor sets. Enjoy!
*Additions*
- Added 12 new flametal weapons! Each flametal weapon has a unique effect, from hurling waves of fire across the battlefield with Mistilteinn to teleporting to the hit location using Rhongomiant. Each effect can be configured in weaponBalance.json, and the durability drain can be adjusted there as well. You can also turn off all effects in the same location.
- Added the Blackmetal Bow, a fast firing sidegrade to the Draugr Fang.
- Added the Parrying Dagger, which has low block values, but has a very high parry bonus.
- Implemented the Béotes set, for those of you who want a unique challenge. This set can be crafted by reforging rag armor to tier 2.
- Renamed all armor sets.

- **Terraheim has migrated to JVL, ValheimLib is no longer required, but JVL MUST be installed for this mod to work**
- You can now disable individual weapons in the weaponBalance.json

*Balance*
- Reworked the Jotunn(Troll Leather) set
	- Jotunn Hood now provides a max +15% damage bonus to bows, spears, and daggers.
	- Jotunn Legs now deal +10% poison damage when you hit an enemy with a damage type it is vulnerable to.
	- Jotunn Set Bonus is now Pinning: Hitting an enemy with a damage type it is vulnerable to Pins it for 10s. Pinned enemies are vulnerable to all damage types and have reduced movement speed.
- Reworked the Eorl(Bronze) Set
	- Eorl Legs now provide a % based health bonus as opposed to a flat buff.
	- Eorl Set Bonus is now Brassflesh: Hitting an enemy grants 2% damage reduction for 10 seconds, stacking up to 20%. This reduction applies before any armor bonuses are calculated.
- Greatswords have had some balance adjustments
	- Greatsword block values have all increased by 4, so Iron is now at 7, Folcbrand is at 8, and the Blackmetal greatsword is now at 9.
	- Greatsword attack speed has been overall slowed by 20% to give them a different feel from normal swords.

*Fixes*
- Fixed the Black Void of Doom bug for the Marked for Death VFX
- Fixed incorrect upgrade materials
- Fixed duplication bug
- Fixed greatswords being incorrectly flagged under the Run skill
- Fixed breaking other mods localization
- Fixed lag spike when grabbing corpses or taking all from chests.
- Probably introduced a bunch of new bugs ¯\\\_(ツ)_/¯

**v1.7.4**
- The first attack for greatswords has been sped up by 20%.
- The rest of the greatswords attack combo has been sped up 40%.
- Added field in balance.json to disable the Marked For Death FX until I can find a proper solution for the black void bug.
- Fixed bug where greatswords animation speed tweaks would not apply, leading to inconsistent attack timing.
- Fixed bug where thorns would do crazy amounts of damage to bosses. This has had the side effect of thorns damage being calc'd after armor damage reduction, so to compensate, thorns reflect % is now 110%.
**v1.7.3**
- Fixed wolftears 1 hit protection triggering in non-applicaple cases
**v1.7.2**
- Fixed status effects reading from the last instance of that effect. This fixes the issue with the bronze armor using the padded armor's values for its damage bonus.
**v1.7.1**
- Fixed Wolftears preventing healing on crouch
- Fixed reforging not consuming previous armor
- Fixed missing requirements for reforging
- Fixed Poisoned Iron Throwing axe's damages. 75 Slash -> 30 Slash, 20 Poison
**v1.7.0 - Greatswords Reforged**
- Greatswords have had a balance pass. The primary moveset is now the battleaxes, but the first attack is 1.6x faster. Their range has been increased to 3.
- Wolftears has recieved some love too. For starters, it's max damage bonus has been upped to +50% at 20% hp. Also every 4 minutes (260 seconds) if you take lethal damage, you will survive at 1hp.
- The thorns set effect will now kill enemies that are below 50% hp.
- You can now configure armor set's effects in the balance.json! This means you can swap around individual effects to your pleasure. Want bronze to have ranged damage, go ahead and change it!
- Fixed armor VFX sticking around after the armor set had been removed
- Fixed the leather armor set bonus not scaling
- Loosened restrictions on folder naming conventions and locations, this should improve Terraheim's ability to be integrated into modpacks.
- There is a potential fix for the Barbarian throwing axe Black Void of DoomTM included as well.
**v1.6.5**
- Reduced Battle Furor VFX and made it clear when the armor is removed
- Fixed Wolftears description values and it's damage bonus calculation
- Fixed recipes requiring previous armor sets to upgrade
- Fixed issues with Vulkan shaders
**v1.6.4**
- Actually fixed inventory issues
**v1.6.3**
- Fixed missing recipe for Blackmetal Throwing Axe
- Removed extraneous logging.
**v1.6.1**
- Fixed broken translation folder structure for the thunderstore.
- Potential fix for inventory issues
**v1.6.0 - Daroþas of Ullr Update**
Oh wow this one took a bit, but there is a ton of stuff here.
- Added a new weapon type: Throwing Axes! There is a version for each tier of material. - Integrated the Barbarian Armor mod as a throwing weapon set. 
- Added the Eorthtilia Belt, which increases the number of resources gained when harvest bushes or crops. 
- Added in the Stulor Belt, which increases sneak speed by 25% and reduces stamina cost for sneaking by 25%. 
- Added in the Casul Belt, which prevents you from gaining the Wet effect while worn. 
- Added in visuals for Lifesteal, Thorns, and Wolftears armor effects. 
- Added in tooltips that will display adjusted damages on weapons based on what armor effects you currently have active. 
- Changed the color of armor effect tooltips to reflect the changes made to weapon tooltips 
- Leather set bonus is now Battle Furor, which provides a damage buff when above a certain HP threshold. 
- Iron set bonus is now Wolftears, which provides a gradually scaling damage buff based on the amount of HP you're missing 
- Silver set bonus is now Wyrdarrow, which lets you cause an explosion with dagger/spears/bows after hitting enemies a set number of times. 
- Leather Leg armor's effect has been changed to Lifesteal -Lifesteal now caps at 4% lifesteal maximum 
- Padded Leg armor's effect has been changed to Heal On Block/Parry 
- Thorns has been increased to 40% reflected damage max, but it can no longer kill enemies 
- Weapon damages and recipes can now be adjusted in the weaponBalance.json file 
- Massively rebalanced armor values across the board. Bronze armor no longer provides a crazy amount of armor for its initial tier, and troll leather wearers can actually take a hit now. 
- Upgrading armor now increases its defense by 2 instead of 1 
- Material costs for upgrading armor has been increased to reflect this. 
- Greatsword block bonuses have been reduced in order to make the parry bonus a bit less strong. 
- The number of crafted bombs has been increased to 5 
- Greatsword swing speed has been reduced to 80% of normal
- -Increased the Spirit damage from the Wolf cape to 10 
- Fixed compatibility issues with the Mjolnir and Hemorrhage mods 
- Somewhat fixed compatibility with epic loot. When you reforge an enchanted item, it will initially lose its effect but if you go back to menu and rejoin/reload the world it will preserve its effect. This is obviously not perfect, and a better fix is coming. 
- Fixed bug where you would not take damage on unarmed block while the Thorns effect is active 
- Fixed bug where the localization values where not applying correctly 
- Fixed a number of recipe bugs and issues 
- Fixed incorrect stamina regen values for Iron Legs 
- Refactored Bow Draw Speed Code
**v1.4.0 - The Zweihander Update**
- Added 4 new weapons! The Iron Greatsword, the Blackmetal Greatsword, the Silver Battleaxe, and the Blackmetal Battleaxe.
- Damage modifier for the Blackmetal Axehammer's slam attack has been buffed to 2x.
- Blackmetal Axehammer's slam attack has been improved.
**v1.3.0**
- Added three new types of bombs! The Fire, Frost and Lightning Bomb
- Rebalanced Folcbrand to better fit the upcoming two handed weapon update
- Fixed issue with leather pants not scaling
- Actually fixed bug where set bonuses would not improve when an armor was uptiered for real this time.
- Fixed bugged particle effects for Forstasca and Blackmetal pickaxe
- The stutter bug has been fixed, we're just waiting on the release of the merged ValheimLib and JotunnLib!
**v1.2.1**
- Reworked Silver armor set bonus! It now provides a damage boost while below a set health percentage. You can prevent healing by crouching too!
- Replaced the Blackmetal Axehammer's alternate attack with an AoE slam ala Stagbreaker.
- Fixed bug where set bonuses would not improve when an armor was uptiered.
- Fixed(?) bug where errors would appear when two tree logs ran into each other.
**v1.2.0**
- Added in 9 new weapons and tools!
- Fixed error where the Block Power buff would continually stack to the moon. Sorry ¯\\_(ツ)\_/¯
**v1.1.1**
- Potential fix for the null reference in the mining/tree cutting patch.
- Fixed balance.json issue that caused Troll Leather Chest's effect to not function.
**v1.1.0**
- Added two new utility belts! The Aescfell Belt and the Stánbrysan Belt which increase your damage dealt to trees and ores, respectively.
- Fixed bug where uptiering armor removed the set bonus.
**v1.0.2:**
- Fixed bug with weird duplicating arrows when an arrow was refunded by an armor effect
- Moved Padded equipment to the forge so they can be upgraded
