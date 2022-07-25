using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Constant
{
    // RESOLUTION
    public static readonly int SCREEN_RESOLUTION_WIDTH = 1080;
    public static readonly int SCREEN_RESOLUTION_HEIGHT = 1920;

    public static readonly int SCREEN_RESOLUTION_WIDTH_ONGAME = 1920;
    public static readonly int SCREEN_RESOLUTION_HEIGHT_ONGAME = 1080;

    // SCENE NAME
    public const string NAME_SCENE_TITLE = "01_Title";
    public const string NAME_SCENE_LOBBY = "02_Lobby";
    public const string NAME_SCENE_GAME = "03_Game";

    public static readonly float TIME_NETWORK_NOTICE = 2.0f;
    public static readonly float TIME_SCROLL = 0.8f;

    public static readonly float COLOR_ALPHA_OPACITY = 1f;
    public static readonly float COLOR_ALPHA_TRANSLUCENT = 0.5f;
}
