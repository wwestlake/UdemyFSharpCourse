namespace LagDaemon.FSharp.Utilities

(* 
   Password requirements

   Passwords must meet the following requirements:
        1. Be a configurable number of characters in length.
        2. Contain no part of the users name or email address.
        3. Is neither null nor empty.
        4. Does not contain white space.
        5. Contain at least:
            a. Configurable number of uppercase characters.
            b. Configurable number of lowercase characters.
            c. Configurable number of digits.
            d. Configurable number of special characters.

*)

module Password =

    open System
    open Result

    type PasswordRules = {
        MinLength: int;
        MaxLength: int;
        MinLowerCase: int;
        MinUpperCase: int;
        MinDigits: int;
        MinSpecial: int;
    }

    let createRules minlen maxlen minlower minupper mindigits minspecil = {
        MinLength = minlen;
        MaxLength = maxlen;
        MinLowerCase = minlower;
        MinUpperCase = minupper;
        MinDigits = mindigits;
        MinSpecial = minspecil;
    }



    let passwordIsNotNull password =
        if String.IsNullOrEmpty(password) 
        then Failure "Password must not be null or empty"
        else Success password

    let passwordLength rules (password:string) =
        let errorString = sprintf "Password length must be >= %d and <= %d" rules.MinLength rules.MaxLength

        match password.Length < rules.MinLength, password.Length > rules.MaxLength with
        | true, false -> Failure errorString
        | false, true -> Failure errorString
        | false, false -> Success password
        | _,_ -> Failure "can't happen"



