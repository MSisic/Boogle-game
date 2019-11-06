using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Boogle_game___Tacta.Models;
using static Boogle_game___Tacta.Models.WordResults;

namespace Boogle_game___Tacta.Controllers
{
    public class HomeController : Controller
    {
        private static char[,] Letters;
        private static Random random = new Random();
        private static List<string> Words = new List<string>(); 
        public HomeController()
        {
            if (Letters == null)
            {
                Letters = new char[4, 4];
                Inicijalizacija();
            }
        }
        public ActionResult Index()
        {
            BoogleModel Model = new BoogleModel();
            Model.Words = Words;
            Model.Letters = Letters;
            return View(Model);
        }

        private static char GetLetter()
        {
            int num = random.Next(0, 26);
            char let = (char)('a' + num);
            return let;
        }
        private void Inicijalizacija()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Letters[i,j] = GetLetter();                                      
                }
            }           
        }

        public ActionResult AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words.Add(word.Replace(" ", ""));
            return RedirectToAction("Index");
        }

        public ActionResult CheckWords()
        {
            WordResults wordResults = new WordResults { Results = new List<WordResults.Result>() ,Player="Player one"};
            foreach (string word in Words)
            {
                Result WR = GetPoints(word);
                wordResults.Results.Add(WR);
            }
            wordResults.TotalPoints = wordResults.Results.Sum(x => x.Points);
            return View(wordResults);
        }

        public ActionResult MultiPlayer()
        {
            List<string> Lucas = new List<string>();
            Lucas.InsertRange(Lucas.Count,new string[] { "am", "bibble", "loo", "malarkey", "nudiustertain", "quire", "widdershins", "xertz", "bloviate", "pluto" });

            List<string> Clara = new List<string>();
            Clara.InsertRange(Clara.Count, new string[] { "xertz","gardyloo","catty","fuzzle","mars","sialoquent","quire","lollygag","colly",
                "taradiddle","snickersnee","widdershins","gardy"});

            List<string> Klaus = new List<string>();
            Klaus.InsertRange(Klaus.Count, new string[] { "bumfuzzle", "wabbit", "catty", "flibbertigibbet", "am", "loo", "wampus", "bibble", "nudiustertain", "xertz" });

            List<string> Raphael = new List<string>();
            Raphael.InsertRange(Raphael.Count, new string[] { "bloviate", "loo", "xertz", "mars", "erinaceous", "wampus", "am", "bibble", "cattywampus" });

            List<string> Tom = new List<string>();
            Tom.InsertRange(Tom.Count, new string[] { "bibble", "loo", "snickersnee", "quire", "am", "malarkey" });

            List<WordResults> Model = new List<WordResults>();
            
            WordResults wordResultsLucas = new WordResults { Results = new List<Result>(),Player="Lucas" };

            foreach (string word in Lucas)
            {
                Result r = new Result { Word = word };
                if(Clara.IndexOf(word)!=-1|| Klaus.IndexOf(word) != -1 || Raphael.IndexOf(word) != -1 || Tom.IndexOf(word) != -1)
                {
                    r.Description = "Invalid";
                    r.Points = 0;
                }
                else
                {
                    r = GetPoints(word);
                }
                wordResultsLucas.Results.Add(r);
            }
            WordResults wordResultsClara= new WordResults { Results = new List<Result>(), Player = "Clara" };

            foreach (string word in Clara)
            {
                Result r = new Result { Word = word };
                if (Lucas.IndexOf(word) != -1 || Klaus.IndexOf(word) != -1 || Raphael.IndexOf(word) != -1 || Tom.IndexOf(word) != -1)
                {
                    r.Description = "Invalid";
                    r.Points = 0;
                }
                else
                {
                    r = GetPoints(word);
                }
                wordResultsClara.Results.Add(r);
            }
            WordResults wordResultsKlaus= new WordResults { Results = new List<Result>(), Player = "Klaus" };

            foreach (string word in Klaus)
            {
                Result r = new Result { Word = word };
                if (Clara.IndexOf(word) != -1 || Lucas.IndexOf(word) != -1 || Raphael.IndexOf(word) != -1 || Tom.IndexOf(word) != -1)
                {
                    r.Description = "Invalid";
                    r.Points = 0;
                }
                else
                {
                    r = GetPoints(word);
                }
                wordResultsKlaus.Results.Add(r);
            }
            WordResults wordResultsRaphael= new WordResults { Results = new List<Result>(), Player = "Raphael" };

            foreach (string word in Raphael)
            {
                Result r = new Result { Word = word };
                if (Clara.IndexOf(word) != -1 || Klaus.IndexOf(word) != -1 || Lucas.IndexOf(word) != -1 || Tom.IndexOf(word) != -1)
                {
                    r.Description = "Invalid";
                    r.Points = 0;
                }
                else
                {
                    r = GetPoints(word);
                }
                wordResultsRaphael.Results.Add(r);
            }
            WordResults wordResultsTom= new WordResults { Results = new List<Result>(), Player = "Tom" };

            foreach (string word in Tom)
            {
                Result r = new Result { Word = word };
                if (Clara.IndexOf(word) != -1 || Klaus.IndexOf(word) != -1 || Raphael.IndexOf(word) != -1 || Lucas.IndexOf(word) != -1)
                {
                    r.Description = "Invalid";
                    r.Points = 0;
                }
                else
                {
                    r = GetPoints(word);
                }
                wordResultsTom.Results.Add(r);
            }

            wordResultsLucas.TotalPoints = wordResultsLucas.Results.Sum(x => x.Points);
            Model.Add(wordResultsLucas);
            wordResultsTom.TotalPoints = wordResultsTom.Results.Sum(x => x.Points);
            Model.Add(wordResultsTom);
            wordResultsRaphael.TotalPoints = wordResultsRaphael.Results.Sum(x => x.Points);
            Model.Add(wordResultsRaphael);
            wordResultsKlaus.TotalPoints = wordResultsKlaus.Results.Sum(x => x.Points);
            Model.Add(wordResultsKlaus);
            wordResultsClara.TotalPoints = wordResultsClara.Results.Sum(x => x.Points);
            Model.Add(wordResultsClara);
            return View(Model);
        }

        private Result GetPoints(string word)
        {
            Result WR = new Result { Word = word };
            int length = word.Length;
            if (length < 3)
            {
                WR.Points = 0;
                WR.Description = "Invalid";
            }
            else if (length == 3 || length == 4)
            {
                WR.Points = 1;
                WR.Description =length.ToString()+ " letters";
            }
            else if (length == 5)
            {
                WR.Points = 2;
                WR.Description = length.ToString() + " letters";
            }
            else if (length == 6)
            {
                WR.Points = 3;
                WR.Description = length.ToString() + " letters";
            }
            else if (length == 7)
            {
                WR.Points = 5;
                WR.Description = length.ToString() + " letters";
            }
            else
            {
                WR.Points = 11;
                WR.Description = length.ToString() + " letters";
            }
            return WR;
        }
      

    }
}