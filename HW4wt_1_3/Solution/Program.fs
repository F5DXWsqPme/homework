open System

let rec revertInner list result =
  match list with
  | [] -> result
  | head::tail -> revertInner tail (head :: result)

let revert list =
  revertInner list []

let rec power2 n acc =
    if n < 0 then
        0
    elif n = 0 then
        acc
    else
        power2 (n - 1) (acc * 2)

let rec getPowersInner n m acc powerValue =
    if n = m then
        powerValue :: acc
    else
        getPowersInner (n + 1) m (powerValue :: acc) (powerValue * 2)

let getPowers n m =
    if n > m || n < 0 then
        None
    else
        Some(revert (getPowersInner n m [] (power2 n 1)))

[<EntryPoint>]
let main argv =
    printfn "%A" (getPowers 1 10)
    0
