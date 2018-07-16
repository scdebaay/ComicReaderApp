using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComicReaderApp.Behaviors
{
    class ListViewItemTappedBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ListViewItemTappedBehavior));
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(ListViewItemTappedBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemTapped += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemTapped -= OnListViewItemTapped;
            AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            if (Command == null)
            { return; }

            object parameter = Converter.Convert(e, typeof(object), null, null);
            if (Command.CanExecute(parameter))
                Command.Execute(parameter);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
