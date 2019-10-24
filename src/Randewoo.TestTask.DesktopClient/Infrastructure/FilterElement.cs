using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public class FilterElement
    {

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.RegisterAttached(
                "Filter", 
                typeof(string), 
                typeof(FilterElement), 
                new FrameworkPropertyMetadata( null,FrameworkPropertyMetadataOptions.AffectsRender ));

        public static void SetFilter( DependencyObject d, string value )
        {
            d.SetValue( FilterElement.FilterProperty, value );
        }

        public static string GetFilter( DependencyObject d )
        {
            return ( string )d.GetValue( FilterElement.FilterProperty );
        }
    }
}
