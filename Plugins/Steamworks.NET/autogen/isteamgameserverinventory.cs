// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2019 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if UNITY_ANDROID || UNITY_IOS || UNITY_TIZEN || UNITY_TVOS || UNITY_WEBGL || UNITY_WSA || UNITY_PS4 || UNITY_WII || UNITY_XBOXONE || UNITY_SWITCH
	#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	public static class SteamGameServerInventory {
		/// <summary>
		/// <para> INVENTORY ASYNC RESULT MANAGEMENT</para>
		/// <para> Asynchronous inventory queries always output a result handle which can be used with</para>
		/// <para> GetResultStatus, GetResultItems, etc. A SteamInventoryResultReady_t callback will</para>
		/// <para> be triggered when the asynchronous result becomes ready (or fails).</para>
		/// <para> Find out the status of an asynchronous inventory result handle. Possible values:</para>
		/// <para>  k_EResultPending - still in progress</para>
		/// <para>  k_EResultOK - done, result ready</para>
		/// <para>  k_EResultExpired - done, result ready, maybe out of date (see DeserializeResult)</para>
		/// <para>  k_EResultInvalidParam - ERROR: invalid API call parameters</para>
		/// <para>  k_EResultServiceUnavailable - ERROR: service temporarily down, you may retry later</para>
		/// <para>  k_EResultLimitExceeded - ERROR: operation would exceed per-user inventory limits</para>
		/// <para>  k_EResultFail - ERROR: unknown / generic error</para>
		/// </summary>
		public static EResult GetResultStatus(SteamInventoryResult_t resultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetResultStatus(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		/// <summary>
		/// <para> Copies the contents of a result set into a flat array. The specific</para>
		/// <para> contents of the result set depend on which query which was used.</para>
		/// </summary>
		public static bool GetResultItems(SteamInventoryResult_t resultHandle, SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetResultItems(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		/// <summary>
		/// <para> In combination with GetResultItems, you can use GetResultItemProperty to retrieve</para>
		/// <para> dynamic string properties for a given item returned in the result set.</para>
		/// <para> Property names are always composed of ASCII letters, numbers, and/or underscores.</para>
		/// <para> Pass a NULL pointer for pchPropertyName to get a comma - separated list of available</para>
		/// <para> property names.</para>
		/// <para> If pchValueBuffer is NULL, *punValueBufferSize will contain the</para>
		/// <para> suggested buffer size. Otherwise it will be the number of bytes actually copied</para>
		/// <para> to pchValueBuffer. If the results do not fit in the given buffer, partial</para>
		/// <para> results may be copied.</para>
		/// </summary>
		public static bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut) {
			InteropHelp.TestIfAvailableGameServer();
			IntPtr pchValueBuffer2 = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				bool ret = NativeMethods.ISteamInventory_GetResultItemProperty(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, unItemIndex, pchPropertyName2, pchValueBuffer2, ref punValueBufferSizeOut);
				pchValueBuffer = ret ? InteropHelp.PtrToStringUTF8(pchValueBuffer2) : null;
				Marshal.FreeHGlobal(pchValueBuffer2);
				return ret;
			}
		}

		/// <summary>
		/// <para> Returns the server time at which the result was generated. Compare against</para>
		/// <para> the value of IClientUtils::GetServerRealTime() to determine age.</para>
		/// </summary>
		public static uint GetResultTimestamp(SteamInventoryResult_t resultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetResultTimestamp(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		/// <summary>
		/// <para> Returns true if the result belongs to the target steam ID, false if the</para>
		/// <para> result does not. This is important when using DeserializeResult, to verify</para>
		/// <para> that a remote player is not pretending to have a different user's inventory.</para>
		/// </summary>
		public static bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_CheckResultSteamID(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, steamIDExpected);
		}

		/// <summary>
		/// <para> Destroys a result handle and frees all associated memory.</para>
		/// </summary>
		public static void DestroyResult(SteamInventoryResult_t resultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamInventory_DestroyResult(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		/// <summary>
		/// <para> INVENTORY ASYNC QUERY</para>
		/// <para> Captures the entire state of the current user's Steam inventory.</para>
		/// <para> You must call DestroyResult on this handle when you are done with it.</para>
		/// <para> Returns false and sets *pResultHandle to zero if inventory is unavailable.</para>
		/// <para> Note: calls to this function are subject to rate limits and may return</para>
		/// <para> cached results if called too frequently. It is suggested that you call</para>
		/// <para> this function only when you are about to display the user's full inventory,</para>
		/// <para> or if you expect that the inventory may have changed.</para>
		/// </summary>
		public static bool GetAllItems(out SteamInventoryResult_t pResultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetAllItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		/// <summary>
		/// <para> Captures the state of a subset of the current user's Steam inventory,</para>
		/// <para> identified by an array of item instance IDs. The results from this call</para>
		/// <para> can be serialized and passed to other players to "prove" that the current</para>
		/// <para> user owns specific items, without exposing the user's entire inventory.</para>
		/// <para> For example, you could call GetItemsByID with the IDs of the user's</para>
		/// <para> currently equipped cosmetic items and serialize this to a buffer, and</para>
		/// <para> then transmit this buffer to other players upon joining a game.</para>
		/// </summary>
		public static bool GetItemsByID(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemsByID(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pInstanceIDs, unCountInstanceIDs);
		}

		/// <summary>
		/// <para> RESULT SERIALIZATION AND AUTHENTICATION</para>
		/// <para> Serialized result sets contain a short signature which can't be forged</para>
		/// <para> or replayed across different game sessions. A result set can be serialized</para>
		/// <para> on the local client, transmitted to other players via your game networking,</para>
		/// <para> and deserialized by the remote players. This is a secure way of preventing</para>
		/// <para> hackers from lying about posessing rare/high-value items.</para>
		/// <para> Serializes a result set with signature bytes to an output buffer. Pass</para>
		/// <para> NULL as an output buffer to get the required size via punOutBufferSize.</para>
		/// <para> The size of a serialized result depends on the number items which are being</para>
		/// <para> serialized. When securely transmitting items to other players, it is</para>
		/// <para> recommended to use "GetItemsByID" first to create a minimal result set.</para>
		/// <para> Results have a built-in timestamp which will be considered "expired" after</para>
		/// <para> an hour has elapsed. See DeserializeResult for expiration handling.</para>
		/// </summary>
		public static bool SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_SerializeResult(CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutBuffer, out punOutBufferSize);
		}

		/// <summary>
		/// <para> Deserializes a result set and verifies the signature bytes. Returns false</para>
		/// <para> if bRequireFullOnlineVerify is set but Steam is running in Offline mode.</para>
		/// <para> Otherwise returns true and then delivers error codes via GetResultStatus.</para>
		/// <para> The bRESERVED_MUST_BE_FALSE flag is reserved for future use and should not</para>
		/// <para> be set to true by your game at this time.</para>
		/// <para> DeserializeResult has a potential soft-failure mode where the handle status</para>
		/// <para> is set to k_EResultExpired. GetResultItems() still succeeds in this mode.</para>
		/// <para> The "expired" result could indicate that the data may be out of date - not</para>
		/// <para> just due to timed expiration (one hour), but also because one of the items</para>
		/// <para> in the result set may have been traded or consumed since the result set was</para>
		/// <para> generated. You could compare the timestamp from GetResultTimestamp() to</para>
		/// <para> ISteamUtils::GetServerRealTime() to determine how old the data is. You could</para>
		/// <para> simply ignore the "expired" result code and continue as normal, or you</para>
		/// <para> could challenge the player with expired data to send an updated result set.</para>
		/// </summary>
		public static bool DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE = false) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_DeserializeResult(CSteamGameServerAPIContext.GetSteamInventory(), out pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		/// <summary>
		/// <para> INVENTORY ASYNC MODIFICATION</para>
		/// <para> GenerateItems() creates one or more items and then generates a SteamInventoryCallback_t</para>
		/// <para> notification with a matching nCallbackContext parameter. This API is only intended</para>
		/// <para> for prototyping - it is only usable by Steam accounts that belong to the publisher group</para>
		/// <para> for your game.</para>
		/// <para> If punArrayQuantity is not NULL, it should be the same length as pArrayItems and should</para>
		/// <para> describe the quantity of each item to generate.</para>
		/// </summary>
		public static bool GenerateItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GenerateItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		/// <summary>
		/// <para> GrantPromoItems() checks the list of promotional items for which the user may be eligible</para>
		/// <para> and grants the items (one time only).  On success, the result set will include items which</para>
		/// <para> were granted, if any. If no items were granted because the user isn't eligible for any</para>
		/// <para> promotions, this is still considered a success.</para>
		/// </summary>
		public static bool GrantPromoItems(out SteamInventoryResult_t pResultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GrantPromoItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		/// <summary>
		/// <para> AddPromoItem() / AddPromoItems() are restricted versions of GrantPromoItems(). Instead of</para>
		/// <para> scanning for all eligible promotional items, the check is restricted to a single item</para>
		/// <para> definition or set of item definitions. This can be useful if your game has custom UI for</para>
		/// <para> showing a specific promo item to the user.</para>
		/// </summary>
		public static bool AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_AddPromoItem(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemDef);
		}

		public static bool AddPromoItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint unArrayLength) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_AddPromoItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, unArrayLength);
		}

		/// <summary>
		/// <para> ConsumeItem() removes items from the inventory, permanently. They cannot be recovered.</para>
		/// <para> Not for the faint of heart - if your game implements item removal at all, a high-friction</para>
		/// <para> UI confirmation process is highly recommended.</para>
		/// </summary>
		public static bool ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_ConsumeItem(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemConsume, unQuantity);
		}

		/// <summary>
		/// <para> ExchangeItems() is an atomic combination of item generation and consumption.</para>
		/// <para> It can be used to implement crafting recipes or transmutations, or items which unpack</para>
		/// <para> themselves into other items (e.g., a chest).</para>
		/// <para> Exchange recipes are defined in the ItemDef, and explicitly list the required item</para>
		/// <para> types and resulting generated type.</para>
		/// <para> Exchange recipes are evaluated atomically by the Inventory Service; if the supplied</para>
		/// <para> components do not match the recipe, or do not contain sufficient quantity, the</para>
		/// <para> exchange will fail.</para>
		/// </summary>
		public static bool ExchangeItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_ExchangeItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		/// <summary>
		/// <para> TransferItemQuantity() is intended for use with items which are "stackable" (can have</para>
		/// <para> quantity greater than one). It can be used to split a stack into two, or to transfer</para>
		/// <para> quantity from one stack into another stack of identical items. To split one stack into</para>
		/// <para> two, pass k_SteamItemInstanceIDInvalid for itemIdDest and a new item will be generated.</para>
		/// </summary>
		public static bool TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TransferItemQuantity(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		/// <summary>
		/// <para> TIMED DROPS AND PLAYTIME CREDIT</para>
		/// <para> Deprecated. Calling this method is not required for proper playtime accounting.</para>
		/// </summary>
		public static void SendItemDropHeartbeat() {
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamInventory_SendItemDropHeartbeat(CSteamGameServerAPIContext.GetSteamInventory());
		}

		/// <summary>
		/// <para> Playtime credit must be consumed and turned into item drops by your game. Only item</para>
		/// <para> definitions which are marked as "playtime item generators" can be spawned. The call</para>
		/// <para> will return an empty result set if there is not enough playtime credit for a drop.</para>
		/// <para> Your game should call TriggerItemDrop at an appropriate time for the user to receive</para>
		/// <para> new items, such as between rounds or while the player is dead. Note that players who</para>
		/// <para> hack their clients could modify the value of "dropListDefinition", so do not use it</para>
		/// <para> to directly control rarity.</para>
		/// <para> See your Steamworks configuration to set playtime drop rates for individual itemdefs.</para>
		/// <para> The client library will suppress too-frequent calls to this method.</para>
		/// </summary>
		public static bool TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TriggerItemDrop(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, dropListDefinition);
		}

		/// <summary>
		/// <para> Deprecated. This method is not supported.</para>
		/// </summary>
		public static bool TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_TradeItems(CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		/// <summary>
		/// <para> ITEM DEFINITIONS</para>
		/// <para> Item definitions are a mapping of "definition IDs" (integers between 1 and 1000000)</para>
		/// <para> to a set of string properties. Some of these properties are required to display items</para>
		/// <para> on the Steam community web site. Other properties can be defined by applications.</para>
		/// <para> Use of these functions is optional; there is no reason to call LoadItemDefinitions</para>
		/// <para> if your game hardcodes the numeric definition IDs (eg, purple face mask = 20, blue</para>
		/// <para> weapon mod = 55) and does not allow for adding new item types without a client patch.</para>
		/// <para> LoadItemDefinitions triggers the automatic load and refresh of item definitions.</para>
		/// <para> Every time new item definitions are available (eg, from the dynamic addition of new</para>
		/// <para> item types while players are still in-game), a SteamInventoryDefinitionUpdate_t</para>
		/// <para> callback will be fired.</para>
		/// </summary>
		public static bool LoadItemDefinitions() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_LoadItemDefinitions(CSteamGameServerAPIContext.GetSteamInventory());
		}

		/// <summary>
		/// <para> GetItemDefinitionIDs returns the set of all defined item definition IDs (which are</para>
		/// <para> defined via Steamworks configuration, and not necessarily contiguous integers).</para>
		/// <para> If pItemDefIDs is null, the call will return true and *punItemDefIDsArraySize will</para>
		/// <para> contain the total size necessary for a subsequent call. Otherwise, the call will</para>
		/// <para> return false if and only if there is not enough space in the output array.</para>
		/// </summary>
		public static bool GetItemDefinitionIDs(SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemDefinitionIDs(CSteamGameServerAPIContext.GetSteamInventory(), pItemDefIDs, ref punItemDefIDsArraySize);
		}

		/// <summary>
		/// <para> GetItemDefinitionProperty returns a string property from a given item definition.</para>
		/// <para> Note that some properties (for example, "name") may be localized and will depend</para>
		/// <para> on the current Steam language settings (see ISteamApps::GetCurrentGameLanguage).</para>
		/// <para> Property names are always composed of ASCII letters, numbers, and/or underscores.</para>
		/// <para> Pass a NULL pointer for pchPropertyName to get a comma - separated list of available</para>
		/// <para> property names. If pchValueBuffer is NULL, *punValueBufferSize will contain the</para>
		/// <para> suggested buffer size. Otherwise it will be the number of bytes actually copied</para>
		/// <para> to pchValueBuffer. If the results do not fit in the given buffer, partial</para>
		/// <para> results may be copied.</para>
		/// </summary>
		public static bool GetItemDefinitionProperty(SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut) {
			InteropHelp.TestIfAvailableGameServer();
			IntPtr pchValueBuffer2 = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				bool ret = NativeMethods.ISteamInventory_GetItemDefinitionProperty(CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, pchPropertyName2, pchValueBuffer2, ref punValueBufferSizeOut);
				pchValueBuffer = ret ? InteropHelp.PtrToStringUTF8(pchValueBuffer2) : null;
				Marshal.FreeHGlobal(pchValueBuffer2);
				return ret;
			}
		}

		/// <summary>
		/// <para> Request the list of "eligible" promo items that can be manually granted to the given</para>
		/// <para> user.  These are promo items of type "manual" that won't be granted automatically.</para>
		/// <para> An example usage of this is an item that becomes available every week.</para>
		/// </summary>
		public static SteamAPICall_t RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(CSteamGameServerAPIContext.GetSteamInventory(), steamID);
		}

		/// <summary>
		/// <para> After handling a SteamInventoryEligiblePromoItemDefIDs_t call result, use this</para>
		/// <para> function to pull out the list of item definition ids that the user can be</para>
		/// <para> manually granted via the AddPromoItems() call.</para>
		/// </summary>
		public static bool GetEligiblePromoItemDefinitionIDs(CSteamID steamID, SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetEligiblePromoItemDefinitionIDs(CSteamGameServerAPIContext.GetSteamInventory(), steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		/// <summary>
		/// <para> Starts the purchase process for the given item definitions.  The callback SteamInventoryStartPurchaseResult_t</para>
		/// <para> will be posted if Steam was able to initialize the transaction.</para>
		/// <para> Once the purchase has been authorized and completed by the user, the callback SteamInventoryResultReady_t</para>
		/// <para> will be posted.</para>
		/// </summary>
		public static SteamAPICall_t StartPurchase(SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_StartPurchase(CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		/// <summary>
		/// <para> Request current prices for all applicable item definitions</para>
		/// </summary>
		public static SteamAPICall_t RequestPrices() {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestPrices(CSteamGameServerAPIContext.GetSteamInventory());
		}

		/// <summary>
		/// <para> Returns the number of items with prices.  Need to call RequestPrices() first.</para>
		/// </summary>
		public static uint GetNumItemsWithPrices() {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetNumItemsWithPrices(CSteamGameServerAPIContext.GetSteamInventory());
		}

		/// <summary>
		/// <para> Returns item definition ids and their prices in the user's local currency.</para>
		/// <para> Need to call RequestPrices() first.</para>
		/// </summary>
		public static bool GetItemsWithPrices(SteamItemDef_t[] pArrayItemDefs, ulong[] pCurrentPrices, ulong[] pBasePrices, uint unArrayLength) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemsWithPrices(CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
		}

		/// <summary>
		/// <para> Retrieves the price for the item definition id</para>
		/// <para> Returns false if there is no price stored for the item definition.</para>
		/// </summary>
		public static bool GetItemPrice(SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_GetItemPrice(CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, out pCurrentPrice, out pBasePrice);
		}

		/// <summary>
		/// <para> Create a request to update properties on items</para>
		/// </summary>
		public static SteamInventoryUpdateHandle_t StartUpdateProperties() {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamInventoryUpdateHandle_t)NativeMethods.ISteamInventory_StartUpdateProperties(CSteamGameServerAPIContext.GetSteamInventory());
		}

		/// <summary>
		/// <para> Remove the property on the item</para>
		/// </summary>
		public static bool RemoveProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				return NativeMethods.ISteamInventory_RemoveProperty(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2);
			}
		}

		/// <summary>
		/// <para> Accessor methods to set properties on items</para>
		/// </summary>
		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, string pchPropertyValue) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName))
			using (var pchPropertyValue2 = new InteropHelp.UTF8StringHandle(pchPropertyValue)) {
				return NativeMethods.ISteamInventory_SetProperty(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, pchPropertyValue2);
			}
		}

		public static bool SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, bool bValue) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				return NativeMethods.ISteamInventory_SetProperty0(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, bValue);
			}
		}

		public static bool SetProperty1(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, long nValue) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				return NativeMethods.ISteamInventory_SetProperty1(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, nValue);
			}
		}

		public static bool SetProperty2(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t nItemID, string pchPropertyName, float flValue) {
			InteropHelp.TestIfAvailableGameServer();
			using (var pchPropertyName2 = new InteropHelp.UTF8StringHandle(pchPropertyName)) {
				return NativeMethods.ISteamInventory_SetProperty2(CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, flValue);
			}
		}

		/// <summary>
		/// <para> Submit the update request by handle</para>
		/// </summary>
		public static bool SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, out SteamInventoryResult_t pResultHandle) {
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamInventory_SubmitUpdateProperties(CSteamGameServerAPIContext.GetSteamInventory(), handle, out pResultHandle);
		}
	}
}

#endif // !DISABLESTEAMWORKS
