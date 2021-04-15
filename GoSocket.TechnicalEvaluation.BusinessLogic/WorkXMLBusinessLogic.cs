using GoSocket.TechnicalEvaluation.Scheme;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GoSocket.TechnicalEvaluation.BusinessLogic
{
    public class WorkXMLBusinessLogic
    {
        private gosocket _gosocketXMLDTO;

        public gosocket GoSocketXMLDTO
        {
            get
            {
                if (_gosocketXMLDTO is null)
                    _gosocketXMLDTO = Utils.DeserializeToObject<gosocket>(Path.Combine(this.PathFileName, ConfigurationManager.AppSettings["FileNameXML"]));

                return _gosocketXMLDTO;

            }
            set { _gosocketXMLDTO = value; }
        }



        public string PathFileName { get; set; }

        public WorkXMLBusinessLogic(string path)
        {
            this.PathFileName = path;
        }

        /// <summary>
        /// Obtiene la cantidad de las áreas de acuerdo a al condición de entrada
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ResponseService GetAreas(Func<gosocketArea, bool> predicate = null)
        {
            ResponseService response = new ResponseService();
            try
            {
                if (predicate is null)
                    response.Data = GoSocketXMLDTO.Items.Count();
                else
                    response.Data = GoSocketXMLDTO.Items.Count(predicate);

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        /// <summary>
        /// Obtiene la información del salario de cada área
        /// </summary>
        /// <returns></returns>
        public ResponseService GetInformationSalary()
        {
            ResponseService response = new ResponseService();
            try
            {
                response.Data = GoSocketXMLDTO.Items.Select(x => new GetInformationSalaryResponse { AreaName = x.name, TotalSalary = x.employees.Sum(y => y.salary) });
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }
    }
}
