using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NuGen.Extensions;
using NuGen.Options.Start;
using NuGen.Services.Services.Interfaces;

namespace NuGen.Services.Services
{
    public class FileWriterService : IWriterService
    {
        private readonly StartOptions _startOptions;
        private readonly IFileSystemService _fs;
        private readonly IStateMonitoringService _state;

        private string FilePath => _startOptions.FilePath == null
            ? Path.Combine(".", $"from_{_startOptions.From}_to_{_startOptions.To}", "result.txt")
            : Path.Combine(".", _startOptions.FilePath);


        public FileWriterService(IOptions<StartOptions> startOptions, IFileSystemService fs, IStateMonitoringService state)
        {
            _fs = fs;
            _state = state;
            _startOptions = startOptions.Value;
        }

        public async Task SaveAllAsync(IAsyncEnumerable<long> array)
        {
            _fs.CreateDirIfNotExists(FilePath);
            int index = 0;
            int chunkNumber = 0;
            await using StreamWriter all = _fs.CreateStreamWriter(_fs.AddPrefixToFile(FilePath, "_all#"));

            await foreach (var number in array)
            {
                if (index % _startOptions.NumbersInOneFile == 0)
                {
                    chunkNumber++;
                }

                await using StreamWriter chunkFile =
                    _fs.CreateStreamWriter(_fs.AddPrefixToFile(FilePath, $"{chunkNumber}#"));
                var line = $"{_startOptions.Prefix}{index + _startOptions.From:000000};{number:000000}";
                await all.WriteLineAsync(line);
                await chunkFile.WriteLineAsync(line);
                index++;
            }
        }

        public async Task SaveAllAsync(IEnumerable<long> array)
        {
            var list = array.Select((number, index) => new {number, index});
            _fs.CreateDirIfNotExists(FilePath);
            var chunkedArray = list.Chunk(100).ToList();
            await using StreamWriter all = _fs.CreateStreamWriter(_fs.AddPrefixToFile(FilePath, "_all#"));
            int chunkNumber = 1;
            foreach (var chunk in chunkedArray)
            {
                var chunkList = chunk.ToList();
                if (!chunkList.Any())
                {
                    break;
                }

                await using StreamWriter chunkFile =
                    _fs.CreateStreamWriter(_fs.AddPrefixToFile(FilePath, $"{chunkNumber}#"));
                foreach (var item in chunkList.ToList())
                {
                    var line =
                        $"{_startOptions.Prefix}{item.index + _startOptions.From:000000};{item.number:000000}";
                    await all.WriteLineAsync(line);
                    await chunkFile.WriteLineAsync(line);
                    _state.NumberSaved();
                }

                chunkNumber++;
            }
        }
    }
}