using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{
    public static class Pens
    {
        private static Pen GetPen(Color color)
        {
            return new Pen(color);
        }
        public static Pen Transparent => GetPen(Color.Transparent);

        public static Pen AliceBlue => GetPen(Color.AliceBlue);
        public static Pen AntiqueWhite => GetPen(Color.AntiqueWhite);
        public static Pen Aqua => GetPen(Color.Aqua);
        public static Pen Aquamarine => GetPen(Color.Aquamarine);
        public static Pen Azure => GetPen(Color.Azure);

        public static Pen Beige => GetPen(Color.Beige);
        public static Pen Bisque => GetPen(Color.Bisque);
        public static Pen Black => GetPen(Color.Black);
        public static Pen BlanchedAlmond => GetPen(Color.BlanchedAlmond);
        public static Pen Blue => GetPen(Color.Blue);
        public static Pen BlueViolet => GetPen(Color.BlueViolet);
        public static Pen Brown => GetPen(Color.Brown);
        public static Pen BurlyWood => GetPen(Color.BurlyWood);

        public static Pen CadetBlue => GetPen(Color.CadetBlue);
        public static Pen Chartreuse => GetPen(Color.Chartreuse);
        public static Pen Chocolate => GetPen(Color.Chocolate);
        public static Pen Coral => GetPen(Color.Coral);
        public static Pen CornflowerBlue => GetPen(Color.CornflowerBlue);
        public static Pen Cornsilk => GetPen(Color.Cornsilk);
        public static Pen Crimson => GetPen(Color.Crimson);
        public static Pen Cyan => GetPen(Color.Cyan);

        public static Pen DarkBlue => GetPen(Color.DarkBlue);
        public static Pen DarkCyan => GetPen(Color.DarkCyan);
        public static Pen DarkGoldenrod => GetPen(Color.DarkGoldenrod);
        public static Pen DarkGray => GetPen(Color.DarkGray);
        public static Pen DarkGreen => GetPen(Color.DarkGreen);
        public static Pen DarkKhaki => GetPen(Color.DarkKhaki);
        public static Pen DarkMagenta => GetPen(Color.DarkMagenta);
        public static Pen DarkOliveGreen => GetPen(Color.DarkOliveGreen);
        public static Pen DarkOrange => GetPen(Color.DarkOrange);
        public static Pen DarkOrchid => GetPen(Color.DarkOrchid);
        public static Pen DarkRed => GetPen(Color.DarkRed);
        public static Pen DarkSalmon => GetPen(Color.DarkSalmon);
        public static Pen DarkSeaGreen => GetPen(Color.DarkSeaGreen);
        public static Pen DarkSlateBlue => GetPen(Color.DarkSlateBlue);
        public static Pen DarkSlateGray => GetPen(Color.DarkSlateGray);
        public static Pen DarkTurquoise => GetPen(Color.DarkTurquoise);
        public static Pen DarkViolet => GetPen(Color.DarkViolet);
        public static Pen DeepPink => GetPen(Color.DeepPink);
        public static Pen DeepSkyBlue => GetPen(Color.DeepSkyBlue);
        public static Pen DimGray => GetPen(Color.DimGray);
        public static Pen DodgerBlue => GetPen(Color.DodgerBlue);

        public static Pen Firebrick => GetPen(Color.Firebrick);
        public static Pen FloralWhite => GetPen(Color.FloralWhite);
        public static Pen ForestGreen => GetPen(Color.ForestGreen);
        public static Pen Fuchsia => GetPen(Color.Fuchsia);

        public static Pen Gainsboro => GetPen(Color.Gainsboro);
        public static Pen GhostWhite => GetPen(Color.GhostWhite);
        public static Pen Gold => GetPen(Color.Gold);
        public static Pen Goldenrod => GetPen(Color.Goldenrod);
        public static Pen Gray => GetPen(Color.Gray);
        public static Pen Green => GetPen(Color.Green);
        public static Pen GreenYellow => GetPen(Color.GreenYellow);

        public static Pen Honeydew => GetPen(Color.Honeydew);
        public static Pen HotPink => GetPen(Color.HotPink);

        public static Pen IndianRed => GetPen(Color.IndianRed);
        public static Pen Indigo => GetPen(Color.Indigo);
        public static Pen Ivory => GetPen(Color.Ivory);

        public static Pen Khaki => GetPen(Color.Khaki);

        public static Pen Lavender => GetPen(Color.Lavender);
        public static Pen LavenderBlush => GetPen(Color.LavenderBlush);
        public static Pen LawnGreen => GetPen(Color.LawnGreen);
        public static Pen LemonChiffon => GetPen(Color.LemonChiffon);
        public static Pen LightBlue => GetPen(Color.LightBlue);
        public static Pen LightCoral => GetPen(Color.LightCoral);
        public static Pen LightCyan => GetPen(Color.LightCyan);
        public static Pen LightGoldenrodYellow => GetPen(Color.LightGoldenrodYellow);
        public static Pen LightGreen => GetPen(Color.LightGreen);
        public static Pen LightGray => GetPen(Color.LightGray);
        public static Pen LightPink => GetPen(Color.LightPink);
        public static Pen LightSalmon => GetPen(Color.LightSalmon);
        public static Pen LightSeaGreen => GetPen(Color.LightSeaGreen);
        public static Pen LightSkyBlue => GetPen(Color.LightSkyBlue);
        public static Pen LightSlateGray => GetPen(Color.LightSlateGray);
        public static Pen LightSteelBlue => GetPen(Color.LightSteelBlue);
        public static Pen LightYellow => GetPen(Color.LightYellow);
        public static Pen Lime => GetPen(Color.Lime);
        public static Pen LimeGreen => GetPen(Color.LimeGreen);
        public static Pen Linen => GetPen(Color.Linen);

        public static Pen Magenta => GetPen(Color.Magenta);
        public static Pen Maroon => GetPen(Color.Maroon);
        public static Pen MediumAquamarine => GetPen(Color.MediumAquamarine);
        public static Pen MediumBlue => GetPen(Color.MediumBlue);
        public static Pen MediumOrchid => GetPen(Color.MediumOrchid);
        public static Pen MediumPurple => GetPen(Color.MediumPurple);
        public static Pen MediumSeaGreen => GetPen(Color.MediumSeaGreen);
        public static Pen MediumSlateBlue => GetPen(Color.MediumSlateBlue);
        public static Pen MediumSpringGreen => GetPen(Color.MediumSpringGreen);
        public static Pen MediumTurquoise => GetPen(Color.MediumTurquoise);
        public static Pen MediumVioletRed => GetPen(Color.MediumVioletRed);
        public static Pen MidnightBlue => GetPen(Color.MidnightBlue);
        public static Pen MintCream => GetPen(Color.MintCream);
        public static Pen MistyRose => GetPen(Color.MistyRose);
        public static Pen Moccasin => GetPen(Color.Moccasin);

        public static Pen NavajoWhite => GetPen(Color.NavajoWhite);
        public static Pen Navy => GetPen(Color.Navy);

        public static Pen OldLace => GetPen(Color.OldLace);
        public static Pen Olive => GetPen(Color.Olive);
        public static Pen OliveDrab => GetPen(Color.OliveDrab);
        public static Pen Orange => GetPen(Color.Orange);
        public static Pen OrangeRed => GetPen(Color.OrangeRed);
        public static Pen Orchid => GetPen(Color.Orchid);

        public static Pen PaleGoldenrod => GetPen(Color.PaleGoldenrod);
        public static Pen PaleGreen => GetPen(Color.PaleGreen);
        public static Pen PaleTurquoise => GetPen(Color.PaleTurquoise);
        public static Pen PaleVioletRed => GetPen(Color.PaleVioletRed);
        public static Pen PapayaWhip => GetPen(Color.PapayaWhip);
        public static Pen PeachPuff => GetPen(Color.PeachPuff);
        public static Pen Peru => GetPen(Color.Peru);
        public static Pen Pink => GetPen(Color.Pink);
        public static Pen Plum => GetPen(Color.Plum);
        public static Pen PowderBlue => GetPen(Color.PowderBlue);
        public static Pen Purple => GetPen(Color.Purple);

        public static Pen Red => GetPen(Color.Red);
        public static Pen RosyBrown => GetPen(Color.RosyBrown);
        public static Pen RoyalBlue => GetPen(Color.RoyalBlue);

        public static Pen SaddleBrown => GetPen(Color.SaddleBrown);
        public static Pen Salmon => GetPen(Color.Salmon);
        public static Pen SandyBrown => GetPen(Color.SandyBrown);
        public static Pen SeaGreen => GetPen(Color.SeaGreen);
        public static Pen SeaShell => GetPen(Color.SeaShell);
        public static Pen Sienna => GetPen(Color.Sienna);
        public static Pen Silver => GetPen(Color.Silver);
        public static Pen SkyBlue => GetPen(Color.SkyBlue);
        public static Pen SlateBlue => GetPen(Color.SlateBlue);
        public static Pen SlateGray => GetPen(Color.SlateGray);
        public static Pen Snow => GetPen(Color.Snow);
        public static Pen SpringGreen => GetPen(Color.SpringGreen);
        public static Pen SteelBlue => GetPen(Color.SteelBlue);

        public static Pen Tan => GetPen(Color.Tan);
        public static Pen Teal => GetPen(Color.Teal);
        public static Pen Thistle => GetPen(Color.Thistle);
        public static Pen Tomato => GetPen(Color.Tomato);
        public static Pen Turquoise => GetPen(Color.Turquoise);

        public static Pen Violet => GetPen(Color.Violet);

        public static Pen Wheat => GetPen(Color.Wheat);
        public static Pen White => GetPen(Color.White);
        public static Pen WhiteSmoke => GetPen(Color.WhiteSmoke);

        public static Pen Yellow => GetPen(Color.Yellow);
        public static Pen YellowGreen => GetPen(Color.YellowGreen);

    }
}
