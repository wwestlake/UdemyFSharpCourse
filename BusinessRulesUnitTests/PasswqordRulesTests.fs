namespace Tests

open NUnit.Framework
open LagDaemon.FSharp.Utilities.Result
open LagDaemon.FSharp.Utilities.Password



[<TestFixture>]
type PasswordRulesTests () =

    member this.rules = {
        MinLength = 8;
        MaxLength = 16;
        MinLowerCase = 1;
        MinUpperCase = 1;
        MinDigits = 1;
        MinSpecial = 1;
    }

    [<SetUp>]
    member this.Setup () =
        ()

    [<Test>]
    member this.PasswordContainsMinLowerCaseCharacters () =
        let password = "aaa"
        match checkLowerCase this.rules password with
        | Success _ -> Assert.Pass()
        | Failure _ -> Assert.Fail()

    [<Test>]
    member this.PasswordContainsMinUpperCaseCharacters () =
        let password = "AA"
        match checkUpperCase this.rules password with
        | Success _ -> Assert.Pass()
        | Failure _ -> Assert.Fail()

    [<Test>]
    member this.PasswordContainsMinDigitCharacters () =
        let password = "99"
        match checkDigits this.rules password with
        | Success _ -> Assert.Pass()
        | Failure _ -> Assert.Fail()
    
    [<Test>]
    member this.PasswordContainsSpecialCaseCharacters () =
        let password = "AA;"
        match checkSpecial this.rules password with
        | Success _ -> Assert.Pass()
        | Failure _ -> Assert.Fail()

    [<Test>]
    member this.PasswordContainsNoWhteSoace () =
        let password = "abc"
        match checkPasswordForWhitespace password with
        | Success _ -> Assert.Pass()
        | Failure _ -> Assert.Fail()

    [<Test>]
    member this.CombinedPasswordRules () =
        let password = "A9aaaaaaaa!"
        match combinedPasswordChecks this.rules password with
        | Success _ -> Assert.Pass()
        | Failure f -> Assert.Fail(f)
