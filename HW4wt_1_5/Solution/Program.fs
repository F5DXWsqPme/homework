open System

let rec fibonacciInner fibonacci0 fibonacci1 n =
    if n = 0 then
        fibonacci0
    else
        let newFibonacci = fibonacci0 + fibonacci1
        fibonacciInner fibonacci1 newFibonacci (n - 1)

let fibonacci n =
    if n < 0 then
        None
    else
        Some(fibonacciInner 0 1 n)

[<EntryPoint>]
let main argv =
    printfn "%A" (fibonacci 3)
    0
