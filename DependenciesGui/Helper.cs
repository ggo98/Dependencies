using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dependencies
{
    internal class Helper
    {
        // Recursively set font for all controls in a container (Window, Grid, etc.)
        public static void SetFontForAllChildren(DependencyObject parent, FontFamily fontFamily, double fontSize)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Control control)
                {
                    control.FontFamily = fontFamily;
                    control.FontSize = fontSize;
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.FontFamily = fontFamily;
                    textBlock.FontSize = fontSize;
                }

                SetFontForAllChildren(child, fontFamily, fontSize);
            }
        }

        // Usage:
//        SetFontForAllChildren(this, new FontFamily("Segoe UI"), 14);

        public static void SetFontForWholeApp(FontFamily fontFamily, double fontSize)
        {
            // Change the application-wide style
            var newStyle = new Style(typeof(Control));
            newStyle.Setters.Add(new Setter(Control.FontFamilyProperty, fontFamily));
            newStyle.Setters.Add(new Setter(Control.FontSizeProperty, fontSize));

            Application.Current.Resources[typeof(Control)] = newStyle;

            // For TextBlocks specifically
            var textBlockStyle = new Style(typeof(TextBlock));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, fontFamily));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, fontSize));

            Application.Current.Resources[typeof(TextBlock)] = textBlockStyle;
        }
    }
}
