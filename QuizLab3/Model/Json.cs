using QuizLab3.Model;
using QuizLab3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizLab3.Json
{
    public class Json
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal async void SaveJson(List<QuestionPack> packs) 
        {
            string json = JsonSerializer.Serialize(packs);
          await File.WriteAllTextAsync(filePath, json); //lägger in min path o filen
        }
        internal async Task<List<QuestionPack>> LoadJson()
        {
            string json = await File.ReadAllTextAsync(filePath); //läsa av filen
            return JsonSerializer.Deserialize<List<QuestionPack>>(json); //Jag vill ha tillbaks mina packs
        }

    }
}
