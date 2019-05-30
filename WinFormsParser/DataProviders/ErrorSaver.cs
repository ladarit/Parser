using System;
using System.Net;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse.DataProviders
{
    public class ErrorSaver
    {
        public SaveFilesErrorMessage SendErrorMsgToDataBase(ErrorModel error)
        {
            var netHelper = new NetHelper();
            var errorModel = new SaveFilesErrorMessage
            {
                UserMsg = GetUserMsg(error.ControlName),
                SaveFilesErrorMsg = "ErrorMsg:" + error.ErrorMsg + " Operation:" + error.Operation,
                DocumentType = GetDocType(error.ControlName),
                Ip = netHelper.GetHostIp(),
                Terminal = netHelper.GetHostName()
            };
            var ini = new IniReader().AutoReadIni();
            return new ApiHandler(new NetworkCredential()).SaveErrorMessage(ini.ApiSaveErrorMsg, errorModel);
        }

        private string GetUserMsg(string controlName)
        {
            var str = new ResourceReader().GetString("ParseError") + " ";
            switch (controlName)
            {
                case "SaveLawsBtn":
                    return str + "про нові законопроекти за минулий тиждень";
                case "UpdateLaws":
                    return str + "про усі оновлені за минулу добу законопроекти";
                case "SaveСommitteesBtn":
                    return str + "про комітети";
                case "SavePlenarySessionCalendarPlanBtn":
                    return str + "про план проведення пленарних засіданнь на поточну сессію";
                case "SavePlenarySessionDatesBtn":
                    return str + "про дати проведення пленарних засіданнь на поточну сессію";
                case "SaveDeputyBtn":
                    return str + "про депутатів поточного скликання";
                case "SaveFractionsBtn":
                    return str + "про фракції поточного скликання";
                default:
                    return null;
            }
        }

        private int GetDocType(string controlName)
        {
            return (int)Enum.Parse(typeof(ControlNameToNumber), controlName);
        }
    }
}
