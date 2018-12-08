namespace LagDaemon.FSharp.Utilities

module Result =
    open System

    type Result<'Tsuccess, 'Tfailure> =
        | Success of 'Tsuccess
        | Failure of 'Tfailure

    let succeed x = Success x
    let fail x = Failure x

    let either successFunc failureFunc input =
        match input with
        | Success s -> successFunc s
        | Failure f -> failureFunc f

    let bind f =
        either f fail
     
    let (>>=) input func =
        bind func input
    
    let (>=>) func1 func2 =
        func1 >> bind func2

    let lift f =
        f >> succeed

    let map f =
        either (f >> succeed) fail

    let tee f x =
        f x ; x

    let tryCatch exnHandler func input =
        try
            func input |> succeed
        with
        | ex -> exnHandler ex |> fail



    






    