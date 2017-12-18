using System;
using System.Collections.Generic;

namespace LI_MAR_S_RPAM
{
    [Serializable]
    public class AppDetails:IEqualityComparer<AppDetails>,IComparer<AppDetails>
    {
        public string Name;
        public string Summary;
        public double Price;
        public string Currency;
        public string Title;
        public string Href;
        public string BundleID;
        public string AppID;
        public string Owner;
        public string Category;
        public double CategoryCode;
        public DateTime ReleaseDate;
        public override string ToString()
        {
            return ("Name:" + Name +
                ",Price:" + Price +
                ",Currency:" + Currency +
                ",Title:" + Title +
                ",Link:" + Href +
                ",BundleID:" + BundleID +
                ",AppID:" + AppID +
                ",Owner:" + Owner +
                ",Category:" + Category +
                ",CategoryCode:" + CategoryCode +
                ",ReleaseDate:" + ReleaseDate.ToString()).Replace("\n", "%$%#").Replace("\r", "#$#%$");
        }
        public static AppDetails Parse(string Data)
        {
            AppDetails Obj = new AppDetails();
            Data = Data.Replace("%$%#", "\n").Replace("#$#%$", "\r");
            Obj.Name = Data.Substring(0,Data.IndexOf("Price:"));
            Data = Data.Replace(Obj.Name, "");
            Obj.Name = Obj.Name.Remove(Obj.Name.Length - 1).Replace("Name:", "");
            String Temp = Data.Substring(0, Data.IndexOf("Currency:"));
            Data = Data.Replace(Temp, "");
            Temp = Temp.Remove(Temp.Length - 1).Replace("Price:", "");
            Obj.Price = double.Parse(Temp);
            Obj.Currency = Data.Substring(0, Data.IndexOf("Title:"));
            Data = Data.Replace(Obj.Currency, "");
            Obj.Currency = Obj.Currency.Remove(Obj.Currency.Length - 1).Replace("Currency:", "");
            Obj.Title = Data.Substring(0, Data.IndexOf("Link:"));
            Data = Data.Replace(Obj.Title, "");
            Obj.Title = Obj.Title.Remove(Obj.Title.Length - 1).Replace("Title:", "");
            Obj.Href = Data.Substring(0, Data.IndexOf("BundleID:"));
            Data = Data.Replace(Obj.Href, "");
            Obj.Href = Obj.Href.Remove(Obj.Href.Length - 1).Replace("Link:", "");
            Obj.BundleID = Data.Substring(0, Data.IndexOf("AppID:"));
            Data = Data.Replace(Obj.BundleID, "");
            Obj.BundleID = Obj.BundleID.Remove(Obj.BundleID.Length - 1).Replace("BundleID:", "");
            Obj.AppID = Data.Substring(0, Data.IndexOf("Owner:"));
            Data = Data.Replace(Obj.AppID, "");
            Obj.AppID = Obj.AppID.Remove(Obj.AppID.Length - 1).Replace("AppID:", "");
            Obj.Owner = Data.Substring(0, Data.IndexOf("Category:"));
            Data = Data.Replace(Obj.Owner, "");
            Obj.Owner = Obj.Owner.Remove(Obj.Owner.Length - 1).Replace("Owner:", "");
            Obj.Category = Data.Substring(0, Data.IndexOf("CategoryCode:"));
            Data = Data.Replace(Obj.Category, "");
            Obj.Category = Obj.Category.Remove(Obj.Category.Length - 1).Replace("Category:", "");
            Temp = Data.Substring(0, Data.IndexOf("ReleaseDate:"));
            Data = Data.Replace(Temp, "");
            Temp = Temp.Remove(Temp.Length - 1).Replace("CategoryCode:", "");
            Obj.CategoryCode = double.Parse(Temp);
            Temp = Data;
            Data = Data.Replace(Temp, "");
            Temp = Temp.Replace("ReleaseDate:", "");
            Obj.ReleaseDate = DateTime.Parse(Temp);

            return Obj;
        }
        public bool Equals(AppDetails x, AppDetails y)
        {
            if (x.AppID == y.AppID)
                return true;
            else
                return false;
        }

        public int GetHashCode(AppDetails obj)
        {
            return int.Parse(obj.AppID);
        }

        public int Compare(AppDetails x, AppDetails y)
        {
            if (x.CategoryCode == y.CategoryCode)
                return 0;
            else if (x.CategoryCode > y.CategoryCode)
                return 1;
            else
                return -1;
        }
    }
}
