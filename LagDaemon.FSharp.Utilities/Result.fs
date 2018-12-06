namespace LagDaemon.FSharp.Utilities

module Result =
    
    
    type Result<'Tsuccess, 'Tfailure> =
        | Success of 'Tsuccess
        | Failure of 'Tfailure
    
    let bind f input =
        match input with
        | Success s -> f s
        | Failure f -> Failure f
    
    let (>>=) input func =
        bind func input
    
    
