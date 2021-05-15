module NuGen.Services.Fsharp.ServicesFsharp

open System.Collections.Generic
open Microsoft.Extensions.Options
open NuGen.Options.Start
open NuGen.Services.Interfaces

type UniqCheckSimple(opts: IOptions<StartOptions>) =
    let options = opts.Value;
    let _cache = List()
    interface IUniqCheckService with
        member this.CheckUniquenessAsync(value) =
            if _cache.Contains value then
                _cache.Add value
                async { return true } |> Async.StartAsTask
            else
                async { return true } |> Async.StartAsTask
