(*
    F# Implementation of Xamarin Switch Demo
    Original C# Version: https://github.com/xamarin/monodroid-samples/tree/master/SwitchDemo
    Port by: Francisco
*)

namespace SwitchDemo

    open System

    open Android.App
    open Android.Content
    open Android.OS
    open Android.Runtime
    open Android.Views
    open Android.Widget
            
    //Our Main Activty
    [<Activity (Label = "SwitchDemo", MainLauncher = true)>]
    type MainActivity () =
        inherit Activity ()
    
        override this.OnCreate (bundle) =
            base.OnCreate (bundle)
            // Set our view from the "main" layout resource
            this.SetContentView (Resource_Layout.Main)

            //We get our switch
            let mySwitch = this.FindViewById<Switch> (Resource_Id.monitored_switch)

            mySwitch.CheckedChange.Add(fun args ->
                //Coustom opperator for "?:"
                let (|?) left_Side right_Side = (if args.IsChecked then left_Side 
                                                 else right_Side)

                let toast = Toast.MakeText(this, "Your answer is...." + ("Correct" |? "Incorrect"), ToastLength.Short)
                toast.Show()
            
            )






