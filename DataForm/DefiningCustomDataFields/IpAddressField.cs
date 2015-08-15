using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace WpfApplication1
{
    public class IpAddressField : DataFormDataField
    {
        protected override DependencyProperty GetControlBindingProperty()
        {
            return RadMaskedTextInput.ValueProperty;
        }

        protected override Control GetControl()
        {
            DependencyProperty dependencyProperty = this.GetControlBindingProperty();
            var input = new RadMaskedTextInput();
            input.Style = (Style) FindResource("IpMaskedInputStyle");

            if (this.DataMemberBinding != null)
            {
                var binding = this.DataMemberBinding;
                input.SetBinding(dependencyProperty, binding);
            }
            input.SetBinding(RadMaskedTextInput.IsEnabledProperty, new Binding("IsReadOnly") { Source = this, Converter = new InvertedBooleanConverter() });

            return input;
        }
    }
}
