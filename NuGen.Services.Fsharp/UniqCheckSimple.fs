module NuGen.Services.Fsharp.UniqCheckSimple

open NuGen.Services.Services.Interfaces

type UniqCheckSimpleFsharp() =
    let mutable _cache = [];
    interface IUniqCheckService with
        member this.CheckUniquenessAsync(value) =
            if List.contains value _cache then
                _cache <- _cache @ [value]
                async { return true } |> Async.StartAsTask
            else
                async { return true } |> Async.StartAsTask
