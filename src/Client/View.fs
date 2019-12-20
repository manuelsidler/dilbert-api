module DilbertApi.View

open Shared
open DilbertApi.Types
open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Fulma

let button txt isPrimary onClick =
    Button.button
        [ Button.IsFullWidth
          Button.Color isPrimary
          Button.OnClick onClick ]
        [ str txt ]

let primaryButton txt onClick =
    button txt IsPrimary onClick

let searchBox dispatch =
    Columns.columns [ ]
        [ Column.column [ Column.Width (Screen.All, Column.Is6) ]
            [ Columns.columns [ ]
                [ Column.column [ ]
                    [ Input.text [ Input.Placeholder "Ex: Boss" 
                                   Input.OnChange(fun e -> dispatch (SetSearchTag e.Value)) ] ] ] ]
          Column.column [ ]
            [ Columns.columns [ ]
                [ Column.column [ ]
                    [ primaryButton "Search" (fun _ -> dispatch Search) ] ] ] ]

let renderComic (comic : Comic) =
    div
        [ ]
        [ p [ ] [ str comic.Title ] ]

let render (state : State) dispatch =
    let comics =
        state.Comics
        |> List.map renderComic

    div []
        [ Navbar.navbar [ Navbar.Color IsPrimary ]
            [ Navbar.Item.div [ ]
                [ Heading.h2 [ ]
                    [ str "Dilbert API" ] ] ]

          Container.container []
            [ searchBox dispatch ]

          Container.container []
            [ yield! comics ]

          Footer.footer [ ]
                [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                    [ str "Footer" ] ] ]
