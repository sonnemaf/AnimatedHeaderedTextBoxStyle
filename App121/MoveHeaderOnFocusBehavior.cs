using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App121 {

    public class MoveHeaderOnFocusBehavior : Behavior<TextBox> {

        private bool _hasFocus;

        private void AssociatedObject_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            _hasFocus = true;
            UpdateState(true);
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            UpdateState(false);
            this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded;
        }

        private void AssociatedObject_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            _hasFocus = false;
            UpdateState(true);
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e) {
            UpdateState(false);
        }

        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.Loaded += this.AssociatedObject_Loaded;
            this.AssociatedObject.GotFocus += this.AssociatedObject_GotFocus;
            this.AssociatedObject.LostFocus += this.AssociatedObject_LostFocus;
            this.AssociatedObject.TextChanged += this.AssociatedObject_TextChanged;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.GotFocus -= this.AssociatedObject_GotFocus;
            this.AssociatedObject.LostFocus -= this.AssociatedObject_LostFocus;
            this.AssociatedObject.TextChanged -= this.AssociatedObject_TextChanged;
        }


        private void UpdateState(bool animate) {
            if (_hasFocus || !string.IsNullOrEmpty(this.AssociatedObject.Text)) {
                VisualStateManager.GoToState(this.AssociatedObject, "NotEmpty", animate);
            } else {
                VisualStateManager.GoToState(this.AssociatedObject, "Empty", animate);
            }
        }

    }
}
