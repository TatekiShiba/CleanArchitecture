using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleApp.Core.Entities;
using SampleApp.Core.Interface;
using CsvHelper;

namespace SampleApp.Infrastructure
{
    public class CsvUserRepositoryOptions
    {
        public string FileName { get; set; }
    }

    public class CsvUserRepository : IUserRepository
    {
        private readonly ILogger<CsvUserRepository> _logger;
        private readonly IOptions<CsvUserRepositoryOptions> _options;
        private List<User> _records;

        public CsvUserRepository(
            ILogger<CsvUserRepository> logger,
            IOptions<CsvUserRepositoryOptions> options)
        {
            _logger = logger;
            _options = options;

            using (var stream = new StreamReader(options.Value.FileName))
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                _records = csv.GetRecords<User>().ToList();
            }
        }

        public async Task<User> FindByUserId(string userId)
        {
            return await Task<User>.Run(() =>
                _records.SingleOrDefault(u => u.UserId == userId));
        }

        public async Task<User> Add(User user)
        {
            return await Task<User>.Run(() =>
            {
                var userId = new Guid().ToString();
                user.UserId = userId;
                _records.Add(user);
                return user;
            });
        }

        public async Task Save()
        {
            await Task.Run(() =>
            {
                using (var stream = new StreamWriter(_options.Value.FileName))
                using (var csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords<User>(_records);
                }
            });
        }
    }
}
