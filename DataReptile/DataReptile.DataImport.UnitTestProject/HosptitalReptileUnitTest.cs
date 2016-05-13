using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataReptile.DataImport.UnitTestProject
{
    [TestClass]
    public class HosptitalReptileUnitTest
    {
        //[TestMethod]
        //public void TestFindHosptitalNodes()
        //{
        //    var context = new HosptitalReptile();
        //    var xmlData = context.GetHospitials();
        //    var result = context.FindHosptitalNodes(xmlData);
        //    var hos = context.UpdateToDB(result);
        //    Assert.IsNotNull(hos);
        //}

        [TestMethod]
        public void TestDoctorschedualUpdateToDB()
        {
            var context = new Doctorschedual();
            var de = context.GetHospitallist();
            Assert.IsNotNull(de);
            
        }

        [TestMethod]
        public void TestGetHospitialDepartments()
        {
           var context = new HosptitalDepartmentsReptile();
            var de = context.DepartmentList();
           //var hospitalName = "苏州大学附属第一医院";
           //var xmlData = context.GetHospitialDepartments(hospitalName);
           //var department = context.ConvertToNodes(xmlData);
           //var de = context.UpdateToDB(hospitalName, department);
            Assert.IsNotNull(de);
        }
         [TestMethod]
        public void TesthospitalDoc()
        {
            var context = new HosptitalDocReptile();
            var data = context.GetDoctorlist();
            Assert.IsNotNull(data);
            //context.UpdateToDB(result);
        }
         [TestMethod]
         public void TestSchedual()
         {
             var context = new Doctorschedual();
             var result= context.GetHospitallist();
             //var xmlData = context.GetSchedualList(hospitalName);
             //var result = context.ConvertToNodes(xmlData);
            // var shedual = context.UpdateToDB(hospitalName,result);

             Assert.IsNotNull(result);
         }
         [TestMethod]
         public void TestSina()
         {
             var context = new SinaData();
             var result = context.Reptile();
             Assert.IsNotNull(true);
         }

        

    }
}
