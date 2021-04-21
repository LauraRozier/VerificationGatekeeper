/* --- Contributor information ---
 * Please follow the following set of guidelines when working on this plugin,
 * this to help others understand this file more easily.
 *
 * NOTE: On Authors, new entries go BELOW the existing entries. As with any other software header comment.
 *
 * -- Authors --
 * Thimo (ThibmoRozier) <thibmorozier@live.nl> 2021-04-19 +
 *
 * -- Naming --
 * Avoid using non-alphabetic characters, eg: _
 * Avoid using numbers in method and class names (Upgrade methods are allowed to have these, for readability)
 * Private constants -------------------- SHOULD start with a uppercase "C" (PascalCase)
 * Private readonly fields -------------- SHOULD start with a uppercase "C" (PascalCase)
 * Private fields ----------------------- SHOULD start with a uppercase "F" (PascalCase)
 * Arguments/Parameters ----------------- SHOULD start with a lowercase "a" (camelCase)
 * Classes ------------------------------ SHOULD start with a uppercase character (PascalCase)
 * Methods ------------------------------ SHOULD start with a uppercase character (PascalCase)
 * Public properties (constants/fields) - SHOULD start with a uppercase character (PascalCase)
 * Variables ---------------------------- SHOULD start with a lowercase character (camelCase)
 *
 * -- Style --
 * Max-line-width ------- 160
 * Single-line comments - // Single-line comment
 * Multi-line comments -- Just like this comment block!
 */

