module DataAccess

open FSharp.Data
open Shared

type SearchComics = string -> Comic list

let getComicContainers (doc:HtmlDocument) = doc.CssSelect("div.img-comic-container")
    
let getImageElement (comicContainer:HtmlNode) =
    comicContainer.CssSelect("div.img-comic-container > a.img-comic-link > img.img-comic")
    |> List.head

let getImageAttribute attributeName (imageElement:HtmlNode) = 
    imageElement.AttributeValue(attributeName)

let getTitle = getImageElement >> getImageAttribute "alt"

let getImage = getImageElement >> getImageAttribute "src"

let getTags (comicContainer:HtmlNode) =
    comicContainer.CssSelect("div.meta-info-container > p.comic-tags > a.link")
    |> List.map (fun x -> x.InnerText())

let searchComics : SearchComics =
    fun tag ->
        sprintf "https://dilbert.com/search_results?terms=%s" tag
        |> HtmlDocument.Load
        |> getComicContainers
        |> List.map (fun comicContainer -> { Title = comicContainer |> getTitle
                                             ImageUrl = comicContainer |> getImage
                                             Tags = comicContainer |> getTags } )