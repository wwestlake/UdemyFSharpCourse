
open System

// some more types, List and Tuple

// a tuple is a group of values enclosed in parentheses (1, "bill", "john", (fun a -> a + 1)) 

// val it : int * string * string * (int -> int) =
//   (1, "bill", "john", <fun:it@8>)

let myTuple = (1, "bill", "john", (fun a -> a + 1)) 

let funcMatch (a,b,c,d) =
    printfn "%A, %A, %A, %A" a b c (d 4)


let func1 tup =
    match tup with
    | (a,b,c,d) -> printfn "%A, %A, %A, %A" a b c (d 4)
    
func1 myTuple

// list of ints
[1;2;3;4;5]

// list of strings
["a";"b";"c"]

// list of functions (must be the same exact type)
[(fun a -> a + 1);(fun b -> b * 2)]

// this is an error (they are not the same type)
// -- [(fun () -> "bill"); (fun a -> a + 1)]

// a tuple of different function types
((fun () -> "bill"), (fun a -> a + 1))


// pattern matching
let func2 a =
    match a with
    | (x,y) -> printfn "%d, %d" x y
 
func2 (2,3)

// recursion
let rec recursive list accum =
    match list with
    | [] -> accum
    | head :: tail -> recursive tail (accum + head)

recursive [1;2;3;4;5] 0
recursive [1..1000] 0

let sum list =
    let rec inner list accum =
        match list with
        | head :: tail -> inner tail (accum + head)
        | [] -> accum
    inner list 0

sum [1..100]

// tail recursion in F#
// a recursive function in F# is tail recursive IFF there is no more work to be done after the recursive call

// non tail recursive function

// Process is terminated due to StackOverflowException.

let rec map1 func = function
    | [] -> []
    | head :: tail -> func head :: map1 func tail



let rec map2 func list =
    match list with
    | [] -> []
    | head :: tail -> func head :: map1 func tail


map2 (fun x -> x * 2) [1..100000]

// use an accumulator to make it tail recursive
let map3 func list =
    let rec loop acc = function
        | [] -> List.rev acc
        | head :: tail -> loop (func head :: acc) tail
    loop [] list


map3 (fun x -> x * 2) [1..10000000]

