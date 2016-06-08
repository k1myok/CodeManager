using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataReptile.DataImport.Hosptital
{
   public class Hospital
    {
       private string hosname;
       public string HosName
        {
            get { return hosname; }
            set { hosname = value; }
        }
       private string grade;
       public string Grade
       {
           get { return grade; }
           set { grade = value; }
       }
       private string kind;
       public string Kind
       {
           get { return kind; }
           set { kind = value; }
       
       }
       private string address;
       public string Address
       {
           get { return address; }
           set { address = value; }
       }
       private string phone;
       public string Phone
       {
           get {return phone; }
           set { phone = value; }
       
       }
       private string intro;
   
      public string Intro
      { 
       get { return intro; }
       set { intro = value; }
     }
}
}
