using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace JHControl.Panel
{
    /// <summary>
    /// A drawer control class that can provide a similar functionality of
    /// android's navigation drawer and UWP's SplitView.
    /// </summary>
    public sealed class DrawerControl : ContentControl
    {
        #region Constructors
        public DrawerControl()
        {
            DefaultStyleKey = typeof(DrawerControl);
        }
        #endregion

        #region Local Variables
        /// <summary>
        /// The semi transparent menu underlay grid that appears when the menu is opened.
        /// </summary>
        private Grid menuUnderlayGrid;

        /// <summary>
        /// The element that holds the menu.
        /// </summary>
        private FrameworkElement menuElement;

        /// <summary>
        /// The drawer open state of the menu.
        /// </summary>
        VisualState drawerOpenState;

        /// <summary>
        /// The drawer closed state of the menu.
        /// </summary>
        VisualState drawerClosedState;
        #endregion

        #region Dependency Properties
        /// <summary>
        /// The dependency property that controls the value of MenuControl.
        /// </summary>
        public static readonly DependencyProperty MenuControlProperty = DependencyProperty.Register("MenuControl", typeof(object), typeof(DrawerControl), null);

        /// <summary>
        /// The dependency property that controls the value of ContentHeader.
        /// </summary>
        public static readonly DependencyProperty ContentHeaderProperty = DependencyProperty.Register("ContentHeader", typeof(object), typeof(DrawerControl), null);

        /// <summary>
        /// The dependency property that controls the value of the IsDrawerOpen.
        /// </summary>
        public static readonly DependencyProperty IsDrawerOpenProperty = DependencyProperty.Register("IsDrawerOpen", typeof(bool), typeof(DrawerControl), new PropertyMetadata(false, new PropertyChangedCallback(IsDrawerOpenChanged)));
        #endregion

        #region Properties
        /// <summary>
        /// The property that holds the menu control.
        /// </summary>
        public object MenuControl
        {
            get
            {
                return GetValue(MenuControlProperty);
            }
            set
            {
                SetValue(MenuControlProperty, value);
            }
        }

        /// <summary>
        /// The property that holds the content header.
        /// </summary>
        public object ContentHeader
        {
            get
            {
                return GetValue(ContentHeaderProperty);
            }
            set
            {
                SetValue(ContentHeaderProperty, value);
            }
        }

        /// <summary>
        /// The property that determines whether the header is open or not.
        /// </summary>
        public bool IsDrawerOpen
        {
            get
            {
                return (bool)GetValue(IsDrawerOpenProperty);
            }
            set
            {
                changeVisualState(true);
                SetValue(IsDrawerOpenProperty, value);
            }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Called when the template is applied.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            menuUnderlayGrid = GetTemplateChild("Menu_View_Underlay") as Grid;
            drawerOpenState = GetTemplateChild("MenuOpened") as VisualState;
            drawerClosedState = GetTemplateChild("MenuClosed") as VisualState;
            menuElement = GetTemplateChild("MenuControl") as FrameworkElement;
            if (menuUnderlayGrid != null)
            {
                menuUnderlayGrid.Tapped += MenuUnderlayGrid_Tapped;
            }
            if (drawerClosedState != null)
            {
                drawerClosedState.Storyboard.Completed += HideMenuAnimation_Completed;
            }
            if (menuElement != null)
            {
                menuElement.Loaded += MenuElement_Loaded;
            }
            changeVisualState(false);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Called when the IsDrawerOpen property is changed.
        /// </summary>
        /// <param name="sender">
        /// The instance of DrawerControl that sent the event.
        /// </param>
        /// <param name="e">
        /// The property changed event arguments.
        /// </param>
        private static void IsDrawerOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DrawerControl source = sender as DrawerControl;
            source.IsDrawerOpen = (bool)e.NewValue;
        }

        /// <summary>
        /// Called when the hide menu animation is completed.
        /// </summary>
        private void HideMenuAnimation_Completed(object sender, object e)
        {
            menuUnderlayGrid.Visibility = Visibility.Collapsed;
            menuElement.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Called when the menu underlay grid is tapped.
        /// This should close the navigation drawer.
        /// </summary>
        private void MenuUnderlayGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsDrawerOpen = false;
        }

        /// <summary>
        /// Called when the menu element is loaded.
        /// </summary>
        private void MenuElement_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation translate = (DoubleAnimation)drawerClosedState.Storyboard.Children[1];
            translate.To = 0 - menuElement.ActualWidth;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Change the visual state of the drawer.
        /// </summary>
        /// <param name="animated">
        /// whether the visual state chenge need to be animated or not.
        /// </param>
        private void changeVisualState(bool animated)
        {
            if (IsDrawerOpen)
            {
                if (menuUnderlayGrid != null)
                {
                    menuUnderlayGrid.Visibility = Visibility.Visible;
                }
                if (menuElement != null)
                {
                    menuElement.Visibility = Visibility.Visible;
                }
                VisualStateManager.GoToState(this, "MenuOpened", animated);
            }
            else
            {
                VisualStateManager.GoToState(this, "MenuClosed", animated);
            }
        }
        #endregion
    }
}
