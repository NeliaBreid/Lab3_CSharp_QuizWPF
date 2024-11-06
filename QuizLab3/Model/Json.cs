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

        public async void SaveJson(object data)
        {
            string json = JsonSerializer.Serialize(data);
          await File.WriteAllTextAsync(filePath, json); //lägger in min path o filen
        }
        //public async List<QuestionPack> LoadJson()
        //{
        //    string json = File.ReadAllText(filePath); //läsa av filen
        //    return JsonSerializer.Deserialize(ObservableCollection<QuestionPackViewModel> Packs = new ObservableCollection<QuestionPackViewModel>()); //Jag vill ha tillbaks mina packs
        //}

    }
}
