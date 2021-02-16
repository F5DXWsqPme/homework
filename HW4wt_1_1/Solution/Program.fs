let rec getPositionInner list acc find =
    match list with
    | [] -> None
    | head::tail ->
        if head = find then
            Some(acc)
        else
            getPositionInner tail (acc + 1) find

let getPosition list find =
    getPositionInner list 0 find

[<EntryPoint>]
let main argv =
    let list = [1; 2; 3]
    match (getPosition list 4) with
    | None -> printfn "None"
    | Some(value) -> printfn "%i" value
    0
