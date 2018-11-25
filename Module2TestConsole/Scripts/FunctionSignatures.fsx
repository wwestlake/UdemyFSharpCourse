open System

// Let's examin some functions and their signatures

//val add : a:int -> b:int -> int
let add a b = a + b

// val mul : a:float -> b:float -> float
let mul (a:float) (b:float) = a * b

//val log : s:'a -> unit
//val log : s:string -> unit

let log s = printfn "%s" s

//val randomAlphanumericString : unit -> String
let randomAlphanumericString () =
    let chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    let random = new Random()
    seq {
        for i in {0..7} do
            yield chars.[random.Next(chars.Length)]
    } |> Seq.toArray |> (fun x -> new String(x))


//val max : list:int list -> int
let max (list: int list) = list |> List.max

randomAlphanumericString ()

//val CurriedFunction : a:int -> (unit -> int)
let CurriedFunction a =
    let innerFunc () =
        a + 1
    innerFunc

CurriedFunction 3 ()

let add1 = CurriedFunction 3

add1 ()

