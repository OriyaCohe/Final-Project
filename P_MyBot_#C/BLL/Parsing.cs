using Org.BouncyCastle.Crypto.Agreement.Srp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP.Tools.Ling;
using Microsoft.ML.Data;
using Microsoft.ML;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Presentation;

namespace BLL
{
    public class Parsing
    {
        public DFA dfa;
        public String[][] sultion = new string[5][];
        public Dictionary<string, Transition> transitions = new Dictionary<string, Transition>();

        public Parsing(DFA dfa, string[][] sultion, Dictionary<string, Transition> transitions)
        {
            this.dfa = dfa;
            this.sultion = sultion;
            this.transitions = transitions;
        }
        string[] Positive = new string[] { "חיובי", "כן תודה רבה","לגמרי", "מאה אחוז", "כן", "אכן",
            "בהחלט", "ללא ספק", "בטח", "מאשר", "צודק", "בדיוק ככה", "אני מסכים", " באופן מוחלט" };
        string[] Negative = new string[] { "לא", "טעות", "שלילי", "ממש לא", "לא התכוונתי", "איני מסכים", "מה הקשר" };
        //יצירת מופע של   המחלקה "מילון העברי
        HebrewDictionary hebrewDictionary = new HebrewDictionary();
        //פונקציה שמקבלת את הטקסט שהלקוח שלח, מופרד למילים ומחזירה את מערך שורשים.
        public async Task<string[]> WordRoot(string[] words)
        {
            //שליחה לפונקציה במילון העברי שמוציאה שורשים והכנסה למערך עזר.
            Task<string>[] tasks = new Task<string>[words.Length];
            string[] results = new string[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("ו") || words[i].StartsWith("מ") && !words[i].Equals("מים") && !words[i].Equals("מניע") && !words[i].Equals("מגיבה") && !words[i].Equals("מד")) { words[i] = words[i].Substring(1); }
                if (words[i].StartsWith("ב") && !words[i].Equals("בנזין") && !words[i].Equals("בורג") && !words[i].Equals("בוצי") && !words[i].Equals("בוץ") ||
                    words[i].StartsWith("מ") && !words[i].Equals("מים") && !words[i].Equals("מניע") && !words[i].Equals("מגיבה") && !words[i].Equals("מד")
                    || words[i].StartsWith("ה") && !words[i].Equals("הלך") && !words[i].Equals("התפוצץ") && !words[i].Equals("התפנצ'ר"))
                    words[i] = words[i].Substring(1);
                tasks[i] = hebrewDictionary.WordRoot(words[i]);
                //בדיקה אם חזר ערך אם לא חזר תשאיר את המילה שהיהת(אתכן שלא יחזור עבור שמות עצם
                if (tasks[i] == null)
                {
                    results[i] = words[i];
                }
                else
                {
                    results[i] = await tasks[i];
                }
            }
            return results;
        }
        public string[] MeaningfulWords(string[] words)
        {
            //  פונקציה מקבלת מערך של שורשים ומחזירה רק את המילים הרלוונטיות.
            string[] result = new string[words.Length];
            int i = 0;
            foreach (string word in words)
            {
                //בודק אם השורש קיים במאגר השורשים (כל המילים שיכולת להיות) אם כן מכניס למערך החדש של השורשים הרצויים.
                if (dfa.states[0].transitions.ContainsKey(word.Trim()))
                {
                    result[i++] = word;
                }
            }
            if (i != words.Length)
            {
                string[] result1 = new string[i];
                for (int i1 = 0; i1 < i; i1++)
                    result1[i1] = result[i1];
                return result1;
            }
            return result;
        }
        //בנית הTransitions
        public void fillTransitions()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            string wordRoot;
            int rCnt;
            int rw = 0;
            int cl = 0;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@"C:\P_MyBot_#C\Transition.xlsx", 0, true, 5, "", "", true,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;
            for (rCnt = 1; rCnt <= rw; rCnt++)
            {
                try
                {
                    wordRoot = (string)(range.Cells[rCnt, 1] as Excel.Range).Value2;
                    transitions.Add(wordRoot, new Transition());
                }
                catch (Exception)
                {
                    Console.WriteLine(rCnt);
                }
            }
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
            // Clean up Excel objects
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }
        //בנית DFA
        public void fillDFA()
        { //     DFAפונקציה שבונה את ה 
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            string wordRoot;
            int rCnt;
            int beforState, nextState, indexS, indexj;
            int rw = 0;
            int cl = 0;
            xlApp = new Excel.Application();
            //קריאה מקובץ XL
            xlWorkBook = xlApp.Workbooks.Open(@"C:\P_MyBot_#C\DFA.xlsx", 0, true, 5, "", "", true, Microsoft.
                Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;
            dfa.states = new State[rw];
            //State dfaStates = State.InitializeStatesFromExcel();
            for (int i = 0; i < rw; i++)
            { // State אתחול מערך ה
                dfa.states[i] = new State(transitions);
            }
            for (rCnt = 1; rCnt < rw; rCnt++)
            {
                try
                {
                    beforState = Convert.ToInt32((range.Cells[rCnt, 1] as Excel.Range).Value2);
                    nextState = Convert.ToInt32((range.Cells[rCnt, 3] as Excel.Range).Value2);
                    indexS = Convert.ToInt32((range.Cells[rCnt, 4] as Excel.Range).Value2);
                    indexj = Convert.ToInt32((range.Cells[rCnt, 5] as Excel.Range).Value2);
                    wordRoot = (string)(range.Cells[rCnt, 2] as Excel.Range).Value2;
                    dfa.states[beforState].transitions[wordRoot.Trim()].nextState = nextState;
                    dfa.states[nextState].indexS = indexS;
                    dfa.states[nextState].indexJ = indexj;
                    dfa.states[beforState].transitions[wordRoot.Trim()].indexS = indexS;
                    dfa.states[beforState].transitions[wordRoot.Trim()].indexJ = indexj;
                }
                catch (Exception)
                {
                    Console.WriteLine(rCnt);
                }
            }
        }
        //בנית מערך הפתרונות
        public void solution()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            int rCnt;
            int rw = 0;
            int cl = 0;
            xlApp = new Excel.Application();
            //קריאה מקובץ XL
            xlWorkBook = xlApp.Workbooks.Open(@"C:\P_MyBot_#C\solutions.xlsx", 0, true, 5, "", "", true, Microsoft.
                Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            int i = 0;
            string s;
            for (rCnt = 1; rCnt <= rw; rCnt++)
            {
                cl = range.Columns.Count;
                sultion[rCnt - 1] = new string[cl];
                for (i = 0; i < cl; i++)
                {
                    s = (string)(range.Cells[rCnt, i + 1] as Excel.Range).Value2;
                    sultion[rCnt - 1][i] = s;
                }
            }
        }
        // הפונקציה מקבלת את הטקסט מין האנגולר
        public async Task<string> sentenceAnalysis(string str)
        {
            // ממירה משפט מאנגלית לעברית
            //  str = ConvertToHebrew(str);
            str = new string(str.Where(c => c != '.' && c != ',' && c != '"').ToArray());
            //מחלקת את המשפט למילים
            string[] AllWords = str.Split(' ');
            // שורשים
            AllWords = await WordRoot(AllWords);
            // השמטת מילים לא רלוונטיות לניתוח
            AllWords = MeaningfulWords(AllWords);
            int currentState = 0, nextState = 0, i = 0, max = 0, j = 0, ij = 0;
            int[] maxj = new int[5];
            while (i < AllWords.Length)
            {
                //חיפוש המעבר הבא
                nextState = dfa.states[currentState].transitions[AllWords[i].Trim()].nextState;
                j = dfa.states[currentState].transitions[AllWords[i].Trim()].indexS > max ? 0 : j;
                max = dfa.states[currentState].transitions[AllWords[i].Trim()].indexS > max ? dfa.states[currentState].transitions[AllWords[i].Trim()].indexS : max;
                maxj[j] = dfa.states[currentState].transitions[AllWords[i].Trim()].indexS == max ? dfa.states[currentState].transitions[AllWords[i].Trim()].indexJ : maxj[j];
                j += maxj[j] == dfa.states[currentState].transitions[AllWords[i].Trim()].indexJ && max == dfa.states[currentState].transitions[AllWords[i].Trim()].indexS ? 1 : 0;
                ij++;
                i += nextState != -1 || ij - 2 == i ? 1 : 0;
                ij = ij - 2 == i ? i : ij;
                currentState = nextState == -1 ? 0 : nextState;
            }
            if (max == 0 || max == 1)
                return sultion[max][0];
            string s = "זהיתי שבעייתך היא ";
            s += sultion[max][maxj[0]];
            for (int k = 1; k < j; k++)
            {
                if (maxj[k] != maxj[k - 1])
                {
                    s += ",";
                    s += sultion[max][maxj[k]];
                }
            }
            s += " האם זה נכון? ";
            return s;
        }
        // הפונקציה מקבלת את המשך ההתכתבות 
        public async Task<string> AnalyzingCustomerResponse(string message)
        {//בודקת אם חזרה תשובה חיובית ע"י מעבר על מערך מילים החיוביות 
            if (Positive.Contains(message))
            {
                return "יופי תודה אני מחפש משהו שיבוא לעזור לך";
            }
            if (Negative.Contains(message))
            {
                return "אני מצטער, נראה שלא הבנתי באופן מדויק את הבעיה שהוגשה. האם תוכל/י להסביר בצורה יותר מפורטת או להביא דוגמאות נוספות כדי שאוכל להבין טוב יותר?";
            }
            // אתכן שיחזור רק תשובה שלילית ואתכן שיחזור תשובה שלילית עם הסבר שום של הבעיה 
            return await sentenceAnalysis(message); ;
        }
        //מילון האותיות מאנגלית לעברית
        public Dictionary<char, char> englishToHebrewDict = new Dictionary<char, char>() {      //לסדר
        {'T', 'א'},
        {'C', 'ב'},
        {'D', 'ג'},
        {'S', 'ד'},
        {'V', 'ה'},
        {'U', 'ו'},
        {'Z', 'ז'},
        {'J', 'ח'},
        {'Y', 'ט'},
        {'H', 'י'},
        {'F', 'כ'},
        {'K', 'ל'},
        {'N', 'מ'},
        {'O', 'ם'},
        {'B', 'נ'},
        {'I', 'ן'},
        {'X', 'ס'},
        {'G', 'ע'},
        {'P', 'פ'},
        {'M', 'צ'},
        {'.', 'ץ'},
        {'E', 'ק'},
        {'R', 'ר'},
        {'A', 'ש'},
        {',', 'ת'}
    };
        //ממירה משפט מאנגלית לעברית
        //public string ConvertToHebrew(string englishSentence)
        //{
        //    //ממירה לאותיות גדולות
        //    englishSentence = englishSentence.ToUpper();
        //    //מחלקה המייצגת רצף של תווים שניתן לשינוי
        //    StringBuilder hebrewSentence = new StringBuilder();
        //    foreach (char item in englishSentence)
        //    {
        //        if (englishToHebrewDict.ContainsKey(item))
        //        {
        //            hebrewSentence.Append(englishToHebrewDict[item]);
        //        }
        //        else
        //        {
        //            if (item == ' ')
        //                hebrewSentence.Append(" ");
        //            else
        //            {//במידה וזה אות בעברית
        //                hebrewSentence.Append(item);
        //            }
        //        }
        //    }
        //    return hebrewSentence.ToString();
        //}
    }

}


