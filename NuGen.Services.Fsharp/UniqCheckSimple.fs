module NuGen.Services.Fsharp.UniqCheckSimple

open NuGen.Services.Services.Interfaces

type UniqCheckFsharp() = interface IUniqCheckService with
                             member this.CheckUniquenessAsync(value) = failwith "todo"

