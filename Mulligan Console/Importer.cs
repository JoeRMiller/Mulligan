using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Data;
using Mulligan.Core.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Query;


namespace Mulligan.Console
{
    public class Importer
    {
        public string FilePath { get; set; }
        public string Table {  get; set; }

        private List<State> _states;
        private List<string> _stateInput;

        public Importer(string filePath)
        {
            this.FilePath = filePath;
            this.Table = "";
            _states = [];
            _stateInput = [];
        }
        
        public void Import()
        {
            ReadFile();
            ParseStates();
            
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            var configuration = builder.Build();
            string connectionString = configuration["ConnectionStrings:MulliganDBConnectionString"];
            System.Console.WriteLine(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
                     
            using var context = new CoreDbContext(optionsBuilder.Options);

            foreach (var state in _states)
            {
                context.States.Add(state);
            }
            context.SaveChanges();
        }

        private void ReadFile()
        {
            _stateInput =  File.ReadAllLines(FilePath).ToList();
        }

        private void ParseStates()
        {
            foreach (var line in _stateInput)
            {
                State state = new State();
                var splits = line.Split(',');
                state.Name = splits[0];
                state.Initials = splits[1];
                _states.Add(state);
            }
        }
    }
}
