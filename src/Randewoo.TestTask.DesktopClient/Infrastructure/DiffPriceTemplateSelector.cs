using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public class DiffPriceTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( container is FrameworkElement element ) {

                if ( item is ProductViewModel pvm ) {

                    if ( pvm.DiffPrice < 0.0 ) {
                        var t = element.FindResource( "dt_NegateDiffPrice" ) as DataTemplate;
                        return t;
                    }

                    if ( pvm.DiffPrice > 0.0 ) {
                        var t = element.FindResource( "dt_PositiveDiffPrice" ) as DataTemplate;
                        return t;
                    }

                    var defaultTemplate = element.FindResource( "dt_DefaultDiffPrice" ) as DataTemplate;
                    return defaultTemplate; 
                }
            }
            return base.SelectTemplate( item, container );
        }
    }
}
