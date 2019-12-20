module DilbertApi.State

open DilbertApi.Types
open Elmish

let initialState() = 
    let initState = {
        Comics = []
        Tag = ""
    }

    initState, Cmd.ofMsg Clear

let update (msg: Msg) (prevState: State) = 
    match msg with
    | Search ->
        prevState, Server.getComics prevState.Tag
    | SearchCompleted comics ->
        let nextState = { prevState with Comics = comics }
        nextState, Cmd.none
    | SearchFailure ex ->
        prevState, Cmd.none // TODO: display error?
    | SetSearchTag tag ->
        let nextState = { prevState with Tag = tag }
        nextState, Cmd.none
    | Clear ->
        let prevState = { Comics = []; Tag = "" }
        prevState, Cmd.none