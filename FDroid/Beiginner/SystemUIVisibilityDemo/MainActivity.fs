(*
    F# Implementation of Xamarin System UI Demo
    Original C# Version: https://github.com/xamarin/monodroid-samples/tree/master/SystemUIVisibilityDemo
    Port by: Francisco
*)

namespace SystemUIVisibilityDemo

    open System

    open Android.App
    open Android.Content
    open Android.OS
    open Android.Runtime
    open Android.Views
    open Android.Widget
    open Android.OS


    [<Activity (Label = "SystemUIVisibilityDemo", MainLauncher = true)>]
    type MainActivity () =
        inherit Activity ()

        override this.OnCreate (bundle) =
            base.OnCreate (bundle)

            // Set our view from the "main" layout resource
            this.SetContentView (Resource_Layout.Main)

            let tv = this.FindViewById<TextView> (Resource_Id.systemUiFlagTextView);
            let hideNavButton = this.FindViewById<Button> (Resource_Id.hideNavigation);
            let visibleButton = this.FindViewById<Button> (Resource_Id.visibleButton);

            //On click you hide it
            hideNavButton.Click.Add(fun args ->
                
                tv.SystemUiVisibility <- StatusBarVisibility.Hidden
            )

            //On click it becomes visible
            visibleButton.Click.Add(fun args ->
                
                tv.SystemUiVisibility <- StatusBarVisibility.Visible 
            )

            //It prints out a message saying if its visible or not   
            tv.SystemUiVisibilityChange.Add(fun args ->
                tv.Text <- String.Format ("Visibility = {0}", args.Visibility);
            )










