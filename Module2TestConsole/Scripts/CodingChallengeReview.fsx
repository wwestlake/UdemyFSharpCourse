
open System


type StringResult =
    | Success of string
    | Failure of string

let testString successFunc failureFunc str =
    if String.IsNullOrEmpty(str) then
        failureFunc "String must not be null or empty"
    else
        if str.Length >= 10 && str.Length <= 20 then
            successFunc str
        else
            failureFunc "string must be atleast 10 characters long but not more than 20 characters long"

let useTestString str =
    let successFunc str = str |> Success
    let failureFunc str = str |> Failure
    testString successFunc failureFunc str

    // or 

let useTestString2 =
    let successFunc str = str |> Success
    let failureFunc str = str |> Failure
    testString successFunc failureFunc

useTestString null
useTestString ""
useTestString "too short"
useTestString "not too short"
useTestString "Way to long of a string to pass this test successfully"

