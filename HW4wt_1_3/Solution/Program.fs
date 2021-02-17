open System

let revert list =
    let rec revertInner list result =
        match list with
        | [] -> result
        | head :: tail -> revertInner tail (head :: result)
    revertInner list []

let getPowers n m =
    let rec getPowersInner n m acc powerValue =
        if n = m then
            powerValue :: acc
        else
            getPowersInner (n + 1) m (powerValue :: acc) (powerValue * 2)
    if n > m || n < 0 then
        None
    else
        Some(revert (getPowersInner n m [] (pown 2 n)))

[<EntryPoint>]
let main argv =
    printfn "%A" (getPowers 5 10)
    0
