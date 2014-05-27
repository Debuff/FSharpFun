(*
    F# Implementation of Xamarin ICS Tabs Demo
    Original C# Version: https://github.com/xamarin/monodroid-samples/tree/master/HelloTabsICS
    Port by: Francisco
*)

namespace HelloTabsICS

    open System

    open Android.App
    open Android.Content
    open Android.OS
    open Android.Runtime
    open Android.Views
    open Android.Widget

    type SampleTabFragment () = 
        inherit Fragment()

        override this.OnCreateView (inflater : LayoutInflater, container : ViewGroup, savedInstanceState : Bundle) =
            base.OnCreateView (inflater, container, savedInstanceState) |> ignore

            let view = inflater.Inflate (Resource_Layout.Tab, container, false)
            let sampleTextView = view.FindViewById<TextView> (Resource_Id.sampleTextView)           
            sampleTextView.Text <- "sample fragment text"
            view

    type SampleTabFragment2 () =
        inherit Fragment()

        override this.OnCreateView (inflater : LayoutInflater, container : ViewGroup, savedInstanceState : Bundle) =
            base.OnCreateView(inflater, container, savedInstanceState) |> ignore

            let view = inflater.Inflate(Resource_Layout.Tab, container, false)
            let sampleTextView = view.FindViewById<TextView>(Resource_Id.sampleTextView)
            sampleTextView.Text <- "sample fragment text 2"
            view



    [<Activity (Label = "HelloTabsICS", MainLauncher = true)>]
    type MainActivity () as this =
        inherit Activity ()

        [<DefaultValue>]
        val mutable e : ActionBar.TabEventArgs

        let AddTab (tabText : string, iconResourceId : int, view : Fragment) = 
            let tab = this.ActionBar.NewTab ()      
            tab.SetText (tabText) |> ignore
            tab.SetIcon (Resource_Drawable.ic_tab_white) |> ignore

            // must set event handler before adding tab
            tab.TabSelected.Add (fun e ->
                let fragment = this.FragmentManager.FindFragmentById(Resource_Id.fragmentContainer)
                if (fragment <> null) then
                    e.FragmentTransaction.Remove(fragment) |> ignore
                e.FragmentTransaction.Add (Resource_Id.fragmentContainer, view) |> ignore
            )

            tab.TabUnselected.Add(fun e ->
                e.FragmentTransaction.Remove(view) |> ignore
            )
            
            this.ActionBar.AddTab (tab)


        override this.OnSaveInstanceState(outState : Bundle) =
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex)
            base.OnSaveInstanceState(outState)


        override this.OnCreate (bundle) =
            base.OnCreate (bundle)

            // Set our view from the "main" layout resource
            this.SetContentView (Resource_Layout.Main)

            this.ActionBar.NavigationMode <- ActionBarNavigationMode.Tabs
                 
            AddTab ("Tab 1", Resource_Drawable.ic_tab_white, new SampleTabFragment ())
            AddTab ("Tab 2", Resource_Drawable.ic_tab_white, new SampleTabFragment2 ())

            if (bundle <> null) then
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")))


