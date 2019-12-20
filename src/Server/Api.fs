module DilbertApi.Api

open DataAccess
open Shared

let getComics (tag : string) =
    async {
        let comics = tag |> searchComics
        return comics
    }

let dilbertApi : IDilbertApi = {
    getComics = getComics
}