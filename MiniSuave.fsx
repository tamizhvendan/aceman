// Show your Awesomeness!

type HttpMethod = 
| Get 
| Put 
| Delete 
| Post

type Header = string * string

type Request = {
  Path : string
  Headers : Header list
  HttpMethod : HttpMethod
}

type StatusCode =
| Ok
| BadRequest
| NotFound

type Response = {
  Content : string
  Headers : Header list
  StatusCode : StatusCode
}

type Context = {
  Request : Request
  Response : Response
}

type WebPart = Context -> Async<Context option>

let response statusCode content ctx =
  let response = 
    {ctx.Response with 
      StatusCode = statusCode
      Content = content}  
  {ctx with Response = response} |> Some |> async.Return

let OK = response Ok
let BAD_REQUEST = response BadRequest
let NOT_FOUND = response NotFound
let GET ctx = 
  match ctx.Request.HttpMethod = Get with
  | true -> ctx |> Some |> async.Return 
  | _ -> None |> async.Return

let POST ctx = 
  match ctx.Request.HttpMethod = Post with
  | true -> ctx |> Some |> async.Return 
  | _ -> None |> async.Return

let PUT ctx = 
  match ctx.Request.HttpMethod = Put with
  | true -> ctx |> Some |> async.Return 
  | _ -> None |> async.Return

let DELETE ctx = 
  match ctx.Request.HttpMethod = Delete with
  | true -> ctx |> Some |> async.Return 
  | _ -> None |> async.Return