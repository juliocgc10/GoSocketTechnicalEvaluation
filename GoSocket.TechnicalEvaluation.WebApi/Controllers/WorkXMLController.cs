using GoSocket.TechnicalEvaluation.BusinessLogic;
using GoSocket.TechnicalEvaluation.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace GoSocket.TechnicalEvaluation.WebApi.Controllers
{
    public class WorkXMLController : ApiController
    {
        private WorkXMLBusinessLogic _workXMLBusinessLogic;

        public WorkXMLBusinessLogic workXMLBusinessLogic
        {
            get
            {
                if (_workXMLBusinessLogic is null)
                    _workXMLBusinessLogic = new WorkXMLBusinessLogic(System.Web.Hosting.HostingEnvironment.MapPath("~/Files"));

                return _workXMLBusinessLogic;
            }
            set { _workXMLBusinessLogic = value; }
        }


        /// <summary>
        /// Obtiene el total de las áreas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/WorkXML/GetNumberAreas")]
        public JsonResult<ResponseService> GetAreas()
        {
            ResponseService loReturn = workXMLBusinessLogic.GetAreas();
            return Json(loReturn);
        }

        /// <summary>
        /// Obtienen el total de las áreas en el que el número de empelados sea mayor al ingresado
        /// </summary>
        /// <param name="nodesPerEmployee"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/WorkXML/GetAreasPerEmployee")]
        public JsonResult<ResponseService> GetAreasPerEmployee(int nodesPerEmployee)
        {
            ResponseService loReturn = workXMLBusinessLogic.GetAreas(x => x.employees.Count() > nodesPerEmployee);
            return Json(loReturn);
        }

        /// <summary>
        /// Obtiene la suma del salario de todo los empelados de cada área
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/WorkXML/GetInformationSalary")]
        public JsonResult<ResponseService> GetInformationSalary()
        {
            ResponseService loReturn = workXMLBusinessLogic.GetInformationSalary();
            return Json(loReturn);
        }
    }
}