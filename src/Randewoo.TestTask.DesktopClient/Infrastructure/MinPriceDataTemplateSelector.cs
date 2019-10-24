using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public class MinPriceDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( container is FrameworkElement element ) {

                var filter = "";
                string template = "dt_DefaultMinPrice";

                if ( VisualTreeHelper.GetChildrenCount( container ) > 0 ) {
                    var d = VisualTreeHelper.GetChild( container, 0 );
                    filter = (string)d.GetValue( FilterElement.FilterProperty );
                }


                if ( item is ProductViewModel pvm ) {


                    if ( filter == Filters.All ) {

                        if ( pvm.MinPrice.HasValue && pvm.MinPrice < pvm.Price ) {
                            template = "dt_SuccessMinPrice";
                        }

                    }
                    else if ( filter == Filters.MinOthers ) {

                        if (  pvm.MinPrice.HasValue && pvm.MinPrice < pvm.Price ) {
                            template = "dt_SuccessMinPrice";
                        }
                        else {
                            template = "dt_NoMinPrice";
                        }
                    }
                    else if ( filter == Filters.MinSelf ) {


                    }
                }

                return element.FindResource( template ) as DataTemplate;
            }
            return base.SelectTemplate( item, container );
        }
    }
}
