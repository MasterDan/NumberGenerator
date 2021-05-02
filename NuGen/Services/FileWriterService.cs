using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NuGen.Extensions;
using NuGen.Options.Start;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class FileWriterService : IWriterService
    {
        private readonly StartOptions _startOptions;

        private string FilePath => _startOptions.FilePath == null
            ? Path.Combine(".", $"from_{_startOptions.From}_to_{_startOptions.To}", "result.txt")
            : Path.Combine(".", _startOptions.FilePath);

        private void CheckDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private StreamWriter CreateifNotExists(string path)
        {
            if (!File.Exists(path))
            {
                return File.CreateText(path);
            }

            return new StreamWriter(path);
        }

        private string AddPrefix(string path, string prefix)
        {
            return Path.Combine(Path.GetDirectoryName(path) ?? ".", prefix + Path.GetFileName(path));
        }

        public FileWriterService(IOptions<StartOptions> startOptions)
        {
            _startOptions = startOptions.Value;
        }

        public async Task SaveAllAsync(IAsyncEnumerable<long> array)
        {
            CheckDirectory(FilePath);
            int index = 0;
            int chunkNumber = 0;
            await using StreamWriter all = CreateifNotExists(AddPrefix(FilePath, "_all#"));

            await foreach (var number in array)
            {
                if (index % _startOptions.NumbersInOneFile == 0)
                {
                    chunkNumber++;
                }

                await using StreamWriter chunkFile = CreateifNotExists(AddPrefix(FilePath, $"{chunkNumber}#"));
                var line = $"{_startOptions.Prefix}{index + _startOptions.From:000000};{number:000000}";
                await all.WriteLineAsync(line);
                await chunkFile.WriteLineAsync(line);
                index++;
            }
        }

        public async Task SaveAllAsync(IEnumerable<long> array)
        {
            var list = array.Select((number, index) => new {number, index});
            CheckDirectory(FilePath);
            var chunkedArray = list.Chunk(100).ToList();
            await using StreamWriter all = CreateifNotExists(AddPrefix(FilePath, "_all#"));
            int chunkNumber = 1;
            foreach (var chunk in chunkedArray)
            {
                var chunkList = chunk.ToList();
                if (!chunkList.Any())
                {
                    break;
                }

                await using StreamWriter chunkFile = CreateifNotExists(AddPrefix(FilePath, $"{chunkNumber}#"));
                foreach (var item in chunkList.ToList())
                {
                    var line =
                        $"{_startOptions.Prefix}{item.index + _startOptions.From:000000};{item.number:000000}";
                    await all.WriteLineAsync(line);
                    await chunkFile.WriteLineAsync(line);
                }

                chunkNumber++;
            }
        }
    }
}