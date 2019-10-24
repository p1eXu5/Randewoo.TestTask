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
    public class MinPriceDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( container is FrameworkElement element ) {

                var filter = (string)container.GetValue( FilterElement.FilterProperty );

                if ( item is ProductViewModel pvm ) {

                    if ( filter == Filters.All ) {

                    }
                    else if ( filter == Filters.MinOthers ) {

                    }
                    else if ( filter == Filters.MinSelf ) {

                    }
                }
            }
            return base.SelectTemplate( item, container );
        }
    }
}
