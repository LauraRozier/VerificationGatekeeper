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
    [Info("Verification Gatekeeper", "ThibmoRozier", "1.0.3")]
    [Description("Prevents players from doing anything on the server until they are given the bypass permission.")]
    public class VerificationGatekeeper : RustPlugin
    {
        #region Constants
        private const string CPermBypass = "verificationgatekeeper.bypass";
        #endregion Constants

        #region Variables
        private ConfigData FConfigData;
        #endregion Variables

        #region Config
        /// <summary>
        /// The config type class
        /// </summary>
        private class ConfigData
        {
            [JsonProperty("Admin Is Always Verified", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool AdminAlwaysVerified = true;
            [JsonProperty("Prevent (Dis-)Mount", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMount = true;
            [JsonProperty("Prevent Bed Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventBedActions = true;
            [JsonProperty("Prevent Build", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventBuild = true;
            [JsonProperty("Prevent Card Swiping", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCardSwiping = true;
            [JsonProperty("Prevent Chat", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventChat = true;
            [JsonProperty("Prevent Collectible Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCollectiblePickup = true;
            [JsonProperty("Prevent Command", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCommand = true;
            [JsonProperty("Prevent Counter Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCounterActions = true;
            [JsonProperty("Prevent Crafting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCrafting = true;
            [JsonProperty("Prevent Crate Hack", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCrateHack = true;
            [JsonProperty("Prevent Cupboard Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventCupboardActions = true;
            [JsonProperty("Prevent Custom UI", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(false)]
            public bool PreventCustomUI = false;
            [JsonProperty("Prevent Demolish", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDemolish = true;
            [JsonProperty("Prevent Deploy Item", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDeployItem = true;
            [JsonProperty("Prevent Door Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventDoorActions = true;
            [JsonProperty("Prevent Elevator Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventElevatorActions = true;
            [JsonProperty("Prevent Entity Looting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventEntityLooting = true;
            [JsonProperty("Prevent Entity Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventEntityPickup = true;
            [JsonProperty("Prevent Explosives", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventExplosives = true;
            [JsonProperty("Prevent Flamers", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventFlamers = true;
            [JsonProperty("Prevent Fuel Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventFuelActions = true;
            [JsonProperty("Prevent Growable Gathering", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventGrowableGathering = true;
            [JsonProperty("Prevent Healing Item Usage", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventHealingItemUsage = true;
            [JsonProperty("Prevent Helicopter Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventHelicopterActions = true;
            [JsonProperty("Prevent Item Action", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemAction = true;
            [JsonProperty("Prevent Item Dropping", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemDropping = true;
            [JsonProperty("Prevent Item Moving", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemMoving = true;
            [JsonProperty("Prevent Item Pickup", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemPickup = true;
            [JsonProperty("Prevent Item Stacking", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemStacking = true;
            [JsonProperty("Prevent Item Wearing", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventItemWearing = true;
            [JsonProperty("Prevent Lift Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventLiftActions = true;
            [JsonProperty("Prevent Lock Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventLockActions = true;
            [JsonProperty("Prevent Mailbox Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMailboxActions = true;
            [JsonProperty("Prevent Melee", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventMelee = true;
            [JsonProperty("Prevent Oven & Furnace Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventOvenActions = true;
            [JsonProperty("Prevent Phone Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPhoneActions = true;
            [JsonProperty("Prevent Player Assist", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPlayerAssist = true;
            [JsonProperty("Prevent Player Looting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPlayerLooting = true;
            [JsonProperty("Prevent Push", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventPush = true;
            [JsonProperty("Prevent Recycler Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRecyclerActions = true;
            [JsonProperty("Prevent Reloading", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventReloading = true;
            [JsonProperty("Prevent Repair", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRepair = true;
            [JsonProperty("Prevent Research", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventResearch = true;
            [JsonProperty("Prevent Rockets", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventRockets = true;
            [JsonProperty("Prevent Shop Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventShopActions = true;
            [JsonProperty("Prevent Sign Update", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventSignUpdate = true;
            [JsonProperty("Prevent Stash Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventStashActions = true;
            [JsonProperty("Prevent Structure Rotate", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventStructureRotate = true;
            [JsonProperty("Prevent Switch Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventSwitchActions = true;
            [JsonProperty("Prevent Team Creation", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTeamCreation = true;
            [JsonProperty("Prevent Trap Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTrapActions = true;
            [JsonProperty("Prevent Turret Actions", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventTurretActions = true;
            [JsonProperty("Prevent Upgrade", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventUpgrade = true;
            [JsonProperty("Prevent Vending Admin", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventVendingAdmin = true;
            [JsonProperty("Prevent Vending Usage", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventVendingUsage = true;
            [JsonProperty("Prevent Weapon Firing", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWeaponFiring = true;
            [JsonProperty("Prevent Wiring", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWiring = true;
            [JsonProperty("Prevent Wood Cutting", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWoodCutting = true;
            [JsonProperty("Prevent World Projectiles", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWorldProjectiles = true;
            [JsonProperty("Prevent Wounded", DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(true)]
            public bool PreventWounded = true;
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            FConfigData = Config.ReadObject<ConfigData>();
            SaveConfig();
        }

        protected override void LoadDefaultConfig()
        {
            FConfigData = new ConfigData();
        }

        protected override void SaveConfig() => Config.WriteObject(FConfigData);
        #endregion Config

        #region Script Methods
        private bool? CheckAndReturnNullOrFalse(BasePlayer aPlayer)
        {
            if (aPlayer == null || aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasPermission(aPlayer.UserIDString, CPermBypass))
                return null;

            return false;
        }

        private bool? CheckAndReturnNullOrFalse(IPlayer aPlayer)
        {
            if (aPlayer == null || aPlayer.IsServer || (FConfigData.AdminAlwaysVerified && aPlayer.IsAdmin) ||
                permission.UserHasPermission(aPlayer.Id, CPermBypass))
                return null;

            return false;
        }
        #endregion Script Methods

        #region Hooks
        void OnServerInitialized()
        {
            permission.RegisterPermission(CPermBypass, this);

            /*
            // Just as a nice-to-have I'll leave this here
            if (!permission.GroupExists(FConfigData.VerifiedPlayerGroup))
                permission.CreateGroup(FConfigData.VerifiedPlayerGroup, "", 0);
            */

            if (!FConfigData.PreventMount) {
                Unsubscribe(nameof(CanDismountEntity));
                Unsubscribe(nameof(CanMountEntity));
                Unsubscribe(nameof(CanSwapToSeat));
                Unsubscribe(nameof(OnRidableAnimalClaim));
            }

            if (!FConfigData.PreventBedActions) {
                Unsubscribe(nameof(CanAssignBed));
                Unsubscribe(nameof(CanRenameBed));
                Unsubscribe(nameof(CanSetBedPublic));
            }

            if (!FConfigData.PreventBuild) {
                Unsubscribe(nameof(CanAffordToPlace));
                Unsubscribe(nameof(CanBuild));
                Unsubscribe(nameof(OnConstructionPlace));
            }

            if (!FConfigData.PreventCardSwiping)
                Unsubscribe(nameof(OnCardSwipe));

            if (!FConfigData.PreventChat)
                Unsubscribe(nameof(OnUserChat));

            if (!FConfigData.PreventCollectiblePickup)
                Unsubscribe(nameof(OnCollectiblePickup));

            if (!FConfigData.PreventCommand)
                Unsubscribe(nameof(OnUserCommand));

            if (!FConfigData.PreventCounterActions) {
                Unsubscribe(nameof(OnCounterModeToggle));
                Unsubscribe(nameof(OnCounterTargetChange));
            }

            if (!FConfigData.PreventCrafting)
                Unsubscribe(nameof(CanCraft));

            if (!FConfigData.PreventCrateHack)
                Unsubscribe(nameof(CanHackCrate));

            if (!FConfigData.PreventCupboardActions) {
                Unsubscribe(nameof(OnCupboardAuthorize));
                Unsubscribe(nameof(OnCupboardClearList));
                Unsubscribe(nameof(OnCupboardDeauthorize));
            }

            if (!FConfigData.PreventCustomUI)
                Unsubscribe(nameof(CanUseUI));

            if (!FConfigData.PreventDemolish) {
                Unsubscribe(nameof(CanDemolish));
                Unsubscribe(nameof(OnStructureDemolish));
            }

            if (!FConfigData.PreventDeployItem)
                Unsubscribe(nameof(CanDeployItem));

            if (!FConfigData.PreventDoorActions) {
                Unsubscribe(nameof(OnDoorClosed));
                Unsubscribe(nameof(OnDoorOpened));
            }

            if (!FConfigData.PreventElevatorActions)
                Unsubscribe(nameof(OnElevatorButtonPress));

            if (!FConfigData.PreventEntityLooting)
                Unsubscribe(nameof(CanLootEntity));

            if (!FConfigData.PreventEntityPickup)
                Unsubscribe(nameof(CanPickupEntity));

            if (!FConfigData.PreventExplosives) {
                Unsubscribe(nameof(OnExplosiveDropped));
                Unsubscribe(nameof(OnExplosiveThrown));
            }

            if (!FConfigData.PreventFlamers)
                Unsubscribe(nameof(OnFlameThrowerBurn));

            if (!FConfigData.PreventFuelActions)
                Unsubscribe(nameof(CanCheckFuel));

            if (!FConfigData.PreventGrowableGathering)
                Unsubscribe(nameof(OnGrowableGather));

            if (!FConfigData.PreventHealingItemUsage)
                Unsubscribe(nameof(OnHealingItemUse));

            if (!FConfigData.PreventHelicopterActions)
                Unsubscribe(nameof(CanUseHelicopter));

            if (!FConfigData.PreventItemAction)
                Unsubscribe(nameof(OnItemAction));

            if (!FConfigData.PreventItemDropping)
                Unsubscribe(nameof(CanDropActiveItem));

            if (!FConfigData.PreventItemMoving) {
                Unsubscribe(nameof(CanAcceptItem));
                Unsubscribe(nameof(CanMoveItem));
            }

            if (!FConfigData.PreventItemPickup)
                Unsubscribe(nameof(OnItemPickup));

            if (!FConfigData.PreventItemStacking)
                Unsubscribe(nameof(CanStackItem));

            if (!FConfigData.PreventItemWearing)
                Unsubscribe(nameof(CanWearItem));

            if (!FConfigData.PreventLiftActions)
                Unsubscribe(nameof(OnLiftUse));

            if (!FConfigData.PreventLockActions) {
                Unsubscribe(nameof(CanChangeCode));
                Unsubscribe(nameof(CanLock));
                Unsubscribe(nameof(CanPickupLock));
                Unsubscribe(nameof(CanUnlock));
                Unsubscribe(nameof(CanUseLockedEntity));
                Unsubscribe(nameof(OnCodeEntered));
                Unsubscribe(nameof(OnItemLock));
                Unsubscribe(nameof(OnItemUnlock));
            }

            if (!FConfigData.PreventMailboxActions)
                Unsubscribe(nameof(CanUseMailbox));

            if (!FConfigData.PreventMelee) {
                Unsubscribe(nameof(OnMeleeAttack));
                Unsubscribe(nameof(OnMeleeThrown));
            }

            if (!FConfigData.PreventOvenActions)
                Unsubscribe(nameof(OnOvenToggle));

            if (!FConfigData.PreventPhoneActions) {
                Unsubscribe(nameof(OnPhoneDial));
                Unsubscribe(nameof(OnPhoneCallStart));
                Unsubscribe(nameof(OnPhoneNameUpdate));
            }

            if (!FConfigData.PreventPlayerAssist) {
                Unsubscribe(nameof(OnPlayerAssist));
                Unsubscribe(nameof(OnPlayerRevive));
            }

            if (!FConfigData.PreventPlayerLooting)
                Unsubscribe(nameof(CanLootPlayer));

            if (!FConfigData.PreventPush) {
                Unsubscribe(nameof(CanPushBoat));
                Unsubscribe(nameof(OnVehiclePush));
            }

            if (!FConfigData.PreventRecyclerActions)
                Unsubscribe(nameof(OnRecyclerToggle));

            if (!FConfigData.PreventReloading) {
                Unsubscribe(nameof(OnReloadMagazine));
                Unsubscribe(nameof(OnReloadWeapon));
                Unsubscribe(nameof(OnSwitchAmmo));
            }

            if (!FConfigData.PreventRepair) {
                Unsubscribe(nameof(OnHammerHit));
                Unsubscribe(nameof(OnStructureRepair));
            }

            if (!FConfigData.PreventResearch) {
                Unsubscribe(nameof(CanResearchItem));
                Unsubscribe(nameof(CanUnlockTechTreeNode));
                Unsubscribe(nameof(CanUnlockTechTreeNodePath));
            }

            if (!FConfigData.PreventRockets)
                Unsubscribe(nameof(OnRocketLaunched));

            if (!FConfigData.PreventShopActions)
                Unsubscribe(nameof(OnShopCompleteTrade));

            if (!FConfigData.PreventSignUpdate)
                Unsubscribe(nameof(CanUpdateSign));

            if (!FConfigData.PreventStashActions) {
                Unsubscribe(nameof(CanHideStash));
                Unsubscribe(nameof(CanSeeStash));
            }

            if (!FConfigData.PreventStructureRotate)
                Unsubscribe(nameof(OnStructureRotate));

            if (!FConfigData.PreventSwitchActions)
                Unsubscribe(nameof(OnSwitchToggle));

            if (!FConfigData.PreventTeamCreation)
                Unsubscribe(nameof(OnTeamCreate));

            if (!FConfigData.PreventTrapActions) {
                Unsubscribe(nameof(OnTrapArm));
                Unsubscribe(nameof(OnTrapDisarm));
            }

            if (!FConfigData.PreventTurretActions) {
                Unsubscribe(nameof(OnTurretAuthorize));
                Unsubscribe(nameof(OnTurretClearList));
                Unsubscribe(nameof(OnTurretRotate));
            }

            if (!FConfigData.PreventUpgrade) {
                Unsubscribe(nameof(CanAffordUpgrade));
                Unsubscribe(nameof(CanChangeGrade));
                Unsubscribe(nameof(OnStructureUpgrade));
            }

            if (!FConfigData.PreventVendingAdmin) {
                Unsubscribe(nameof(CanAdministerVending));
                Unsubscribe(nameof(OnRotateVendingMachine));
            }

            if (!FConfigData.PreventVendingUsage) {
                Unsubscribe(nameof(CanUseVending));
                Unsubscribe(nameof(OnBuyVendingItem));
                Unsubscribe(nameof(OnVendingTransaction));
            }

            if (!FConfigData.PreventWeaponFiring)
                Unsubscribe(nameof(OnWeaponFired));

            if (!FConfigData.PreventWiring)
                Unsubscribe(nameof(CanUseWires));

            if (!FConfigData.PreventWoodCutting)
                Unsubscribe(nameof(CanTakeCutting));

            if (!FConfigData.PreventWorldProjectiles) {
                Unsubscribe(nameof(CanCreateWorldProjectile));
                Unsubscribe(nameof(OnCreateWorldProjectile));
            }

            if (!FConfigData.PreventWounded)
                Unsubscribe(nameof(CanBeWounded));
        }

        // PreventMount
        bool? CanDismountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanMountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanSwapToSeat(BasePlayer aPlayer, BaseMountable aMountable) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnRidableAnimalClaim(BaseRidableAnimal aAnimal, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventBedActions
        bool? CanAssignBed(BasePlayer aPlayer, SleepingBag aBag, ulong aTargetPlayerId) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanRenameBed(BasePlayer aPlayer, SleepingBag aBed, string aBedName) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanSetBedPublic(BasePlayer aPlayer, SleepingBag aBed) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventBuild
        bool? CanAffordToPlace(BasePlayer aPlayer, Planner aPlanner, Construction aConstruction) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? CanBuild(Planner aPlanner, Construction aPrefab, Construction.Target aTarget) =>
            CheckAndReturnNullOrFalse(aPlanner.GetOwnerPlayer());

        bool? OnConstructionPlace(BaseEntity aEntity, Construction aComponent, Construction.Target aConstructionTarget, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventCardSwiping
        bool? OnCardSwipe(CardReader cardReader, Keycard card, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventChat
        bool? OnUserChat(IPlayer aPlayer, string aMessage) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventCollectiblePickup
        bool? OnCollectiblePickup(Item aItem, BasePlayer aPlayer, CollectibleEntity aEntity) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventCommand
        bool? OnUserCommand(IPlayer aPlayer, string command, string[] args) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventCounterActions
        bool? OnCounterModeToggle(PowerCounter aCounter, BasePlayer aPlayer, bool aMode) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnCounterTargetChange(PowerCounter aCounter, BasePlayer aPlayer, int aTargetNumber) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventCrafting
        bool? CanCraft(ItemCrafter aItemCrafter, ItemBlueprint aBp, int aAmount) =>
            CheckAndReturnNullOrFalse(aItemCrafter.baseEntity);

        bool? CanCraft(PlayerBlueprints aPlayerBlueprints, ItemDefinition aItemDefinition, int aSkinItemId) =>
            CheckAndReturnNullOrFalse(aPlayerBlueprints.baseEntity);


        // PreventCrateHack
        bool? CanHackCrate(BasePlayer aPlayer, HackableLockedCrate aCrate) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventCupboardActions
        bool? OnCupboardAuthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnCupboardClearList(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnCupboardDeauthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventCustomUI
        bool? CanUseUI(BasePlayer aPlayer, string aJson) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventDemolish
        bool? CanDemolish(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnStructureDemolish(BaseCombatEntity aEntity, BasePlayer aPlayer, bool aImmediate) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventDeployItem
        bool? CanDeployItem(BasePlayer aPlayer, Deployer aDeployer, uint aEntityId) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventDoorActions
        void OnDoorClosed(Door aDoor, BasePlayer aPlayer)
        {
            if (aPlayer == null || aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasPermission(aPlayer.UserIDString, CPermBypass))
                return;

            aDoor.SetOpen(true, false);
        }

        void OnDoorOpened(Door aDoor, BasePlayer aPlayer)
        {
            if (aPlayer == null || aPlayer.IsNpc || aPlayer.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(aPlayer)) ||
                permission.UserHasPermission(aPlayer.UserIDString, CPermBypass))
                return;

            aDoor.SetOpen(false, false);
        }


        // PreventElevatorActions
        bool? OnElevatorButtonPress(ElevatorLift aLift, BasePlayer aPlayer, Elevator.Direction aDirection, bool aToTopOrBottom) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventEntityLooting
        bool? CanLootEntity(BasePlayer aPlayer, BaseRidableAnimal aAnimal) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanLootEntity(BasePlayer aPlayer, DroppedItemContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanLootEntity(BasePlayer aPlayer, LootableCorpse aCorpse) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanLootEntity(BasePlayer aPlayer, ResourceContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanLootEntity(BasePlayer aPlayer, StorageContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventEntityPickup
        bool? CanPickupEntity(BasePlayer aPlayer, BaseEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventExplosives
        void OnExplosiveDropped(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer) != null)
                aEntity.AdminKill();
        }

        void OnExplosiveThrown(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer) != null)
                aEntity.AdminKill();
        }


        // PreventFlamers
        void OnFlameThrowerBurn(FlameThrower aThrower, BaseEntity aFlame)
        {
            if (CheckAndReturnNullOrFalse(aThrower.GetOwnerPlayer()) != null)
                aThrower.SetFlameState(false);
        }


        // PreventFuelActions
        bool? CanCheckFuel(EntityFuelSystem aFuelSystem, StorageContainer aFuelContainer, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventGrowableGathering
        bool? OnGrowableGather(GrowableEntity plant, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventHealingItemUsage
        bool? OnHealingItemUse(MedicalTool aTool, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventHelicopterActions
        bool? CanUseHelicopter(BasePlayer aPlayer, CH47HelicopterAIController aHelicopter) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventItemAction
        bool? OnItemAction(Item aItem, string aAction, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventItemDropping
        bool? CanDropActiveItem(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventItemMoving
        ItemContainer.CanAcceptResult? CanAcceptItem(ItemContainer aContainer, Item aItem, int aTargetPos)
        {
            BasePlayer player = aItem.GetOwnerPlayer();

            if (player == null || player.IsNpc || player.IPlayer.IsServer || (FConfigData.AdminAlwaysVerified && Player.IsAdmin(player)) ||
                permission.UserHasPermission(player.UserIDString, CPermBypass))
                return null;

            return ItemContainer.CanAcceptResult.CannotAccept;
        }

        bool? CanMoveItem(Item aItem, PlayerInventory aPlayerLoot, uint aTargetContainer, int aTargetSlot, int aAmount)
        {
            bool? result = CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer());
            return result is bool
                ? result
                : CheckAndReturnNullOrFalse(aPlayerLoot.baseEntity);
        }


        // PreventItemPickup
        bool? OnItemPickup(Item aItem, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventItemStacking
        bool? CanStackItem(Item aItem, Item aTargetItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer());


        // PreventItemWearing
        bool? CanWearItem(PlayerInventory aInventory, Item aItem, int aTargetSlot) =>
            CheckAndReturnNullOrFalse(aInventory.baseEntity);


        // PreventLiftActions
        bool? OnLiftUse(Lift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnLiftUse(ProceduralLift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventLockActions
        bool? CanChangeCode(BasePlayer aPlayer, CodeLock aCodeLock, string aNewCode, bool aIsGuestCode) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? CanLock(BasePlayer aPlayer, BaseLock aLock) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanPickupLock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanUnlock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanUseLockedEntity(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnCodeEntered(CodeLock aCodeLock, BasePlayer aPlayer, string aCode) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnItemLock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer());

        bool? OnItemUnlock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer());


        // PreventMailboxActions
        bool? CanUseMailbox(BasePlayer aPlayer, Mailbox aMailbox) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventMelee
        bool? OnMeleeAttack(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer);

        void OnMeleeThrown(BasePlayer aPlayer, Item aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer) != null)
                aItem.Remove();
        }


        // PreventOvenActions
        bool? OnOvenToggle(BaseOven aOven, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventPhoneActions
        bool? OnPhoneDial(PhoneController aCallerPhone, PhoneController aReceiverPhone, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnPhoneCallStart(PhoneController aPhone, PhoneController aOtherPhone, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnPhoneNameUpdate(PhoneController aPhoneController, string aName, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventPlayerAssist
        bool? OnPlayerAssist(BasePlayer aTarget, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnPlayerRevive(BasePlayer aReviver, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventPlayerLooting
        bool? CanLootPlayer(BasePlayer aTarget, BasePlayer aLooter) => CheckAndReturnNullOrFalse(aLooter);


        // PreventPush
        bool? CanPushBoat(BasePlayer aPlayer, MotorRowboat aBoat) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnVehiclePush(BaseVehicle aVehicle, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventRecyclerActions
        bool? OnRecyclerToggle(Recycler aRecycler, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventReloading
        bool? OnReloadMagazine(BasePlayer aPlayer, BaseProjectile aProjectile, int aDesiredAmount) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnReloadWeapon(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnSwitchAmmo(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventRepair
        bool? OnHammerHit(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnStructureRepair(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventResearch
        bool? CanResearchItem(BasePlayer aPlayer, Item aTargetItem) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanUnlockTechTreeNode(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? CanUnlockTechTreeNodePath(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventRockets
        void OnRocketLaunched(BasePlayer aPlayer, BaseEntity aEntity)
        {
            if (CheckAndReturnNullOrFalse(aPlayer) != null)
                aEntity.AdminKill();
        }


        // PreventShopActions
        bool? OnShopCompleteTrade(ShopFront aShop, BasePlayer aCustomer) => CheckAndReturnNullOrFalse(aCustomer);


        // PreventSignUpdate
        bool? CanUpdateSign(BasePlayer aPlayer, PhotoFrame aPhotoFrame) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanUpdateSign(BasePlayer aPlayer, Signage aSign) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventStashActions
        bool? CanHideStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer);

        bool? CanSeeStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventStructureRotate
        bool? OnStructureRotate(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventSwitchActions
        bool? OnSwitchToggle(IOEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventTeamCreation
        bool? OnTeamCreate(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventTrapActions
        bool? OnTrapArm(BearTrap aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnTrapDisarm(Landmine aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventTurretActions
        bool? OnTurretAuthorize(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnTurretClearList(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnTurretRotate(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventUpgrade
        bool? CanAffordUpgrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? CanChangeGrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnStructureUpgrade(BaseCombatEntity aEntity, BasePlayer aPlayer, BuildingGrade.Enum aGrade) =>
            CheckAndReturnNullOrFalse(aPlayer);


        // PreventVendingAdmin
        bool? CanAdministerVending(BasePlayer aPlayer, VendingMachine aMachine) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnRotateVendingMachine(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventVendingUsage
        bool? CanUseVending(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);

        bool? OnBuyVendingItem(VendingMachine aMachine, BasePlayer aPlayer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aPlayer);

        bool? OnVendingTransaction(VendingMachine aMachine, BasePlayer aBuyer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aBuyer);


        // PreventWeaponFiring
        void OnWeaponFired(BaseProjectile aProjectile, BasePlayer aPlayer, ItemModProjectile aMod, ProtoBuf.ProjectileShoot aProjectiles)
        {
            if (CheckAndReturnNullOrFalse(aPlayer) != null)
                aProjectiles.projectiles.Clear();
        }


        // PreventWiring
        bool? CanUseWires(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventWoodCutting
        bool? CanTakeCutting(BasePlayer aPlayer, GrowableEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer);


        // PreventWorldProjectiles
        bool? CanCreateWorldProjectile(HitInfo aInfo, ItemDefinition aItemDef) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer);

        bool? OnCreateWorldProjectile(HitInfo aInfo, Item aItem) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer);


        // PreventWounded
        bool? CanBeWounded(BasePlayer aPlayer, HitInfo aInfo) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer);
        #endregion Hooks
    }
}
