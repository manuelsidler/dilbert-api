module DilbertApi.Server

open Shared
open Elmish
open DilbertApi.Types
open Fable.Remoting.Client

let api : IDilbertApi =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IDilbertApi>

let getComics(tag : string) =
    Cmd.OfAsync.either
        api.getComics tag
        SearchCompleted
        SearchFailure