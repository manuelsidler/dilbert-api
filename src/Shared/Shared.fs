namespace Shared

type Comic = { 
    Title : string
    ImageUrl : string  
    Tags : string list 
}

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type IDilbertApi = { 
    getComics : string -> Async<Comic list> 
}
