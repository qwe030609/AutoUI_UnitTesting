using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP5AutoUITests
{
    public enum Colors
    {
        [System.ComponentModel.Description("#FF000000")]
        Black,

        [System.ComponentModel.Description("#FF444444")]
        DarkGray,

        [System.ComponentModel.Description("#FF666666")]
        DimGray,

        [System.ComponentModel.Description("#FF888888")]
        Gray,

        [System.ComponentModel.Description("#FFAAAAAA")]
        LightGray,

        [System.ComponentModel.Description("#FFDDDDDD")]
        VeryLightGray,

        [System.ComponentModel.Description("#FFFFFFFF")]
        White,

        [System.ComponentModel.Description("#FFBB5500")]
        DarkBrown,

        [System.ComponentModel.Description("#FFCC6600")]
        MediumBrown,

        [System.ComponentModel.Description("#FFEE7700")]
        PersianOrange,

        [System.ComponentModel.Description("#FFFF8800")]
        PumpkinOrange,

        [System.ComponentModel.Description("#FFFFAA33")]
        MangoOrange,

        [System.ComponentModel.Description("#FFFFBB66")]
        LightOrange,

        [System.ComponentModel.Description("#FFFFDDAA")]
        PeachOrange,

        [System.ComponentModel.Description("#FF886600")]
        Olive,

        [System.ComponentModel.Description("#FFAA7700")]
        BronzeYellow,

        [System.ComponentModel.Description("#FFDDAA00")]
        MustardYellow,

        [System.ComponentModel.Description("#FFFFBB00")]
        TangerineYellow,

        [System.ComponentModel.Description("#FFFFCC22")]
        CanaryYellow,

        [System.ComponentModel.Description("#FFFFDD55")]
        LightGoldenrodYellow,

        [System.ComponentModel.Description("#FFFFEE99")]
        PaleCanaryYellow,

        [System.ComponentModel.Description("#FF888800")]
        OliveDrab,

        [System.ComponentModel.Description("#FFBBBB00")]
        LemonYellow,

        [System.ComponentModel.Description("#FFEEEE00")]
        BrightYellow,

        [System.ComponentModel.Description("#FFFFFF00")]
        Yellow,

        [System.ComponentModel.Description("#FFFFFF33")]
        LaserLemon,

        [System.ComponentModel.Description("#FFFFFF77")]
        LightYellow,

        [System.ComponentModel.Description("#FFFFFFBB")]
        PaleLightYellow,

        [System.ComponentModel.Description("#FF668800")]
        DarkOliveGreen,

        [System.ComponentModel.Description("#FF88AA00")]
        AppleGreen,

        [System.ComponentModel.Description("#FF99DD00")]
        ChartreuseGreen,

        [System.ComponentModel.Description("#FFBBFF00")]
        NeonGreen,

        [System.ComponentModel.Description("#FFCCFF33")]
        LimeGreen,

        [System.ComponentModel.Description("#FFDDFF77")]
        LightLime,

        [System.ComponentModel.Description("#FFEEFFBB")]
        PaleLime,

        [System.ComponentModel.Description("#FF227700")]
        DarkForestGreen,

        [System.ComponentModel.Description("#FF55AA00")]
        KellyGreen,

        [System.ComponentModel.Description("#FF66DD00")]
        BrightGreen,

        [System.ComponentModel.Description("#FF77FF00")]
        Harlequin,

        [System.ComponentModel.Description("#FF99FF33")]
        LightHarlequin,

        [System.ComponentModel.Description("#FFBBFF66")]
        LightChartreuse,

        [System.ComponentModel.Description("#FFCCFF99")]
        PaleLightGreen,

        [System.ComponentModel.Description("#FF008800")]
        OfficeGreen,

        [System.ComponentModel.Description("#FF00AA00")]
        IslamicGreen,

        [System.ComponentModel.Description("#FF00DD00")]
        Green,

        [System.ComponentModel.Description("#FF00FF00")]
        Lime,

        [System.ComponentModel.Description("#FF33FF33")]
        ScreaminGreen,

        [System.ComponentModel.Description("#FF66FF66")]
        PastelGreen,

        [System.ComponentModel.Description("#FF99FF99")]
        VeryPaleGreen,

        [System.ComponentModel.Description("#FF008844")]
        DeepGreen,

        [System.ComponentModel.Description("#FF00AA55")]
        MediumSeaGreen,

        [System.ComponentModel.Description("#FF00DD77")]
        MediumSpringGreen,

        [System.ComponentModel.Description("#FF00FF99")]
        SpringGreen,

        [System.ComponentModel.Description("#FF33FFAA")]
        MediumAquamarine,

        [System.ComponentModel.Description("#FF77FFCC")]
        Aquamarine,

        [System.ComponentModel.Description("#FFBBFFEE")]
        LightAquamarine,

        [System.ComponentModel.Description("#FF008866")]
        DarkTurquoise,

        [System.ComponentModel.Description("#FF00AA88")]
        MediumTurquoise,

        [System.ComponentModel.Description("#FF00DDAA")]
        Turquoise,

        [System.ComponentModel.Description("#FF00FFCC")]
        BrightTurquoise,

        [System.ComponentModel.Description("#FF33FFDD")]
        LightTurquoise,

        [System.ComponentModel.Description("#FF77FFEE")]
        PaleTurquoise,

        [System.ComponentModel.Description("#FFAAFFEE")]
        PaleCyan,

        [System.ComponentModel.Description("#FF008888")]
        Teal,

        [System.ComponentModel.Description("#FF00AAAA")]
        DarkCyan,

        [System.ComponentModel.Description("#FF00DDDD")]
        TurquoiseBlue,

        [System.ComponentModel.Description("#FF00FFFF")]
        Cyan,

        [System.ComponentModel.Description("#FF33FFFF")]
        ElectricBlue,

        [System.ComponentModel.Description("#FF66FFFF")]
        PaleBlue,

        [System.ComponentModel.Description("#FF99FFFF")]
        LightCyan,

        [System.ComponentModel.Description("#FF007799")]
        TealBlue,

        [System.ComponentModel.Description("#FF0088A8")]
        PeacockBlue,

        [System.ComponentModel.Description("#FF009FCC")]
        SkyBlue,

        [System.ComponentModel.Description("#FF00BBFF")]
        DeepSkyBlue,

        [System.ComponentModel.Description("#FF33CCFF")]
        VividSkyBlue,

        [System.ComponentModel.Description("#FF77DDFF")]
        LightSkyBlue,

        [System.ComponentModel.Description("#FFCCEEFF")]
        PaleSkyBlue,

        [System.ComponentModel.Description("#FF003377")]
        DarkBlue,

        [System.ComponentModel.Description("#FF003C9D")]
        MediumBlue1,

        [System.ComponentModel.Description("#FF0044BB")]
        NeonBlue,

        [System.ComponentModel.Description("#FF0066FF")]
        BrightBlue,

        [System.ComponentModel.Description("#FF5599FF")]
        LightNeonBlue,

        [System.ComponentModel.Description("#FF99BBFF")]
        SoftBlue,

        [System.ComponentModel.Description("#FFCCDDFF")]
        VeryPaleBlue,

        [System.ComponentModel.Description("#FF000088")]
        NavyBlue,

        [System.ComponentModel.Description("#FF0000AA")]
        MediumBlue2,

        [System.ComponentModel.Description("#FF0000CC")]
        BlueRibbon,

        [System.ComponentModel.Description("#FF0000FF")]
        Blue,

        [System.ComponentModel.Description("#FF5555FF")]
        BrilliantBlue,

        [System.ComponentModel.Description("#FF9999FF")]
        LightBrilliantBlue,

        [System.ComponentModel.Description("#FFCCCCFF")]
        LavenderBlue,

        [System.ComponentModel.Description("#FF220088")]
        Indigo,

        [System.ComponentModel.Description("#FF2200AA")]
        PersianIndigo,

        [System.ComponentModel.Description("#FF4400CC")]
        BlueGem,

        [System.ComponentModel.Description("#FF5500FF")]
        ElectricIndigo,

        [System.ComponentModel.Description("#FF7744FF")]
        BrightPurple,

        [System.ComponentModel.Description("#FF9F88FF")]
        LightPurple,

        [System.ComponentModel.Description("#FFCCBBFF")]
        PaleLavender,

        [System.ComponentModel.Description("#FF3A0088")]
        DeepIndigo,

        [System.ComponentModel.Description("#FF4400B3")]
        RoyalPurple,

        [System.ComponentModel.Description("#FF5500DD")]
        BlueViolet,

        [System.ComponentModel.Description("#FF7700FF")]
        VividViolet,

        [System.ComponentModel.Description("#FF9955FF")]
        LightViolet,

        [System.ComponentModel.Description("#FFB088FF")]
        SoftViolet,

        [System.ComponentModel.Description("#FFD1BBFF")]
        VeryPaleViolet,

        [System.ComponentModel.Description("#FF550088")]
        DeepPurple,

        [System.ComponentModel.Description("#FF66009D")]
        DarkOrchid,

        [System.ComponentModel.Description("#FF7700BB")]
        Purple,

        [System.ComponentModel.Description("#FF9900FF")]
        ElectricPurple,

        [System.ComponentModel.Description("#FFB94FFF")]
        BrightOrchid,

        [System.ComponentModel.Description("#FFD28EFF")]
        SoftOrchid,

        [System.ComponentModel.Description("#FFE8CCFF")]
        LavenderPink,

        [System.ComponentModel.Description("#FF660077")]
        Eggplant,

        [System.ComponentModel.Description("#FF7A0099")]
        VividPurple,

        [System.ComponentModel.Description("#FFA500CC")]
        DarkMagenta1,

        [System.ComponentModel.Description("#FFCC00FF")]
        NeonMagenta,

        [System.ComponentModel.Description("#FFE93EFF")]
        HotPink,

        [System.ComponentModel.Description("#FFE38EFF")]
        LightMagenta1,

        [System.ComponentModel.Description("#FFF0BBFF")]
        PalePink,

        [System.ComponentModel.Description("#FF770077")]
        DarkMagenta2,

        [System.ComponentModel.Description("#FF990099")]
        Violet,

        [System.ComponentModel.Description("#FFCC00CC")]
        DeepMagenta,

        [System.ComponentModel.Description("#FFFF00FF")]
        Magenta,

        [System.ComponentModel.Description("#FFFF3EFF")]
        Fuchsia,

        [System.ComponentModel.Description("#FFFF77FF")]
        LightMagenta2,

        [System.ComponentModel.Description("#FFFFB3FF")]
        PaleMagenta,

        [System.ComponentModel.Description("default")]
        TransparentWhite,
    }
}