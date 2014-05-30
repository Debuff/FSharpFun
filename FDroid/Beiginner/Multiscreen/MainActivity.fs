(*
    F# Implementation of Xamarin ICS Tabs Demo
    Original C# Version: https://github.com/xamarin/monodroid-samples/tree/master/HelloTabsICS
    Port by: Francisco
*)

namespace Multiscreen

    open System
    open System.Collections.Generic
    open System.Linq
    open System.Text

    open Android.App
    open Android.Content
    open Android.OS
    open Android.Runtime
    open Android.Views
    open Android.Widget

    [<Activity (Label = "SecondActivity")>]
    type SecondActivity () =

        inherit Activity()

        override x.OnCreate(bundle) =

            base.OnCreate (bundle)
            // Load the UI defined in Second.axml
            x.SetContentView(Resource_Layout.Second)

            //Coustom opperator for Coalescing Null
            let (|??) left_Side right_Side = (if left_Side = null then right_Side 
                                             else left_Side)
            // Get a reference to the TextView
            let mutable label = x.FindViewById<TextView> (Resource_Id.screen2Label)

            // Populate the TextView with the data that was added to the intent in FirstActivity 
            label.Text <- x.Intent.GetStringExtra("FirstData") |?? "Data not available"


    [<Activity (Label = "MainActivity", MainLauncher = true)>]
    type MainActivity () =
        inherit Activity ()

        override this.OnCreate (bundle) =
            base.OnCreate (bundle)

            // Set our view from the "main" layout resource
            this.SetContentView (Resource_Layout.Main)

            let showSecond = this.FindViewById(Resource_Id.showSecond)
            showSecond.Click.Add(fun args ->

                let second = new Intent( this, typeof< SecondActivity  > )

                second.PutExtra( "FirstData", "Data from FirstActivity" )
                this.StartActivity(second)
                )





