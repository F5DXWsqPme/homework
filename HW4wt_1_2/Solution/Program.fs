let rec revertInner list result =
    match list with
    | [] -> result
    | head::tail -> revertInner tail (head :: result)

let rec revert list =
    revertInner list []

[<EntryPoint>]
let main argv =
    let list = [1; 2; 3]
    printfn "%A" (revert list)
    0
