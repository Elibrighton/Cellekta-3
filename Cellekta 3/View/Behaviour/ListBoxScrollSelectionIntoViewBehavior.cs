using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace Cellekta_3.View.Behavior
{
    public class ListBoxScrollSelecionIntoViewBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private static void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox == null)
            {
                return;
            }

            Action action = () =>
            {
                var selectedItem = listBox.SelectedItem;

                if (selectedItem != null)
                {
                    listBox.ScrollIntoView(selectedItem);
                }
            };

            listBox.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
        }
    }
}
