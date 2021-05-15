namespace NuGen.Services.Fsharp

open System.Collections.Generic
open Microsoft.Extensions.Options
open NuGen.Options.Start
open NuGen.Services.Interfaces

type UniqCheckSimple(opts: IOptions<StartOptions>) =
    let options = opts.Value;
    let _cache = List()
    interface IUniqCheckService with
        member this.CheckUniquenessAsync(value) =
            if not (_cache.Contains value) then
                _cache.Add value
                async { return true } |> Async.StartAsTask
            else
                async { return false } |> Async.StartAsTask
