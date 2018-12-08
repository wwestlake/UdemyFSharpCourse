
open System

// Password requirements
// 1. Must not be null or empty
// 2. Must be a configurable min and max length
// ...

type Result<'Tsuccess, 'Tfailure> =
    | Success of 'Tsuccess
    | Failure of 'Tfailure

let succeed x = Success x
let fail x = Failure x

let bind f input =
    match input with
    | Success s -> f s
    | Failure f -> Failure f

let (>>=) input func =
    bind func input


let passwordIsNotNull password =
    if String.IsNullOrEmpty(password) 
    then Failure "Password must not be null or empty"
    else Success password

let passwordLength min max (password:string) =
    match password.Length < min, password.Length > max with
    | true, false -> Failure "password too short"
    | false, true -> Failure "password too long"
    | false, false -> Success password
    | _,_ -> Failure "can't happen"

passwordIsNotNull null
passwordIsNotNull ""
passwordIsNotNull "something"

passwordLength 4 8 null
passwordLength 4 8 ""
passwordLength 4 8  "something"
passwordLength 4 8 "password"

let checkPassword min max input = 
    input  |> passwordIsNotNull >>= passwordLength min max

checkPassword 4 8 null
checkPassword 4 8 ""
checkPassword 4 8 "something"
checkPassword 4 8 "password"


let func1 x = x + 3
let func2 y = y + 4

let lift f =
    f >> succeed

let lifted x = lift func1






