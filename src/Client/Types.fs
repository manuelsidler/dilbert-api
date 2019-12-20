module DilbertApi.Types

open Shared
open System

type State = { 
    Comics : Comic list
    Tag : string
}

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
    | Search
    | Clear
    | SearchCompleted of Comic list
    | SearchFailure of Exception
    | SetSearchTag of string