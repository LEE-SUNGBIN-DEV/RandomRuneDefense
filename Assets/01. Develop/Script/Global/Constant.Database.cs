using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Constant
{
    // SERVER FILE NAME
    public static readonly string SERVER_NAME_FILE_PLAYER_STATS = "PlayerStats";
    public static readonly string SERVER_NAME_FILE_PLAYER_CURRENCY = "PlayerCurrency";
    public static readonly string SERVER_NAME_FILE_PLAYER_INVENTORY = "PlayerInventory";
    public static readonly string SERVER_NAME_FILE_PLAYER_COLLECTIONS = "PlayerCollections";

    public static readonly string SERVER_NAME_FILE_CARD_DATABASE = "CardDatabase";

    // AZURE FUNCTION NAME
    public static readonly string SERVER_NAME_FUNCTION_UPDATE_PLAYER_DATA = "UpdatePlayerData";
    public static readonly string SERVER_NAME_FUNCTION_GRANT_ITEMS_TO_USERS = "GrantItemsToUsers";

    // SERVER CATALOG
    public static readonly string SERVER_CATALOG_VERSION_CARD = "Card";
    public static readonly string SERVER_CATALOG_VERSION_GOLDBOX_STORE = "GoldBoxStore";
    public static readonly string SERVER_CATALOG_VERSION_CARDBOX_STORE = "CardBoxStore";

    public const string SERVER_NAME_CATALOG_CLASS_CARD = "Card";
    public const string SERVER_NAME_CATALOG_CLASS_CARDBOX = "CardBox";
    public const string SERVER_NAME_CATALOG_CLASS_GOLDBOX = "GoldBox";
    public const string SERVER_NAME_CATALOG_CLASS_ENEMY = "Enemy";

    // RESOURCE
    public static readonly string NAME_PREFAB_CARD_SLOT = "Card Slot";
    public static readonly string NAME_PREFAB_SELL_SLOT = "Sell Slot";
}
