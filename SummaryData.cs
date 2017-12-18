using System;
using System.Collections.Generic;

namespace LI_MAR_S_RPAM
{
    [Serializable]
    public class SummaryData:IComparer<SummaryData>
    {
        public string Topic;
        public AppDetails App { get { return Matches?[0]?.App; } }
        public ReviewCategory? ReviewCat { get { return Matches[0]?.Category; } }
        public SentimentValue Sentiment { get
            {
                int HP = Matches.FindAll(X => X.Sentiment == SentimentValue.HighlyPositive).Count;
                int P = Matches.FindAll(X => X.Sentiment == SentimentValue.Positive).Count;
                int HN = Matches.FindAll(X => X.Sentiment == SentimentValue.HighlyNegative).Count;
                int N = Matches.FindAll(X => X.Sentiment == SentimentValue.Negative).Count;
                int PN = Matches.FindAll(X => X.Sentiment == SentimentValue.Neutral).Count;
                if ((PN >= (HP + P + HN + N)) || (HP + P == N + HN)) return SentimentValue.Neutral;
                else if (HN + N > P + HP)
                    if (HN >= N) return SentimentValue.HighlyNegative;
                    else return SentimentValue.Negative;
                else 
                    if (HP >= P) return SentimentValue.HighlyPositive;
                    else return SentimentValue.Positive;
            } }

        public List<ReviewDetails> Matches;
        public string Summary;
        public string OTS_Summary { get
            {
                OpenTextSummarizer.SummarizerArguments args = new OpenTextSummarizer.SummarizerArguments();
                args.InputString = Summary;
                args.DictionaryLanguage = "en";
                args.DisplayPercent = 35;
                return string.Join(".", OpenTextSummarizer.Summarizer.Summarize(args).Sentences);
            } }

        public SummaryData(string topic,List<ReviewDetails> matches,string summary)
        {
            Topic = topic;
            Matches = matches;
            Summary = summary;
        }
        public SummaryData() { }

        public int Compare(SummaryData x, SummaryData y)
        {
            if (x.Matches.Count > y.Matches.Count)
                return -1;
            else if (x.Matches.Count == y.Matches.Count)
                return 0;
            else return 1;
        }
        public override string ToString()
        {
            string Value = "Topic:" + Topic + 
                ",Summary:" + Summary +
                ",ReviewCount:" + Matches.Count;
            foreach (var Review in Matches)
                Value += "@##STARTREVIEW##@" + Review.ToString() + "@##ENDREVIEW##@";
            return Value.Replace("\n", "%$%#").Replace("\r", "#$#%$");
        }
        public static SummaryData Parse(string Data)
        {
            SummaryData Obj = new SummaryData();
            Data = Data.Replace("%$%#", "\n").Replace("#$#%$", "\r");
            Obj.Topic = Data.Substring(0, Data.IndexOf("Summary:"));
            Data = Data.Replace(Obj.Topic, "");
            Obj.Topic = Obj.Topic.Remove(Obj.Topic.Length - 1).Replace("Topic:", "");

            Obj.Summary = Data.Substring(0, Data.IndexOf("ReviewCount:"));
            Data = Data.Replace(Obj.Summary, "");
            Obj.Summary = Obj.Summary.Remove(Obj.Summary.Length - 1).Replace("Summary:", "");
            Obj.Matches = new List<ReviewDetails>();
            while (Data.Contains("@##STARTREVIEW##@"))
            {
                Data = Data.Substring(Data.IndexOf("@##STARTREVIEW##@"));
                string Temp = Data.Substring(0, Data.IndexOf("@##ENDREVIEW##@"));
                Data = Data.Replace(Temp, "");
                Temp = Temp.Replace("@##STARTREVIEW##@", "");
                Obj.Matches.Add(ReviewDetails.Parse(Temp));
            }
            return Obj;
        }
    }


    [Serializable]
    public class ClusterSummaryData : IComparer<ClusterSummaryData>
    {
        public string Topic;
        public ReviewCategory? ReviewCat { get { return Matches[0]?.Matches[0]?.Category; } }
        public SentimentValue Sentiment
        {
            get
            {
                int HP = Matches.FindAll(X => X.Sentiment == SentimentValue.HighlyPositive).Count;
                int P = Matches.FindAll(X => X.Sentiment == SentimentValue.Positive).Count;
                int HN = Matches.FindAll(X => X.Sentiment == SentimentValue.HighlyNegative).Count;
                int N = Matches.FindAll(X => X.Sentiment == SentimentValue.Negative).Count;
                int PN = Matches.FindAll(X => X.Sentiment == SentimentValue.Neutral).Count;
                if ((PN >= (HP + P + HN + N)) || (HP + P == N + HN)) return SentimentValue.Neutral;
                else if (HN + N > P + HP)
                    if (HN >= N) return SentimentValue.HighlyNegative;
                    else return SentimentValue.Negative;
                else
                    if (HP >= P) return SentimentValue.HighlyPositive;
                else return SentimentValue.Positive;
            }
        }
        public List<SummaryData> Matches;
        public string Summary;
        public string OTS_Summary
        {
            get
            {
                OpenTextSummarizer.SummarizerArguments args = new OpenTextSummarizer.SummarizerArguments();
                args.InputString = Summary;
                args.DictionaryLanguage = "en";
                args.DisplayPercent = 30;
                return string.Join(".", OpenTextSummarizer.Summarizer.Summarize(args).Sentences);
            }
        }
        public ClusterSummaryData(string topic, List<SummaryData> matches, string summary)
        {
            Topic = topic;
            Matches = matches;
            Summary = summary;
        }
        public ClusterSummaryData() { }
        public int Compare(ClusterSummaryData x, ClusterSummaryData y)
        {
            if (x.Matches.Count > y.Matches.Count)
                return -1;
            else if (x.Matches.Count == y.Matches.Count)
                return 0;
            else return 1;
        }
        public override string ToString()
        {
            string ReturnString = "Topic:" + Topic.Replace("\n", "#123453afs#").Replace("\r", "#2232se#")
                .Replace(",", "#co23#") + ",Summary:" + Summary.Replace("\n", "#123453afs#").Replace("\r", "#2232se#")
                .Replace(",", "#co23#") + ",";
            foreach (var Review in Matches)
                ReturnString += "#StartReview#" + Review.ToString() + "#EndReview#";
            return ReturnString;
        }
    }
}
