namespace TestPlatform.Database.Seed.Seeders
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using Newtonsoft.Json;

    internal static class Deserializer
    {
        private const string DATA_SETS_JSON_RELATIVE_DIRECTORY = @"Database\TestPlatform.Database.Seed\DataSets";

        internal static async Task<IEnumerable<DTO>> DeserializeAsync<DTO>(string jsonFileName)
        {
            string jsonContent = await GetJSONContentAsync(jsonFileName);

            var validDTOs = new List<DTO>();

            DTO[] deserializedDTOs = JsonConvert.DeserializeObject<DTO[]>(jsonContent);

            foreach (var deserializedDTO in deserializedDTOs)
            {
                if (IsDTOValid(deserializedDTO) == false)
                {
                    // TODO add ILogger with message $"DTO {deserializedDTO.GetType().Name} is invalid."
                    continue;
                }

                validDTOs.Add(deserializedDTO);
            }

            return validDTOs;
        }

        private static bool IsDTOValid(object obj)
        {
            var objectValidationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, objectValidationContext, validationResults, true);

            return isValid;
        }

        private static async Task<string> GetJSONContentAsync(string jsonFileName)
        {
            string solutionDirectory = GetSolutionDirectory();
            string dataSetsDirectory = Path.Combine(solutionDirectory, DATA_SETS_JSON_RELATIVE_DIRECTORY);
            string[] allSeedJsonFiles = Directory.GetFiles(dataSetsDirectory, "*.json", SearchOption.AllDirectories);

            string jsonFile = jsonFileName + ".json";
            string jsonPath = allSeedJsonFiles.SingleOrDefault(path => path.EndsWith(jsonFile));

            string jsonContent = await File.ReadAllTextAsync(jsonPath);

            return jsonContent;
        }

        private static string GetSolutionDirectory()
        {
            string directory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

            while (!Directory.GetFiles(directory, "*.sln").Any())
            {
                directory = Directory.GetParent(directory).FullName;
                if (directory == null)
                {
                    throw new Exception("Solution directory not found.");
                }
            }

            return directory;
        }
    }
}
