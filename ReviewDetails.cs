using System;
using System.Collections;
using System.Collections.Generic;

namespace LI_MAR_S_RPAM
{
    [Serializable]
    public class ReviewDetails : IComparer<ReviewDetails>
    {
        public string Title;
        public string Href;
        public string Author;
        public string Rating;
        public string Version;
        public string Content;
        public AppDetails App;
        public ReviewCategory Category;
        public string Topic;
        public SentimentValue Sentiment;
        public ReviewCategory? UserDefineCategory;

        public string TFIDFData { get { return Title + " " + Topic; } }
        public override string ToString()
        {
            return ("Title:" + Title +
                ",Link:" + Href +
                ",Author:" + Author +
                ",Rating:" + Rating +
                ",Version:" + Version +
                ",Content:" + Content +
                ",AppDetails:" + App.ToString() + "#$3$#" +
                ",Category:" + Category +
                ",Topic:" + Topic +
                ",SentimentValue:" + (int)Sentiment +
                ",UserDefineCategory:" + UserDefineCategory).Replace("\n", "%$%#").Replace("\r", "#$#%$");
        }
        public static ReviewDetails Parse(string Data)
        {
            ReviewDetails Obj = new ReviewDetails();
            Data = Data.Replace("%$%#", "\n").Replace("#$#%$", "\r");
            Obj.Title = Data.Substring(0, Data.IndexOf("Link:"));
            Data = Data.Replace(Obj.Title, "");
            Obj.Title = Obj.Title.Remove(Obj.Title.Length - 1).Replace("Title:", "");
            Obj.Href = Data.Substring(0, Data.IndexOf("Author:"));
            Data = Data.Replace(Obj.Href, "");
            Obj.Href = Obj.Href.Remove(Obj.Href.Length - 1).Replace("Link:", "");
            Obj.Author = Data.Substring(0, Data.IndexOf("Rating:"));
            Data = Data.Replace(Obj.Author, "");
            Obj.Author = Obj.Author.Remove(Obj.Author.Length - 1).Replace("Author:", "");
            Obj.Rating = Data.Substring(0, Data.IndexOf("Version:"));
            Data = Data.Replace(Obj.Rating, "");
            Obj.Rating = Obj.Rating.Remove(Obj.Rating.Length - 1).Replace("Rating:", "");
            Obj.Version = Data.Substring(0, Data.IndexOf("Content:"));
            Data = Data.Replace(Obj.Version, "");
            Obj.Version = Obj.Version.Remove(Obj.Version.Length - 1).Replace("Version:", "");
            Obj.Content = Data.Substring(0, Data.IndexOf("AppDetails:"));
            Data = Data.Replace(Obj.Content, "");
            Obj.Content = Obj.Content.Remove(Obj.Content.Length - 1).Replace("Content:", "");
            string Temp = Data.Substring(0, Data.IndexOf("#$3$#"));
            Data = Data.Replace(Temp, "");
            Temp = Temp.Replace("AppDetails:", "").Replace("#$3$#", "");
            Obj.App = AppDetails.Parse(Temp);
            if (Data.Contains("Category:"))
            {
                Data = Data.Replace("#$3$#,", "").Replace("#$3$#","");
                if (Data.Contains("Topic:"))
                {
                    Temp = Data.Substring(0, Data.IndexOf("Topic:"));
                    Data = Data.Replace(Temp, "");
                    Temp = Temp.Remove(Temp.Length - 1).Replace("Category:", "");
                    switch (Temp)
                    {
                        case "BugReport":
                            Obj.Category = ReviewCategory.BugReport;
                            break;
                        case "FeatureRequest":
                            Obj.Category = ReviewCategory.FeatureRequest;
                            break;
                        case "UserExperience":
                            Obj.Category = ReviewCategory.UserExperience;
                            break;
                        case "Rating":
                            Obj.Category = ReviewCategory.Rating;
                            break;
                        case "Other":
                            Obj.Category = ReviewCategory.Other;
                            break;
                    }
                    if (Data.Contains("SentimentValue:"))
                    {
                        Obj.Topic = Data.Substring(0, Data.IndexOf("SentimentValue:"));
                        Data = Data.Replace(Obj.Topic, "");
                        Obj.Topic = Obj.Topic.Remove(Obj.Topic.Length - 1).Replace("Topic:", "");
                        if (Data.Contains("UserDefineCategory:"))
                        {
                            Temp = Data.Substring(0, Data.IndexOf("UserDefineCategory:"));
                            Data = Data.Replace(Temp, "");
                            Temp = Temp.Remove(Temp.Length - 1).Replace("SentimentValue:", "");
                            Obj.Sentiment = (SentimentValue)int.Parse(Temp);

                            Data = Data.Replace("UserDefineCategory:", "");
                            switch (Data)
                            {
                                case "BugReport":
                                    Obj.UserDefineCategory = ReviewCategory.BugReport;
                                    break;
                                case "FeatureRequest":
                                    Obj.UserDefineCategory = ReviewCategory.FeatureRequest;
                                    break;
                                case "UserExperience":
                                    Obj.UserDefineCategory = ReviewCategory.UserExperience;
                                    break;
                                case "Rating":
                                    Obj.UserDefineCategory = ReviewCategory.Rating;
                                    break;
                                case "Other":
                                    Obj.UserDefineCategory = ReviewCategory.Other;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Data = Data.Replace("SentimentValue:", "");
                            Obj.Sentiment = (SentimentValue)int.Parse(Data);
                        }
                    }
                    else
                    {
                        Obj.Topic = Data;
                        Data = Data.Replace(Obj.Topic, "");
                        Obj.Topic = Obj.Topic.Replace("Topic:", "");
                    }
                }
                else
                {
                    Data = Data.Replace("Category:", "");
                    switch (Data)
                    {
                        case "BugReport":
                            Obj.Category = ReviewCategory.BugReport;
                            break;
                        case "FeatureRequest":
                            Obj.Category = ReviewCategory.FeatureRequest;
                            break;
                        case "UserExperience":
                            Obj.Category = ReviewCategory.UserExperience;
                            break;
                        case "Rating":
                            Obj.Category = ReviewCategory.Rating;
                            break;
                        case "Other":
                            Obj.Category = ReviewCategory.Other;
                            break;
                    }
                }
            }
            return Obj;
        }

        public int Compare(ReviewDetails x, ReviewDetails y)
        {
            if (x.Content.Split(' ').Length < y.Content.Split(' ').Length)
                return -1;
            else if (x.Content.Split(' ').Length == y.Content.Split(' ').Length)
                return 0;
            else
                return 1;
        }
    }
    [Serializable]
    public enum ReviewCategory { BugReport,FeatureRequest,UserExperience,Rating,Other}
    [Serializable]
    public enum SentimentValue { HighlyPositive,Positive,Neutral,Negative,HighlyNegative }
}
