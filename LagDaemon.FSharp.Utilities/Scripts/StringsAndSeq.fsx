

let a = "This is a string"

Seq.toList a

seq { 
    for i in a do yield i  
}

let b = seq {
    for i in 1..100000 do yield i
}

b

Seq.toList b


