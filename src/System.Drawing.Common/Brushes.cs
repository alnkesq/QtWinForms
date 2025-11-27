using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public static class Brushes
    {
        private static Brush GetBrush(Color color)
        {
            return new SolidBrush(color);
        }

        public static Brush Transparent => GetBrush(Color.Transparent);
        public static Brush AliceBlue => GetBrush(Color.AliceBlue);
        public static Brush AntiqueWhite => GetBrush(Color.AntiqueWhite);
        public static Brush Aqua => GetBrush(Color.Aqua);
        public static Brush Aquamarine => GetBrush(Color.Aquamarine);
        public static Brush Azure => GetBrush(Color.Azure);

        public static Brush Beige => GetBrush(Color.Beige);
        public static Brush Bisque => GetBrush(Color.Bisque);
        public static Brush Black => GetBrush(Color.Black);
        public static Brush BlanchedAlmond => GetBrush(Color.BlanchedAlmond);
        public static Brush Blue => GetBrush(Color.Blue);
        public static Brush BlueViolet => GetBrush(Color.BlueViolet);
        public static Brush Brown => GetBrush(Color.Brown);
        public static Brush BurlyWood => GetBrush(Color.BurlyWood);

        public static Brush CadetBlue => GetBrush(Color.CadetBlue);
        public static Brush Chartreuse => GetBrush(Color.Chartreuse);
        public static Brush Chocolate => GetBrush(Color.Chocolate);
        public static Brush Coral => GetBrush(Color.Coral);
        public static Brush CornflowerBlue => GetBrush(Color.CornflowerBlue);
        public static Brush Cornsilk => GetBrush(Color.Cornsilk);
        public static Brush Crimson => GetBrush(Color.Crimson);
        public static Brush Cyan => GetBrush(Color.Cyan);

        public static Brush DarkBlue => GetBrush(Color.DarkBlue);
        public static Brush DarkCyan => GetBrush(Color.DarkCyan);
        public static Brush DarkGoldenrod => GetBrush(Color.DarkGoldenrod);
        public static Brush DarkGray => GetBrush(Color.DarkGray);
        public static Brush DarkGreen => GetBrush(Color.DarkGreen);
        public static Brush DarkKhaki => GetBrush(Color.DarkKhaki);
        public static Brush DarkMagenta => GetBrush(Color.DarkMagenta);
        public static Brush DarkOliveGreen => GetBrush(Color.DarkOliveGreen);
        public static Brush DarkOrange => GetBrush(Color.DarkOrange);
        public static Brush DarkOrchid => GetBrush(Color.DarkOrchid);
        public static Brush DarkRed => GetBrush(Color.DarkRed);
        public static Brush DarkSalmon => GetBrush(Color.DarkSalmon);
        public static Brush DarkSeaGreen => GetBrush(Color.DarkSeaGreen);
        public static Brush DarkSlateBlue => GetBrush(Color.DarkSlateBlue);
        public static Brush DarkSlateGray => GetBrush(Color.DarkSlateGray);
        public static Brush DarkTurquoise => GetBrush(Color.DarkTurquoise);
        public static Brush DarkViolet => GetBrush(Color.DarkViolet);
        public static Brush DeepPink => GetBrush(Color.DeepPink);
        public static Brush DeepSkyBlue => GetBrush(Color.DeepSkyBlue);
        public static Brush DimGray => GetBrush(Color.DimGray);
        public static Brush DodgerBlue => GetBrush(Color.DodgerBlue);

        public static Brush Firebrick => GetBrush(Color.Firebrick);
        public static Brush FloralWhite => GetBrush(Color.FloralWhite);
        public static Brush ForestGreen => GetBrush(Color.ForestGreen);
        public static Brush Fuchsia => GetBrush(Color.Fuchsia);

        public static Brush Gainsboro => GetBrush(Color.Gainsboro);
        public static Brush GhostWhite => GetBrush(Color.GhostWhite);
        public static Brush Gold => GetBrush(Color.Gold);
        public static Brush Goldenrod => GetBrush(Color.Goldenrod);
        public static Brush Gray => GetBrush(Color.Gray);
        public static Brush Green => GetBrush(Color.Green);
        public static Brush GreenYellow => GetBrush(Color.GreenYellow);

        public static Brush Honeydew => GetBrush(Color.Honeydew);
        public static Brush HotPink => GetBrush(Color.HotPink);

        public static Brush IndianRed => GetBrush(Color.IndianRed);
        public static Brush Indigo => GetBrush(Color.Indigo);
        public static Brush Ivory => GetBrush(Color.Ivory);

        public static Brush Khaki => GetBrush(Color.Khaki);

        public static Brush Lavender => GetBrush(Color.Lavender);
        public static Brush LavenderBlush => GetBrush(Color.LavenderBlush);
        public static Brush LawnGreen => GetBrush(Color.LawnGreen);
        public static Brush LemonChiffon => GetBrush(Color.LemonChiffon);
        public static Brush LightBlue => GetBrush(Color.LightBlue);
        public static Brush LightCoral => GetBrush(Color.LightCoral);
        public static Brush LightCyan => GetBrush(Color.LightCyan);
        public static Brush LightGoldenrodYellow => GetBrush(Color.LightGoldenrodYellow);
        public static Brush LightGreen => GetBrush(Color.LightGreen);
        public static Brush LightGray => GetBrush(Color.LightGray);
        public static Brush LightPink => GetBrush(Color.LightPink);
        public static Brush LightSalmon => GetBrush(Color.LightSalmon);
        public static Brush LightSeaGreen => GetBrush(Color.LightSeaGreen);
        public static Brush LightSkyBlue => GetBrush(Color.LightSkyBlue);
        public static Brush LightSlateGray => GetBrush(Color.LightSlateGray);
        public static Brush LightSteelBlue => GetBrush(Color.LightSteelBlue);
        public static Brush LightYellow => GetBrush(Color.LightYellow);
        public static Brush Lime => GetBrush(Color.Lime);
        public static Brush LimeGreen => GetBrush(Color.LimeGreen);
        public static Brush Linen => GetBrush(Color.Linen);

        public static Brush Magenta => GetBrush(Color.Magenta);
        public static Brush Maroon => GetBrush(Color.Maroon);
        public static Brush MediumAquamarine => GetBrush(Color.MediumAquamarine);
        public static Brush MediumBlue => GetBrush(Color.MediumBlue);
        public static Brush MediumOrchid => GetBrush(Color.MediumOrchid);
        public static Brush MediumPurple => GetBrush(Color.MediumPurple);
        public static Brush MediumSeaGreen => GetBrush(Color.MediumSeaGreen);
        public static Brush MediumSlateBlue => GetBrush(Color.MediumSlateBlue);
        public static Brush MediumSpringGreen => GetBrush(Color.MediumSpringGreen);
        public static Brush MediumTurquoise => GetBrush(Color.MediumTurquoise);
        public static Brush MediumVioletRed => GetBrush(Color.MediumVioletRed);
        public static Brush MidnightBlue => GetBrush(Color.MidnightBlue);
        public static Brush MintCream => GetBrush(Color.MintCream);
        public static Brush MistyRose => GetBrush(Color.MistyRose);
        public static Brush Moccasin => GetBrush(Color.Moccasin);

        public static Brush NavajoWhite => GetBrush(Color.NavajoWhite);
        public static Brush Navy => GetBrush(Color.Navy);

        public static Brush OldLace => GetBrush(Color.OldLace);
        public static Brush Olive => GetBrush(Color.Olive);
        public static Brush OliveDrab => GetBrush(Color.OliveDrab);
        public static Brush Orange => GetBrush(Color.Orange);
        public static Brush OrangeRed => GetBrush(Color.OrangeRed);
        public static Brush Orchid => GetBrush(Color.Orchid);

        public static Brush PaleGoldenrod => GetBrush(Color.PaleGoldenrod);
        public static Brush PaleGreen => GetBrush(Color.PaleGreen);
        public static Brush PaleTurquoise => GetBrush(Color.PaleTurquoise);
        public static Brush PaleVioletRed => GetBrush(Color.PaleVioletRed);
        public static Brush PapayaWhip => GetBrush(Color.PapayaWhip);
        public static Brush PeachPuff => GetBrush(Color.PeachPuff);
        public static Brush Peru => GetBrush(Color.Peru);
        public static Brush Pink => GetBrush(Color.Pink);
        public static Brush Plum => GetBrush(Color.Plum);
        public static Brush PowderBlue => GetBrush(Color.PowderBlue);
        public static Brush Purple => GetBrush(Color.Purple);

        public static Brush Red => GetBrush(Color.Red);
        public static Brush RosyBrown => GetBrush(Color.RosyBrown);
        public static Brush RoyalBlue => GetBrush(Color.RoyalBlue);

        public static Brush SaddleBrown => GetBrush(Color.SaddleBrown);
        public static Brush Salmon => GetBrush(Color.Salmon);
        public static Brush SandyBrown => GetBrush(Color.SandyBrown);
        public static Brush SeaGreen => GetBrush(Color.SeaGreen);
        public static Brush SeaShell => GetBrush(Color.SeaShell);
        public static Brush Sienna => GetBrush(Color.Sienna);
        public static Brush Silver => GetBrush(Color.Silver);
        public static Brush SkyBlue => GetBrush(Color.SkyBlue);
        public static Brush SlateBlue => GetBrush(Color.SlateBlue);
        public static Brush SlateGray => GetBrush(Color.SlateGray);
        public static Brush Snow => GetBrush(Color.Snow);
        public static Brush SpringGreen => GetBrush(Color.SpringGreen);
        public static Brush SteelBlue => GetBrush(Color.SteelBlue);

        public static Brush Tan => GetBrush(Color.Tan);
        public static Brush Teal => GetBrush(Color.Teal);
        public static Brush Thistle => GetBrush(Color.Thistle);
        public static Brush Tomato => GetBrush(Color.Tomato);
        public static Brush Turquoise => GetBrush(Color.Turquoise);

        public static Brush Violet => GetBrush(Color.Violet);

        public static Brush Wheat => GetBrush(Color.Wheat);
        public static Brush White => GetBrush(Color.White);
        public static Brush WhiteSmoke => GetBrush(Color.WhiteSmoke);

        public static Brush Yellow => GetBrush(Color.Yellow);
        public static Brush YellowGreen => GetBrush(Color.YellowGreen);

    }
}
