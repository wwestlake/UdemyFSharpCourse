


open System

// lanbda functions
//
// A lambda function is a function that is returned or passed into another function
// but has no name.  Here is an example of a lambda function passed into List.map

// val upperCaseAll : list:string list -> string list

let upperCaseAll (list:string list) = 
    list |> List.map (fun (x:string) -> x.ToUpper())

upperCaseAll ["bill";"steve";"jane";"carol"]

// lambdas start with fun (there's that fun again) have parameters and option types 
// -> the arrow seperates the body and the body executes the function.

let myLambda = (fun x -> x * x)

let myLambda2 x = x * x

myLambda2 2

// Curring, names after Haskell Curry

// a function in 2 arguments
let add a b = a + b

// can be curried to a function in one argument that returns a function in one argument

let addCurried a = (fun b -> a + b)

// In F# we get curried functions for free

let add4Numbers a b c d = a + b + c + d

let add4NumbersCurried a = 
    (fun b -> 
        fun c ->
            fun d -> a + b + c + d)

// Because of curried functions we can do partial application

let add4NumbersCurried3 = add4NumbersCurried 3

let add4NumbersCurried3p4 = add4NumbersCurried3 4

let addTest = add4NumbersCurried 2 3 4

addTest 5


// partial application has many uses especially when managing state.  A function 
// can be partially applied in another function to set some value that will be 
// used repeatedly during that functions operation.

// practical partial application

// Suppose we have a list of numbers [6;3;8;2;6;9;0] and we want a new list with 
// true or false, true if the x is less than the number, false otherwise

let XlessThan x = (<) x

// let us partially apply this lessThanX function and get a lessThanTwo function

let twoLessThan = XlessThan 2

// create a list of number
let myList = [6;3;8;2;6;9;0]

let newList = myList |> List.map twoLessThan

// a bit about operators
// in F# operators are just functions and we can create our own.  We can also use
// existing operators, or out own operators, like they were functions.
// legal symbost for custom operators are
// !, %, &, *, +, -, ., /, <, =, >, ?, @, ^, |, and ~.
// ~ is special
// a boolean operator not x or not y
let (|-|) x y = not x || not y

let b = false |-| false
let c = false |-| true
let d = true |-| false
let e = true |-| true

let check = (|-|) true

check true

check false

