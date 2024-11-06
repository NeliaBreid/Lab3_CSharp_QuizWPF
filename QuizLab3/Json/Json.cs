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
    //public class JsonH
    //{
    //    private string FilePath = "Quizlab3.json"; //skapar min filePath som en field
    //    public void SaveJson(object data)
    //    {
    //        string json = JsonSerializer.Serialize(data);
    //        File.WriteAllText(FilePath, json); //lägger in min path o filen
    //    }
    //    public List<QuestionPack> LoadJson(object data) 
    //    {
    //        string json = File.ReadAllText(FilePath); //läsa av filen
    //        return JsonSerializer.DeserializeObservableCollection<QuestionPackViewModel> Packs ?? new ObservableCollection<QuestionPackViewModel>(); //Jag vill ha tillbaks mina packs
    //    }

    //}
}
