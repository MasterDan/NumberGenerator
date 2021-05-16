namespace NuGen.Services.Fsharp

open System
open Microsoft.Extensions.Options
open NuGen.Options.Start
open NuGen.Services.Interfaces

type RandomGenerator(check: IUniqCheckService, opts: IOptions<StartOptions>) =
    let rnd = Random()

    interface IRandomGeneratorService with
        member this.GenerateUniqueNumbersAsync(beginNumber, endNumber) =
            let maxNumber =
                int (
                    Math.Pow(10.0, float opts.Value.NumberOfDigits)
                    - 1.0
                )

            for i in opts.Value.From.Value .. opts.Value.To.Value do
                let value = rnd.Next(0, maxNumber)
                

            failwith "todo"
