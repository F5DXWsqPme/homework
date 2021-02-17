open System

let rec factorialInner acc n =
    if n = 0 then
        acc
    else
        factorialInner (acc * n) (n - 1)

let factorial n =
    if n < 0 then
        None
    else
        Some(factorialInner 1 n)

[<EntryPoint>]
let main argv =
    printfn "%A" (factorial 4)
    0
