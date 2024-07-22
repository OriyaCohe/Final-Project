using HtmlAgilityPack;
using SharpNL.Extensions;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HebrewDictionary
    {
        //הסרת דגש
        public static string RemoveDiacritics(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                {
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }

            }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            
        }
            // מציאת שורש 
            public async Task<string> WordRoot(string word)
        {
            string url = $"https://milog.co.il/{word}";
            try
            {
                Console.WriteLine($"Starting request to {url} at {DateTime.Now}");

                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(22233) })
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Request to {url} completed at {DateTime.Now}");

                    // ניתוח דף HTML
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(responseBody);

                    // מציאת האלמנט שמכיל את השורש
                    var translationNodes = doc.DocumentNode.SelectNodes("//div[@class='sr_e']//div//a//span");
                    if (translationNodes != null)
                    {
                        foreach (var tn in translationNodes)
                        {
                            string a = tn.InnerText;
                            if (a.Contains("\""))
                            {
                                string[] d = a.Split(':');
                                d = d[1].Split(',');
                                d[0]= RemoveDiacritics(d[0].Trim());
                                return  d[0].Equals("פסכל\"ג")?word:d[0];
                            }
                        }
                        return word;
                        //string a = translationNodes[0].InnerText;
                        //if (a.Contains("\""))
                        //{
                        //    string[] d = a.Split(':');
                        //    d = d[1].Split(',');
                        //    return d[0].Trim();
                        //}
                        //else
                        //    return word;

                    }
                    else
                    {
                        return "Translation not found.";
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HttpRequestException: {e.Message}");
                return $"HttpRequestException: {e.Message}";
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine("TaskCanceledException: Request timed out.");
                return "TaskCanceledException: Request timed out.";
            }
            catch (Exception e)
            {
                Console.WriteLine($"General exception: {e.Message}");
                return $"General exception: {e.Message}";
            }
        }
    }
}
