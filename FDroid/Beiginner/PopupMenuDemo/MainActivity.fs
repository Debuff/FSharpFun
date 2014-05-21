(*
    F# Implementation of Xamarin Popup Menu Demo
    Original C# Version: https://github.com/xamarin/monodroid-samples/tree/master/PopupMenuDemo
    Port by: Francisco
*)

namespace PopupMenuDemo

    open System

    open Android.App
    open Android.Content
    open Android.OS
    open Android.Runtime
    open Android.Views
    open Android.Widget
    open Android.OS

    // Our Main Activity
    [<Activity (Label = "PopupMenuDemo", MainLauncher = true)>]
    type MainActivity () =
        inherit Activity ()

        override this.OnCreate (bundle) =
            base.OnCreate (bundle)

            // Set our view from the "main" layout resource
            this.SetContentView (Resource_Layout.Main)

            let showPopupMenu = this.FindViewById<Button>(Resource_Id.popupButton)

            showPopupMenu.Click.Add(fun args ->
                let menu : PopupMenu = new PopupMenu (this, showPopupMenu)
                
                // with Android 3 need to use MenuInfater to inflate the menu
                //menu.MenuInflater.Inflate (Resource.Menu.popup_menu, menu.Menu);

                // with Android 4 Inflate can be called directly on the menu
                menu.Inflate (Resource_Menu.popup_menu)
                
                menu.MenuItemClick.Add(fun args -> Console.WriteLine ("{0} Selected", args.Item.TitleFormatted) )
                
                // Android 4 now has the DismissEvent
                menu.DismissEvent.Add(fun args -> Console.WriteLine ("Menu Dismissed") )
                
                menu.Show ()
            )

