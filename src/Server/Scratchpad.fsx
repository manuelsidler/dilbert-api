#I @"C:\Users\Manuel\.nuget\packages" // FIXME: current user?
#r @"fsharp.data\3.3.2\lib\netstandard2.0\FSharp.Data.dll"

open FSharp.Data

type DilbertComic = 
    { Title : string
      ImageUrl : string  
      Tags : string list }

type SearchComic = string -> DilbertComic list

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

let searchComic : SearchComic =
    fun tag ->
        sprintf "https://dilbert.com/search_results?terms=%s" tag
        |> HtmlDocument.Load
        |> getComicContainers
        |> List.map (fun comicContainer -> { Title = comicContainer |> getTitle
                                             ImageUrl = comicContainer |> getImage
                                             Tags = comicContainer |> getTags } )

let comics = searchComic "Boss"