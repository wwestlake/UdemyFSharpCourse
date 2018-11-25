

// Inner Functions or Nested functions
// F# allows functions to contain function within them
// These functions have access to all of the arguments and values (or mutable variables)
// within the scope of the parent function and it's parents.


// A function with a nested or inner function

let TopLevelFunction x y =
    let inner z =
        x + y + z
    inner

// Functions may be nested arbitrarilly deap
// However, deaply nested functions are a bad practice as they
// are as difficult to reason about as deaply nested if statements
// in other languages.

let add a =
    let add1 b =
        let add2 c =
            let add3 d =
                a + b + c + d
            add3
        add2
    add1

add 1 2 3 4

// inner functions are good for use with continuations.
// Continuations are functions passed into other functions
// for handling events.  A continuation allows us to 
// move some of the decision making out of a function
// and allow the caller to decide what happens under
// certain conditions.

// here is a simple example


// This type is called a Discriminated Union
// we will be using this extensively to model our domain
// as well as other types we will introduce.
// A discriminated union can take on one of several
// types specified in the definition.  Value of this
// type must be one of these sub-types and nothing else.

type TryParseIntResult =
    | Success of int64
    | Failure of string

let tryParseInt successFunc failFunc intString =
    try
        successFunc (System.Int64.Parse(intString))
    with
    | ex -> failFunc (ex.ToString())

let useTryParse intString =
    let successFunc x = x |> Success
    let failureFunc ex = ex |> Failure
    tryParseInt successFunc failureFunc intString

useTryParse "325"
useTryParse "325-"

let applyTryParse =
    let successFunc x = x |> Success
    let failureFunc ex = ex |> Failure
    tryParseInt successFunc failureFunc

applyTryParse "325adescasdc"
