*Verification Gatekeeper* Prevents players from doing anything on the server until they are member of a specific group.

This plugin was written after a suggestion by [TFNBlackMarket](https://umod.org/user/TFNBlackMarket)


Keep in mind that this plugin will disable every possible action a player can perform, other then walking around.


## Configuration

- **Verified Player Group** -- Defines the group that disables all the preventive measures of this plugin.
- **Admin Is Always Verified** -- (true/false) When set to "true" players with the Admin rank will bypass the preventive measures.
- **Prevent (Dis-)Mount** -- (true/false) Prevents mountable entity usage. Hooks: *CanDismountEntity, CanMountEntity, CanSwapToSeat, OnRidableAnimalClaim*
- **Prevent Bed Actions** -- (true/false) Prevents players from interacting with beds. Hooks: *CanAssignBed, CanRenameBed, CanSetBedPublic*
- **Prevent Build** -- (true/false) Prevents players from building. Hooks: *CanAffordToPlace, CanBuild, OnConstructionPlace*
- **Prevent Card Swiping** -- (true/false) Prevents players from swiping cards. Hooks: *OnCardSwipe*
- **Prevent Chat** -- (true/false) PRevents players from chatting. Hooks: *OnUserChat*
- **Prevent Collectible Pickup** -- (true/false) Prevents players from picking collectables up. Hooks: *OnCollectiblePickup*
- **Prevent Command** -- (true/false) Prevents players from sending commands to the server. Hooks: *OnUserCommand*
- **Prevent Counter Actions** -- (true/false) Prevents players from interacting with counters. Hooks: *OnCounterModeToggle, OnCounterTargetChange*
- **Prevent Crafting** -- (true/false) Prevents players from crafting items. Hooks: *CanCraft*
- **Prevent Crate Hack** -- (true/false) Prevents players from initialing crate hacks. Hooks: *CanHackCrate*
- **Prevent Cupboard Actions** -- (true/false) Prevents players from interacting with TCs. Hooks: *OnCupboardAuthorize, OnCupboardClearList, OnCupboardDeauthorize*
- **Prevent Custom UI** -- (true/false) Prevents players from showing custom UIs sent by the server's other plugins. Hooks: *CanUseUI*
- **Prevent Demolish** -- (true/false) Prevents players from demolish structures. Hooks: *CanDemolish, OnStructureDemolish*
- **Prevent Deploy Item** -- (true/false) Prevents players from deploying items. Hooks: *CanDeployItem*
- **Prevent Door Actions** -- (true/false) Prevents players from opening/closing doors. Hooks: *OnDoorClosed, OnDoorOpened*
- **Prevent Elevator Actions** -- (true/false) Prevents players from interacting with elevator buttons. Hooks: *OnElevatorButtonPress*
- **Prevent Entity Looting** -- (true/false) Prevents players from looting entities. Hooks: *CanLootEntity*
- **Prevent Entity Pickup** -- (true/false) Prevents players from picking up an entity. Hooks: *CanPickupEntity*
- **Prevent Explosives** -- (true/false) Prevents explosives from being dropped/thrown. Hooks: *OnExplosiveDropped, OnExplosiveThrown*
- **Prevent Flamers** -- (true/false) Prevents the use/spread of flamers. Hooks: *OnFlameThrowerBurn*
- **Prevent Fuel Actions** -- (true/false) Prevents interaction with fuel storages. Hooks: *anCheckFuel*
- **Prevent Growable Gathering** -- (true/false) Prevents players from gathering growables. Hooks: *OnGrowableGather*
- **Prevent Healing Item Usage** -- (true/false) Prevents players from healing themselves. Hooks: *OnHealingItemUse*
- **Prevent Helicopter Actions** -- (true/false) Prevents players from using helicopters. Hooks: *CanUseHelicopter*
- **Prevent Item Action** -- (true/false) Prevents item actions from being performed in your inventory (drop, unwrap, ...). Hooks: *OnItemAction*
- **Prevent Item Dropping** -- (true/false) Prevents players from dropping items. Hooks: *CanDropActiveItem*
- **Prevent Item Moving** -- (true/false) Prevents players from moving items around (In inventories and to/from storages). Hooks: *CanAcceptItem, CanMoveItem*
- **Prevent Item Pickup** -- (true/false) Prevents players from picking items up. Hooks: *OnItemPickup*
- **Prevent Item Stacking** -- (true/false) PRevents players from stacking items. Hooks: *CanStackItem*
- **Prevent Item Wearing** -- (true/false) Prevents players from wearing items. Hooks: *CanWearItem*
- **Prevent Lift Actions** -- (true/false) Prevents players from using lifts and procedural lifts. Hooks: *OnLiftUse*
- **Prevent Lock Actions** -- (true/false) Prevents players from interacting with locks. Hooks: *CanChangeCode, CanLock, CanPickupLock, CanUnlock, CanUseLockedEntity, OnCodeEntered, OnItemLock, OnItemUnlock*
- **Prevent Mailbox Actions** -- (true/false) Prevents players from interacting with mailboxes. Hooks: *CanUseMailbox*
- **Prevent Melee** -- (true/false) Prevents players from performing melee attacks. Hooks: *OnMeleeAttack, OnMeleeThrown*
- **Prevent Oven Actions** -- (true/false) Prevents players from toggeling ovens (Campfire, Furnace,...). Hooks: *OnOvenToggle*
- **Prevent Phone Actions** -- (true/false) Prevents players from using phones in any meaningful manner. Hooks: *OnPhoneDial, OnPhoneCallStart, OnPhoneNameUpdate*
- **Prevent Player Assist** -- (true/false) Prevents players from assisting (healing) and reviving other players. Hooks: *OnPlayerAssist, OnPlayerRevive*
- **Prevent Player Looting** -- (true/false) Prevents players from looting other players. Hooks: *CanLootPlayer*
- **Prevent Push** -- (true/false) Prevents players from pushing vehicles. Hooks: *CanPushBoat, OnVehiclePush*
- **Prevent Recycler Actions** -- (true/false) PRevents players from toggeling recyclers. Hooks: *OnRecyclerToggle*
- **Prevent Reloading** -- (true/false) Prevents players from reloading weapons and changing the current ammo type. Hooks: *OnReloadMagazine, OnReloadWeapon, OnSwitchAmmo*
- **Prevent Repair** -- (true/false) Prevents players from repairing. Hooks: *OnHammerHit, OnStructureRepair*
- **Prevent Research** -- (true/false) Prevents players from researching. Hooks: *CanResearchItem, CanUnlockTechTreeNode, CanUnlockTechTreeNodePath*
- **Prevent Rockets** -- (true/false) Prevents rockets from doing anything other then disappearing in a plume of smoke. Hooks: *OnRocketLaunched*
- **Prevent Shop Actions** -- (true/false) Prevents players from actually trading items at shops. Hooks: *OnShopCompleteTrade*
- **Prevent Sign Update** -- (true/false) Prevents players from updating signs and paintings. Hooks: *CanUpdateSign*
- **Prevent Stash Actions** -- (true/false) Prevents players from interacting with stashes. Hooks: *CanHideStash, CanSeeStash*
- **Prevent Structure Rotate** -- (true/false) Prevents players from rotating structures. Hooks: *OnStructureRotate*
- **Prevent Switch Actions** -- (true/false) Prevents players from toggeling switches. Hooks: *OnSwitchToggle*
- **Prevent Team Creation** -- (true/false) Prevents players from creating teams. Hooks: *OnTeamCreate*
- **Prevent Trap Actions** -- (true/false) Prevents players from interacting with traps (Other then triggering them). Hooks: *OnTrapArm, OnTrapDisarm*
- **Prevent Turret Actions** -- (true/false) Prevents players from interacting with turrets (Other then getting shot by them). Hooks: *OnTurretAuthorize, OnTurretClearList, OnTurretRotate*
- **Prevent Upgrade** -- (true/false) Prevents players from being able to upgrade structures. Hooks: *CanAffordUpgrade, CanChangeGrade, OnStructureUpgrade*
- **Prevent Vending Admin** -- (true/false) Prevents vending machine administration. Hooks: *CanAdministerVending, OnRotateVendingMachine*
- **Prevent Vending Usage** -- (true/false) Prevents guest usage of vending machines. Hooks: *CanUseVending, OnBuyVendingItem, OnVendingTransaction*
- **Prevent Weapon Firing** -- (true/false) Prevents a fired weapon's projectiles from doing any damage. Hooks: *OnWeaponFired*
- **Prevent Wiring** -- (true/false) Prevents players from being able to hook wires up. Hooks: *CanUseWires*
- **Prevent Wood Cutting** -- (true/false) Prevents players from cutting wood. Hooks: *CanTakeCutting*
- **Prevent World Projectiles** -- (true/false) Prevents a projectile from being created in the world. Hooks: *CanCreateWorldProjectile, OnCreateWorldProjectile*
- **Prevent Wounded** -- (true/false) Prevents players from being wounded/hurt. Hooks: *CanBeWounded*


```json
{
  "Verified Player Group": "verified",
  "Admin Is Always Verified": true,
  "Prevent (Dis-)Mount": true,
  "Prevent Bed Actions": true,
  "Prevent Build": true,
  "Prevent Card Swiping": true,
  "Prevent Chat": true,
  "Prevent Collectible Pickup": true,
  "Prevent Command": true,
  "Prevent Counter Actions": true,
  "Prevent Crafting": true,
  "Prevent Crate Hack": true,
  "Prevent Cupboard Actions": true,
  "Prevent Custom UI": false,
  "Prevent Demolish": true,
  "Prevent Deploy Item": true,
  "Prevent Door Actions": true,
  "Prevent Elevator Actions": true,
  "Prevent Entity Looting": true,
  "Prevent Entity Pickup": true,
  "Prevent Explosives": true,
  "Prevent Flamers": true,
  "Prevent Fuel Actions": true,
  "Prevent Growable Gathering": true,
  "Prevent Healing Item Usage": true,
  "Prevent Helicopter Actions": true,
  "Prevent Item Action": true,
  "Prevent Item Dropping": true,
  "Prevent Item Moving": true,
  "Prevent Item Pickup": true,
  "Prevent Item Stacking": true,
  "Prevent Item Wearing": true,
  "Prevent Lift Actions": true,
  "Prevent Lock Actions": true,
  "Prevent Mailbox Actions": true,
  "Prevent Melee": true,
  "Prevent Oven & Furnace Actions": true,
  "Prevent Phone Actions": true,
  "Prevent Player Assist": true,
  "Prevent Player Looting": true,
  "Prevent Push": true,
  "Prevent Recycler Actions": true,
  "Prevent Reloading": true,
  "Prevent Repair": true,
  "Prevent Research": true,
  "Prevent Rockets": true,
  "Prevent Shop Actions": true,
  "Prevent Sign Update": true,
  "Prevent Stash Actions": true,
  "Prevent Structure Rotate": true,
  "Prevent Switch Actions": true,
  "Prevent Team Creation": true,
  "Prevent Trap Actions": true,
  "Prevent Turret Actions": true,
  "Prevent Upgrade": true,
  "Prevent Vending Admin": true,
  "Prevent Vending Usage": true,
  "Prevent Weapon Firing": true,
  "Prevent Wiring": true,
  "Prevent Wood Cutting": true,
  "Prevent World Projectiles": true,
  "Prevent Wounded": true
}
```
