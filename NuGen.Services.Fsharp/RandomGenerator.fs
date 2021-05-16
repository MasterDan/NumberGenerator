namespace NuGen.Services.Fsharp

open System
open Microsoft.Extensions.Options
open NuGen.Options.Start
open NuGen.Services.Interfaces

type RandomGenerator(check: IUniqCheckService, opts: IOptions<StartOptions>) =
    let rnd = Random()
    let maxNumber = int (Math.Pow(10.0, float opts.Value.NumberOfDigits) - 1.0)
    
    let getNumberAsync =
        while true do
            let value = int64 (rnd.Next(0,maxNumber))
            let isUnique = async { return check.CheckUniquenessAsync(value) }
        

    interface IRandomGeneratorService with
        member this.GenerateUniqueNumbersAsync(beginNumber, endNumber) =
            

            for i in opts.Value.From.Value .. opts.Value.To.Value do
                let value = rnd.Next(0, maxNumber)
                

            failwith "todo"
