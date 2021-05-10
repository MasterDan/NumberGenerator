module NuGen.Services.Fsharp.UniqCheckSimple

open NuGen.Services.Services.Interfaces

type UniqCheckSimpleFsharp() =
    let _cache = []

    interface IUniqCheckService with
        member this.CheckUniquenessAsync(value) = failwith "todo"
