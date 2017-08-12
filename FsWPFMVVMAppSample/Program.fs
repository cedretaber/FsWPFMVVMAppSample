open System
open System.Windows

open FsXaml

type MainWindow = XAML<"MainWindow.xaml">

open ViewModule
open ViewModule.FSharp

type DataModel () as this =
  inherit ViewModelBase ()

  let _x = this.Factory.Backing (<@ this.x @>, 0)
  let _y = this.Factory.Backing (<@ this.y @>, 0)

  do
    this.DependencyTracker.AddPropertyDependencies
      (<@@ this.z @@>, [ <@@ this.x @@>; <@@ this.y @@> ])
  
  member this.x with get () = _x.Value and set v = _x.Value <- v
  member this.y with get () = _y.Value and set v = _y.Value <- v
  member this.z = this.x + this.y

[<STAThread>]
[<EntryPoint>]
let main _ =
  let mainWindow = MainWindow () in
  let model = DataModel () in
  mainWindow.DataContext <- model
  (new Application()).Run mainWindow