using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("Verification Gatekeeper", "ThibmoRozier", "1.0.1")]
    [Description("Prevents players from doing anything on the server until they are member of a specific group.")]
    public class VerificationGatekeeper : RustPlugin
    {
        #region Variables
        private ConfigData FConfigData;
        #endregion Variables

        #region Config
        /// <summary>
        /// The config type class
        /// </summary>
        private class ConfigData
        {
            [JsonProperty("Verified Player Group", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue("verified")]
            public string VerifiedPlayerGroup { get; set; }
            [JsonProperty("Admin Is Always Verified", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool AdminAlwaysVerified { get; set; }
            [JsonProperty("Prevent (Dis-)Mount", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMount { get; set; }
            [JsonProperty("Prevent Bed Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventBedActions { get; set; }
            [JsonProperty("Prevent Build", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventBuild { get; set; }
            [JsonProperty("Prevent Card Swiping", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCardSwiping { get; set; }
            [JsonProperty("Prevent Chat", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventChat { get; set; }
            [JsonProperty("Prevent Collectible Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCollectiblePickup { get; set; }
            [JsonProperty("Prevent Command", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCommand { get; set; }
            [JsonProperty("Prevent Counter Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCounterActions { get; set; }
            [JsonProperty("Prevent Crafting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCrafting { get; set; }
            [JsonProperty("Prevent Crate Hack", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCrateHack { get; set; }
            [JsonProperty("Prevent Cupboard Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCupboardActions { get; set; }
            [JsonProperty("Prevent Custom UI", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(false)]
            public bool PreventCustomUI { get; set; } = false;
            [JsonProperty("Prevent Demolish", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDemolish { get; set; }
            [JsonProperty("Prevent Deploy Item", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDeployItem { get; set; }
            [JsonProperty("Prevent Door Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDoorActions { get; set; }
            [JsonProperty("Prevent Elevator Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventElevatorActions { get; set; }
            [JsonProperty("Prevent Entity Looting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventEntityLooting { get; set; }
            [JsonProperty("Prevent Entity Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventEntityPickup { get; set; }
            [JsonProperty("Prevent Explosives", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventExplosives { get; set; }
            [JsonProperty("Prevent Flamers", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventFlamers { get; set; }
            [JsonProperty("Prevent Fuel Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventFuelActions { get; set; }
            [JsonProperty("Prevent Growable Gathering", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventGrowableGathering { get; set; }
            [JsonProperty("Prevent Healing Item Usage", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventHealingItemUsage { get; set; }
            [JsonProperty("Prevent Helicopter Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventHelicopterActions { get; set; }
            [JsonProperty("Prevent Item Action", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemAction { get; set; }
            [JsonProperty("Prevent Item Dropping", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemDropping { get; set; }
            [JsonProperty("Prevent Item Moving", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemMoving { get; set; }
            [JsonProperty("Prevent Item Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemPickup { get; set; }
            [JsonProperty("Prevent Item Stacking", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemStacking { get; set; }
            [JsonProperty("Prevent Item Wearing", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemWearing { get; set; }
            [JsonProperty("Prevent Lift Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventLiftActions { get; set; }
            [JsonProperty("Prevent Lock Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventLockActions { get; set; }
            [JsonProperty("Prevent Mailbox Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMailboxActions { get; set; }
            [JsonProperty("Prevent Melee", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMelee { get; set; }
            [JsonProperty("Prevent Oven & Furnace Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventOvenActions { get; set; }
            [JsonProperty("Prevent Phone Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPhoneActions { get; set; }
            [JsonProperty("Prevent Player Assist", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPlayerAssist { get; set; }
            [JsonProperty("Prevent Player Looting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPlayerLooting { get; set; }
            [JsonProperty("Prevent Push", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPush { get; set; }
            [JsonProperty("Prevent Recycler Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRecyclerActions { get; set; }
            [JsonProperty("Prevent Reloading", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventReloading { get; set; }
            [JsonProperty("Prevent Repair", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRepair { get; set; }
            [JsonProperty("Prevent Research", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventResearch { get; set; }
            [JsonProperty("Prevent Rockets", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRockets { get; set; }
            [JsonProperty("Prevent Shop Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventShopActions { get; set; }
            [JsonProperty("Prevent Sign Update", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventSignUpdate { get; set; }
            [JsonProperty("Prevent Stash Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventStashActions { get; set; }
            [JsonProperty("Prevent Structure Rotate", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventStructureRotate { get; set; }
            [JsonProperty("Prevent Switch Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventSwitchActions { get; set; }
            [JsonProperty("Prevent Team Creation", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTeamCreation { get; set; }
            [JsonProperty("Prevent Trap Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTrapActions { get; set; }
            [JsonProperty("Prevent Turret Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTurretActions { get; set; }
            [JsonProperty("Prevent Upgrade", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventUpgrade { get; set; }
            [JsonProperty("Prevent Vending Admin", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventVendingAdmin { get; set; }
            [JsonProperty("Prevent Vending Usage", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventVendingUsage { get; set; }
            [JsonProperty("Prevent Weapon Firing", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWeaponFiring { get; set; }
            [JsonProperty("Prevent Wiring", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWiring { get; set; }
            [JsonProperty("Prevent Wood Cutting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWoodCutting { get; set; }
            [JsonProperty("Prevent World Projectiles", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWorldProjectiles { get; set; }
            [JsonProperty("Prevent Wounded", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWounded { get; set; }
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();

            try
            {
                FConfigData = Config.ReadObject<ConfigData>();

                if (FConfigData == null)
                    LoadDefaultConfig();
            }
            catch (Exception)
            {
                LoadDefaultConfig();
            }

            SaveConfig();
        }

        protected override void LoadDefaultConfig()
        {
            FConfigData = new ConfigData
            {
                VerifiedPlayerGroup = "verified",
                AdminAlwaysVerified = true,
                PreventMount = true,
                PreventBedActions = true,
                PreventBuild = true,
                PreventCardSwiping = true,
                PreventChat = true,
                PreventCollectiblePickup = true,
                PreventCommand = true,
                PreventCounterActions = true,
                PreventCrafting = true,
                PreventCrateHack = true,
                PreventCupboardActions = true,
                PreventCustomUI = false,
                PreventDemolish = true,
                PreventDeployItem = true,
                PreventDoorActions = true,
                PreventElevatorActions = true,
                PreventEntityLooting = true,
                PreventEntityPickup = true,
                PreventExplosives = true,
                PreventFlamers = true,
                PreventFuelActions = true,
                PreventGrowableGathering = true,
                PreventHealingItemUsage = true,
                PreventHelicopterActions = true,
                PreventItemAction = true,
                PreventItemDropping = true,
                PreventItemMoving = true,
                PreventItemPickup = true,
                PreventItemStacking = true,
                PreventItemWearing = true,
                PreventLiftActions = true,
                PreventLockActions = true,
                PreventMailboxActions = true,
                PreventMelee = true,
                PreventOvenActions = true,
                PreventPhoneActions = true,
                PreventPlayerAssist = true,
                PreventPlayerLooting = true,
                PreventPush = true,
                PreventRecyclerActions = true,
                PreventReloading = true,
                PreventRepair = true,
                PreventResearch = true,
                PreventRockets = true,
                PreventShopActions = true,
                PreventSignUpdate = true,
                PreventStashActions = true,
                PreventStructureRotate = true,
                PreventSwitchActions = true,
                PreventTeamCreation = true,
                PreventTrapActions = true,
                PreventTurretActions = true,
                PreventUpgrade = true,
                PreventVendingAdmin = true,
                PreventVendingUsage = true,
                PreventWeaponFiring = true,
                PreventWiring = true,
                PreventWoodCutting = true,
                PreventWorldProjectiles = true,
                PreventWounded = true
            };
        }

        protected override void SaveConfig() => Config.WriteObject(FConfigData);
        #endregion Config

        #region Script Methods
        private object CheckAndReturnNullOrFalse(BasePlayer aPlayer, bool aIndEnforce)
        {
            if (aPlayer == null || !aIndEnforce)
                return null;

            if (aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasGroup(aPlayer.UserIDString, FConfigData.VerifiedPlayerGroup))
                return null;

            return false;
        }

        private object CheckAndReturnNullOrFalse(IPlayer aPlayer, bool aIndEnforce)
        {
            if (aPlayer == null || !aIndEnforce)
                return null;

            if (aPlayer.IsServer || (FConfigData.AdminAlwaysVerified && aPlayer.IsAdmin) ||
                permission.UserHasGroup(aPlayer.Id, FConfigData.VerifiedPlayerGroup))
                return null;

            return false;
        }
        #endregion Script Methods

        #region Hooks
        void OnServerInitialized()
        {
            LoadConfig();

            /*
            // Just as a nice-to-have I'll leave this here
            if (!permission.GroupExists(FConfigData.VerifiedPlayerGroup))
                permission.CreateGroup(FConfigData.VerifiedPlayerGroup, "", 0);
            */
        }

        // PreventMount
        object CanDismountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMount);

        object CanMountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMount);

        object CanSwapToSeat(BasePlayer aPlayer, BaseMountable aMountable) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMount);

        object OnRidableAnimalClaim(BaseRidableAnimal aAnimal, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMount);


        // PreventBedActions
        object CanAssignBed(BasePlayer aPlayer, SleepingBag aBag, ulong aTargetPlayerId) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventBedActions);

        object CanRenameBed(BasePlayer aPlayer, SleepingBag aBed, string aBedName) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventBedActions);

        object CanSetBedPublic(BasePlayer aPlayer, SleepingBag aBed) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventBedActions);


        // PreventBuild
        object CanAffordToPlace(BasePlayer aPlayer, Planner aPlanner, Construction aConstruction) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventBuild);

        object CanBuild(Planner aPlanner, Construction aPrefab, Construction.Target aTarget) =>
            CheckAndReturnNullOrFalse(aPlanner.GetOwnerPlayer(), FConfigData.PreventBuild);

        object OnConstructionPlace(BaseEntity aEntity, Construction aComponent, Construction.Target aConstructionTarget, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventBuild);


        // PreventCardSwiping
        object OnCardSwipe(CardReader cardReader, Keycard card, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCardSwiping);


        // PreventChat
        object OnUserChat(IPlayer aPlayer, string aMessage) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventChat);


        // PreventCollectiblePickup
        object OnCollectiblePickup(Item aItem, BasePlayer aPlayer, CollectibleEntity aEntity) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCollectiblePickup);


        // PreventCommand
        object OnUserCommand(IPlayer aPlayer, string command, string[] args) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCommand);


        // PreventCounterActions
        object OnCounterModeToggle(PowerCounter aCounter, BasePlayer aPlayer, bool aMode) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCounterActions);

        object OnCounterTargetChange(PowerCounter aCounter, BasePlayer aPlayer, int aTargetNumber) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCounterActions);


        // PreventCrafting
        object CanCraft(ItemCrafter aItemCrafter, ItemBlueprint aBp, int aAmount) =>
            CheckAndReturnNullOrFalse(aItemCrafter.baseEntity, FConfigData.PreventCrafting);

        object CanCraft(PlayerBlueprints aPlayerBlueprints, ItemDefinition aItemDefinition, int aSkinItemId) =>
            CheckAndReturnNullOrFalse(aPlayerBlueprints.baseEntity, FConfigData.PreventCrafting);


        // PreventCrateHack
        object CanHackCrate(BasePlayer aPlayer, HackableLockedCrate aCrate) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCrateHack);


        // PreventCupboardActions
        object OnCupboardAuthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCupboardActions);

        object OnCupboardClearList(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCupboardActions);

        object OnCupboardDeauthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCupboardActions);


        // PreventCustomUI
        object CanUseUI(BasePlayer aPlayer, string aJson) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventCustomUI);


        // PreventDemolish
        object CanDemolish(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventDemolish);

        object OnStructureDemolish(BaseCombatEntity aEntity, BasePlayer aPlayer, bool aImmediate) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventDemolish);


        // PreventDeployItem
        object CanDeployItem(BasePlayer aPlayer, Deployer aDeployer, uint aEntityId) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventDeployItem);


        // PreventDoorActions
        void OnDoorClosed(Door aDoor, BasePlayer aPlayer)
        {
            if (aPlayer == null || !FConfigData.PreventDoorActions)
                return;

            if (aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasGroup(aPlayer.UserIDString, FConfigData.VerifiedPlayerGroup))
                return;

            aDoor.SetOpen(true, false);
        }

        void OnDoorOpened(Door aDoor, BasePlayer aPlayer)
        {
            if (aPlayer == null || !FConfigData.PreventDoorActions)
                return;

            if (aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasGroup(aPlayer.UserIDString, FConfigData.VerifiedPlayerGroup))
                return;

            aDoor.SetOpen(false, false);
        }


        // PreventElevatorActions
        object OnElevatorButtonPress(ElevatorLift aLift, BasePlayer aPlayer, Elevator.Direction aDirection, bool aToTopOrBottom) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventElevatorActions);


        // PreventEntityLooting
        object CanLootEntity(BasePlayer aPlayer, BaseRidableAnimal aAnimal) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityLooting);

        object CanLootEntity(BasePlayer aPlayer, DroppedItemContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityLooting);

        object CanLootEntity(BasePlayer aPlayer, LootableCorpse aCorpse) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityLooting);

        object CanLootEntity(BasePlayer aPlayer, ResourceContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityLooting);

        object CanLootEntity(BasePlayer aPlayer, StorageContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityLooting);


        // PreventEntityPickup
        object CanPickupEntity(BasePlayer aPlayer, BaseEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventEntityPickup);


        // PreventExplosives
        void OnExplosiveDropped(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventExplosives) != null)
                aEntity.AdminKill();
        }

        void OnExplosiveThrown(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventExplosives) != null)
                aEntity.AdminKill();
        }


        // PreventFlamers
        void OnFlameThrowerBurn(FlameThrower aThrower, BaseEntity aFlame)
        {
            if (CheckAndReturnNullOrFalse(aThrower.GetOwnerPlayer(), FConfigData.PreventFlamers) != null)
                aThrower.SetFlameState(false);
        }


        // PreventFuelActions
        object CanCheckFuel(EntityFuelSystem aFuelSystem, StorageContainer aFuelContainer, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventFuelActions);


        // PreventGrowableGathering
        object OnGrowableGather(GrowableEntity plant, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventGrowableGathering);


        // PreventHealingItemUsage
        object OnHealingItemUse(MedicalTool aTool, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventHealingItemUsage);


        // PreventHelicopterActions
        object CanUseHelicopter(BasePlayer aPlayer, CH47HelicopterAIController aHelicopter) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventHelicopterActions);


        // PreventItemAction
        object OnItemAction(Item aItem, string aAction, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventItemAction);


        // PreventItemDropping
        object CanDropActiveItem(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventItemDropping);


        // PreventItemMoving
        ItemContainer.CanAcceptResult? CanAcceptItem(ItemContainer aContainer, Item aItem, int aTargetPos)
        {
            BasePlayer player = aItem.GetOwnerPlayer();

            if (player == null || !FConfigData.PreventItemMoving)
                return null;

            if (player.IsNpc || player.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(player)) ||
                permission.UserHasGroup(player.UserIDString, FConfigData.VerifiedPlayerGroup))
                return null;

            return ItemContainer.CanAcceptResult.CannotAccept;
        }

        object CanMoveItem(Item aItem, PlayerInventory aPlayerLoot, uint aTargetContainer, int aTargetSlot, int aAmount)
        {
            object result = CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer(), FConfigData.PreventItemMoving);
            return result is bool
                ? result
                : CheckAndReturnNullOrFalse(aPlayerLoot.baseEntity, FConfigData.PreventItemMoving);
        }


        // PreventItemPickup
        object OnItemPickup(Item aItem, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventItemPickup);


        // PreventItemStacking
        object CanStackItem(Item aItem, Item aTargetItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer(), FConfigData.PreventItemStacking);


        // PreventItemWearing
        object CanWearItem(PlayerInventory aInventory, Item aItem, int aTargetSlot) =>
            CheckAndReturnNullOrFalse(aInventory.baseEntity, FConfigData.PreventItemWearing);


        // PreventLiftActions
        object OnLiftUse(Lift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLiftActions);

        object OnLiftUse(ProceduralLift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLiftActions);


        // PreventLockActions
        object CanChangeCode(BasePlayer aPlayer, CodeLock aCodeLock, string aNewCode, bool aIsGuestCode) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object CanLock(BasePlayer aPlayer, BaseLock aLock) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object CanPickupLock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object CanUnlock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object CanUseLockedEntity(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object OnCodeEntered(CodeLock aCodeLock, BasePlayer aPlayer, string aCode) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventLockActions);

        object OnItemLock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer(), FConfigData.PreventLockActions);

        object OnItemUnlock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer(), FConfigData.PreventLockActions);


        // PreventMailboxActions
        object CanUseMailbox(BasePlayer aPlayer, Mailbox aMailbox) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMailboxActions);


        // PreventMelee
        object OnMeleeAttack(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMelee);

        void OnMeleeThrown(BasePlayer aPlayer, Item aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventMelee) != null)
                aItem.Remove();
        }


        // PreventOvenActions
        object OnOvenToggle(BaseOven aOven, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventOvenActions);


        // PreventPhoneActions
        object OnPhoneDial(PhoneController aCallerPhone, PhoneController aReceiverPhone, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPhoneActions);

        object OnPhoneCallStart(PhoneController aPhone, PhoneController aOtherPhone, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPhoneActions);

        object OnPhoneNameUpdate(PhoneController aPhoneController, string aName, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPhoneActions);


        // PreventPlayerAssist
        object OnPlayerAssist(BasePlayer aTarget, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPlayerAssist);

        object OnPlayerRevive(BasePlayer aReviver, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPlayerAssist);


        // PreventPlayerLooting
        object CanLootPlayer(BasePlayer aTarget, BasePlayer aLooter) => CheckAndReturnNullOrFalse(aLooter, FConfigData.PreventPlayerLooting);


        // PreventPush
        object CanPushBoat(BasePlayer aPlayer, MotorRowboat aBoat) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPush);

        object OnVehiclePush(BaseVehicle aVehicle, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventPush);


        // PreventRecyclerActions
        object OnRecyclerToggle(Recycler aRecycler, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventRecyclerActions);


        // PreventReloading
        object OnReloadMagazine(BasePlayer aPlayer, BaseProjectile aProjectile, int aDesiredAmount) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventReloading);

        object OnReloadWeapon(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventReloading);

        object OnSwitchAmmo(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventReloading);


        // PreventRepair
        object OnHammerHit(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventRepair);

        object OnStructureRepair(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventRepair);


        // PreventResearch
        object CanResearchItem(BasePlayer aPlayer, Item aTargetItem) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventResearch);

        object CanUnlockTechTreeNode(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventResearch);

        object CanUnlockTechTreeNodePath(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventResearch);


        // PreventRockets
        void OnRocketLaunched(BasePlayer aPlayer, BaseEntity aEntity)
        {
            if (CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventRockets) != null)
                aEntity.AdminKill();
        }


        // PreventShopActions
        object OnShopCompleteTrade(ShopFront aShop, BasePlayer aCustomer) => CheckAndReturnNullOrFalse(aCustomer, FConfigData.PreventShopActions);


        // PreventSignUpdate
        object CanUpdateSign(BasePlayer aPlayer, PhotoFrame aPhotoFrame) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventSignUpdate);

        object CanUpdateSign(BasePlayer aPlayer, Signage aSign) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventSignUpdate);


        // PreventStashActions
        object CanHideStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventStashActions);

        object CanSeeStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventStashActions);


        // PreventStructureRotate
        object OnStructureRotate(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventStructureRotate);


        // PreventSwitchActions
        object OnSwitchToggle(IOEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventSwitchActions);


        // PreventTeamCreation
        object OnTeamCreate(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTeamCreation);


        // PreventTrapActions
        object OnTrapArm(BearTrap aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTrapActions);

        object OnTrapDisarm(Landmine aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTrapActions);


        // PreventTurretActions
        object OnTurretAuthorize(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTurretActions);

        object OnTurretClearList(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTurretActions);

        object OnTurretRotate(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventTurretActions);


        // PreventUpgrade
        object CanAffordUpgrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventUpgrade);

        object CanChangeGrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventUpgrade);

        object OnStructureUpgrade(BaseCombatEntity aEntity, BasePlayer aPlayer, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventUpgrade);


        // PreventVendingAdmin
        object CanAdministerVending(BasePlayer aPlayer, VendingMachine aMachine) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventVendingAdmin);

        object OnRotateVendingMachine(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventVendingAdmin);


        // PreventVendingUsage
        object CanUseVending(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventVendingUsage);

        object OnBuyVendingItem(VendingMachine aMachine, BasePlayer aPlayer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventVendingUsage);

        object OnVendingTransaction(VendingMachine aMachine, BasePlayer aBuyer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aBuyer, FConfigData.PreventVendingUsage);


        // PreventWeaponFiring
        void OnWeaponFired(BaseProjectile aProjectile, BasePlayer aPlayer, ItemModProjectile aMod, ProtoBuf.ProjectileShoot aProjectiles)
        {
            if (CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventWeaponFiring) != null)
                aProjectiles.projectiles.Clear();
        }


        // PreventWiring
        object CanUseWires(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventWiring);


        // PreventWoodCutting
        object CanTakeCutting(BasePlayer aPlayer, GrowableEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer, FConfigData.PreventWoodCutting);


        // PreventWorldProjectiles
        object CanCreateWorldProjectile(HitInfo aInfo, ItemDefinition aItemDef) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer, FConfigData.PreventWorldProjectiles);

        object OnCreateWorldProjectile(HitInfo aInfo, Item aItem) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer, FConfigData.PreventWorldProjectiles);


        // PreventWounded
        object CanBeWounded(BasePlayer aPlayer, HitInfo aInfo) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer, FConfigData.PreventWounded);
        #endregion Hooks
    }
}
