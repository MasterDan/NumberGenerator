namespace NuGen.Services.Fsharp

open NuGen.Services.Interfaces
open NuGen.Services.Interfaces

type RandomGenerator(check: IUniqCheckService) =
    interface IRandomGeneratorService with
        member this.GenerateUniqueNumbersAsync(beginNumber, endNumber) = failwith "todo"