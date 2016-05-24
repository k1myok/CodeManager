using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SelectProvidentFundService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDataTransformService”。
    [ServiceContract]
    public interface IDataTransformService
    {
        [OperationContract]
        string GetAccountStatus(string idCard);
            
        [OperationContract]
        string GetProvidentFund(string idCard, string id, string trancode);
        [OperationContract]
        string GetProvidentFundDepositInfo(string idCard, string id, string page, string total, string pages, string trancode = "GJ08L1");

        [OperationContract]
        string GetProvidentFundLoanInfo(string idCard, string trancode = "J005L4");

        [OperationContract]
        string GetProvidentFundRepayInfo(string loadNum,string page, string total, string pages, string trancode = "J021L1");

        [OperationContract]
        CheckCodeResult SendCheckCode(string custAcNo, string paperId, string mobile);
    }
}
