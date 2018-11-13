// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =

    let rec innerFunction () =
        printfn "Enter a phrase, must be greater than 6 charactes long"
    
        let line = Console.ReadLine()

        if line.Length > 6 then
            printfn "Correct length: '%s'" line
        else 
            printfn "I said the phrase must be greater than 6 characters"
            innerFunction ()

    innerFunction ()


    printfn "Done: press any key to exit"
    Console.ReadKey() |> ignore
    0 // return an integer exit code
