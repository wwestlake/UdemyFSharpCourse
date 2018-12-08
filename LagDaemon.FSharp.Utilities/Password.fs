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
    open System.Text.RegularExpressions
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

    let lowerCase = ['a' .. 'z']
    let upperCase = ['A' .. 'Z']
    let digits = ['0' .. '9']
    let special = List.concat [['!'..'/'];[':' .. '@'];['['..'`'];['{'..'~']]

    let countElements list1 list2 =
        let rec counter l1 l2 count =
            match l1 with
            | [] -> count
            | head :: rest -> if List.contains head list2
                              then counter rest l2 (count + 1)
                              else counter rest l2 count
        counter list1 list2 0


    let passwordIsNotNull password =
        if String.IsNullOrEmpty(password) 
        then Failure "Password must not be null or empty"
        else Success password

    let checkPasswordForWhitespace password =
        let regex = new Regex(@"\s")
        match regex.IsMatch(password) with
        | true -> "Password must not contain white space" |> fail
        | false -> password |> succeed

    let checkPasswordLength rules (password:string) =
        let errorString = sprintf "Password length must be >= %d and <= %d" rules.MinLength rules.MaxLength

        match password.Length < rules.MinLength, password.Length > rules.MaxLength with
        | true, false -> Failure errorString
        | false, true -> Failure errorString
        | false, false -> Success password
        | _,_ -> Failure "can't happen"

    let checkLowerCase rules (password: string) =
        let count = countElements (Seq.toList password) lowerCase
        if count >= rules.MinLowerCase 
        then password |> succeed
        else sprintf "Password must contain at least %d lower case characters." rules.MinLowerCase |> fail

    let checkUpperCase rules (password: string) =
        let count = countElements (Seq.toList password) upperCase
        if count >= rules.MinUpperCase
        then password |> succeed
        else sprintf "Password must contain at least %d upper case characters." rules.MinUpperCase |> fail

    let checkDigits rules (password: string) =
        let count = countElements (Seq.toList password) digits
        if count >= rules.MinDigits
        then password |> succeed
        else sprintf "Password must contain at least %d digit characters." rules.MinDigits |> fail

    let checkSpecial rules (password: string) =
        let count = countElements (Seq.toList password) special
        if count >= rules.MinSpecial
        then password |> succeed
        else sprintf "Password must contain at least %d special characters." rules.MinSpecial |> fail

    let combinedPasswordChecks rules password =
        password |> passwordIsNotNull
        >>= checkPasswordLength rules
        >>= checkLowerCase rules
        >>= checkUpperCase rules
        >>= checkDigits rules
        >>= checkSpecial rules
