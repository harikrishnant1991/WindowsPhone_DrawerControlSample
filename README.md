# DrawerControl for Windows and Windows Phone
Drawer control is a control for Windows and Windows Phone 8.1+ that replicate the functionality of Navigation Drawer in android and SplitView in UWP.

## How to Use?

1. Include the `DrawerControl.cs` file in your project.
2. In your applictaion's `Generic.xaml` file, include the namespace for `DrawerControl`, by adding this line:
``` XML
xmlns:jhp="using:JHControl.Panel"
```

So that the `ResourceDictionary` tag now looks like:

``` XML
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:jhp="using:JHControl.Panel">
```

3. Now copy and paste the `DrawerControl` style into your application's `Generic.xaml`.

4. In the `Page`/`UserControl` where you need to use the `DrawerControl`, add the reference to the namespace and then add the following lines:

``` XML
<jhp:DrawerControl x:Name="MainDrawerControl">
        <!--
        This is the content header. You can define the toolbar here.
        If this is not implemented, the content would fill the space, hence this is not required.
        -->
        <jhp:DrawerControl.ContentHeader>
            <Grid Background="Blue"
                  Height="50"
                  HorizontalAlignment="Stretch">
            </Grid>
        </jhp:DrawerControl.ContentHeader>
        <!--
        This is the drawer element.
        -->
        <jhp:DrawerControl.MenuControl>
            <Grid Width="300"
                  VerticalAlignment="Stretch"
                  Background="Red" />
        </jhp:DrawerControl.MenuControl>
        <!--
        This is the main content of the control.
        -->
        <jhp:DrawerControl.Content>
            <Grid HorizontalAlignment="Stretch"
                  VerticalAlignment="Stretch"
                  Background="Green" />
        </jhp:DrawerControl.Content>
    </jhp:DrawerControl>
```

The DrawerControl has a `IsDrawerOpen` property that can be used to open and close the drawer as required.

## Scopes of improvement:

The drawer won't open/close on swipe. This shall be included in future updates.