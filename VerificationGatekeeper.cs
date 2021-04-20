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
    [Info("Verification Gatekeeper", "ThibmoRozier", "1.0.0")]
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
            [DefaultValue("verified")]
            [JsonProperty("Verified Player Group")]
            public string VerifiedPlayerGroup { get; set; }
            [DefaultValue(true)]
            [JsonProperty("Admin Is Always Verified")]
            public bool AdminAlwaysVerified { get; set; }
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
                AdminAlwaysVerified = true
            };
        }

        protected override void SaveConfig() => Config.WriteObject(FConfigData);
        #endregion Config

        #region Script Methods
        private object CheckAndReturnNullOrFalse(IPlayer aPlayer, bool aIndIsNPC = false)
        {
            if (aPlayer == null || aIndIsNPC || aPlayer.IsServer /*|| aPlayer.IsAdmin*/ ||
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

        object CanAffordUpgrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanAssignBed(BasePlayer aPlayer, SleepingBag aBag, ulong aTargetPlayerId) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnUserChat(IPlayer aPlayer, string aMessage) => CheckAndReturnNullOrFalse(aPlayer);

        object OnUserCommand(IPlayer aPlayer, string command, string[] args) => CheckAndReturnNullOrFalse(aPlayer);

        object OnPlayerRevive(BasePlayer aReviver, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanLock(BasePlayer aPlayer, BaseLock aLock) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnMeleeAttack(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanPushBoat(BasePlayer aPlayer, MotorRowboat aBoat) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanDeployItem(BasePlayer aPlayer, Deployer aDeployer, uint aEntityId) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnPlayerAssist(BasePlayer aTarget, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanAffordToPlace(BasePlayer aPlayer, Planner aPlanner, Construction aConstruction) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUpdateSign(BasePlayer aPlayer, Signage aSign) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUpdateSign(BasePlayer aPlayer, PhotoFrame aPhotoFrame) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUseWires(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanBeWounded(BasePlayer aPlayer, HitInfo aInfo) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer?.IPlayer, aInfo.InitiatorPlayer?.IsNpc ?? false);

        object CanBuild(Planner aPlanner, Construction aPrefab, Construction.Target aTarget) =>
            CheckAndReturnNullOrFalse(aPlanner.GetOwnerPlayer().IPlayer);

        object CanChangeCode(BasePlayer aPlayer, CodeLock aCodeLock, string aNewCode, bool aIsGuestCode) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanChangeGrade(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanCraft(ItemCrafter aItemCrafter, ItemBlueprint aBp, int aAmount) => CheckAndReturnNullOrFalse(aItemCrafter.baseEntity.IPlayer);

        object CanCraft(PlayerBlueprints aPlayerBlueprints, ItemDefinition aItemDefinition, int aSkinItemId) =>
            CheckAndReturnNullOrFalse(aPlayerBlueprints.baseEntity.IPlayer);

        object CanDemolish(BasePlayer aPlayer, BuildingBlock aBlock, BuildingGrade.Enum aGrade) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanHackCrate(BasePlayer aPlayer, HackableLockedCrate aCrate) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanMountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanDismountEntity(BasePlayer aPlayer, BaseMountable aEntity) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanHideStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanLootPlayer(BasePlayer aTarget, BasePlayer aLooter) => CheckAndReturnNullOrFalse(aLooter.IPlayer);

        object CanLootEntity(BasePlayer aPlayer, DroppedItemContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanLootEntity(BasePlayer aPlayer, LootableCorpse aCorpse) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanLootEntity(BasePlayer aPlayer, ResourceContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanLootEntity(BasePlayer aPlayer, BaseRidableAnimal aAnimal) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanLootEntity(BasePlayer aPlayer, StorageContainer aContainer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanPickupEntity(BasePlayer aPlayer, BaseEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanPickupLock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanRenameBed(BasePlayer aPlayer, SleepingBag aBed, string aBedName) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanResearchItem(BasePlayer aPlayer, Item aTargetItem) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUseLockedEntity(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanSeeStash(BasePlayer aPlayer, StashContainer aStash) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanSetBedPublic(BasePlayer aPlayer, SleepingBag aBed) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUnlock(BasePlayer aPlayer, BaseLock aBaseLock) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanUseMailbox(BasePlayer aPlayer, Mailbox aMailbox) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanUseUI(BasePlayer aPlayer, string aJson) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanWearItem(PlayerInventory aInventory, Item aItem, int aTargetSlot) =>
            CheckAndReturnNullOrFalse(aInventory.baseEntity.IPlayer, aInventory.baseEntity?.IsNpc ?? false);

        object CanDropActiveItem(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        ItemContainer.CanAcceptResult? CanAcceptItem(ItemContainer aContainer, Item aItem, int aTargetPos)
        {
            BasePlayer bPlayer = aItem.GetOwnerPlayer();
            IPlayer player = bPlayer?.IPlayer;

            if (player == null || bPlayer.IsNpc || player.IsServer || (FConfigData.AdminAlwaysVerified && player.IsAdmin) ||
                permission.UserHasGroup(player.Id, FConfigData.VerifiedPlayerGroup))
                return null;

            return ItemContainer.CanAcceptResult.CannotAccept;
        }

        object OnCardSwipe(CardReader cardReader, Keycard card, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanMoveItem(Item aItem, PlayerInventory aPlayerLoot, uint aTargetContainer, int aTargetSlot, int aAmount)
        {
            object result = CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer()?.IPlayer);
            return result is bool
                ? result
                : CheckAndReturnNullOrFalse(aPlayerLoot.baseEntity?.IPlayer);
        }

        object CanStackItem(Item aItem, Item aTargetItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer()?.IPlayer);

        object OnHealingItemUse(MedicalTool aTool, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnItemAction(Item aItem, string aAction, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnItemPickup(Item aItem, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnTrapArm(BearTrap aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnTrapDisarm(Landmine aTrap, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnItemLock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer()?.IPlayer);

        object OnItemUnlock(Item aItem) => CheckAndReturnNullOrFalse(aItem.GetOwnerPlayer()?.IPlayer);

        object CanCreateWorldProjectile(HitInfo aInfo, ItemDefinition aItemDef) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer?.IPlayer, aInfo.InitiatorPlayer?.IsNpc ?? false);

        object OnCreateWorldProjectile(HitInfo aInfo, Item aItem) =>
            CheckAndReturnNullOrFalse(aInfo.InitiatorPlayer?.IPlayer, aInfo.InitiatorPlayer?.IsNpc ?? false);

        void OnExplosiveDropped(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc) != null)
                aEntity.AdminKill();
        }

        void OnExplosiveThrown(BasePlayer aPlayer, BaseEntity aEntity, ThrownWeapon aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc) != null)
                aEntity.AdminKill();
        }

        void OnFlameThrowerBurn(FlameThrower aThrower, BaseEntity aFlame)
        {
            BasePlayer player = aThrower.GetOwnerPlayer();

            if (CheckAndReturnNullOrFalse(player?.IPlayer, player?.IsNpc ?? false) != null)
                aThrower.SetFlameState(false);
        }

        void OnMeleeThrown(BasePlayer aPlayer, Item aItem)
        {
            if (CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc) != null)
                aItem.Remove();
        }

        object OnReloadMagazine(BasePlayer aPlayer, BaseProjectile aProjectile, int aDesiredAmount) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnReloadWeapon(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        void OnRocketLaunched(BasePlayer aPlayer, BaseEntity aEntity)
        {
            if (CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc) != null)
                aEntity.AdminKill();
        }

        object OnSwitchAmmo(BasePlayer aPlayer, BaseProjectile aProjectile) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        void OnWeaponFired(BaseProjectile aProjectile, BasePlayer aPlayer, ItemModProjectile aMod, ProtoBuf.ProjectileShoot aProjectiles)
        {
            if (CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc) != null)
                aProjectiles.projectiles.Clear();
        }

        object CanUseHelicopter(BasePlayer aPlayer, CH47HelicopterAIController aHelicopter) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanCheckFuel(EntityFuelSystem aFuelSystem, StorageContainer aFuelContainer, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnLiftUse(Lift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnLiftUse(ProceduralLift aLift, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnOvenToggle(BaseOven aOven, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnRecyclerToggle(Recycler aRecycler, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnShopCompleteTrade(ShopFront aShop, BasePlayer aCustomer) => CheckAndReturnNullOrFalse(aCustomer.IPlayer, aCustomer.IsNpc);

        object OnTurretAuthorize(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnTurretClearList(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnTurretRotate(AutoTurret aTurret, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnCounterTargetChange(PowerCounter aCounter, BasePlayer aPlayer, int aTargetNumber) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnCounterModeToggle(PowerCounter aCounter, BasePlayer aPlayer, bool aMode) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnSwitchToggle(IOEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnElevatorButtonPress(ElevatorLift aLift, BasePlayer aPlayer, Elevator.Direction aDirection, bool aToTopOrBottom) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object CanSwapToSeat(BasePlayer aPlayer, BaseMountable aMountable) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnRidableAnimalClaim(BaseRidableAnimal aAnimal, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer, aPlayer.IsNpc);

        object OnTeamCreate(BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnGrowableGather(GrowableEntity plant, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnCollectiblePickup(Item aItem, BasePlayer aPlayer, CollectibleEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanTakeCutting(BasePlayer aPlayer, GrowableEntity aEntity) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanAdministerVending(BasePlayer aPlayer, VendingMachine aMachine) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUseVending(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnRotateVendingMachine(VendingMachine aMachine, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnBuyVendingItem(VendingMachine aMachine, BasePlayer aPlayer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnVendingTransaction(VendingMachine aMachine, BasePlayer aBuyer, int aSellOrderId, int aNumberOfTransactions) =>
            CheckAndReturnNullOrFalse(aBuyer.IPlayer);

        object OnCodeEntered(CodeLock aCodeLock, BasePlayer aPlayer, string aCode) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnCupboardAuthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnCupboardClearList(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnCupboardDeauthorize(BuildingPrivlidge aPrivilege, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        void OnDoorClosed(Door aDoor, BasePlayer aPlayer)
        {
            IPlayer player = aPlayer?.IPlayer;

            if (aPlayer == null || aPlayer.IsNpc || player.IsServer || (FConfigData.AdminAlwaysVerified && player.IsAdmin) ||
                permission.UserHasGroup(player.Id, FConfigData.VerifiedPlayerGroup))
                return;

            aDoor.SetOpen(true, false);
        }

        void OnDoorOpened(Door aDoor, BasePlayer aPlayer)
        {
            IPlayer player = aPlayer?.IPlayer;

            if (aPlayer == null || aPlayer.IsNpc || player.IsServer || (FConfigData.AdminAlwaysVerified && player.IsAdmin) ||
                permission.UserHasGroup(player.Id, FConfigData.VerifiedPlayerGroup))
                return;

            aDoor.SetOpen(false, false);
        }

        object OnHammerHit(BasePlayer aPlayer, HitInfo aInfo) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnStructureDemolish(BaseCombatEntity aEntity, BasePlayer aPlayer, bool aImmediate) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnStructureRepair(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnStructureRotate(BaseCombatEntity aEntity, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnStructureUpgrade(BaseCombatEntity aEntity, BasePlayer aPlayer, BuildingGrade.Enum aGrade) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnConstructionPlace(BaseEntity aEntity, Construction aComponent, Construction.Target aConstructionTarget, BasePlayer aPlayer) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUnlockTechTreeNode(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object CanUnlockTechTreeNodePath(BasePlayer aPlayer, TechTreeData.NodeInstance aNode, TechTreeData aTechTree) =>
            CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnPhoneDial(PhoneController aCallerPhone, PhoneController aReceiverPhone, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnPhoneCallStart(PhoneController aPhone, PhoneController aOtherPhone, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);

        object OnPhoneNameUpdate(PhoneController aPhoneController, string aName, BasePlayer aPlayer) => CheckAndReturnNullOrFalse(aPlayer.IPlayer);
        #endregion Hooks
    }
}
