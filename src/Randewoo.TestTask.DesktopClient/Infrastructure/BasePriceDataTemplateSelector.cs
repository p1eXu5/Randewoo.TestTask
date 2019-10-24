using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Randewoo.TestTask.DesktopClient.ViewModels;

namespace Randewoo.TestTask.DesktopClient.Infrastructure
{
    public class BasePriceDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( container is FrameworkElement element ) {

                var filter = "";
                string template = "dt_DefaultBasePrice";

                if ( VisualTreeHelper.GetChildrenCount( container ) > 0 ) {
                    var d = VisualTreeHelper.GetChild( container, 0 );
                    filter = (string)d.GetValue( FilterElement.FilterProperty );
                }


                if ( item is ProductViewModel pvm ) {


                    if ( filter == Filters.All ) {

                        if ( pvm.MinPrice.HasValue && pvm.MinPrice < pvm.Price ) {
                            template = "dt_DangerBasePrice";
                        }

                    }
                    else if ( filter == Filters.MinOthers || filter == Filters.MinSelf ) {

                        if (  pvm.MinPrice.HasValue && pvm.MinPrice < pvm.Price ) {
                            template = "dt_DangerBasePrice";
                        }
                        else {
                            template = "dt_SuccessBasePrice";
                        }
                    }
                }

                return element.FindResource( template ) as DataTemplate;
            }
            return base.SelectTemplate( item, container );
        }
    }
}
